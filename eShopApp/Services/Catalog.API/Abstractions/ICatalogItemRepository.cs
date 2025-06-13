using Catalog.API.Models;
using Catalog.API.Models.DTOs;

namespace Catalog.API.Abstractions
{
    public interface ICatalogItemRepository
    {
        public Task<CatalogItem> Get(string id);
        public Task<PaginatedResultDto<ProductDTO>> GetProducts(int page, int pageSize);
        public Task<string> Add(CatalogItemDto catalogItem);
        Task<int> Delete(string id);
        Task<int> Update(string id, CatalogItem item);
    }
}
