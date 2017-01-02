using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Heinbo.Models;

namespace Heinbo.Services
{
    public class CartService : ICartService
    {

        private ISalesRepository _repository;
        private SalesContext _context;

        public CartService(SalesContext context, ISalesRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        public CartItem AddToCart(string userId, int productId, string variationName, int quantity)
        {
            var cartItemQuery = _context.CartItem.Include(p => p.Product)
                .Where(x => x.ProductID == productId && x.UserId == userId);
            var cartItem = cartItemQuery.FirstOrDefault();

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    UserId = userId,
                    ProductID = productId,
                    Quantity = quantity,
                };
                _context.Add(cartItem);
            }
            else
            {
                cartItem.Quantity = quantity;
            }
            _repository.SaveChangesAsync();
            return cartItem;
        }

        public IList<CartItem> GetCartItems(string userId)
        {
            if (userId != "guest.simplcommerce.com")
            {
                var query = _context.CartItem.Include(p => p.Product)
                    .Where(x => x.UserId == userId);
                return query.ToList();
            }
            return Enumerable.Empty<CartItem>().ToList();
        }

        public void MigrateCart(long fromUserId, long toUserId)
        {
            throw new NotImplementedException();
        }

        public void RemoveCartItem(string userId, int productId, string variationName)
        {
            var cartItemQuery = _context.CartItem.Include(p => p.Product)
               .Where(x => x.ProductID == productId && x.UserId == userId);
            var cartItem = cartItemQuery.FirstOrDefault();

            if (cartItem != null)
            {
                _context.Remove(cartItem);
            }
            else
            {
                //error
            }
            _repository.SaveChangesAsync();
        }

        public void UpdateQuantity(string userId, int cartItemId, int quantity)
        {
            var cartItemQuery = _context.CartItem.Include(p => p.Product)
               .Where(x => x.ProductID == cartItemId && x.UserId == userId);
            var cartItem = cartItemQuery.FirstOrDefault();
            cartItem.Quantity = quantity;

            _repository.SaveChangesAsync();
        }
    }
}
