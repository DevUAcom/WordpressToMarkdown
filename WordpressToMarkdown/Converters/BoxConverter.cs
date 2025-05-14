using System.Text.RegularExpressions;
using WordpressToMarkdown.DataProviders;

namespace WordpressToMarkdown.Converters;

public static class BoxConverter
{
    public static Post ConvertBoxes(this Post post) =>
        post.WithContent(content =>
        {
            string pattern = """\[(?:box\s+type="(\w+)"|stextbox\s+id="(\w+)")[^\]]*\]""";

            return Regex
                .Replace(content, pattern, match =>
                {
                    string value = match.Groups[1].Success
                        ? match.Groups[1].Value
                        : match.Groups[2].Success
                            ? match.Groups[2].Value
                            : "info";
                    return $":::{value}\r\n";
                })
                .Replace("[/box]", "\r\n:::")
                .Replace("[/stextbox]", "\r\n:::")
                ;

        });
}