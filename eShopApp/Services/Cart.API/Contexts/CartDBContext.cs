using Cart.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Cart.API.Contexts
{
    public partial class CartDBContext : DbContext
    {
            
        public CartDBContext(DbContextOptions<CartDBContext> options)
        : base(options)
        {
        }

        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<UserCart> UserCart { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

    }
}