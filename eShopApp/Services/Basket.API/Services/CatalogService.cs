using Catalog.API.Abstractions;
using Catalog.API.Models;
using System.IO;
using System.Net.Sockets;

namespace Catalog.API.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IBasketRepository _catalogRepository;

        public CatalogService(IBasketRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }


        public async Task<BasketItem> Get(string id)
        {
            return await _catalogRepository.Get(id);
        }

        public async Task<List<BasketItem>> GetAll()
        {
            return await _catalogRepository.GetAll();
        }

        public void Add()
        {
            _catalogRepository.Add();
        }

        
    }
}