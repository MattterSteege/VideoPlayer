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
    
    [BetterPages]
    [Route("/Bestelgeschiedenis")]
    public IActionResult BestelGeschiedenis()
    {
        dynamic orders = new List<dynamic>
        {
            new {id = 67, date = new DateTime(2024, 1, 1), price = 10.00, state = "Betaald", omschrijving = "1 maand toegang"},
            new {id = 53, date = new DateTime(2024, 2, 1), price = 40.00, state = "Verwerkt", omschrijving = "3 maanden toegang"},
            new {id = 2, date = new DateTime(2024, 5, 1), price = 10.00, state = "Bezig", omschrijving = "1 maand toegang"},
            new {id = 39, date = new DateTime(2024, 6, 1), price = 10.00, state = "Geannuleerd", omschrijving = "1 maand toegang"},
            new {id = 8, date = new DateTime(2024, 7, 1), price = 10.00, state = "Betaald", omschrijving = "1 maand toegang"},
            
        };
        return PartialView(orders);
    }
    
    [Route("/404")]
    public IActionResult Error404()
    {
        return NotFound("You've been hit with an uno reverse card! (404)");
    }

    [BetterPages]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return PartialView(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}