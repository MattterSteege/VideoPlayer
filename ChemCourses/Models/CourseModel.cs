using ChemCourses.Models;
using ChemCourses.Utils;

namespace ChemCourses.Models;

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
    public int selectedSectionItem { get; set; }
}

public class Section
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<SectionItem> Items { get; set; }
}

public abstract class SectionItem
{
    public string Name { get; set; }
    public Type Type => GetType();
    public int Id { get; set; }
    
    public abstract string RenderToHTML();
}


//SECTION ITEMS
#region TextExplanation
public class TextExplanation : SectionItem
{
    public enum TextTypes
    {
        Text,
        Markdown,
    }
    
    public string Text { get; set; }
    public TextTypes TextType { get; set; }

    public override string RenderToHTML()
    {
        switch (TextType)
        {
            case TextTypes.Text:
            default:
            {
                return  $"<div class=\"content-item\" data-type=\"text\" id=\"item-{Id}\" style=\"z-index: {10000 - Id}; display: {(Id == 1 ? "block" : "none")}\">\n" +
                        $"    <h1>{Name}</h1>\n" +
                        $"    <p>{Text}</p>\n" +
                        $"</div>";
            }
            
            case TextTypes.Markdown:
            {
                return  $"<div class=\"content-item\" data-type=\"markdown\" id=\"item-{Id}\" style=\"z-index: {10000 - Id}; display: {(Id == 1 ? "block" : "none")}\">\n" +
                        $"    <h1>{Name}</h1>\n" +
                        $"    {MarkdownUtils.ToHtml(Text)}\n" +
                        $"</div>";
            }
        }
    }
}
#endregion

#region Video
public class VideoExplanation : SectionItem
{
    public string VideoId { get; set; }

    public override string RenderToHTML()
    {
        return "<div class=\"content-item\" data-type=\"video\" id=\"item-" + Id + "\" style=\"z-index: " + (10000 - Id) + "; display: " + (Id == 1 ? "block" : "none") + "\">" +
              $"    <h1>{Name}</h1>" +
              $"    <video controls id=\"video-{Id}\" width=\"100%\" height=\"auto\" data-video-id=\"{VideoId}\"></video>" +
               "</div>";
    }
}
#endregion


#region questionneer
public class Questionneer : SectionItem
{
    public List<Question> Questions { get; set; }

    public override string RenderToHTML()
    {
        var html = "<div class=\"content-item\" data-type=\"questionneer\" id=\"item-" + Id + "\" style=\"z-index: " + (10000 - Id) + "; display: " + (Id == 1 ? "block" : "none") + "\">" +
                   $"    <h1>{Name}</h1>" +
                   "    <div class=\"questionneer-container\">";
        foreach (var question in Questions)
        {
            html += question.RenderToHTML();
        }
        html += "    </div>" +
                "</div>";
        return html;
    }
}

public class Question
{
    public string Text { get; set; }
    public List<Answer> Answers { get; set; }

    public string RenderToHTML()
    {
        var html = "<div class=\"question\">" +
                   $"    <p>{Text}</p>" +
                   "    <div class=\"answers\">";
        foreach (var answer in Answers)
        {
            html += answer.RenderToHTML();
        }
        html += "    </div>" +
                "</div>";
        return html;
    }
}

public class Answer
{
    public string Text { get; set; }
    public bool IsCorrect { get; set; }

    public string RenderToHTML()
    {
        return "<div class=\"answer\">" +
               $"    <input type=\"radio\" name=\"answer\" value=\"{IsCorrect}\" data-correct=\"{IsCorrect}\">" +
               $"    <label>{Text}</label>" +
               "</div>";
    }
}

#endregion