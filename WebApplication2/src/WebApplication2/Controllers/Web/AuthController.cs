using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heinbo.Models;
using Heinbo.ViewModels;

namespace Heinbo.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Register", "App");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var email = vm.Email;
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var _signInResult = await _signInManager.PasswordSignInAsync(user,
                                                                            vm.Password,
                                                                            true, false);
                    if (_signInResult.Succeeded)
                    {
                        if (string.IsNullOrWhiteSpace(returnUrl))
                        {
                            return RedirectToAction("Index", "App");
                        }
                        else
                        {
                            return Redirect(returnUrl);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Nem egyezik a felhasználónév és jelszó.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Ez az email cím nem szerepel az adatbáziusnkban.");
                }
            }
            return View();
        }

        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "App");
        }
    }


}
