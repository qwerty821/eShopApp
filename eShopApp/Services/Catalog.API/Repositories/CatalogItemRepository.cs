using Catalog.API.Abstractions;
using Catalog.API.Contexts;
using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net.Sockets;

namespace Catalog.API.Repositories
{
    public class CatalogItemRepository : ICatalogItemRepository
    {
        public CatalogDBContext _context;

        public CatalogItemRepository(CatalogDBContext context)
        {
            _context = context;
        }

        public async Task<CatalogItem> Get(string id)
        {
            string s_id = id.ToString().ToUpper();
            var x = await _context.Products.FindAsync(id);
            if (x == null)
            {
                return null;
            }

            return x;
        }

        public void Add()
        {
            CatalogItem it = new CatalogItem()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Laptop Gaming ASUS TUF A15",
                Price = 7199,
                Description = "Laptop Gaming ASUS TUF A15 FA507NUR cu procesor AMD Ryzen™ 7 7435HS pana la 4.5GHz, 15.6'', Full HD, IPS, 144Hz, 16GB DDR5 RAM, 1TB SSD, NVIDIA® GeForce RTX™ 4050 6GB GDDR6, No OS, Mecha Gray",
                AvailableStock = 5,
                Image = "https://s13emagst.akamaized.net/products/58384/58383716/images/res_83f7c23087b6f133710090cda0fedce7.jpg?width=720&height=720&hash=9439C0517AC700E7B94E0C74CFFF3DC1"

            };

            _context.Products.Add(it);

            _context.SaveChanges();
        }

        public async Task<List<CatalogItem>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }
    }
}

 