using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Products;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;

namespace bookCatalog.Controllers
{
    [Authorize]
    [ApiController]
    [Route ("/api/products")]
    public class ProductController : ControllerBase {

        private readonly IProductService<Book> service;

             // private static readonly string[] Products = new [] {
        //     "Cellphone",
        //     "Nintendo Switch",
        //     "Playstation 4",
        // };

        private readonly ILogger<ProductController> _logger;

        public ProductController (ILogger<ProductController> logger, IProductService<Book> _service) {
            _logger = logger;
            service = _service;
        }

        [HttpGet("books")]
        public IEnumerable<Book> Get () {
            return service.Get();
        }

        [HttpGet("book/{id}")]
        public Book Get (string id) {
            return service.Get(id);
        }

        [HttpPut("book")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Book> Create([FromBody] Book book ){

            return Ok(service.Create(book));
        }


        [HttpPost("book")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public ActionResult Update([FromBody] Book book ) {

            if(string.IsNullOrEmpty(book.Id)){
                    return BadRequest();
            }

            service.Update(book.Id,book);
            return Ok("Updated");
        }

        [HttpDelete("book")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public ActionResult Delete([FromBody] Book book ) {

            if(string.IsNullOrEmpty(book.Id)){
                    return BadRequest();
            }
            service.Remove(book.Id);
            return Ok("Deleted");
        }
    }
}