using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heinbo.Models;
using Heinbo.Services;

namespace Heinbo
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(
       this IServiceCollection services)
        {
            services.AddScoped<ISalesRepository, SalesRepository>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IMailService, MailService>();
            services.AddIdentity<User, IdentityRole>(config =>
              {
                  config.User.RequireUniqueEmail = true;
                  config.Password.RequiredLength = 6;
                  config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
                  config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                  {
                      OnRedirectToLogin = async ctx =>
                      {
                          if (ctx.Request.Path.StartsWithSegments("/api") &&
                          ctx.Response.StatusCode == 200)
                          {
                              ctx.Response.StatusCode = 401;
                          }
                          else
                          {
                              ctx.Response.Redirect(ctx.RedirectUri);
                          }
                          await Task.Yield();

                      }
                  };
              }).AddEntityFrameworkStores<SalesContext>();

            services.AddLogging();

            return services;
        }
    }
}
