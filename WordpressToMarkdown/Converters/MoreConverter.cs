using WordpressToMarkdown.DataProviders;

namespace WordpressToMarkdown.Converters;

public static class MoreConverter
{
    public static Post RemoveMore(this Post post) => 
        post.WithContent(content => content.Replace("<!--more-->", ""));
}