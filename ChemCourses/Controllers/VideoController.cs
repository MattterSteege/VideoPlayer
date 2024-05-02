using System.IO;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class VideoController : ControllerBase
{
    private const string VideoPath = @"C:\Users\mattt\RiderProjects\ChemCourses\ChemCourses\wwwroot\uploads";

    //I have 100 .ts files in the uploads folder test1.ts, test2.ts, test3.ts, etc. and a test.m3u8 file can you help me with the code to stream the video? I want to be able to just request one file after another and have it play in the browser.
    [HttpGet("{video}")]
    public IActionResult Stream(string video)
    {
        if (video.EndsWith(".ts"))
        {
            return StreamTs(video);
        }
        
        var path = Path.Combine(VideoPath, $"{video}", $"{video}.m3u8");
        return PhysicalFile(path, "application/x-mpegURL");
    }
    
    [HttpGet("{video}.ts")]
    public IActionResult StreamTs(string video)
    {
        var directory = FindFileDirectory(video);
        var path = Path.Combine(directory, $"{video}.ts");
        return PhysicalFile(path, "video/mp2t");
    }
    
    private string FindFileDirectory(string video)
    {
        //directories in VideoPath
        var directories = Directory.GetDirectories(VideoPath);
        foreach (var directory in directories)
        {
            var files = Directory.GetFiles(directory);
            foreach (var file in files)
            {
                if (file.Contains(video))
                {
                    //return the directory
                    return directory;
                }
            }
        }
        return null;
    }
}