using Cart.API.Abstractions;
using Cart.API.Models;
using Cart.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Cart.API.Services
{
    public class CartService : ICartService
    {
        ICartRepository _repository;
        public CartService(ICartRepository repository)
        {
            _repository = repository;
        }
        public async Task AddCartItemAsync(int userId, CartItemDTO cartItemDTO)
        {
            var userCart = await _repository.GetCartByUserIdAsync(userId);;
            if (userCart == null)
            {
                userCart =  await _repository.CreateUserCart(userId);
            }

            await _repository.AddCartItemAsync(userCart, cartItemDTO);
        }

        public async Task<List<CartItem>> GetCartItemsAsync(int userId)
        {
            var userCart = await _repository.GetCartByUserIdAsync(userId);
            if (userCart == null)
            {
               userCart = await _repository.CreateUserCart(userId);
            }
            return await _repository.GetCartItemsByCartIdAsync(userCart.CartId);
        }

        public async Task RemoveItem(int userId, string itemId)
        {
            var userCart = await _repository.GetCartByUserIdAsync(userId);
            if (userCart == null)
            {
                return;
            }
            
            await _repository.RemoveItem(userCart.CartId, itemId);
        }
    }
}
