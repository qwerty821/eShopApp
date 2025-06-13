using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Catalog.API.Models.DTOs
{
    public class CatalogItemDto
    {

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public int AvailableStock { get; set; }

        public int CategoryId { get; set; }

        public string Brand { set; get; }
        public List<string> Images { get; set; } = new();

        public Dictionary<string, string> Attributes { get; set; } = new();
    }
}
