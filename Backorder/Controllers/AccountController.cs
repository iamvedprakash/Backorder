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

        public IActionResult Login()
        {
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Login(users model)
        {
            var user = _context.users.FirstOrDefault(u => u.username == model.username && u.password == model.password);

            if (user != null)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.username),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
