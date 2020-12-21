using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Database.Dto;
using WebApi.Database.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        //Products/Product/6
        public IActionResult GetProduct(int id)
        {
            var product = _productService.Get(id);

            if (product == null)
                return StatusCode(StatusCodes.Status404NotFound);

            return StatusCode(StatusCodes.Status302Found, product);
        }

        [HttpGet]
        public IActionResult GetProducts(
            string search, 
            string sortBy = null,
            string sortOrder = null)
        {
            var products = _productService.GetAll(search, sortBy, sortOrder);
            return Ok(products);
        }

        [HttpPost]
        public IActionResult PostProduct([FromBody] ProductInsertDto productInsert)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newId = _productService.Add(productInsert);

            return StatusCode(StatusCodes.Status201Created, newId);
        }

        [HttpPut]
        public IActionResult PutProduct(int id, [FromBody] ProductUpdateDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _productService.Update(id, product);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,e);
            }

            return StatusCode(StatusCodes.Status202Accepted,"Product updated");
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _productService.Delete(id);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }

            return StatusCode(StatusCodes.Status200OK, "Product deleted");
        }
    }
}