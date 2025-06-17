using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cart.API.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public string ItemId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }

        public int CartId { get; set; }         // cheia straina
        
        [JsonIgnore]
        public UserCart Cart { get; set; }      
    }
}
