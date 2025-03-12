using Basket.API.Abstractions;
using Basket.API.Contexts;
using Basket.API.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net.Sockets;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        public BasketDBContext _context;

        public BasketRepository(BasketDBContext context)
        {
            _context = context;
        }

        public async Task<BasketItem> Get(string id)
        {


            BasketItem x = null;
            return x;
        }

        public void Add()
        {
            BasketItem it = new BasketItem()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Laptop Gaming ASUS TUF A15",
                Price = 7199,
                Description = "Laptop Gaming ASUS TUF A15 FA507NUR cu procesor AMD Ryzen™ 7 7435HS pana la 4.5GHz, 15.6'', Full HD, IPS, 144Hz, 16GB DDR5 RAM, 1TB SSD, NVIDIA® GeForce RTX™ 4050 6GB GDDR6, No OS, Mecha Gray",
                AvailableStock = 5,
                Image = "https://s13emagst.akamaized.net/products/58384/58383716/images/res_83f7c23087b6f133710090cda0fedce7.jpg?width=720&height=720&hash=9439C0517AC700E7B94E0C74CFFF3DC1"

            };

            _context.BasketItems.Add(it);
            _context.SaveChanges();
        }

        public async Task<List<BasketItem>> GetAll()
        {
            return await _context.BasketItems.ToListAsync();
        }
    }
}

 