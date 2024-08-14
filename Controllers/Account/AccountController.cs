using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using celsiaAssetsment.Interfaces;
using celsiaAssetsment.DTOs;

namespace celsiaAssetsment.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountService;

        public AccountController(IAccountRepository accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            //if (!User.Identity!.IsAuthenticated)
            //    return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AdminDTO adminDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Error in the fields.";
                return View();
            }

            try
            {
                var admin = await _accountService.Register(adminDTO);

                if (admin.Id != 0)
                {
                    return RedirectToAction("Index", "Home");
                }

                ViewData["Message"] = "Could not register new admin, fatal error.";
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Error in the fields.";
                return View();
            }

            try
            {
                var admin = await _accountService.Login(loginDTO);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, admin.Email!)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var properties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();  // Clear the session completely
            return RedirectToAction("Login", "Account");
        }
    }
}