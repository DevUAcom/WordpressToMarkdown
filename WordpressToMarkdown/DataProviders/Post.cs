namespace WordpressToMarkdown.DataProviders;

public record Post
{
    public int PostId { get; init; }
    public required string Title { get; init; }
    public required string PostName { get; init; }
    public required string Content { get; init; }
    public DateTime Date { get; init; }
    public string? FeatureImage { get; init; }
    public string? Excerpt { get; init; }
    
    public string? Categories { get; init; }
    public string? Tags { get; init; }
}