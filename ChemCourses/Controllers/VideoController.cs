using Microsoft.AspNetCore.Mvc;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;


[ApiController]
[Route("[controller]")]
public class VideoController : ControllerBase
{
    private readonly string _videoFilePath = @"C:\Users\mattt\RiderProjects\ChemCourses\ChemCourses\wwwroot\videos\test_1.mp4";

    // Action to stream the video
    [HttpGet("StreamVideo/{ss}/{s}")]
    public IActionResult StreamVideo(int ss = 0, int s = 10) //startingSeconds, secondsDuration
    {
        var a = HttpContext;
        
        //if header Sec-Fetch-Dest: video, then return the video
        if (!Request.Headers.ContainsKey("sec-fetch-dest") && Request.Headers["sec-fetch-dest"] != "video")
        {
            return NotFound();
        }
        
        //use SplitVideo to split the video
        var videoPath = SplitVideo(_videoFilePath, "output.mp4", ss, s);
        var video = System.IO.File.OpenRead(videoPath.Filename);
        return File(video, "video/mp4");
    }
    
    static MediaFile SplitVideo(string inputFile, string outputFile, double startTime, double duration)
    {
        using (var engine = new Engine())
        {
            var inputFileInfo = new MediaFile { Filename = inputFile };
            var outputFileInfo = new MediaFile { Filename = outputFile };

            engine.GetMetadata(inputFileInfo);

            var options = new ConversionOptions { Seek = TimeSpan.FromSeconds(startTime), MaxVideoDuration = TimeSpan.FromSeconds(duration) };
            engine.Convert(inputFileInfo, outputFileInfo, options);
            
            return outputFileInfo;
        }
    }

    static double GetVideoDuration(string inputFile)
    {
        using (var engine = new Engine())
        {
            var inputFileInfo = new MediaFile { Filename = inputFile };

            engine.GetMetadata(inputFileInfo);

            return inputFileInfo.Metadata.Duration.TotalSeconds;
        }
    }
}