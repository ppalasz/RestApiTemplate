using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Database.Dto;
using WebApi.Database.Models;
using WebApi.Database.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]//requires parameter "api-version=1.0"
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]//api/v1.0/Products
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;

        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet("Hello")]
        public IActionResult Hello()
        {
            return Ok("Hello");
        }

        [HttpGet]
        [Route("Product")]
        //Products/Product?id=6
        public async Task<ActionResult<Product>> GetProductAsync(int id)
        {
            var product = await _productService.GetAsync(id);

            if (product == null)
                return StatusCode(StatusCodes.Status404NotFound);

            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<Product>>> GetProducts(
            string search, 
            string sortBy = null,
            string sortOrder = null, 
            int pageNr = 1, 
            int pageSize = 50)
        {
            var products = await _productService
                .GetAllAsync(search, sortBy, sortOrder, pageNr, pageSize);
            
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> PostProductAsync([FromBody] ProductInsertDto productInsert)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newId = await _productService.AddAsync(productInsert);

            return StatusCode(StatusCodes.Status201Created, newId);
        }

        [HttpPut]
        public async Task<IActionResult> PutProductAsync(int id, [FromBody] ProductUpdateDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _productService.UpdateAsync(id, product);
            }
            catch (KeyNotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,e);
            }

            return StatusCode(StatusCodes.Status202Accepted,"Product updated");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _productService.DeleteAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }

            return StatusCode(StatusCodes.Status200OK, "Product deleted");
        }
    }
}