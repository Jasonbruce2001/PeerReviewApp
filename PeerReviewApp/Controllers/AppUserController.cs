using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                // Assign the selected role to the user
                await _userManager.AddToRoleAsync(user, model.SelectedRole);

                // Sign in the user
                await _signInManager.SignInAsync(user, isPersistent: false);

                // Redirect based on the selected role
                if (model.SelectedRole == "Instructor")
                {
                    return RedirectToAction("Index", "Courses");  // Instructors go to courses
                }
                else if (model.SelectedRole == "Student")
                {
                    return RedirectToAction("Index", "Assignment");  // Students go to assignments
                }
                else if (model.SelectedRole == "Admin")
                {
                    return RedirectToAction("Index", "Institution");  // Admins go to institutions
                }

                // Default fallback
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

        // If we got this far, something failed, redisplay form
        // Make sure AvailableRoles is populated when redisplaying the form
        if (model.AvailableRoles == null || model.AvailableRoles.Count == 0)
        {
            model.AvailableRoles = new List<SelectListItem>
        {
            new SelectListItem { Value = "Student", Text = "Student" },
            new SelectListItem { Value = "Instructor", Text = "Instructor" }
            // Optionally add Admin
        };
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