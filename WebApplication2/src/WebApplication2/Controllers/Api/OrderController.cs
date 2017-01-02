using Heinbo.Models;
using AutoMapper;
using Heinbo.Services;
using Heinbo.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heinbo.Controllers.Api
{
    [Route("order/")]
    public class OrderController : Controller
    {
        private ISalesRepository _repository;
        private readonly IOrderService _orderService;
        private UserManager<User> _userManager;
        private IMailService _mailService;
        private ILogger<CartController> _logger;

        public OrderController(IMailService mailService, UserManager<User> userManager, ISalesRepository repository, IOrderService orderService, ILogger<CartController> logger)
        {
            _orderService = orderService;
            _repository = repository;
            _userManager = userManager;
            _logger = logger;
            _mailService = mailService;
        }


        [HttpPost("MakeOrder/")]
        public async Task<IActionResult> MakeOrder([FromBody] AddToCartModel model)
        {
            var currentUser = await _repository.GetCurrentUser();
          var mailBody = "Kedves "+ currentUser.LastName +" " +currentUser.FirstName + "! \n\n" + _orderService.MakeOrder(currentUser.Id);
            
            try
            {
                await _mailService.SendEmailAsync(currentUser.Email, "heinbomail555@gmail.com",
                "order", mailBody , currentUser.LastName);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to send the email " + ex.Message);
                return BadRequest("Failed to send the email");
            }
        
        }

    }
}
