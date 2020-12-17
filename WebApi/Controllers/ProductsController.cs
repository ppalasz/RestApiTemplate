using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private static readonly List<Product> _products = new List<Product>()
        {
            new Product
            {
                ProductId = 1,
                ProductName = "magnetofon",
                ProductPrice = 33.4
            },
            new Product
            {
                ProductId = 2,
                ProductName = "kaseta",
                ProductPrice = 4.4
            }
        };
        
        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _products;
        }

        [HttpGet]
        public Product Get(int id)
        {
            var product = _products
                .SingleOrDefault(x => x.ProductId == id);

            if (product == null)
                throw new ArgumentException($"Product id = {id} not found");

            return product;
        }

        [HttpPost]
        public void Post([FromBody]Product product)
        {
            var existingProduct = _products
                .SingleOrDefault(x => x.ProductId == product.ProductId);

            if (existingProduct != null)
                throw new ArgumentException($"Product id = {product.ProductId} already exists");

            _products.Add(product);
        }

        [HttpPut]
        public void Put(int id, [FromBody] Product product)
        {
            var productToUpdate = _products
                .SingleOrDefault(x => x.ProductId == id);
            
            if(productToUpdate==null)
                throw new ArgumentException($"Product id = {id} not found");

            productToUpdate = product;

        }
    }
}
