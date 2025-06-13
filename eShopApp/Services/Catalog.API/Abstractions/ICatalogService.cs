using Catalog.API.Models;
using Catalog.API.Models.DTOs;

namespace Catalog.API.Abstractions
{
    public interface ICatalogService
    {
        Task<PaginatedResultDto<ProductDTO>> GetProducts(int page, int pageSize);
        Task<CatalogItem> Get(string slug);
        Task<string> Add(CatalogItemDto catalogItemDto);
        Task<int> Delete(string id);
        Task<int> Update(string id, CatalogItem catalogItem);
    }
}
