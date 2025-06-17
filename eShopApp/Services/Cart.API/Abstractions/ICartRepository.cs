using Cart.API.Models;
using Cart.API.Models.DTOs;

namespace Cart.API.Abstractions
{
    public interface ICartRepository
    {
        Task<UserCart?> GetCartByUserIdAsync(int userId);
        Task<List<CartItem>> GetCartItemsByCartIdAsync(int cartId);
        public Task AddCartItemAsync(UserCart userCart, CartItemDTO cartItemDTO);
        public Task<UserCart> CreateUserCart(int userId);
        Task RemoveItem(int cartId, string itemId);
        Task UpdateQuantity(int cartId, string itemId, int quantity);
    }
}
