using BetterPages.utilities.attributes;
using ChemCourses.Models;
using Microsoft.AspNetCore.Mvc;
using Section = ChemCourses.Models.Section;

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
            selectedSectionItem = 1,
            Sections = new List<Section>
            {
                new Section
                {
                    Name = "Wat is een BOE-schema?",
                    Description = "In deze sectie wordt uitgelegd wat een BOE-schema is en hoe je deze kunt gebruiken.",
                    Items = new List<SectionItem>
                    {
                        new TextExplanation
                        {
                            Id = 1,
                            Name = "Wat is een Boe-Schema?",
                            Text = "Boe-Schema is een manier om de elektronenverdeling in een molecuul te beschrijven. Het is een schematische weergave van de elektronenverdeling in een molecuul. Het is een manier om de elektronenverdeling in een molecuul te beschrijven. Het is een schematische weergave van de elektronenverdeling in een molecuul.",
                            TextType = TextExplanation.TextTypes.Text
                        },
                        new TextExplanation
                        {
                            Id = 2,
                            Name = "Hoe gebruik je een Boe-Schema?",
                            Text = "Een evenwichtsreactie is een reactie die in beide richtingen werkt. Zulke reacties eindigen niet, zoals bij verbranding wel gebeurt, maar blijven doorgaan. Na een bepaalde tijd bereiken deze reacties een evenwicht waarin de concentraties van de stoffen constant blijven.\n\n### Evenwichtsreacties\n\nEen bekend voorbeeld van een evenwichtsreactie is de volgende reactievergelijking:\n\n3 H\u2082(g) + N\u2082(g) \u21cc 2 NH\u2083(g)\n\nZoals je ziet heeft deze reactievergelijking een dubbele pijl. Dat betekent dat er twee reacties plaatsvinden: de vorming van ammoniak uit waterstof en stikstof, en de omgekeerde reactie.\n\n#### Wat heb je nodig voor een evenwichtsreactie?\n\nEen evenwichtsreactie kan alleen plaatsvinden met vloeistoffen of gassen. Hierdoor is er een bepaalde molariteit (dus een bepaald aantal mol per liter) van elke stof aanwezig die wordt gebruikt om het evenwicht te vormen.\n\nDaarnaast vinden evenwichtsreacties alleen in gesloten ruimtes plaats. Als dit niet het geval is, kunnen de verschillende stoffen gemakkelijk verspreiden, waardoor ze niet meer met elkaar reageren.\n\n### Evenwichtsconstante\n\nDe evenwichtsconstante (K) van een reactie geeft aan in welke verhouding de verschillende stoffen aanwezig zijn in een evenwicht. Je kunt een aantal evenwichtsconstanten vinden in tabel 49 t/m 51 van je BiNaS. Bij de reactie met ammoniak bijvoorbeeld is de evenwichtsconstante bij kamertemperatuur 6,8 * 10^5. De evenwichtsconstante is gelijk aan de concentratiebreuk. Je kunt de concentratiebreuk van een bepaalde reactie op de volgende manier vinden.\n\nAls je reactievergelijking er zo uitziet:\n\nm A + n B \u21cc q C + r D\n\nDan wordt de concentratiebreuk:\n\nConcentratiebreuk = ([C]^q * [D]^r) / ([A]^m * [B]^n)\n\nDe coëfficiënten van de stoffen in de reactievergelijking (hoe vaak een stof voorkomt) komen dus als macht terug in de concentratiebreuk.\n\nIn het geval van ammoniak komt de concentratiebreuk er dan zo uit te zien:\n\nConcentratiebreuk = ([NH\u2083]^2) / ([H\u2082]^3 * [N\u2082])\n\n### BOE-schema\n\nAls je moet rekenen met evenwichtsreacties, dan is het handig om een BOE-schema te maken. Zo’n schema geeft de concentraties weer in het Begin, tijdens de Omzetting, en aan het Einde.\n\n#### Voorbeeld:\n\nEr wordt 2 mol NH\u2083 in een vat van 1,0 dm\u00b3 gestopt. Stel een BOE-schema op.\n\nIn het begin is er alleen 2 mol NH\u2083 aanwezig. In de omzetting verdwijnt er NH\u2083 en komt er H\u2082 en N\u2082 bij. Je kunt zien hoeveel er bij komt door naar de verhoudingen in de reactievergelijking te kijken. Als er 2 mol NH\u2083 verdwijnt, dan komt er 3 mol H\u2082 en 1 mol N\u2082 bij. We weten niet precies hoeveel stof er ontstaan is of verdwenen is, alleen de verhouding. Daarom noemen we dit x. De eindhoeveelheid bereken je dan door de hoeveelheid van het begin op te tellen bij hoeveel er bijkomt of afgaat in de omzetting. Het BOE-schema komt er dan zo uit te zien:\n\n|         | NH\u2083 | H\u2082  | N\u2082  |\n|---------|------|------|------|\n| Begin   | 2    | 0    | 0    |\n| Omzetting | -2x  | +3x  | +x   |\n| Eind    | 2-2x | 3x   | x    |\n\nVervolgens kun je de concentraties uit de eindtoestand invullen in de concentratiebreuk. Met de abc-formule kun je daarna berekenen hoeveel stof er in de overgangsfase is verdwenen of bijgekomen. Dit ziet er als volgt uit:\n\nConcentratiebreuk = ((2-2x)^2) / ((3x)^3 * x)\n\n---\n\n### Hoe gebruik je een BOE-schema?\n\nEen BOE-schema helpt je om de concentraties van de stoffen aan het begin, tijdens de omzetting, en aan het einde van de reactie overzichtelijk op een rij te zetten. Hier is hoe je het moet gebruiken:\n\n1. **Begin**: Noteer de initiële concentraties van alle stoffen.\n2. **Omzetting**: Schrijf de verandering in concentratie op, gebruikmakend van de verhouding uit de reactievergelijking. Gebruik een variabele zoals x om de veranderingen aan te geven.\n3. **Eind**: Bereken de eindconcentraties door de veranderingen uit de omzetting bij de beginconcentraties op te tellen of ervan af te trekken.\n4. **Concentratiebreuk**: Vul de eindconcentraties in de concentratiebreukformule in.\n5. **Oplossen**: Gebruik wiskundige methoden zoals de abc-formule om de waarde van x te vinden, wat aangeeft hoeveel stof er gereageerd heeft.\n\nDoor deze stappen te volgen, kun je de verhoudingen en concentraties in een evenwichtsreactie nauwkeurig bepalen.\n",
                            TextType = TextExplanation.TextTypes.Markdown
                        },
                        new VideoExplanation
                        {
                            Id = 3,
                            Name = "Wat is een Boe-Schema? (video)",
                            VideoId = "0f15b5e8-89f4-4211-875b-5ef3082a294c"
                        },
                        new Questionneer
                        {
                            Id = 4,
                            Name = "Test je kennis",
                            Questions = new List<Question>
                            {
                                new Question
                                {
                                    Text = "Wat is een Boe-Schema?",
                                    Answers = new List<Answer>
                                    {
                                        new Answer
                                        {
                                            IsCorrect = false,
                                            Text = "Een manier om de elektronenverdeling in een molecuul te beschrijven."
                                        },
                                        new Answer
                                        {
                                            IsCorrect = true,
                                            Text = "Een schematische weergave van de elektronenverdeling in een molecuul."
                                        },
                                        new Answer
                                        {
                                            IsCorrect = false,
                                            Text = "Een manier om de elektronenverdeling in een molecuul te beschrijven."
                                        }
                                    }
                                }
                            }
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