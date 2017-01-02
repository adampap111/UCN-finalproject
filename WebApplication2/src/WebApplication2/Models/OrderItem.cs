using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heinbo.Models
{
    public class OrderItem
    {
        public OrderItem(int productID, int quantity)
        {
           
            ProductID = productID;
            Quantity = quantity;
        }

        public OrderItem()
        {

        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrderId { get; set; }

        public Order Order { get; set; }
      

        public int ProductID { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

    }

  
}
