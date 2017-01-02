using Heinbo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NuGet.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heinbo.Services
{
    public class SalesRepository : ISalesRepository
    {

        private SalesContext _context;
        private ILogger<SalesRepository> _logger;
        private UserManager<User> _userManager;
        private User _currentUser;
        private HttpContext _httpContext;

        public SalesRepository(SalesContext context, IHttpContextAccessor contextAccessor, ILogger<SalesRepository> logger, UserManager<User> userManager)
        {
            _context = context;
            _logger = logger;
            _httpContext = contextAccessor.HttpContext;
            _userManager = userManager;
        }
        public SalesRepository(SalesContext context)
        {
            _context = context;
        }

        public Product GetProductByName(string productName)
        {
            return _context.Product
                .Where(t => t.ProductName == productName)
                .FirstOrDefault();
        }

        public Product GetProductById(int productId)
        {
            return _context.Product
                .Where(t => t.ProductID == productId)
                .FirstOrDefault();
        }

        public IEnumerable<Product> GetAllProducts()
        {
         //   _logger.LogInformation("Getting all trips from the database");
            return _context.Product.ToList();
        }

        public async Task AddUser(User user, string password)
        {
            var _user = user;
            _user.UserName = _user.Email;

            await _userManager.CreateAsync(_user, password);
        }

        public async Task<User> GetCurrentUser()
        {
            if (_currentUser != null)
            {
                return _currentUser;
            }

            if (_httpContext.User.Identity.AuthenticationType == "Identity.Application")
            {
                var contextUser = _httpContext.User;
                _currentUser = await _userManager.GetUserAsync(contextUser);
            }

            if (_currentUser != null)
            {
                return _currentUser;
            }

            var dummyEmail = ("guest.simplcommerce.com");
            _currentUser = new User
            {
                FirstName = "Guest",
                LastName = "Guest",
                Email = dummyEmail,
                UserName = dummyEmail
            };
            var abc = await _userManager.CreateAsync(_currentUser, "1qazZAQ!");
            //   await _userManager.AddToRoleAsync(_currentUser, "guest");

            return _currentUser;
        }



        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;

        }

        public ProductContainer GetProductsByCategory(string category)
        {

            ProductContainer product = new ProductContainer(

            );

            product.Product.AddRange(_context.Product.ToList()
                  .Where(t => t.Category == category))
                  ;

            product.Size.AddRange(_context.Product.ToList().Where(c => c.Category == category)
                                 .Select(p => p.Size).Distinct());

            product.Brand.AddRange(_context.Product.ToList().Where(c => c.Category == category)
                                    .Select(p => p.Brand).Distinct());

            product.Category.AddRange(_context.Product.ToList().Where(c => c.Category == category)
                                .Select(p => p.SubCategory).Distinct());


            return product;
        }

        public async Task UpdateUser(User user)
        {
            var contextUser = _httpContext.User;
            var result = await _userManager.GetUserAsync(contextUser);
            if (result != null)
            {
                result.FirstName = user.FirstName;
                result.LastName = user.LastName;
                result.PhoneNumber = user.PhoneNumber;
                result.Email = user.Email;
                result.Street = user.Street;
                result.StreetNumber = user.StreetNumber;
                result.City = user.City;
                result.Zip = user.Zip;
                try
                {
                    await SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError("The user could not be updated" + ex);
                }  
            }
        }
    }
}
