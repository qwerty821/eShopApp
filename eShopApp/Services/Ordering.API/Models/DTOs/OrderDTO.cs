using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ordering.API.Models.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public List<OrderItemDTO> OrderItems { get; set; } = new();
    }
}
