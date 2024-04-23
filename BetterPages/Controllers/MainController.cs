using BetterPages.utilities.attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetterPages.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index(string url = null)
        {
            if (url != null)
                ViewData["url"] = url;
            else if (HttpContext.Request.Cookies.ContainsKey("this_session_last_page"))
                ViewData["url"] = HttpContext.Request.Cookies["this_session_last_page"];
            else
                ViewData["url"] = "/";

            return View();
        }  
        
        [BetterPages]
        [Route("/test")]
        public IActionResult Test()
        {
            return PartialView();
        } 
        
        [BetterPages]
        [Route("/test2")]
        public IActionResult Test2()
        {
            return PartialView();
        } 
    }
}