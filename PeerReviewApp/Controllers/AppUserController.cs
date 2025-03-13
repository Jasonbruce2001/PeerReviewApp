using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PeerReviewApp.Models;

namespace PeerReviewApp.Controllers;

public class AppUserController : Controller
{
    private readonly UserManager<AppUser> _userManager; 
    private readonly SignInManager<AppUser> _signInManager;

    public AppUserController(UserManager<AppUser> userMngr, SignInManager<AppUser> signInMngr)
    {
        _userManager = userMngr; _signInManager = signInMngr; 
    }

    // GET
    [HttpGet]
    public IActionResult Register()
    {
        var model = new RegisterVm
        {
            AvailableRoles = new List<SelectListItem>
        {
            new SelectListItem { Value = "Student", Text = "Student" },
            new SelectListItem { Value = "Instructor", Text = "Instructor" }
        }
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVm model)
    {
        if (ModelState.IsValid)
        {
            DateTime date = DateTime.Now;
            var user = new AppUser() { UserName = model.Username, AccountAge = date };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // Remove role assignment code
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult LogIn(string returnURL = "")
    {
        var model = new LogInVM { ReturnUrl = returnURL };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(LogInVM model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync
                (model.Username, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) &&
                    Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        ModelState.AddModelError(string.Empty, "Invalid username or password.");
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}