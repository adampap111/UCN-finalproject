using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Heinbo.Models;
using Heinbo.ViewModels;
using Heinbo.Services;
using AutoMapper;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;
using Heinbo.Controllers.Web;

namespace Heinbo.Controllers.Api
{
    [Route("cart/")]
    public class CartController : Controller
    {
        private ISalesRepository _repository;
        private readonly ICartService _cartService;
        private UserManager<User> _userManager;
        private ILogger<CartController> _logger;
  

        public CartController(UserManager<User> userManager, ISalesRepository repository, ICartService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _repository = repository;
            _userManager = userManager;
            _logger = logger;
        
        }



        [HttpPost("AddToCart/")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartModel model)
        {
            var currentUser = await _repository.GetCurrentUser();
            CartItem cartItem = _cartService.AddToCart(currentUser.Id, model.ProductId, model.VariationName, model.Quantity);

       
            return Ok();
        }

        [HttpPost("RemoveFromCart/")]
        public async Task<IActionResult> Remove([FromBody] AddToCartModel model)
        {
            var currentUser = await _repository.GetCurrentUser();
            try
            {
                _cartService.RemoveCartItem(currentUser.Id, model.ProductId, model.VariationName);
                return await List();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get the items {ex}");
                return BadRequest("Error occured");
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> List()
        {
            try
            {
                var currentUser = await _repository.GetCurrentUser();
                var cartItems = _cartService.GetCartItems(currentUser.Id);
                return Ok(Mapper.Map<IEnumerable<CartItem>>(cartItems));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get the items {ex}");
                return BadRequest("Error occured");
            }
        }
        
         [HttpPost("UpdateQuantity/")]
        public async Task<IActionResult> UpdateQuantity([FromBody] AddToCartModel model)
        {
            var currentUser = await _repository.GetCurrentUser();
            try
            {
                _cartService.UpdateQuantity(currentUser.Id, model.ProductId, model.Quantity);
                return await List();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get the items {ex}");
                return BadRequest("Error occured");
            }
        }


    }
}
