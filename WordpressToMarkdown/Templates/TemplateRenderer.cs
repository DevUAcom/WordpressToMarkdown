using Scriban;
using WordpressToMarkdown.Converters;
using WordpressToMarkdown.DataProviders;

namespace WordpressToMarkdown.Templates;

public static class TemplateRenderer
{
    private static readonly Template Template;

    static TemplateRenderer()
    {
        Template = Template.Parse(MarkdownTemplate.Template);
    }
    public static Post AddAstroHeader(this Post post, string featureImagesLocation)
    {
        var featureImage = post.FeatureImage is null ? null : Path.GetFileName(post.FeatureImage);
        var result = Template.Render(new
        {
            Title = post.Title.Replace("\"", "\\\""), 
            Slug = post.PostName,
            Id = post.PostId,
            Content = post.Content,
            Date = post.Date.ToString("yyyy-MM-dd"),
            Description = post.Excerpt,
            Image = featureImage is null ? null : $"{featureImagesLocation}{featureImage}",
            Categories = post.Categories,
            Tags = post.Tags is null ? "" : string.Join(",", post.Tags.Split(',').Select(s => $"\"{s}\"")),
            Toc = post.Content.HasToc(),
        });

        return post with {Content = result};
    }
}