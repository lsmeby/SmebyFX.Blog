using System.Linq;
using Dapper;
using SmebyFX_blog.Data.BaseClasses;

namespace SmebyFX_blog.Post.Data
{
    public class PostTagDao : DaoBase
    {
        public virtual void AddTagToPost(int postId, int tagId)
        {
            const string sql = @"INSERT INTO PostTag(PostId, TagId)
                                VALUES(@postId, @tagId)";
            Run(con => con.Execute(sql, new {postId, tagId}));
        }

        public virtual void RemoveTagFromPost(int postId, int tagId)
        {
            const string sql = @"DELETE FROM PostTag
                                WHERE PostId = @postId
                                AND TagId = @tagId";
            Run(con => con.Execute(sql, new {postId, tagId}));
        }

        public virtual int GetPostCount(int tagId)
        {
            const string sql = @"SELECT COUNT(*)
                                FROM PostTag
                                WHERE TagId = @tagId";
            return Run(con => con.Query<int>(sql, new {tagId})).First();
        }

        public virtual void RemoveAllTagsFromPost(int postId)
        {
            const string sql = @"DELETE FROM PostTag
                                WHERE PostId = @postId";
            Run(con => con.Execute(sql, new {postId}));
        }
    }
}
