using Shouldly;
using WordpressToMarkdown.Converters;
using WordpressToMarkdown.Tests.TestHelpers;

namespace WordpressToMarkdown.Tests.Converters.TocConverterTests;

public class ConvertToc
{
    [Fact]
    public void Should_Convert_Toc()
    {
        const string content = """
                               <p>
                                lorem ipsum
                                [toc]
                                lorem ipsum
                               </p>
                               """;

        var result = PostBuilder.FromContent(content).ConvertToc();
        
        result.Content.ShouldBe("""
                        <p>
                         lorem ipsum
                         <!-- toc -->
                         lorem ipsum
                        </p>
                        """
                        );
    }
}