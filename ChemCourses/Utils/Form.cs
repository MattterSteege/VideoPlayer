using System.Text;

namespace ChemCourses.Utils;

public class Form
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Description { get; set; }
    public List<Question> Questions { get; set; }
    public string SubmitUrl { get; set; }

    public Form()
    {
        Questions = new List<Question>();
    }

    public Form SetTitle(string title, string subtitle)
    {
        Title = title;
        Subtitle = subtitle;
        return this;
    }

    public Form AddDescription(string description)
    {
        Description = description;
        return this;
    }

    public Form AddSection(Question question)
    {
        Questions.Add(question);
        return this;
    }
    
    public Form SetSubmitUrl(string url)
    {
        SubmitUrl = url;
        return this;
    }
    
    public string RenderToHTML()
    {
        StringBuilder htmlBuilder = new StringBuilder();

        foreach (var question in Questions)
        {
            htmlBuilder.Append("<div class='question'>");
            htmlBuilder.Append(question.RenderToHTML());
            htmlBuilder.Append("</div>");
        }

        return htmlBuilder.ToString();
    }
}