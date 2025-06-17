using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ordering.API.Models
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "numeric(9,2)")]
        public decimal TotalPrice { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }
    }
}
