using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BetterPages.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace BetterPages.utilities;

public class BetterPagesMiddleware
{
    private readonly RequestDelegate _next;

    public BetterPagesMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path != "/")
        {
            await _next(context);
            return;
        }
        
        var originalBodyStream = context.Response.Body;

        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;

            await _next(context);

            // After the request has been processed, modify the response if necessary
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            string responseContent;
            using (var reader = new StreamReader(context.Response.Body))
            {
                responseContent = await reader.ReadToEndAsync();
            }
            
            if (!responseContent.Contains("</head>"))
            {
                await _next(context);
                return;
            }
            

            responseContent = AddScriptToHead(responseContent);

            // Use a new MemoryStream to write the modified content
            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(responseContent)))
            {
                await memoryStream.CopyToAsync(originalBodyStream);
            }
        }
    }


    private string AddScriptToHead(string htmlContent)
    {
        const string scriptTag = "<script src=\"/BetterPages-framework.js\"></script>";
        const string headEndTag = "</head>";

        var indexOfHeadEnd = htmlContent.IndexOf(headEndTag, StringComparison.OrdinalIgnoreCase);

        if (indexOfHeadEnd >= 0)
        {
            // Insert the script tag just before the </head> tag
            htmlContent = htmlContent.Insert(indexOfHeadEnd, scriptTag);
        }

        return htmlContent;
    }
}

public static class BetterPagesMiddlewareExtensions
{
    public static IApplicationBuilder UseBetterPagesMiddleware(this IApplicationBuilder builder, string fallback = "/Main")
    {
        //set the fallback page
        BetterPagesController.fallback = fallback;
        
        return builder.UseMiddleware<BetterPagesMiddleware>();
    }
}