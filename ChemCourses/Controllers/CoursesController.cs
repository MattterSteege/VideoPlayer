using BetterPages.utilities.attributes;
using ChemCourses.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ChemCourses.Controllers;

public class CoursesController : Controller
{
    List<Course> courses = new();

    private void init()
    {
        courses.Add(new Course
        {
            Name = "BOE-schema",
            Description = "In deze video wordt uitgelegd wat een BOE-schema is en hoe het toegepast kan worden om evenwichtsberekeningen uit te voeren.",
            Banner = "https://img.youtube.com/vi/MT0ByfN3tKw/mqdefault.jpg",
            Id = new Guid("0F15B5E8-89F4-4211-875B-5EF3082A294C"),
            InThisVideo = new List<string>
            {
                "Wat is een BOE-schema?",
                "Hoe gebruik je een BOE-schema?",
                "Rekenvoorbeeld"
            },
            length = 4 * 60 + 19, //4:19
            Difficulty = new Random().Next(1, 6)
        });

    }

    [BetterPages]
    [Route("/Lessen")]
    public IActionResult Index()
    {
        if (courses.Count == 0)
            init();

        return PartialView(courses);
    }
    
    [BetterPages]
    [Route("/Lessen/{courseId}")]
    public IActionResult Les(string courseId)
    {
        if (courses.Count == 0)
            init();

        var course = courses.FirstOrDefault(c => c.Id == Guid.Parse(courseId));
        if (course == null)
            return NotFound();

        return PartialView(course);
    }
    
    private string VideoPath => Environment.GetEnvironmentVariable("VIDEO_PATH") ?? "/app/videos";
}

public class Course
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Banner { get; set; }
    public Guid Id { get; set; }
    public List<string> InThisVideo { get; set; }
    public int length { get; set; }
    public string Duration => TimeUtils.SecondsToTime(length);
    public int Difficulty { get; set; }
}