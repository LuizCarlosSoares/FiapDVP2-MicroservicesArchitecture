using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Products;
using Services;

namespace bookCatalog.Controllers {
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
    }
}