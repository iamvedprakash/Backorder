using Backorder.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Backorder.Data;
using Microsoft.AspNetCore.Authorization;

namespace Backorder.Controllers
{
    public class AccountController : Controller
    {
        private readonly backorderappcontext _context;

        public AccountController(backorderappcontext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Login(users model)
        {
            
            if (ModelState.IsValid)
            {
                // Implement your authentication logic here.
                // Check the user's credentials and sign in if valid.

                var user = _context.users.Where(m => m.username == model.username && m.password == model.password).Count();
                //var user = _context.Users.Count

                if (user == 1)
                {

                    // Sign in the user using the SignInAsync method
                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, model.username) }; // You can use a different property for the user's name.
                                                                                                 // Add other claims as needed.

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    // Redirect the user to a protected page or the home page upon successful login.


                    return RedirectToAction("Index", "Home");
                }

                // If the user authentication fails, show an error message.
                ViewData["LoginError"] = "Provide correct user name and password";
                ModelState.AddModelError("", "Invalid credentials.");
            }

            // If there are any validation errors, return the view with the model.
            return View(model);
        }
    }
}
