using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.Models;
using Ordering.API.Models.DTOs;

namespace Ordering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> AddOrder([FromBody] OrderDTO orderDTO)
        {
            Order order = new Order()
            {

            };

            return Ok();
        }
    }
}
