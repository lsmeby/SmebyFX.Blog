using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using SmebyFX_blog.Data.BaseClasses;

namespace SmebyFX_blog.Post.Data
{
    public class PostDao : DaoBase
    {
        public virtual List<Domain.Post> GetBlogPosts()
        {
            const string sql = @"SELECT * FROM Post
                                ORDER BY Published DESC";
            return Run(con => con.Query<Domain.Post>(sql)).ToList();
        }

        public virtual List<Domain.Post> GetBlogPostsByTag(int tagId)
        {
            const string sql = @"SELECT P.Id, P.Title, P.Description, P.Content, P.UrlSlug, P.Published, P.Modified
                                FROM Post AS P
                                INNER JOIN PostTag AS PT ON PT.PostId = P.Id
                                INNER JOIN Tag AS T ON T.Id = PT.TagId
                                WHERE T.Id = @tagId";
            return Run(con => con.Query<Domain.Post>(sql, new {tagId})).ToList();
        }

        public virtual Domain.Post GetPost(int postId)
        {
            const string sql = @"SELECT * FROM Post
                                WHERE Id = @postId";
            return Run(con => con.Query<Domain.Post>(sql, new{postId})).FirstOrDefault();
        }

        public virtual Domain.Post GetPost(string postUrlSlug, DateTime date)
        {
            const string sql = @"SELECT * FROM Post
                                WHERE UrlSlug = @postUrlSlug
                                AND Published < @dayAfter
                                AND Published > @dayBefore
                                ORDER BY Published DESC";
            var dayAfter = date.AddDays(1).ToString("yyyy-MM-dd");
            var dayBefore = date.AddDays(-1).ToString("yyyy-MM-dd");
            return Run(con => con.Query<Domain.Post>(sql, new {postUrlSlug, dayAfter, dayBefore})).FirstOrDefault();
        }

        public virtual List<Domain.Post> GetPostsByDate(DateTime date)
        {
            const string sql = @"SELECT * FROM Post
                                WHERE Published < @dayAfter
                                AND Published > @dayBefore
                                ORDER BY Published DESC";
            var dayAfter = date.AddDays(1).ToString("yyyy-MM-dd");
            var dayBefore = date.AddDays(-1).ToString("yyyy-MM-dd");
            return Run(con => con.Query<Domain.Post>(sql, new { dayAfter, dayBefore })).ToList();
        }

        public virtual List<Domain.Post> GetPostsByMonth(int year, int month)
        {
            const string sql = @"SELECT * FROM Post
                                WHERE Published < @dayAfter
                                AND Published > @dayBefore
                                ORDER BY Published DESC";
            var date = new DateTime(year, month, 1);
            var dayAfter = date.AddMonths(1).ToString("yyyy-MM-dd");
            var dayBefore = date.AddDays(-1).ToString("yyyy-MM-dd");
            return Run(con => con.Query<Domain.Post>(sql, new { dayAfter, dayBefore })).ToList();
        }

        public virtual List<Domain.Post> GetPostsByYear(int year)
        {
            const string sql = @"SELECT * FROM Post
                                WHERE Published < @dayAfter
                                AND Published > @dayBefore
                                ORDER BY Published DESC";
            var date = new DateTime(year, 1, 1);
            var dayAfter = date.AddYears(1).ToString("yyyy-MM-dd");
            var dayBefore = date.AddDays(-1).ToString("yyyy-MM-dd");
            return Run(con => con.Query<Domain.Post>(sql, new { dayAfter, dayBefore })).ToList();
        }

        public virtual int Add(Domain.Post post)
        {
            Initialize(post);
            const string sql = @"INSERT INTO Post(Title, Description, Content, UrlSlug, Published)
                                VALUES(@Title, @Description, @Content, @UrlSlug, GETDATE())
                                SELECT CAST(SCOPE_IDENTITY() AS int)";
            return Run(con => con.Query<int>(sql, post)).First();
        }

        public virtual void Update(Domain.Post post)
        {
            Initialize(post);
            const string sql = @"UPDATE Post
                                SET Title = @Title,
                                Description = @Description,
                                Content = @Content,
                                UrlSlug = @UrlSlug,
                                Modified = GETDATE()
                                WHERE Id = @Id";
            Run(con => con.Execute(sql, post));
        }

        public virtual void Delete(int postId)
        {
            const string sql = @"DELETE FROM Post
                                WHERE Id = @postId";
            Run(con => con.Execute(sql, new {postId}));
        }

        private void Initialize(Domain.Post post)
        {
            post.Description = post.Description ?? string.Empty;
            post.Content = post.Content ?? string.Empty;
            post.UrlSlug = post.UrlSlug ?? HttpUtility.UrlEncode(post.Title.Replace(' ', '-'));
        }
    }
}
