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
        return PartialView();
    }

    [BetterPages]
    [Route("/Toetsen")]
    public IActionResult Toetsen()
    {
        return PartialView();
    }
    
    [Route("/404")]
    public IActionResult Error404()
    {
        return NotFound("You've been hit with an uno reverse card! (404)");
    }
    
    [BetterPages]
    [Route("/bestelgeschiedenis")]
    public IActionResult BestelGeschiedenis()
    {
        return PartialView();
    }
    
    [BetterPages]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return PartialView(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}