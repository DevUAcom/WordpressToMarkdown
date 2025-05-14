using Shouldly;
using WordpressToMarkdown.Converters;
using WordpressToMarkdown.Tests.TestHelpers;

namespace WordpressToMarkdown.Tests.Converters.CodeBlockConverterTests;

public class ConvertCodeBlock
{
    [Fact]
    public void Should_Convert_Pre_To_BackTicks()
    {
        const string content = """
                                 <pre class="lang:c# decode:true">
                                 public struct MyStruct
                                 {
                                     public int m1;
                                     public string s1;
                                 }
                                 </pre>
                                 """;

        var result = PostBuilder.FromContent(content).ConvertCodeBlock();
        
        result.Content.ShouldBe("""
                                ```c#
                                public struct MyStruct
                                {
                                    public int m1;
                                    public string s1;
                                }
                                ```
                                """);
    }

    [Theory]
    [InlineData("xhtml", "html")]
    [InlineData("asp", "csharp")]
    [InlineData("default", "html")]
    [InlineData("c#", "c#")]
    public void Should_Convert_lang(string sourceLang, string destLang)
    {
        string content = $"""
                                 <pre class="lang:{sourceLang} decode:true">
                                 </pre>
                                 """;

        var result = PostBuilder.FromContent(content).ConvertCodeBlock();
        
        result.Content.ShouldBe($"""
                                ```{destLang}
                                
                                ```
                                """);
    }
}