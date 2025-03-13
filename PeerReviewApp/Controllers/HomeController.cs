using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PeerReviewApp.Models;

namespace PeerReviewApp.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<AppUser> _userManager;

    public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        // Check if user is logged in
        if (User.Identity.IsAuthenticated)
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);

            // Redirect based on role
            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return RedirectToAction("AdminDashboard");
            }
            else if (await _userManager.IsInRoleAsync(user, "Instructor"))
            {
                return RedirectToAction("InstructorDashboard");
            }
            else if (await _userManager.IsInRoleAsync(user, "Student"))
            {
                return RedirectToAction("StudentDashboard");
            }
        }

        // Default view for guests
        return View();
    }

    [Authorize(Roles = "Admin")]
    public IActionResult AdminDashboard()
    {
        return View();
    }

    [Authorize(Roles = "Instructor")]
    public IActionResult InstructorDashboard()
    {
        return View();
    }

    [Authorize(Roles = "Student")]
    public IActionResult StudentDashboard()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}