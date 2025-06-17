using Cart.API.Models;
using Cart.API.Models.DTOs;

namespace Cart.API.Abstractions
{
    public interface ICartService
    {
        Task<List<CartItem>> GetCartItemsAsync(int userId);
        Task AddCartItemAsync(int userId, CartItemDTO cartItemDTO);
        Task RemoveItem(int userId, string itemId);
    }
}
