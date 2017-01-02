using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heinbo.Models;
using Heinbo.ViewModels;
using Heinbo.Services;
using Microsoft.AspNetCore.Identity;

namespace Heinbo.Controllers
{
    [Route("api/register")]
    public class ResisterController : Controller
    {
        private ISalesRepository _repository;
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;

        public ResisterController(ISalesRepository repository, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _repository = repository;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]UserViewModel theUser)
        {
            if (ModelState.IsValid)
            {
                var password = theUser.Password;
                var newUser = Mapper.Map<User>(theUser);
                //Save to the database              
                try
                {
                    await _repository.AddUser(newUser, password);
                    var user = await _userManager.FindByEmailAsync(newUser.Email);
                    if (user != null)
                    {
                        var _signInResult = await _signInManager
                            .PasswordSignInAsync(user, theUser.Password, true, false);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ez az email cím nem szerepel az adatbáziusnkban.");
                    }
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex +"Error occured");
                }
            }
            return BadRequest("failed to save");
        }
    }
}
