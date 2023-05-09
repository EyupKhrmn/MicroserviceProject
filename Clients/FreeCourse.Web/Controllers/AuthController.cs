using System.Runtime.CompilerServices;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Controllers;

public class AuthController : Controller
{
    private readonly IIdentityService _ıdentityService;

    public AuthController(IIdentityService ıdentityService)
    {
        _ıdentityService = ıdentityService;
    }

    public IActionResult SignIn()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInInput signInInput)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var response = await _ıdentityService.SignIn(signInInput);

        if (!response.IsSuccessful)
        {
            response.Errors.ForEach(x => ModelState.AddModelError(string.Empty,x));

            return View();
        }


        return RedirectToAction(nameof(Index), "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await _ıdentityService.RevokeRefreshToken();
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }
}