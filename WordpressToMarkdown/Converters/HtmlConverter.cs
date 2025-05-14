using ReverseMarkdown;
using WordpressToMarkdown.DataProviders;

namespace WordpressToMarkdown.Converters;

public static class HtmlConverter
{
    private static readonly Converter Converter;

    static HtmlConverter()
    {
        Converter = new ReverseMarkdown.Converter(
            new ReverseMarkdown.Config
            {
                PassThroughTags = ["img"],
            }
        );
    }

    public static Post ConvertToMarkdown(this Post post) =>
        post.WithContent(content => Converter.Convert(content));
}