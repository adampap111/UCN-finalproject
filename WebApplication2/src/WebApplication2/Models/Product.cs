﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Heinbo.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public string Brand { get; set; }
        public Product(int productID, string productName, string category)
        {
            ProductID = productID;
            ProductName = productName;
            Category = category;   
        }
        public Product()
        {

        }
        public static Product Create(int productID, string productName, string category)
        {
            return new Product(productID, productName, category);
        }

    }


}
