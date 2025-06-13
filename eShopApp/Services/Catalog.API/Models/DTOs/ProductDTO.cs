namespace Catalog.API.Models.DTOs
{
    public class ProductDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public decimal Rating { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
    }
}
