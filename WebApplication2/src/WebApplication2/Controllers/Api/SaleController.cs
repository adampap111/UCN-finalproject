using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heinbo.Models;
using Heinbo.Services;

namespace Heinbo.Controllers.Api
{
    public class SaleController: Controller
    {
        private ILogger<SaleController> _logger;
        private ISalesRepository _repository;

        public SaleController(ISalesRepository repository, ILogger<SaleController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

   
    }
}
