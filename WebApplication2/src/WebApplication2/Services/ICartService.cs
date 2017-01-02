using System.Collections.Generic;
using  Heinbo.Models;

namespace Heinbo.Services
{
    public interface ICartService
    {
        CartItem AddToCart(string userId, int productId, string variationName, int quantity);
        IList<CartItem> GetCartItems(string userId);
        void RemoveCartItem(string userId, int productId, string variationName);
        void MigrateCart(long fromUserId, long toUserId);
        void UpdateQuantity(string userId, int cartItemId, int quantity);
    }
}
