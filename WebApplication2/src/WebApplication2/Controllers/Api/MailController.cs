using Heinbo.Services;
using Heinbo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Heinbo.Controllers.Api
{
    [Route("api/contact/")]
    public class MailController : Controller
    {
        private ILogger<MailController> _logger;
        private IMailService _mailService;

        public MailController(ILogger<MailController> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        [HttpPost("sendMail")]
        public async Task<IActionResult> Post ([FromBody] ContactViewModel mail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _mailService.SendEmailAsync("heinbomail555@gmail.com", mail.Email,
                    mail.Subject, mail.Message, mail.Name);
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Failed to send the email " + ex.Message);
                    return BadRequest("Failed to send the email");
                }
            }
            else
            {
                return BadRequest("Failed to send the email");
            }
        }
    }
}
