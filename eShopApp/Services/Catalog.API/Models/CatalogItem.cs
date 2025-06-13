using Catalog.API.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Catalog.API.Models
{
    public class CatalogItem
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

        public CatalogItem()
        {

        }

        public CatalogItem(CatalogItemDto other)
        {
            Name = other.Name;
            Price = other.Price;
            Description = other.Description;
            Image = other.Image;
            AvailableStock = other.AvailableStock;
            CategoryId = other.CategoryId;
            Attributes = new Dictionary<string, string>(other.Attributes);
            Images = other.Images.ToList();
            Brand = other.Brand;
        }

    }
     
}
