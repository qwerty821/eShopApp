using Catalog.API.Abstractions;
using Catalog.API.Models;
using Catalog.API.Models.DTOs;
using System.IO;
using System.Net.Sockets;

namespace Catalog.API.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogItemRepository _catalogRepository;

        public CatalogService(ICatalogItemRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        public async Task<string> Add(CatalogItemDto catalogItemDto)
        {
            return await _catalogRepository.Add(catalogItemDto);
        }

        public async Task<int> Delete(string id)
        {
            return await _catalogRepository.Delete(id);
        }

        public async Task<CatalogItem> Get(string slug)
        {
            return await _catalogRepository.Get(slug);
        }

        public async Task<PaginatedResultDto<ProductDTO>> GetProducts(int page, int pageSize)
        {
            return await _catalogRepository.GetProducts(page, pageSize);
        }

        public async Task<int> Update(string id, CatalogItem catalogItem)
        {
            return await _catalogRepository.Update(id, catalogItem);
        }
    }
}