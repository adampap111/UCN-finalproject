using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Routing;
using Heinbo.Models;

namespace Heinbo.Services
{
    public interface ISalesRepository
    {
        //products
        Product GetProductByName(string productName);
        ProductContainer GetProductsByCategory(string category);
        IEnumerable<Product> GetAllProducts();
        //user
        Task<User> GetCurrentUser();
        Task AddUser(User user, string password);
        Task UpdateUser(User user);
        //both
        Task<bool> SaveChangesAsync();
       
   
    }
}
