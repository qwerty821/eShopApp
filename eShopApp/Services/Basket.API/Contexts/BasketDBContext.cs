using Basket.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Basket.API.Contexts
{
    public partial class BasketDBContext : DbContext
    {
            
        public BasketDBContext(DbContextOptions<BasketDBContext> options)
        : base(options)
        {
        }

        public virtual DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString = Configuration.GetConnectionString("DefaultConnection");
            //optionsBuilder.UseSqlServer(connectionString);
            //optionsBuilder.UseSqlServer("Server=sqlserver;Database=ProductsCatalog;User=sa;Password=Qwerty821@!1234;TrustServerCertificate=True;");

        }

    }
}