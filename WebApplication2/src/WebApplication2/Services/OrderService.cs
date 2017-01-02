using Heinbo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heinbo.Services
{
    public class OrderService : IOrderService
    {
        private ISalesRepository _repository;
        private SalesContext _context;

        public OrderService(SalesContext context, ISalesRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        public string MakeOrder(string id)
        {
            var cartItemQuery = _context.CartItem.Include(p => p.Product)
               .Where(x => x.UserId == id);

            var order = new Order
            {
                OrderDate = DateTimeOffset.Now,
                UserId = id,
            };

            foreach (var ct in cartItemQuery)
            {
                var orderItem = new OrderItem
                {
                    Product = ct.Product,
                    Quantity = ct.Quantity
                };
                order.AddOrderItem(orderItem);
                _context.Remove(ct);
            }
            order.TotalPrice = order.OrderItems.Sum(x => x.Product.Price * x.Quantity);
            _context.Add(order);

            _repository.SaveChangesAsync();
            
            return MakeOrderMailBody(order);
        }
        private string MakeOrderMailBody(Order order)
        {
            string orderItems = "";
            foreach (OrderItem o in order.OrderItems)
            {
                orderItems +="Termék: " + o.Product.ProductName + "  Mennyiség" + (o.Quantity.ToString()) + " darab  Egységár:" + (o.Product.Price * o.Quantity).ToString() + " Ft \n\n";
            }
            string message = "Köszönjük, hogy nálunk vásárolt!" + "\n\n\n" + orderItems + "\n" + "Teljes összeg:  " + order.TotalPrice.ToString() + " Ft" + " \n\n Utalja a végösszeget a 4352-4326-2525 bankszámlaszámra  \n Üdvözlettel: Heinbo Kft.";

            return message;
        }
    }
    

}
