using System.Text.RegularExpressions;
using WordpressToMarkdown.DataProviders;

namespace WordpressToMarkdown.Converters;

public static class CodeBlockConverter
{
    public static Post ConvertCodeBlock(this Post post)
    {
        const string pattern = """<pre class="lang:([^"\s]+)[^>]*?">(.*?)</pre>""";
        
        var result = Regex.Replace(post.Content, pattern, match =>
        {
            string lang = SubstituteLang(match.Groups[1].Value);
            string code = match.Groups[2].Value.Trim();
            return $"```{lang}\r\n{code}\r\n```";
        }, RegexOptions.Singleline);

        return post with { Content = result };
    }

    private static string SubstituteLang(string lang) =>
        lang switch
        {
            "default" => "html",
            "asp" => "csharp",
            "xhtml" => "html",
            _ => lang,
        };
}