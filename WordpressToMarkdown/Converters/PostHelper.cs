using WordpressToMarkdown.DataProviders;

namespace WordpressToMarkdown.Converters;

public static class PostHelper
{
    public static Post WithContent(this Post post, Func<string, string> updateContent) => 
        post with { Content = updateContent(post.Content) };
}