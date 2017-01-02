using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heinbo.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(string to, string from, string subject, string message, string sender);

    }
}
