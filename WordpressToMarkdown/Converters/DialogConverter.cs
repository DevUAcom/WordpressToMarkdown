using System.Text.RegularExpressions;
using WordpressToMarkdown.DataProviders;

namespace WordpressToMarkdown.Converters;

public static class DialogConverter
{
    public static Post ConvertDialogs(this Post post) =>
        post.WithContent(content => Regex.Replace(
            post.Content,
            "^-[  ](.+?)\r\n", // Match lines starting with "- " and ending with \r\n
            "\\- $1\r\n", // Replace with "\- " and add trailing spaces
            RegexOptions.Multiline // Ensure ^ and $ work on each line
        ));
}