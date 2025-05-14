using Shouldly;
using WordpressToMarkdown.Converters;
using WordpressToMarkdown.Tests.TestHelpers;

namespace WordpressToMarkdown.Tests.Converters.DialogConverterTests;

public class ConvertDialogs
{
    [Fact]
    public void Should_Convert_Dialogs()
    {
        const string content = """
                               - Hello!
                               - Hi!
                               - How are you?
                               - Fine!
                               
                               """;
        
        var result = PostBuilder.FromContent(content).ConvertDialogs();
        
        result.Content.ShouldBe("""
                                \- Hello!
                                \- Hi!
                                \- How are you?
                                \- Fine!
                                
                                """);
    }
}