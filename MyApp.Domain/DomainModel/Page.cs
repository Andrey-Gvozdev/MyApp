using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Swashbuckle.AspNetCore.Annotations;

namespace MyApp.Domain.DomainModel;
public class Page : Creative
{
    [SwaggerSchema(ReadOnly = true)]
    public List<PageSnippet> PageSnippets { get; set; } = new List<PageSnippet>();

    public Page(string name, string content)
        : base(name, content)
    {
    }

    public override void SetContent(string content)
    {
        this.Content = CorrectHtml(content);
        this.SetPageSnippetList();
    }

    private static IEnumerable<string> FindSnippetNames(string content)
    {
        Regex regex = new ("#SNIPPET.([^#]+)#");
        MatchCollection matches = regex.Matches(content);

        return matches.Select(x => x.Groups[1].Value);
    }

    private static string CorrectHtml(string content)
    {
        var htmlDoc = new HtmlDocument();
        string res;

        htmlDoc.LoadHtml(content);

        content = content.Replace("<!DOCTYPE html>", string.Empty);

        var htmlHtml = htmlDoc.DocumentNode.SelectSingleNode("//html");
        var htmlHead = htmlDoc.DocumentNode.SelectSingleNode("//head");
        var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//body");

        if (htmlHtml == null)
        {
            if (htmlHead == null && htmlBody == null)
            {
                res = "<!DOCTYPE html>\n" + "<html>\n" + "<head></head>\n" + "<body>\n" + content + "\n</body>" + "\n</html>";
            }
            else
            {
                if (htmlHead == null)
                {
                    htmlHead = htmlDoc.CreateElement("head");
                }

                if (htmlBody == null)
                {
                    htmlBody = htmlDoc.CreateElement("body");
                }

                res = "<!DOCTYPE html>\n" + "<html>\n" + htmlHead.OuterHtml + "\n" + htmlBody.OuterHtml + "\n</html>";
            }
        }
        else
        {
            res = "<!DOCTYPE html>\n" + htmlHtml.OuterHtml;
        }

        return res;
    }

    private void SetPageSnippetList()
    {
        IEnumerable<string> snippetNamesList = FindSnippetNames(this.Content);

        if (snippetNamesList.Any())
        {
            this.PageSnippets = new List<PageSnippet>();

            foreach (var item in snippetNamesList)
            {
                this.PageSnippets.Add(new PageSnippet(item));
            }
        }
    }
}