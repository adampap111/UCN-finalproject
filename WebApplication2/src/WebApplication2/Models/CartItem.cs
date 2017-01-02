using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heinbo.Models
{
    public class CartItem
    {
        public CartItem(string userId, int productID, int quantity)
        {
            UserId = userId;
            ProductID = productID;
            Quantity = quantity;
        }

        public CartItem()
        {

        }

        public static CartItem Create(string userId, int productID, int quantity)
        {
            return new CartItem(userId, productID, quantity);
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public int ProductID { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

      
    }

  
}
