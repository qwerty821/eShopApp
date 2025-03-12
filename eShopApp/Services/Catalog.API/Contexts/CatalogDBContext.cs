using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Catalog.API.Contexts
{
    public partial class CatalogDBContext : DbContext
    {


        public CatalogDBContext(DbContextOptions<CatalogDBContext> options)
        : base(options)
        {
        }


        public virtual DbSet<CatalogItem> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString = Configuration.GetConnectionString("DefaultConnection");
            //optionsBuilder.UseSqlServer(connectionString);
            //optionsBuilder.UseSqlServer("Server=sqlserver;Database=ProductsCatalog;User=sa;Password=Qwerty821@!1234;TrustServerCertificate=True;");


        }

    }
}