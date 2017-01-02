using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Heinbo.Models;

namespace Heinbo.Services
{
    public class SalesContext : IdentityDbContext<User>
    {
        private IConfigurationRoot _config;

        public SalesContext(IConfigurationRoot config, DbContextOptions options) : base(options)
        {
            _config = config;
        }

       

        public DbSet<Product> Product { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public new DbSet<User> Users { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Order> Order { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:HeinboContextConnection"]);

        }

    }
}
