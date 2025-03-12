using Catalog.API.Models;

namespace Catalog.API.Abstractions
{
    public interface ICatalogItemRepository
    {
        public Task<CatalogItem> Get(string id);
        public Task<List<CatalogItem>> GetAll();
        public void Add();

    }
}
