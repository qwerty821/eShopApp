﻿using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace Identity.API.Models
{
    [Index(nameof(Email), Name = "IX_Unique_Email", IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Firstname { get; set; }
        public string? Lastname { get; set; }
        
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } 

        public int ShippingAddressId { get; set; }
    }
}
