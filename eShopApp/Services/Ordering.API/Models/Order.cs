using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ordering.API.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        [StringLength(20)]
        public string? Status { get; set; }

        [Required]
        [Column(TypeName = "numeric(9,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        public string ShippingAddress { get; set; } = string.Empty;

        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
