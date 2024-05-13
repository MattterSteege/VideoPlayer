using BetterPages.utilities.attributes;
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
        });
        
        courses.Add(new Course
        {
            Name = "Verdelingsevenwicht en oplosevenwicht",
            Description = "In deze video leer je wat het verdelingsevenwicht en het oplosevenwicht zijn. Ook leer je over betekenis Kv en de Ks. Als laatste leer je waarom het verdelingsevenwicht en oplosevenwicht geen chemische reacties zijn.",
            Banner = "https://img.youtube.com/vi/patvB12X7X4/mqdefault.jpg",
            Id = new Guid("3FE9FCAB-A021-42A4-A6C7-328B2F6EED8E"),
            InThisVideo = new List<string>
            {
                "Wat is verdelingsevenwicht?",
                "Wat is oplosevenwicht?",
                "Betekenis Kv en Ks",
                "Waarom geen chemische reacties?"
            },
        });
        
        courses.Add(new Course
        {
            Name = "Ligging van chemisch evenwicht",
            Description = "In deze video leer je wat de ligging van een chemisch evenwicht is. Ook leer je over de relatie tussen de evenwichtsconstante (K) en de ligging van het evenwicht.",
            Banner = "https://img.youtube.com/vi/ahAVS9PhVrI/mqdefault.jpg",
            Id = new Guid("4CB65001-2973-4E7A-A519-6BD7A5207B75"),
            InThisVideo = new List<string>
            {
                "Wat is de ligging van een chemisch evenwicht?",
                "Relatie tussen K en ligging evenwicht"
            },
        });
        
        courses.Add(new Course
        {
            Name = "K, Q en de evenwichtsvoorwaarde",
            Description = "In deze video leer je wat chemisch evenwicht is en welke drie kenmerken chemisch evenwicht heeft. ",
            Banner = "https://img.youtube.com/vi/My6poj9uZQU/mqdefault.jpg",
            Id = new Guid("64E7CD25-7DDD-4262-B02D-019ED563957F"),
            InThisVideo = new List<string>
            {
                "Wat is chemisch evenwicht?",
                "Kenmerken chemisch evenwicht"
            },
        });
        
        courses.Add(new Course
        {
            Name = "Wat is chemisch evenwicht?",
            Description = "In deze video leer je wat de ligging van een chemisch evenwicht is. Ook leer je over de relatie tussen de evenwichtsconstante (K) en de ligging van het evenwicht.",
            Banner = "https://img.youtube.com/vi/ZbhCQh3lS-A/mqdefault.jpg",
            Id = new Guid("E2CCF22C-8FA9-45E3-BBC3-38773861E759"),
            InThisVideo = new List<string>
            {
                "Wat is chemisch evenwicht?",
                "Kenmerken chemisch evenwicht"
            },
        });
    }
    
    
    [BetterPages]
    public IActionResult Index()
    {
        if (courses.Count == 0)
            init();
        
        

        return PartialView(courses);
    }
    
    [BetterPages]
    [Route("/Courses/Course/{courseId}")]
    public IActionResult Course(string courseId)
    {
        if (courses.Count == 0)
            init();
        
        foreach (var course in courses)
        {
            if (course.Id.ToString() == courseId)
            {
                return PartialView(course);
            }
        }
        
        return NotFound("Course not found.");
    }
}

public class Course
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Banner { get; set; }
    public Guid Id { get; set; }
    public List<string> InThisVideo { get; set; }
}