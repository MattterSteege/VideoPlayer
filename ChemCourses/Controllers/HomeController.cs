using System.Diagnostics;
using BetterPages.utilities.attributes;
using Microsoft.AspNetCore.Mvc;
using ChemCourses.Models;

namespace ChemCourses.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [BetterPages]
    [Route("/Dashboard")]
    public IActionResult Dashboard()
    {
        Thread.Sleep(1000);
        return PartialView();
    }

    [BetterPages]
    [Route("/Videos")]
    public IActionResult Videos()
    {
        Thread.Sleep(1000);
        return PartialView();
    }
    
    [BetterPages]
    [Route("/Toetsen")]
    public IActionResult Toetsen()
    {
        Thread.Sleep(1000);
        return PartialView();
    }
    
    [BetterPages]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return PartialView(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}