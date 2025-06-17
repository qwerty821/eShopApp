namespace Cart.API.Models.DTOs
{
    public class AddCartItemRequest
    {
        public int UserId { get; set; }
        public CartItemDTO CartItem { get; set; }
    }
}
