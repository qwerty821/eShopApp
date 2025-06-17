using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cart.API.Models
{
    public class UserCart
    {
        [Key]
        public int CartId { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public ICollection<CartItem> Items { get; set; }   
    }
}
