using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Catalog.API.Contexts;
using Catalog.API.Models;
using Catalog.API.Abstractions;
using NuGet.Protocol.Core.Types;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogItemsController : ControllerBase
    {
        private readonly ICatalogItemRepository _repository;
        private readonly ILogger<CatalogItemsController> _logger;
        public CatalogItemsController(ICatalogItemRepository repository, ILogger<CatalogItemsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET: api/CatalogItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogItem>>> GetProducts()
        {
            _logger.LogInformation("Cerere GET primită pentru /api/CatalogItems");
            _logger.LogInformation("Detalii cerere: {Method} {Path}, {ip1}, {ip2}", Request.Method, Request.Path, Request.HttpContext.Connection.RemoteIpAddress, Request.HttpContext.Connection.LocalIpAddress);

            return await _repository.GetAll();
        }

        // GET: api/CatalogItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogItem>> GetCatalogItem(string id)
        {
            var catalogItem = await _repository.Get(id);

            if (catalogItem == null)
            {
                return NotFound();
            }

            return catalogItem;
        }

        // PUT: api/CatalogItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatalogItem(string id, CatalogItem catalogItem)
        {
            if (id != catalogItem.Id)
            {
                return BadRequest();
            }

            //_repository.Update()

            return NoContent();
        }

         
        [HttpPost]
        public async Task<ActionResult<CatalogItem>> PostCatalogItem(CatalogItem catalogItem)
        {
            
            try
            {
                _repository.Add();
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }

            return CreatedAtAction("GetCatalogItem", new { id = catalogItem.Id }, catalogItem);
        }

        // DELETE: api/CatalogItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalogItem(string id)
        {
            try
            {
                //await _repository.Delete(id);
            } catch (Exception ex)
            {
                return Conflict();
            }
            return NoContent();
        }
        
    }
}
