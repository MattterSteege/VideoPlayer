using System.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[Route("api/[controller]")]
[ApiController]
public class VideoController : Controller
{
    // #region old
    // /*
    //  
    //  These are the available endpoints:
    //     - GET /api/Video/videoDatabase
    //     - GET /api/Video/{videoId}/data
    //     - GET /api/Video/{videoId}/{fileName}
    //     
    //     # GET /api/Video/videoDatabase
    //     Returns the VideoDatabase.json file in the uploads folder. This file contains all the video data of all available videos.
    //     JSON FORMAT
    //     
    //     # GET /api/Video/{videoId}/data
    //     Returns the data of a specific video. The videoId is the unique identifier of the video. (GUID)
    //     JSON FORMAT
    //     
    //     # GET /api/Video/{videoId}/{fileName}
    //     Returns a specific .ts file of a video (fileName). The videoId is the unique identifier of the video. (GUID)
    //     .ts FORMAT
    //  
    //  */
    //
    // private const string VideoPath = @"C:\Users\mattt\RiderProjects\ChemCourses\ChemCourses\wwwroot\uploads";
    //
    // [HttpGet("videoDatabase")]
    // public IActionResult VideoDatabase()
    // {
    //     var json = Path.Combine(VideoPath, "VideoDatabase.json");
    //     return Content(System.IO.File.ReadAllText(json), "application/json");
    // }
    //
    // [HttpGet("{videoId}/data")]
    // public IActionResult Videodata(string videoId)
    // {
    //     #if DEBUG
    //     //no caching in debug mode
    //     Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
    //     Response.Headers.Add("Pragma", "no-cache");
    //     Response.Headers.Add("Expires", "0");
    //     #endif
    //     
    //     var json = Path.Combine(VideoPath, "VideoDatabase.json");
    //     var jsonText = System.IO.File.ReadAllText(json);
    //     var root = JsonConvert.DeserializeObject<List<Root>>(jsonText);
    //     
    //     foreach (var r in root)
    //     {
    //         if (r.VIDEOID == videoId)
    //         {
    //             //JSON object
    //             return Content(JsonConvert.SerializeObject(r), "application/json");
    //             
    //             //u3m8 object
    //             return PhysicalFile(Path.Combine(VideoPath, videoId, "index.u3m8"), "application/octet-stream");
    //         }
    //     }
    //     
    //     return NotFound();
    // }
    //
    // [HttpGet("{videoId}/{fileName}")]
    // public IActionResult StreamFile(string videoId, string fileName)
    // {
    //     #if DEBUG
    //     //no caching in debug mode
    //     Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
    //     Response.Headers.Add("Pragma", "no-cache");
    //     Response.Headers.Add("Expires", "0");
    //     #endif
    //     
    //     var path = Path.Combine(VideoPath, videoId, fileName);
    //     return PhysicalFile(path, "application/octet-stream");
    // }
    //
    // private string FindFileDirectory(string video)
    // {
    //     //get VideoDatabase.json in the uploads folder
    //     var json = Path.Combine(VideoPath, "VideoDatabase.json");
    //     var jsonText = System.IO.File.ReadAllText(json);
    //     var root = JsonConvert.DeserializeObject<List<Root>>(jsonText);
    //     
    //     foreach (var r in root)
    //     {
    //         if (r.VIDEOID == video)
    //         {
    //             return Path.Combine(VideoPath, r.VIDEOID);
    //         }
    //     }
    //     
    //     return null;
    // }
    //
    // public class Root
    // {
    //     [JsonProperty("VIDEO-ID")]
    //     public string VIDEOID { get; set; }
    //     public List<Segment> segments { get; set; }
    // }
    //
    // public class Segment
    // {
    //     public double duration { get; set; }
    //     public string file { get; set; }
    // }
    // #endregion
    
    private const string VideoPath = @"C:\Users\mattt\RiderProjects\ChemCourses\ChemCourses\wwwroot\videos";
    
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
