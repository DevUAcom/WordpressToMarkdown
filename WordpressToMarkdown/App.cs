using WordpressToMarkdown.Converters;
using WordpressToMarkdown.DataProviders;
using WordpressToMarkdown.Templates;

namespace WordpressToMarkdown;

public class App(IDataProvider dataProvider, AppSettings appSettings)
{
    public async Task Run(CancellationToken cancellationToken)
    {
        var posts = dataProvider.GetPosts();

        int postCount = 0;
        await Parallel.ForEachAsync(posts, cancellationToken, async (post, ct) =>
        {
            await post
                .ConvertCodeBlock()
                .ConvertSingleLineBreaks()
                .ConvertDialogs()
                .RemoveMore()
                
                .ConvertToMarkdown()
                .AddAstroHeader(appSettings.FeatureImagesLocation)
                
                .ConvertToc()
                .ConvertBoxes()
                
                .SaveTo(appSettings.OutputDirectory, ct)
            ;
            postCount++;
        });
        Console.WriteLine($"{postCount} posts converted.");
    }
}