using Shouldly;
using WordpressToMarkdown.Converters;

namespace WordpressToMarkdown.Tests.Converters.TocConverterTests;

public class HasToc
{
    [Fact]
    public void Should_Return_True_When_Contains_TOC()
    {
        const string content = """
                               <p>
                                lorem ipsum
                                [toc]
                                lorem ipsum
                               </p>
                               """;

        var result = content.HasToc();
        
        result.ShouldBeTrue();
    }
    
    [Fact]
    public void Should_Return_False_When_Does_Not_Contain_TOC()
    {
        const string content = """
                               <p>
                                lorem ipsum
                                [notoc]
                                lorem ipsum
                               </p>
                               """;

        var result = content.HasToc();
        
        result.ShouldBeFalse();
    }
}