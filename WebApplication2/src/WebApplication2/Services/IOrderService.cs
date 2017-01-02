using Heinbo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heinbo.Services
{
    public interface IOrderService
    {
        string MakeOrder(string id);
    }
}
