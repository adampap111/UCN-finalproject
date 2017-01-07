using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Heinbo.Services;
using Heinbo.Controllers.Api;
using Heinbo.Models;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace test
{
    public class ControllerTests 
    {
      
        List<Product> product;
        private IConfigurationRoot _config;
     

        [Fact]
        public void GetProducts()
        {
            // Arrange
            var product = new List<Product>
            {
               Product.Create(12,"szar","fos"),
               Product.Create(122,"szddar","fddos"),
                            };
            var mockRepo = new Mock<ISalesRepository>();
            mockRepo.Setup(repo => repo.GetAllProducts())
                .Returns(((List<Product>)product));
            var controller = new ProductController(mockRepo.Object, null);                            

            // Act
            var result = controller.Get();

            // Assert
           Assert.Equal(product, Assert.IsType<OkObjectResult>(result).Value);

        }

        [Fact]
        public void GetProductsByName()
        {
            // Arrange
            var product = new Product(12, "szar", "fos");
           
            var mockRepo = new Mock<ISalesRepository>();
            mockRepo.Setup(repo => repo.GetProductByName("szar"))
                .Returns(product);
            var controller = new ProductController(mockRepo.Object, null);

            // Act
            var result = controller.Get("szar");

            // Assert
            Assert.IsType<OkObjectResult>(result);

        }


        private SalesContext CreateAndSeedContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SalesContext>();
            optionsBuilder.UseInMemoryDatabase();
            var context = new SalesContext(_config, optionsBuilder.Options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            //       context.CartItem.AddRange(BuildCartItems());
            context.SaveChanges();
            return context;

        }



    }
}