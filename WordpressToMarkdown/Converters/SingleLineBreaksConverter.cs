using System.Text.RegularExpressions;
using WordpressToMarkdown.DataProviders;

namespace WordpressToMarkdown.Converters;

public static class SingleLineBreaksConverter
{
    public static Post ConvertSingleLineBreaks(this Post post) =>
        post.WithContent(content => Regex.Replace(
            content,
            @"(?<!\r\n)\r\n(?!\r\n)",
            "  \r\n",
            RegexOptions.Multiline
        ));
}