namespace Cart.API.Models.DTOs
{
    public class CatalogItemDTO
    {
        public string Id { set; get; } = string.Empty;

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }
        public int AvailableStock { get; set; }

        public int CategoryId { get; set; }
        public string Brand { get; set; }
        public string Slug { get; set; } = string.Empty;
        public List<string>? Images { get; set; }
        public Dictionary<string, string> Attributes { get; set; } = new();
    }
}