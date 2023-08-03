using Microsoft.AspNetCore.Mvc;
using SC_statistic.DataLayer.ViewModels.Account;
using SC_statistic.Services.Interfaces;
using SC_statistic.DataLayer.Enums;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Net;

namespace SC_statistic.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Registration() => PartialView("RegistrationPartial");

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel registrationViewModel)
        {
            if (ModelState.IsValid)
            {
                var isUserWithSameLoginExists = await _accountService.IsUserExistsByLogin(registrationViewModel);
                if (isUserWithSameLoginExists.Data)
                {
                    ModelState.AddModelError(key: "Login", errorMessage: isUserWithSameLoginExists.Description);
                }
                else
                {
                    var response = await _accountService.Registration(registrationViewModel);
                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));
                        return Json(new { redirectUrl = Url.Action("Index", "Home") });
                    }
                    ModelState.AddModelError(key: "", errorMessage: response.Description);
                }
            }
            return PartialView("RegistrationPartial", registrationViewModel);
        }

        [HttpGet]
        public IActionResult Login() => PartialView("LoginPartial");

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid) 
            {
                var response = await _accountService.Login(loginViewModel);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));
                    return Json(new { redirectUrl = Url.Action("Index", "Home") });
                }
                ModelState.AddModelError(key: "", errorMessage: response.Description);
            }
            return PartialView("LoginPartial",loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", controllerName: "Home");
        }
    }
}
