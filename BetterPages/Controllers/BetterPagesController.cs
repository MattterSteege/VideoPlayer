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
        public static string fallback = "/Dashboard";
        
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
            return Redirect("/js/BetterPages-framework.js");
        }
    }
}