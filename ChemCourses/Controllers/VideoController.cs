using System;
using System.IO;
using ChemCourses.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[Route("api/[controller]")]
[ApiController]
public class VideoController : Controller
{
    private string VideoPath => Environment.GetEnvironmentVariable("VIDEO_PATH") ?? "/app/videos";

    [HttpGet("{guid}/manifest")]
    public IActionResult GetManifest(string guid)
    {
        // No caching in debug mode
        Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
        Response.Headers.Add("Pragma", "no-cache");
        Response.Headers.Add("Expires", "0");

        var manifestPath = Path.Combine(VideoPath, guid, "manifest.json");
        if (!System.IO.File.Exists(manifestPath))
        {
            MpdParser parser = new MpdParser();
            VideoMPD mpd = parser.Parse(Path.Combine(VideoPath, guid, "stream.mpd"));
            System.IO.File.WriteAllText(manifestPath, JsonConvert.SerializeObject(mpd, Formatting.Indented));
            return Content(JsonConvert.SerializeObject(mpd), "application/json");
        }

        return Content(System.IO.File.ReadAllText(manifestPath), "application/json");
    }

    [HttpGet("{guid}/video/{segment}")]
    public IActionResult GetVideoSegment(string guid, string segment)
    {
        // No caching in debug mode
        Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
        Response.Headers.Add("Pragma", "no-cache");
        Response.Headers.Add("Expires", "0");

        var path = Path.Combine(VideoPath, guid, "video", "avc1", segment);
        return PhysicalFile(path, "application/octet-stream");
    }

    [HttpGet("{guid}/audio/{segment}")]
    public IActionResult GetAudioSegment(string guid, string segment)
    {
        // No caching in debug mode
        Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
        Response.Headers.Add("Pragma", "no-cache");
        Response.Headers.Add("Expires", "0");

        var path = Path.Combine(VideoPath, guid, "audio", "und", "mp4a.40.2", segment);
        return PhysicalFile(path, "application/octet-stream");
    }
}