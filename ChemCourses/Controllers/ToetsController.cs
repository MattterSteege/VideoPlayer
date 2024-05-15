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
        /*
         Possible Options:
            section
            text
            dropdown
            slider
            multiple choice
            checkbox
            true/false
            fill in the blank
            matching
         */

        Question Q1 = new TextQuestion()
            .SetTitle("Beschrijf het proces van covalente binding.")
            .SetDescription("Geef een gedetailleerde uitleg van hoe covalente bindingen worden gevormd en hoe ze verschillen van andere soorten chemische bindingen.")
            .SetSummary("Covalente bindingen")
            .SetMaxLength(1000);

        Question Q2 = new DropdownQuestion()
            .SetTitle("Welke van de volgende elementen is een edelgas?")
            .SetDescription("Selecteer het element dat behoort tot de edelgassen.")
            .SetSummary("Edelgassen")
            .SetOptions(new List<string> {"Helium", "Zuurstof", "Stikstof", "Koolstof"});

        Question Q3 = new SliderQuestion()
            .SetTitle("Op een schaal van 1 tot 14, hoe zuur is een oplossing met een pH van 3?")
            .SetDescription("Geef een numerieke beoordeling van de zuurgraad van de oplossing.")
            .SetSummary("Zuurgraad")
            .SetSliderRange(1, 14)
            .SetStep(0.1f);
        
        Question Q4 = new MultipleChoiceQuestion()
            .SetTitle("Welke van de volgende verbindingen is ionisch?")
            .SetDescription("Selecteer de verbinding die een ionische binding heeft.")
            .SetSummary("Ionische verbindingen")
            .SetOptions(new List<string> {"H2O", "CO2", "NaCl", "CH4"});

        Question Q5 = new MultipleChoiceQuestion()
            .SetTitle("Selecteer alle elementen die metalen zijn.")
            .SetDescription("Kies alle elementen die tot de categorie metalen behoren.")
            .SetSummary("Metalen")
            .SetOptions(new List<string> {"Waterstof", "IJzer", "Zuurstof", "Koper", "Zwavel"})
            .AllowMultipleSelections();
        
        Question Q6 = new TrueFalseQuestion()
            .SetTitle("Water is een polaire molecule.")
            .SetDescription("Geef aan of de volgende stelling waar of onwaar is.")
            .SetSummary("Polaire moleculen");
        
        Question Q7 = new FillInTheBlankQuestion()
            .SetText("Het atoomnummer van koolstof is {{6}}.")
            .SetDescription("Vul het ontbrekende getal in om de zin correct te maken.")
            .SetSummary("Atoomnummers");
        
        Question Q8 = new MatchingQuestion()
            .SetTitle("Koppel de chemische formule aan de juiste naam.")
            .SetDescription("Sleep de juiste naam naar de overeenkomstige chemische formule.")
            .SetSummary("Chemische formules")
            .SetQuestionAndAnswer(new List<string> {"H2O", "CO2", "NaCl", "CH4"}, new List<string> {"Water", "Koolstofdioxide", "Natriumchloride", "Methaan"});
        
        Question Q9 = new TextQuestion()
            .SetTitle("Leg uit waarom ionische verbindingen hoge smeltpunten hebben.")
            .SetDescription("Geef een gedetailleerde uitleg van de eigenschappen van ionische verbindingen die leiden tot hoge smeltpunten.")
            .SetSummary("Ionische verbindingen")
            .SetMaxLength(1000);
        
        Question Q10 = new DropdownQuestion()
            .SetTitle("Welk type chemische binding omvat het delen van elektronenparen tussen atomen?")
            .SetDescription("Selecteer het juiste type chemische binding dat wordt gevormd door het delen van elektronenparen.")
            .SetSummary("Chemische bindingen")
            .SetOptions(new List<string> {"Ionische binding", "Covalente binding", "Metallische binding"});
        
        Question Q11 = new SliderQuestion()
            .SetTitle("Beoordeel de reactiviteit van alkalimetalen op een schaal van 1 tot 10.")
            .SetDescription("Geef een numerieke beoordeling van de reactiviteit van alkalimetalen.")
            .SetSummary("Alkalimetalen")
            .SetSliderRange(1, 10)
            .SetStep(0.1f);
        
        Question Q12 = new MultipleChoiceQuestion()
            .SetTitle("Wat is het chemische symbool voor goud?")
            .SetDescription("Selecteer het juiste chemische symbool voor goud.")
            .SetSummary("Chemische symbolen")
            .SetOptions(new List<string> {"Ag", "Au", "Pb", "Hg"});
        
        Question Q13 = new MultipleChoiceQuestion()
            .SetTitle("Selecteer de volgende die verbindingen zijn.")
            .SetDescription("Kies de verbindingen uit de lijst.")
            .SetSummary("Chemische verbindingen")
            .SetOptions(new List<string> {"H2O", "O2", "NaCl", "He"});
        
        Question Q14 = new TrueFalseQuestion()
            .SetTitle("Een exotherme reactie geeft warmte af aan de omgeving.")
            .SetDescription("Geef aan of de volgende stelling waar of onwaar is.")
            .SetSummary("Exotherme reacties");
        
        Question Q15 = new FillInTheBlankQuestion()
            .SetText("Het proces waarbij een vaste stof direct overgaat in gas wordt {{sublimatie}} genoemd.")
            .SetDescription("Vul het ontbrekende woord in om de zin correct te maken.")
            .SetSummary("Fasenovergangen");
        
        Question Q16 = new MatchingQuestion()
            .SetTitle("Koppel het element aan het juiste atoomnummer.")
            .SetDescription("Sleep het juiste atoomnummer naar het overeenkomstige element.")
            .SetSummary("Atoomnummers")
            .SetQuestionAndAnswer(new List<string> {"Waterstof", "Zuurstof", "Koolstof", "Stikstof"}, new List<string> {"1", "8", "6", "7"});
        
        Question Q17 = new TextQuestion()
            .SetTitle("Wat is het verschil tussen een endotherme en een exotherme reactie.")
            .SetDescription("Geef een gedetailleerde uitleg van de verschillen tussen endotherme en exotherme reacties.")
            .SetSummary("Reacties")
            .SetMaxLength(1000);
        
        Question Q18 = new DropdownQuestion()
            .SetTitle("Welke van de volgende is een aardalkalimetaal?")
            .SetDescription("Selecteer het element dat behoort tot de aardalkalimetalen.")
            .SetSummary("Aardalkalimetalen")
            .SetOptions(new List<string> {"Magnesium", "Chloor", "Helium"});
        
        Question Q19 = new SliderQuestion()
            .SetTitle("Beoordeel de hardheid van diamant op een schaal van 1 tot 10.")
            .SetDescription("Geef een numerieke beoordeling van de hardheid van diamant.")
            .SetSummary("Diamant")
            .SetSliderRange(1, 10)
            .SetStep(0.1f);
        
        Question Q20 = new MultipleChoiceQuestion()
            .SetTitle("Welk element heeft de hoogste elektronegativiteit?")
            .SetDescription("Selecteer het element met de hoogste elektronegativiteit.")
            .SetSummary("Elektronegativiteit")
            .SetOptions(new List<string> {"Fluor", "Zuurstof", "Stikstof", "Chloor"});
        
        Question Q21 = new MultipleChoiceQuestion()
            .SetTitle("Selecteer de allotropen van koolstof.")
            .SetDescription("Kies de allotropen van koolstof uit de lijst.")
            .SetSummary("Allotropen")
            .SetOptions(new List<string> {"Diamant", "Grafiet", "Buckminsterfullereen", "Amorf koolstof"});
        
        Question Q22 = new TrueFalseQuestion()
            .SetTitle("De pH-schaal loopt van 0 tot 14.")
            .SetDescription("Geef aan of de volgende stelling waar of onwaar is.")
            .SetSummary("pH-schaal");
        
        Question Q23 = new FillInTheBlankQuestion()
            .SetText("Het getal van Avogadro is {{6.022}} × 10^23.")
            .SetDescription("Vul het ontbrekende getal in om de zin correct te maken.")
            .SetSummary("Getal van Avogadro");
        
        Question Q24 = new MatchingQuestion()
            .SetTitle("Koppel het type binding aan de beschrijving.")
            .SetDescription("Sleep het juiste type binding naar de overeenkomstige beschrijving.")
            .SetSummary("Chemische bindingen")
            .SetQuestionAndAnswer(new List<string> {"Ionische binding", "Covalente binding", "Metallische binding"}, new List<string> {"Gevormd door de overdracht van elektronen", "Gevormd door het delen van elektronen", "Gevormd door een zee van elektronen"});
        
        Question Q25 = new TextQuestion()
            .SetTitle("Definieer een katalysator en geef een voorbeeld.")
            .SetDescription("Geef een definitie van een katalysator en geef een voorbeeld van een katalysator in een chemische reactie.")
            .SetSummary("Katalysatoren")
            .SetMaxLength(1000);
        
        Form form = new Form()
            .SetTitle("Toets", "Test je kennis van chemie met deze toets.")
            .AddQuestions(Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10, Q11, Q12, Q13, Q14, Q15, Q16, Q17, Q18, Q19, Q20, Q21, Q22, Q23, Q24, Q25);

        return PartialView(form);
    }
}