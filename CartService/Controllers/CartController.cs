using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

    
namespace CartService.Controllers
{
    [Authorize]
    [ApiController]
    [Route ("/api/cart")]
    public class CartController: Controller
    {
        private readonly CartDbContext context;

        public CartController(CartDbContext _context){
            context = _context;
        }

        [HttpPost("item/add")]
        public IActionResult AddToCart(OrderItem item){
            
                if(string.IsNullOrEmpty(item.Id)){
                    item.Id = System.Guid.NewGuid().ToString();
                }

                context.OrderItems.Add(item);
                if(context.SaveChanges() >0)
                    return Ok($"Cart Id: {item.Id}");
                else 
                   return StatusCode(501); 
        }

        [HttpPost("item/delete")]
        public IActionResult RemveFromCart(OrderItem item){
            
                if(string.IsNullOrEmpty(item.Id)){
                    item.Id = System.Guid.NewGuid().ToString();
                }
                context.OrderItems.Remove(item);
                
                if(context.SaveChanges() >0)
                    return Ok($"Cart Id: {item.Id}");
                else 
                   return StatusCode(501); 
        }

        [HttpGet()]
        public IActionResult GetCart(string cartId){

            var items =  context.OrderItems.Where(it=> it.Id == cartId);

            if(items.Count()> 0)
                return Ok(items);
            else
                return NotFound();

        }
    }
}