using Shouldly;
using WordpressToMarkdown.Converters;
using WordpressToMarkdown.Tests.TestHelpers;

namespace WordpressToMarkdown.Tests.Converters.BoxConverterTests;

public class ConvertBoxes
{
    [Theory]
    [InlineData("box", "type")]
    [InlineData("stextbox", "id")]
    public void Should_Convert_Box_To_Colons(string boxTag, string attr)
    {
        string content = $"""
                               [{boxTag} {attr}="info" ]
                               Information.
                               Lorem ipsum.
                               [/{boxTag}]
                               """;
        
        var result = PostBuilder.FromContent(content).ConvertBoxes();
        
        result.Content.ShouldBe("""
                                :::info
                                
                                Information.
                                Lorem ipsum.
                                
                                :::
                                """);
    }
}