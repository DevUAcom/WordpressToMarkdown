namespace WordpressToMarkdown.DataProviders;

public interface IDataProvider
{
    public IEnumerable<Post> GetPosts();
}