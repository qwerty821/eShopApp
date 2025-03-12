using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.API.Abstractions;
using Basket.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly ILogger<BasketController> _logger;
        public BasketController(IBasketRepository repository, ILogger<BasketController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET: api/Basket
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BasketItem>>> GetProducts()
        {
            _logger.LogInformation("Cerere GET primită pentru /api/CatalogItems");
            _logger.LogInformation("Detalii cerere: {Method} {Path}, {ip1}, {ip2}", Request.Method, Request.Path, Request.HttpContext.Connection.RemoteIpAddress, Request.HttpContext.Connection.LocalIpAddress);

            return await _repository.GetAll();
        }

       
         
        [HttpPost]
        public async Task<ActionResult<BasketItem>> PostCatalogItem(BasketItem catalogItem)
        {
            
            
                _repository.Add();
             

            return CreatedAtAction("GetCatalogItem", new { id = catalogItem.Id }, catalogItem);
        }

        // DELETE: api/Basket/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalogItem(string id)
        {
            
            return NoContent();
        }
        
    }
}
