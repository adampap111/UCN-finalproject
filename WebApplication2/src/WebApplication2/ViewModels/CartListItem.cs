﻿using System.Collections.Generic;
using System.Linq;
using Heinbo.Models;

namespace Heinbo.ViewModels
{
    public class CartListItem
    {
        public long Id { get; set; }

        public string ProductName { get; set; }

        public string ProductImage { get; set; }

        public decimal ProductPrice { get; set; }

        public string ProductPriceString => ProductPrice.ToString("C");

        public int Quantity { get; set; }

        public decimal Total => Quantity * ProductPrice;

        public string TotalString => Total.ToString("C");

       
    }
}
