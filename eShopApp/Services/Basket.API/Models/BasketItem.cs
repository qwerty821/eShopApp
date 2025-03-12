using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Basket.API.Models
{
    public class BasketItem
    {
        [Column("product_id")]
        public string Id { get; set; }

        [Column("product_name")]
        public string Name { get; set; }

        [Column("product_price")]
        public double Price { get; set; }

        [Column("product_description")]
        public string Description { get; set; }

        [Column("product_image")]
        public string Image { get; set; }

        [Column("product_stock")]
        public int AvailableStock { get; set; }
    }
}
