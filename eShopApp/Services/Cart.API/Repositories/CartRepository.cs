using Cart.API.Abstractions;
using Cart.API.Contexts;
using Cart.API.Models;
using Cart.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net.Sockets;

namespace Cart.API.Repositories
{
    public class CartRepository : ICartRepository
    {
        public CartDBContext _context;

        public CartRepository(CartDBContext context)
        {
            _context = context;
        }


        public async Task AddCartItemAsync(UserCart userCart, CartItemDTO it)
        {

            CartItem? item = await _context.CartItems.FirstOrDefaultAsync(citem => (citem.ItemId == it.Id && citem.CartId == userCart.CartId));
            if (item != null)
            {
                // mai trebuie de verificat daca exista in stock (prin broker sau direct http)
                await UpdateQuantity(userCart.CartId, it.Id, it.Quantity);
            }
            else
            {
                /*
                 *  cererea ar trebui facuta in afara prin intermediul unui serviciu
                 */

                HttpClient _httpClient = new HttpClient();
                var response = await _httpClient.GetAsync($"http://catalog.api:8080/api/CatalogItems/id/{it.Id}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("eraore la cererea produsului din catalog");
                }

                CatalogItemDTO? catalogItem = await response.Content.ReadFromJsonAsync<CatalogItemDTO>();
                if (catalogItem == null)
                {
                    throw new Exception("eraore la citirea produsului");
                }
                CartItem cartItem = new CartItem()
                {
                    CartId = userCart.CartId,
                    ItemId =  catalogItem.Id,
                    Name = catalogItem.Name,
                    Image = catalogItem.Image,
                    Price = (double)catalogItem.Price,
                    Quantity = it.Quantity
                };


                await _context.CartItems.AddAsync(cartItem);
            }
          
           await _context.SaveChangesAsync();
        }

        public async Task<UserCart?> GetCartByUserIdAsync(int userId)
        {
            return await _context.UserCart.FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<List<CartItem>> GetCartItemsByCartIdAsync(int cartId)
        {
            return await _context.CartItems.Where(it => it.CartId == cartId).ToListAsync();
        }

        public async Task<UserCart> CreateUserCart(int userId)
        {
            await _context.UserCart.AddAsync(new UserCart() { UserId = userId });
            await _context.SaveChangesAsync();

            return await GetCartByUserIdAsync(userId);
        }

        public async Task UpdateQuantity(int cartId, string itemId, int quantity)
        {
            CartItem? item = await _context.CartItems.FirstOrDefaultAsync(c => (c.ItemId == itemId && c.CartId == cartId));
            if (item != null)
            {
                Console.WriteLine($"{item.Quantity} + {quantity}");

                // mai trebuie de verificat daca exista in stock  
                if (item.Quantity + quantity <= 0)
                {
                    await RemoveItem(cartId, itemId);
                }
                else
                {
                    item.Quantity += quantity;
                }
            }
        }
        public async Task RemoveItem(int cartId, string itemId)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.CartId == cartId && c.ItemId == itemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}

 