using Shouldly;
using WordpressToMarkdown.Converters;
using WordpressToMarkdown.Tests.TestHelpers;

namespace WordpressToMarkdown.Tests.Converters.SingleLineBreaksConverterTests;

public class ConvertSingleLineBreaks
{
    [Fact]
    public void Should_Add_2_Spaces_Before_Single_Line_Breaks()
    {
        const string content = """
                               line1
                               line2
                               
                               line3
                               
                               line4
                               line5
                               
                               """;

        var result = PostBuilder.FromContent(content).ConvertSingleLineBreaks();
        
        result.Content.ShouldBe("""
                                line1  
                                line2
                                
                                line3
                                
                                line4  
                                line5  
                                
                                """);
    }
}