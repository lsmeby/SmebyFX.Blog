using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using SmebyFX_blog.Data.BaseClasses;
using SmebyFX_blog.Post.Domain;

namespace SmebyFX_blog.Post.Data
{
    public class TagDao : DaoBase
    {
        public virtual List<Tag> GetTags()
        {
            const string sql = @"SELECT * FROM Tag
                                ORDER BY Title";
            return Run(con => con.Query<Tag>(sql)).ToList();
        }

        public virtual List<Tag> GetTagsForPost(int postId)
        {
            const string sql = @"SELECT T.Id, T.Title, T.UrlSlug
                                FROM Tag AS T
                                INNER JOIN PostTag AS PT ON T.Id = PT.TagId
                                INNER JOIN Post AS P ON P.Id = PT.PostId
                                WHERE P.Id = @postId";
            return Run(con => con.Query<Tag>(sql, new {postId})).ToList();
        }

        public virtual Tag GetTag(string tagUrlSlug)
        {
            const string sql = @"SELECT *
                                FROM Tag
                                WHERE UrlSlug = @tagUrlSlug";
            return Run(con => con.Query<Tag>(sql, new {tagUrlSlug})).FirstOrDefault();
        }

        public virtual int Add(Tag tag)
        {
            Initialize(tag);
            const string sql = @"INSERT INTO Tag(Title, UrlSlug)
                                VALUES(@Title, @UrlSlug)
                                SELECT CAST(SCOPE_IDENTITY() AS int)";
            return Run(con => con.Query<int>(sql, tag)).First();
        }

        public virtual void Update(Tag tag)
        {
            Initialize(tag);
            const string sql = @"UPDATE Tag
                                SET Title = @Title,
                                UrlSlug = @UrlSlug
                                WHERE Id = @Id";
            Run(con => con.Execute(sql, tag));
        }

        public virtual void Delete(int tagId)
        {
            const string sql = @"DELETE FROM Tag
                                WHERE Id = @tagId";
            Run(con => con.Execute(sql, new {tagId}));
        }

        private void Initialize(Tag tag)
        {
            tag.UrlSlug = tag.UrlSlug ?? HttpUtility.UrlEncode(tag.Title);
        }
    }
}
