using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace bookCatalog.Controllers
{
    [ApiController]
    [Route("/api/products")]
    public class ProductController : ControllerBase
    {
        private static readonly string[] Products = new[]
        {
            "Cellphone", "Nintendo Switch", "Playstation 4",
        };

        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet, Route("")]
        public IEnumerable<string> Get()
        {
           return Products;
        }
    }
}
