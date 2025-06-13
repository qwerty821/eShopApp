using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Catalog.API.Models;
using Catalog.API.Abstractions;
using NuGet.Protocol.Core.Types;
using Catalog.API.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogItemsController : ControllerBase
    {
        private readonly ILogger<CatalogItemsController> _logger;
        private readonly ICatalogService _catalogService;
        public CatalogItemsController(ICatalogService catalogService, ILogger<CatalogItemsController> logger)
        {
            _catalogService = catalogService;
            _logger = logger;
        }

        // GET: api/CatalogItems
        [HttpGet]
        public async Task<ActionResult<PaginatedResultDto<ProductDTO>>> GetProducts(int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Page and pageSize must be greater than 0.");

            try
            {
                return await _catalogService.GetProducts(page, pageSize);
            } catch(Exception)
            {
                return BadRequest();
            }
        }

        // GET: api/CatalogItems/{slug}
        [HttpGet("{slug}")]
        public async Task<IActionResult> GetCatalogItem(string slug)
        {
            var item = await _catalogService.Get(slug);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        // PUT: api/CatalogItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatalogItem(string id, [FromBody] CatalogItem catalogItem)
        {
            if (id != catalogItem.Id)
                return BadRequest();
             
            int res = await _catalogService.Update(id, catalogItem);
            if (res != 0)
            {
                return NoContent();
            }
            return Ok();
        }

        // POST: api/CatalogItems
        [HttpPost]
        public async Task<IActionResult> PostCatalogItem([FromBody] CatalogItemDto catalogItemDto)
        {
            try
            {
                var id = await _catalogService.Add(catalogItemDto);
                return Ok(id);
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }
        }

        // DELETE: api/CatalogItems/id/
        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteCatalogItem(string id)
        {
            var res = await _catalogService.Delete(id);
            if (res != 0) {
                return NoContent();
            }
            return Ok();
        }

    }
}
