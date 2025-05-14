using WordpressToMarkdown.DataProviders;

namespace WordpressToMarkdown.Tests.TestHelpers;

public static class PostBuilder
{
    public static Post FromContent(string content) =>
        new()
        {
            Content = content,
            Title = "",
            PostName = "",
        };
}