using System;
using System.IO;
using System.Reflection;
using BetterPages.utilities.attributes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace BetterPages.Controllers
{
    public class BetterPagesController : Controller
    {
        public static string fallback = "/Main";
        
        public IActionResult Index(string url = null)
        {
            if (url != null)
                HttpContext.Response.Cookies.Append("page_to_load", url);
            else if (HttpContext.Request.Cookies.ContainsKey("this_session_last_page"))
                HttpContext.Response.Cookies.Append("page_to_load", (HttpContext.Request.Cookies["this_session_last_page"] == "" ? fallback : HttpContext.Request.Cookies["this_session_last_page"]) ?? fallback);
            else
                HttpContext.Response.Cookies.Append("page_to_load", fallback);

            return View();
        }


        [Route("/BetterPages-framework.js")]
        public IActionResult BetterPagesFramework()
        {
            //get wwwroot/js/BetterPages-framework.js (this is a class library) problem is that this is called from the root of the project (BetterPages.example)
            // Get the directory where the current assembly (BetterPages) is located
            string assemblyDirectory = AppDomain.CurrentDomain.BaseDirectory;
            // Construct the path to the file relative to the assembly directory
            string filePath = Path.Combine(assemblyDirectory, "..", "..", "..", "..", "BetterPages", "wwwroot", "js", "BetterPages-framework.js");
            // Return the file
            return PhysicalFile(filePath, "application/javascript");
        }
    }
}