using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cart.API.Abstractions;
using Cart.API.Models;
using Cart.API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
 

namespace Cart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;
        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        // GET: api/Cart/213
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItems(int userId)
        {
            try
            {
                return await _cartService.GetCartItemsAsync(userId);
            } catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return StatusCode(400, "Utilizatorul nu exista ");
            }
        }



        [HttpPost]
        public async Task<ActionResult> AddCartItemAsync(AddCartItemRequest request)
        {
            //Console.WriteLine($"{userId} + {cartItemDTO.Id} + {cartItemDTO.Quantity}");
            try
            {
                await _cartService.AddCartItemAsync(request.UserId, request.CartItem);
                return Ok();
            } catch
            
            {
                return StatusCode(500, "Eroare la adaugarea produsului");
            }
        }

        
        [HttpDelete]
        public async Task<IActionResult> RemoveCartItem(RemoveCartItemRequest request)
        {
            try
            {
                await _cartService.RemoveItem(request.UserId, request.ItemId);
                return Ok();
            } catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return StatusCode(500, "Eroare stergerea produsului");
            }
            
        }
        
    }
}
