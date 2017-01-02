using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heinbo.Models;
using Heinbo.Services;
using Heinbo.ViewModels;

namespace Heinbo.Controllers.Web

{
    public class AppController : Controller
    {

        private IConfigurationRoot _config;
        private ISalesRepository _repository;
        private ILogger<AppController> _logger;

        public AppController(IConfigurationRoot config, ISalesRepository repository,
            ILogger<AppController> logger)
        {
            _config = config;
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Product()
        {
            return View();
        }

        public IActionResult CartBadge()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult ProductInfo()
        {
            return View();
        }

        [Authorize]
        public IActionResult ShoppingCart()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }


        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
