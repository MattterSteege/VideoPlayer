// using BetterPages.utilities.attributes;
// using ChemCourses.Utils;
// using Microsoft.AspNetCore.Mvc;
//
// namespace ChemCourses.Controllers;
//
// public class ToetsController : Controller
// {
//     // [BetterPages]
//     [Route("/Toets/CreateForm")]
//     public IActionResult CreateForm()
//     {
//         /*
//          Possible Options:
//             section
//             text
//             dropdown
//             slider
//             multiple choice
//             checkbox
//             true/false
//             fill in the blank
//             matching
//          */
//
//         Question Q1 = new TextQuestion()
//             .SetTitle("Geef een korte uitleg.")
//             .SetDescription("Wat is een boe-schema en waar wordt het voor gebruikt?")
//             .SetSummary("Boe-schema")
//             .SetMaxLength(1000);
//         
//         Question Q2 = new MatchingQuestion()
//             .SetTitle("Match de volgende begrippen.")
//             .SetDescription("Wat betekenen de letters B, O en E in een boe-schema?")
//             .SetSummary("Boe-schema")
//             .SetQuestionAndAnswer(new List<string> { "B", "O", "E" }, new List<string> { "beginsituatie", "omgezet", "eindesituatie" });
//         
//         Question Q3 = new TextQuestion()
//             .SetTitle("Geef een korte uitleg.")
//             .SetDescription("Hoe bereken je de hoeveelheid stikstof, waterstof en ammoniak in de evenwichtsituatie met behulp van het busschema?")
//             .SetSummary("Molrekenen");
//         
//         Question Q4 = new TextQuestion()
//             .SetTitle("Geef een korte uitleg.")
//             .SetDescription("Kun je uitleggen hoe je een boe-schema moet gebruiken om de evenwichtsconcentraties te bepalen?")
//             .SetSummary("Evenwichtsberekeningen");
//
//         
//         Form form = new Form()
//             .SetTitle("Toets", "Test je kennis van chemie met deze toets.")
//             .AddQuestions(Q1, Q2, Q3, Q4);
//
//         return Json(form.RenderToJSON());
//     }
// }