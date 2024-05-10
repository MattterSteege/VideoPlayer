using System.IO;
using ChemCourses.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[Route("api/[controller]")]
[ApiController]
public class VideoController : Controller
{
    private const string VideoPath = @"C:\Users\mattt\RiderProjects\ChemCourses\ChemCourses\wwwroot\videos";
    
    [HttpGet("{guid}/manifest")]
    public IActionResult GetManifest(string guid)
    {
#if DEBUG
        //no caching in debug mode
        Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
        Response.Headers.Add("Pragma", "no-cache");
        Response.Headers.Add("Expires", "0");
#endif
        
        if (!System.IO.File.Exists(Path.Combine(VideoPath, guid, "manifest.json")))
        {
            MpdParser parser = new MpdParser();
            VideoMPD mpd = parser.Parse(Path.Combine(VideoPath, guid, "stream.mpd"));
            System.IO.File.WriteAllText(Path.Combine(VideoPath, guid, "manifest.json"), JsonConvert.SerializeObject(mpd, Formatting.Indented));
            return Content(JsonConvert.SerializeObject(mpd), "application/json");
        }

        return Content(System.IO.File.ReadAllText(Path.Combine(VideoPath, guid, "manifest.json")), "application/json");
    }
    
    [HttpGet("{guid}/video/{segment}")]
    public IActionResult GetVideoSegment(string guid, string segment)
    {
        #if DEBUG
        //no caching in debug mode
        Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
        Response.Headers.Add("Pragma", "no-cache");
        Response.Headers.Add("Expires", "0");
        #endif
        
        var path = Path.Combine(VideoPath, guid, @"video\avc1", segment);
        return PhysicalFile(path, "application/octet-stream");
    }
    
    [HttpGet("{guid}/audio/{segment}")]
    public IActionResult GetAudioSegment(string guid, string segment)
    {
        #if DEBUG
        //no caching in debug mode
        Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
        Response.Headers.Add("Pragma", "no-cache");
        Response.Headers.Add("Expires", "0");
        #endif
        
        var path = Path.Combine(VideoPath, guid, @"audio\und\mp4a.40.2", segment);
        return PhysicalFile(path, "application/octet-stream");
    }
}
