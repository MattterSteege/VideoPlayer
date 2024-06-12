using System.Text;
using System.Text.RegularExpressions;

namespace ChemCourses.Utils;

public class MarkdownUtils
{
    public static string ToHtml(string markdown)
    {
        return Parse(markdown);
    }
    
    public static string Parse(string markdown)
    {
        var html = new StringBuilder();
        var lines = markdown.Split('\n');
        bool inCodeBlock = false;

        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            
            
            if (line == string.Empty || line == "")
            {
                html.AppendLine();
                continue;
            }

            if (line.StartsWith("```")) // Code block
            {
                inCodeBlock = !inCodeBlock;
                html.AppendLine(inCodeBlock ? "<pre><code>" : "</code></pre>");
                continue;
            }

            if (inCodeBlock)
            {
                html.AppendLine(line);
                continue;
            }

            //headings
            if (line.StartsWith("#"))
            {
                var headerLevel = line.TakeWhile(c => c == '#').Count();
                var headerText = line.Substring(headerLevel).Trim();
                html.AppendLine($"<h{headerLevel}>{headerText}</h{headerLevel}>");
            }
            
            //divider
            else if (line == "---")
            {
                html.AppendLine("<hr>");
            }
            
            //List 1. 2. 3. etc. (ordered list)
            else if (Regex.IsMatch(line, @"^\d+\.\s"))
            {
                html.AppendLine("<ol>");
                html.AppendLine($"<li>{NormalFormatting(line.Substring(3))}</li>");
                
                for (int j = i + 1; j < lines.Length; j++)
                {
                    if (Regex.IsMatch(lines[j], @"^\d+\.\s"))
                    {
                        html.AppendLine($"<li>{NormalFormatting(lines[j].Substring(3))}</li>");
                        i++;
                    }
                    else
                        break;
                }
                
                html.AppendLine("</ol>");
            }
            
            //List - - - etc. (unordered list)
            else if (Regex.IsMatch(line, @"^-+\s"))
            {
                html.AppendLine("<ul>");
                html.AppendLine($"<li>{NormalFormatting(line.Substring(2))}</li>");
                
                for (int j = i + 1; j < lines.Length; j++)
                {
                    if (Regex.IsMatch(lines[j], @"^-+\s"))
                    {
                        html.AppendLine($"<li>{NormalFormatting(lines[j].Substring(2))}</li>");
                        i++;
                    }
                    else
                        break;
                }
                
                html.AppendLine("</ul>");
            }
            
            //table
            else if (Regex.IsMatch(line, @"\|.+\|"))
            {
                List<string> tableLines = new List<string>();
                tableLines.Add(line);
                
                for (int j = i + 1; j < lines.Length; j++)
                {
                    if (lines[j].Contains("|"))
                    {
                        tableLines.Add(lines[j]);
                        i++;
                    }
                    else
                        break;
                }
                
                html.AppendLine(ParseTable(tableLines));
            }
            
            //paragraph text (bold, italic, underline, strikethrough, inline code)
            else
            {
                html.AppendLine($"<p>{NormalFormatting(line)}</p>");
            }
        }

        return html.ToString();
    }
    
    private static string NormalFormatting(string line)
    {
        line = Regex.Replace(line, @"(\*\*|__)([a-zA-Z0-9]+.*?)\1", "<strong>$2</strong>"); // Bold
        line = Regex.Replace(line, @"(\*|_)([a-zA-Z0-9]+.*?)\1", "<em>$2</em>"); // Italic
        line = Regex.Replace(line, @"~~([a-zA-Z0-9]+.*?)~~", "<del>$1</del>"); // Strikethrough
        line = Regex.Replace(line, @"__([a-zA-Z0-9]+.*?)__", "<u>$1</u>"); // Underline
        line = Regex.Replace(line, @"`([a-zA-Z0-9]+.*?)`", "<code>$1</code>"); // Inline Code
        line = Regex.Replace(line, @"\[(.+)\]\((.+)\)", "<a href=\"$2\">$1</a>");
        return line;
    }

    private static string ParseTable(List<string> lines)
    {
        var table = new StringBuilder();
        table.AppendLine("<table>");
        
        foreach (var line in lines)
        {
            //if line only contains | and - skip
            if (line.All(c => c == '|' || c == '-'))
                continue;
            
            var cells = line.Split('|').Where(c => c != "").Select(c => c.Trim()).ToList();
            table.AppendLine("<tr>");
            foreach (var cell in cells)
            {
                table.AppendLine($"<td>{cell}</td>");
            }
            table.AppendLine("</tr>");
        }
        
        table.AppendLine("</table>");
        return table.ToString();
    }
}