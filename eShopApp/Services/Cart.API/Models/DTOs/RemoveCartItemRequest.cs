namespace Cart.API.Models.DTOs
{
    public class RemoveCartItemRequest
    {
        public int UserId { get; set; }
        public string ItemId { get; set; }
    }
}
