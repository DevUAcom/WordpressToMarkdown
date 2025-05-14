using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace WordpressToMarkdown.DataProviders;

public class DbDataProvider(string connectionString, string prefix) : IDataProvider
{
    public IEnumerable<Post> GetPosts()
    {
        using IDbConnection db = new MySqlConnection(connectionString);

        string sql = $"""
                     select 
                         p.ID AS PostId,
                         post_title AS Title, 
                         post_name AS PostName, 
                         post_content AS content, 
                         post_date AS Date,
                         post_excerpt AS Excerpt,
                         pa.feature_image AS FeatureImage,
                         GROUP_CONCAT(DISTINCT c.`name`) AS categories,
                         GROUP_CONCAT(DISTINCT t.`name`) AS tags
                     FROM {prefix}_posts p
                     LEFT JOIN {prefix}_term_relationships cr
                         ON (p.`id`=cr.`object_id`)
                     LEFT JOIN {prefix}_term_taxonomy ct
                         ON (ct.`term_taxonomy_id`=cr.`term_taxonomy_id`
                         AND ct.`taxonomy`='category')
                     LEFT JOIN {prefix}_terms c ON
                         (ct.`term_id` = c.`term_id`)
                     LEFT JOIN {prefix}_term_relationships tr
                         ON (p.`id`=tr.`object_id`)
                     LEFT JOIN {prefix}_term_taxonomy tt
                         ON (tt.`term_taxonomy_id`=tr.`term_taxonomy_id`
                         AND tt.`taxonomy`='post_tag')
                     LEFT JOIN {prefix}_terms t
                         ON (tt.`term_id`=t.`term_id`)
                     LEFT JOIN (SELECT * FROM {prefix}_postmeta WHERE meta_key = '_thumbnail_id') pm 
                     	ON p.ID = pm.post_id
                     LEFT JOIN (SELECT ID, guid AS feature_image FROM {prefix}_posts WHERE post_type = 'attachment') pa
                     	ON pa.ID = pm.meta_value 
                     WHERE post_type = 'post' AND post_status = 'publish' 
                     GROUP BY p.id, pa.feature_image
                     """;
        return db.Query<Post>(sql);
    }
}