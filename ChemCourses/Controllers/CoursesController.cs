using BetterPages.utilities.attributes;
using ChemCourses.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ChemCourses.Controllers;

public class CoursesController : Controller
{
    List<Course> courses = new();

    private void Init()
    {
        courses.Add(new Course
        {
            Name = "BOE-schema",
            Description = "In deze lessenreeks word het BOE-schema uitgelegd. Dit schema is handig om te gebruiken bij het berekenen van de concentratie van een oplossing.",
            Banner = "https://img.youtube.com/vi/MT0ByfN3tKw/mqdefault.jpg",
            Id = new Guid("0F15B5E8-89F4-4211-875B-5EF3082A294C"),
            InThisCourse = new List<string>
            {
                "Wat is een BOE-schema?",
                "Hoe gebruik je een BOE-schema?"
            },
            Sections = new List<Section>
            {
                new Section
                {
                    Name = "Wat is een BOE-schema?",
                    Description = "In deze sectie wordt uitgelegd wat een BOE-schema is en hoe je deze kunt gebruiken.",
                    Items = new List<SectionItems>
                    {
                        new SectionItems
                        {
                            Name = "Wat is een BOE-schema?",
                            Description = "In deze video wordt uitgelegd wat een BOE-schema is.",
                            Video = $"{VideoPath}/BOE-schema/1.mp4",
                            Type = "video"
                        },
                        new SectionItems
                        {
                            Name = "Hoe gebruik je een BOE-schema?",
                            Description = "In deze video wordt uitgelegd hoe je een BOE-schema gebruikt.",
                            Video = $"{VideoPath}/BOE-schema/2.mp4",
                            Type = "video"
                        }
                    }
                },
                new Section
                {
                    Name = "Oefeningen",
                    Description = "In deze sectie kun je oefeningen maken om te kijken of je de stof begrijpt.",
                    Items = new List<SectionItems>
                    {
                        new SectionItems
                        {
                            Name = "Oefening 1",
                            Description = "Maak deze oefening om te kijken of je de stof begrijpt.",
                            Video = $"{VideoPath}/BOE-schema/3.mp4",
                            Type = "text"
                        },
                        new SectionItems
                        {
                            Name = "Oefening 2",
                            Description = "Maak deze oefening om te kijken of je de stof begrijpt.",
                            Video = $"{VideoPath}/BOE-schema/4.mp4",
                            Type = "question"
                        }
                    }
                },
                new Section
                {
                    Name = "Toets",
                    Description = "Maak deze toets om te kijken of je de stof begrijpt.",
                    Items = new List<SectionItems>
                    {
                        new SectionItems
                        {
                            Name = "Toets",
                            Description = "Maak deze toets om te kijken of je de stof begrijpt.",
                            Video = $"{VideoPath}/BOE-schema/5.mp4",
                            Type = "question"
                        }
                    }
                }
            },
            Length = 300,
            Difficulty = 1,
            Categories = new List<string>
            {
                "Theoretische chemie",
                "Concentratie"
            },
            Requirements = new List<string>
            {
                "Rekenmachine",
                "Pen en papier",
                "BiNaS"
            }
        });

    }

    [BetterPages]
    [Route("/Lessen")]
    public IActionResult Index()
    {
        if (courses.Count == 0)
            Init();

        return PartialView(courses);
    }
    
    [BetterPages]
    [Route("/Lessen/{courseId}")]
    public IActionResult Les(string courseId)
    {
        if (courses.Count == 0)
            Init();

        var course = courses.FirstOrDefault(c => c.Id == Guid.Parse(courseId));
        if (course == null)
            return NotFound();

        return PartialView(course);
    }
    
    [BetterPages]
    [Route("/Lessen/{courseId}/LesInhoud")]
    public IActionResult LesInhoud(string courseId)
    {
        if (courses.Count == 0)
            Init();

        var course = courses.FirstOrDefault(c => c.Id == Guid.Parse(courseId));
        if (course == null)
            return NotFound();

        return PartialView(course);
    }
    
    private string VideoPath => Environment.GetEnvironmentVariable("VIDEO_PATH") ?? "/app/videos";
}

public class Section
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<SectionItems> Items { get; set; }
}

public class SectionItems
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Video { get; set; }
    public string Type { get; set; }
}

public class Course
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Banner { get; set; }
    public Guid Id { get; set; }
    public List<string> InThisCourse { get; set; }
    public List<Section> Sections { get; set; }
    public int Length { get; set; }
    public string Duration => TimeUtils.SecondsToTime(Length);
    public int Difficulty { get; set; }
    public List<string> Categories { get; set; }
    public List<string> Requirements { get; set; }
}