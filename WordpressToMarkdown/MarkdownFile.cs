using WordpressToMarkdown.DataProviders;

namespace WordpressToMarkdown;

public static class MarkdownFile
{
    public static async Task SaveTo(this Post post, string path, CancellationToken cancellationToken = default)
    {
        string fullPath = Path.Combine(path, "posts", post.Date.ToString(@"yyyy\\MM"));
        Directory.CreateDirectory(fullPath);
        await File.WriteAllTextAsync(Path.Combine(fullPath, $"{post.PostName}.md"), post.Content, cancellationToken); 
    }
}