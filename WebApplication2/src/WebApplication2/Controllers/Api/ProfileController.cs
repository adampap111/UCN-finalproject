using AutoMapper;
using Heinbo.Models;
using Heinbo.Services;
using Heinbo.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heinbo.Controllers.Api
{
    [Route("user/")]
    public class ProfileController : Controller
    {
        private ILogger<ProfileController> _logger;
        private ISalesRepository _repository;
        private UserManager<User> _userManager;

        public ProfileController(ISalesRepository repository, UserManager<User> userManager, ILogger<ProfileController> logger)
        {
            _repository = repository;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Get ()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                return Ok(Mapper.Map<ProfileViewModel>(user));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get user {ex}");
                return BadRequest("Error occured");
            }
        }

        [HttpPost("profile")]
        public async Task<IActionResult> Post ([FromBody]ProfileViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updateUser = Mapper.Map<User>(user);
                    await _repository.UpdateUser(updateUser);
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Failed to update user " + ex.Message);
                    return BadRequest("failed to update user");
                }
            } else
            {
                return BadRequest("failed to update user");
            }
        }
    }
}
