using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heinbo.Models
{
    public class ProductContainer
    {
        public  List<Product> Product { get; set; }
        public List<string> Size { get; set; }
        public List<string> Brand { get; set; }
        public List<string> Category { get; set; }



        public ProductContainer()
        {
            Product = new List<Product>();
            Size = new List<string>();
            Brand = new List<string>();
            Category = new List<string>();

        }
    }
}
