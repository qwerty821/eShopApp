﻿using System.ComponentModel.DataAnnotations;

namespace Identity.API.Models.DTO
{
    public class LoginDTO
    {
        // Email input from the user during login.
 
        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(100, ErrorMessage = "Email must be less than or equal to 100 characters.")]
        public string Username { get; set; }


        // Password input from the user during login.
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [MaxLength(100, ErrorMessage = "Password must be less than or equal to 100 characters.")]
        public string Password { get; set; }
        
        //[Required(ErrorMessage = "ClientId is required.")]
        //public string ClientId { get; set; }
    }
}
