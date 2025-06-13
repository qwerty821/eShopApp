using Catalog.API.Abstractions;
using Catalog.API.Models;
using Catalog.API.Models.DTOs;
using Couchbase;
using Couchbase.KeyValue;
using Couchbase.Query;
using Google.Api;
using Google.Rpc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.IO;
using System.Net.Sockets;

namespace Catalog.API.Repositories
{
    public class CatalogItemRepository : ICatalogItemRepository
    {
        private readonly Task<ICouchbaseCollection> _collectionTask;
        Task<ICluster> _clusterTask;
        public CatalogItemRepository(Task<ICouchbaseCollection> collectionTask, Task<ICluster> clusterTask)
        {
            _collectionTask = collectionTask;
            _clusterTask = clusterTask;
        }

        public async Task<CatalogItem> Get(string slug)
        {
            var cluster = await _clusterTask;

            var query = @"
                SELECT c.* FROM Catalog c 
                WHERE c.Slug = $slug
                LIMIT 1;
            ";

            var result = await cluster.QueryAsync<CatalogItem>(query, options =>
            {
                options.Parameter("slug", slug);
            });

            var item = await result.Rows.FirstOrDefaultAsync();
            return item;
        }

        public async Task<string> Add(CatalogItemDto catalogItemDto)
        {
            CatalogItem catalogItem = new CatalogItem(catalogItemDto)
            {
                Id = Guid.NewGuid().ToString(),
                Slug = GenerateSlug(catalogItemDto)
            };

            var collection = await _collectionTask;
            collection.UpsertAsync<CatalogItem>(catalogItem.Id.ToString(), catalogItem);
            
            return catalogItem.Id;

        }

        public async Task<PaginatedResultDto<ProductDTO>> GetProducts(int page, int pageSize)
        {
            var cluster = await _clusterTask;

            if (page <= 0 || pageSize <= 0)
                throw new ArgumentException("page și pageSize trebuie să fie > 0");

            int offset = (page - 1) * pageSize;

            var countQuery = @"
                SELECT COUNT(1) AS totalCount
                FROM `Catalog` c
            ";

            var countResult = await cluster.QueryAsync<dynamic>(countQuery);
            var countRow = await countResult.Rows.FirstOrDefaultAsync();
            int totalItems = countRow?.totalCount ?? 0;

            var productQuery = @"
                SELECT Meta(c).id, c.Name, c.Price, c.Image, c.Slug, c.Rating, c.Discount
                FROM `Catalog` c
                ORDER BY META(c).id, c.name
                LIMIT $pageSize
                OFFSET $offset
            ";

            var options = new QueryOptions()
                .Parameter("pageSize", pageSize)
                .Parameter("offset", offset);

            var productsResult = await cluster.QueryAsync<ProductDTO>(productQuery, options);
            var items = await productsResult.Rows.ToListAsync();

            return new PaginatedResultDto<ProductDTO>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }

        private string GenerateSlug(CatalogItemDto item)
        {
            var slug = item.Name.ToLowerInvariant()
            .Replace(" ", "-")
            .Replace("--", "-")
            .Replace(".", "")
            .Replace(",", "")
            .Replace("ă", "a")
            .Replace("î", "i")
            .Replace("â", "a")
            .Replace("ș", "s")
            .Replace("ț", "t");
            slug += '-' + Guid.NewGuid().ToString().Substring(0, 7);

            return slug;
        }

        public async Task<int> Delete(string id)
        {
            var it = await Get(id);
            return 0;
        }

        public async Task<int> Update(string id, CatalogItem item)
        {
            var it = await Get(id);
            return 0;
        }
    }
}

 