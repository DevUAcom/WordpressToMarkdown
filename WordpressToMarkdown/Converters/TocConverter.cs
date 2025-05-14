using WordpressToMarkdown.DataProviders;

namespace WordpressToMarkdown.Converters;

public static class TocConverter
{
    public static bool HasToc(this string content) => 
        content.Contains("[toc]");

    public static Post ConvertToc(this Post post) => 
        post.WithContent(content => content.Replace("[toc]", "<!-- toc -->"));
}