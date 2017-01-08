using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using Heinbo.Services;


namespace Heinbo.Controllers.Api
{
    [Route("api/")]
    public class ProductController : Controller
    {
        private ILogger<ProductController> _logger;
        private ISalesRepository _repository;

        public ProductController(ISalesRepository repository, ILogger<ProductController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
          
            try
            {
                var product = _repository.GetAllProducts();
                return Ok((product));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Products {ex}");
                return BadRequest("Error occured");
            }
        }

        [HttpGet("product/{category}")]
        public IActionResult GetProductsByCategory(string category)
        {
            try
            {
                var product = _repository.GetProductsByCategory(WebUtility.UrlDecode(category));
                return Ok((product));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get products {ex}");
                return BadRequest("Error occured");
            }
        }

        [HttpGet("productInfo/{productName}")]
        public IActionResult Get(string productName)
        {
            try
            {
                var product = _repository.GetProductByName(productName);
                return Ok((product));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get products {ex}");
                return BadRequest("Error occured");
            }
        }
    }
}

