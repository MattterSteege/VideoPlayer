using BetterPages.utilities.attributes;
using ChemCourses.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ChemCourses.Controllers;

public class ToetsController : Controller
{
    //[BetterPages]
    [Route("/Toets/CreateForm")]
    public IActionResult CreateForm()
    {
        /*
         Possible Options:
            section
            text
            dropdown
            slider
            multiple choice
            checkbox
         */

        List<Question> questions = new List<Question>();

        Section Intro = new Section()
            .SetTitle("Introductie")
            .SetDescription("Deze vragen gaan over de introductie van de cursus. Vul de vragen zo goed mogelijk in.");
        
        Question Q1 = new TextQuestion()
            .SetTitle("Wat vond je van de introductie?")
            .SetDescription("Geef een cijfer van 1 tot 10.")
            .SetMaxLength(2);

        Question Q2 = new SliderQuestion()
            .SetTitle("Hoe duidelijk was de introductie?")
            .SetDescription("Geef een cijfer van 1 tot 10.")
            .SetSliderRange(1, 10)
            .SetDefaultValue(5)
            .SetStep(1);
        
        Question Q3 = new DropdownQuestion()
            .SetTitle("Hoe duidelijk was de introductie?")
            .SetDescription("Kies een van de opties.")
            .SetOptions(new List<string> { "Heel duidelijk", "Duidelijk", "Niet duidelijk" });

        Question Q4 = new MultipleChoiceQuestion()
            .SetTitle("Wat vond je van de introductie?")
            .SetDescription("Kies een van de opties.")
            .SetOptions(new List<string> {"Heel goed", "Goed", "Matig", "Slecht", "Heel slecht"});
        
        Question Q5 = new CheckboxQuestion()
            .SetTitle("Wil je vaker een introductie zoals deze?")
            .SetDescription("Klik op de checkbox als je vaker een introductie zoals deze wilt.")
            .SetOption("Ja");
        
        Intro.AddQuestions(Q1, Q2, Q3, Q4, Q5);
        
        Question Q6 = new TextQuestion()
            .SetTitle("Wat vond je van de eerste les?")
            .SetDescription("Geef een cijfer van 1 tot 10.")
            .SetMaxLength(2);
        

        // Creating the form
        Form form = new Form()
            .SetTitle("Chemistry Courses Survey", "Please fill out the survey below.")
            .AddDescription("Your feedback is important to us.")
            .AddSection(Intro)
            .AddQuetion(Q6)
            .AddSection(Intro);

        return PartialView(form);
    }
}