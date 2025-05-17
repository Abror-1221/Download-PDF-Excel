using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DocumentationWebApp.Models;

namespace DocumentationWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login(string email, string password)
    {
        if (email == "admin@aegis.com" && password == "1234")
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Entry"});
        }

        TempData["LoginError"] = "Invalid email or password.";
        return RedirectToAction("Index", "Home");
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
