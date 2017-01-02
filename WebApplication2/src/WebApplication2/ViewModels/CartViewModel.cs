using System.Collections.Generic;
using System.Linq;

namespace Heinbo.ViewModels
{
    public class CartViewModel
    {
        public IList<CartListItem> CartItems { get; set; }

        public string SubTotal => CartItems.Sum(x => x.Total).ToString("C");
    }
}
