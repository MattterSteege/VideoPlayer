using BetterPages.utilities.attributes;
using ChemCourses.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ChemCourses.Controllers;

public class ToetsController : Controller
{
    [BetterPages]
    [Route("/Toets/CreateForm")]
    public IActionResult CreateForm()
    {
        // Creating form sections
        Question personalInfoSection = new Section()
            .SetTitle("Personal Information")
            .AddDescription("We need your personal information to better serve you.");

        Question courseInfoSection = new Section()
            .SetTitle("Course Information")
            .AddDescription("Choose the courses you are interested in attending.");

        // Creating individual questions
        Question nameQuestion = new TextQuestion()
            .SetDescription("Please input your name here")
            .SetPlaceholder("John Doe")
            .SetTitle("Name");

        Question emailQuestion = new TextQuestion()
            .SetDescription("Please enter your email address. E.g. example@gmail.com")
            .SetPlaceholder("example@gmail.com")
            .SetTitle("Email");

        Question courseSelectionQuestion = new DropdownQuestion()
            .SetId("courses")
            .SetLabel("Select Courses")
            .AddOption("Chemistry 101")
            .AddOption("Organic Chemistry")
            .AddOption("Chemical Engineering")
            .SetDefaultValue("")
            .SetValue("");

        Question satisfactionQuestion = new SliderQuestion()
            .SetId("satisfaction")
            .SetLabel("Overall Satisfaction")
            .SetSliderRange(5, 11)
            .SetSliderStep(2)
            .SetSliderDefaultValue(8);

        Question interestsQuestion = new MultipleChoiceQuestion()
            .SetId("interests")
            .SetLabel("Select Your Interests")
            .AddOption("Lab Work")
            .AddOption("Research")
            .AddOption("Teaching")
            //.AllowMultipleSelections()
            .SetDefaultValue("Lab Work");

        Question subscriptionQuestion = new CheckboxQuestion()
            .SetId("subscription")
            .SetLabel("Subscribe to Newsletter")
            .SetOption("Yes, subscribe me to the newsletter");

        // Creating the form
        Form form = new Form()
            .SetTitle("Chemistry Courses Survey", "Please fill out the survey below.")
            .AddDescription("Your feedback is important to us.")
            .AddSection(personalInfoSection)
            .AddSection(courseInfoSection)
            .AddSection(nameQuestion)
            .AddSection(emailQuestion)
            .AddSection(courseSelectionQuestion)
            .AddSection(satisfactionQuestion)
            .AddSection(interestsQuestion)
            .AddSection(subscriptionQuestion);
        
        return PartialView(form);
    }
}