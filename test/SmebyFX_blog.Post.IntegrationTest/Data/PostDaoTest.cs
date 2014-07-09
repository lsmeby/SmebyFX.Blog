using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SmebyFX_blog.Post.Data;

namespace SmebyFX_blog.Post.IntegrationTest.Data
{
    [TestFixture]
    public class PostDaoTest : BaseDaoTest
    {
        private readonly PostDao _postDao = new PostDao();

        [Test]
        public void ShouldBeAbleToAddNewPost()
        {
            //arrange
            var post = new Domain.Post
            {
                Title = "Title",
                Description = "Description",
                Content = "Content",
                UrlSlug = "UrlSlug",
            };

            //act
            var id = _postDao.Add(post);

            //assert
            id.Should().BeGreaterThan(0);
        }

        [Test]
        public void ShouldBeAbleToAddNewPostWithoutOptionalProperties()
        {
            //arrange
            var post = new Domain.Post
            {
                Title = "Title"
            };

            //act
            var id = _postDao.Add(post);

            //assert
            id.Should().BeGreaterThan(0);
        }

        [Test]
        public void ShouldBeAbleToGetAllPosts()
        {
            //arrange
            var post = new Domain.Post
            {
                Title = "Title"
            };

            //act
            for(var i = 0; i < 10; i++)
                _postDao.Add(post);
            var posts = _postDao.GetBlogPosts();

            //assert
            posts.Count.Should().Be(10);
        }

        [Test]
        public void ShouldBeAbleToUpdatePost()
        {
            //arrange
            var post = new Domain.Post
            {
                Title = "Title"
            };

            //act
            post.Id = _postDao.Add(post);
            post.Title = "NewTitle";
            post.Description = "Description";
            post.Content = "Content";
            post.UrlSlug = "UrlSlug";
            _postDao.Update(post);
            var updatedPost = _postDao.GetBlogPosts().First();

            //assert
            updatedPost.Id.Should().Be(post.Id);
            updatedPost.Title.Should().Be((post.Title));
            updatedPost.Description.Should().Be(post.Description);
            updatedPost.Content.Should().Be(post.Content);
            updatedPost.UrlSlug.Should().Be(post.UrlSlug);
        }

        [Test]
        public void ShouldBeAbleToDeletePost()
        {
            //arrange
            var post = new Domain.Post
            {
                Title = "Title"
            };

            //act
            var id = _postDao.Add(post);
            _postDao.Delete(id);
            var posts = _postDao.GetBlogPosts();

            //assert
            posts.Should().BeEmpty();
        }

        [Test]
        public void ShouldBeAbleToGetPostsByTagId()
        {
            //arrange
            var tagDao = new TagDao();
            var postTagDao = new PostTagDao();

            var post = new Domain.Post
            {
                Title = "Title"
            };
            var tag = new Domain.Tag
            {
                Title = "Title"
            };

            //act
            var id1 = _postDao.Add(post);
            var id2 = _postDao.Add(post);
            _postDao.Add(post);
            _postDao.Add(post);

            var tagId = tagDao.Add(tag);

            postTagDao.AddTagToPost(id1, tagId);
            postTagDao.AddTagToPost(id2, tagId);

            var posts = _postDao.GetBlogPostsByTag(tagId);

            //assert
            posts.Count.Should().Be(2);
        }

        [Test]
        public void ShouldBeAbleToGetPostByUrlSlugAndDate()
        {
            //arrange
            var post = new Domain.Post
            {
                Title = "Title"
            };

            //act
            _postDao.Add(post);
            var newPost = _postDao.GetPost("Title", DateTime.Now.AddHours(2));

            //assert
            newPost.Should().NotBeNull();
            newPost.Title.Should().Be("Title");
        }

        [Test]
        public void ShouldBeAbleToGetPostByDate()
        {
            //arrange
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO Post(Title, Description, Content, UrlSlug, Published)
                                          VALUES('Title', '', '', '', '2014-05-02')
                                          INSERT INTO Post(Title, Description, Content, UrlSlug, Published)
                                          VALUES('Title', '', '', '', '2014-05-03')
                                          INSERT INTO Post(Title, Description, Content, UrlSlug, Published)
                                          VALUES('Title', '', '', '', '2014-05-04')";
                    command.ExecuteNonQuery();
                }
            }

            //act
            var posts = _postDao.GetPostsByDate(new DateTime(2014, 05, 03));

            //assert
            posts.Count.Should().Be(1);
        }

        [Test]
        public void ShouldBeAbleToGetPostByMonth()
        {
            //arrange
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO Post(Title, Description, Content, UrlSlug, Published)
                                          VALUES('Title', '', '', '', '2014-04-30')
                                          INSERT INTO Post(Title, Description, Content, UrlSlug, Published)
                                          VALUES('Title', '', '', '', '2014-05-01')
                                          INSERT INTO Post(Title, Description, Content, UrlSlug, Published)
                                          VALUES('Title', '', '', '', '2014-05-31')
                                          INSERT INTO Post(Title, Description, Content, UrlSlug, Published)
                                          VALUES('Title', '', '', '', '2014-06-01')";
                    command.ExecuteNonQuery();
                }
            }

            //act
            var posts = _postDao.GetPostsByMonth(2014, 05);

            //assert
            posts.Count.Should().Be(2);
        }

        [Test]
        public void ShouldBeAbleToGetPostByYear()
        {
            //arrange
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO Post(Title, Description, Content, UrlSlug, Published)
                                          VALUES('Title', '', '', '', '2013-12-31')
                                          INSERT INTO Post(Title, Description, Content, UrlSlug, Published)
                                          VALUES('Title', '', '', '', '2014-01-01')
                                          INSERT INTO Post(Title, Description, Content, UrlSlug, Published)
                                          VALUES('Title', '', '', '', '2014-12-31')
                                          INSERT INTO Post(Title, Description, Content, UrlSlug, Published)
                                          VALUES('Title', '', '', '', '2015-01-01')";
                    command.ExecuteNonQuery();
                }
            }

            //act
            var posts = _postDao.GetPostsByYear(2014);

            //assert
            posts.Count.Should().Be(2);
        }

        [TearDown]
        public void TearDown()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM PostTag";
                    command.ExecuteNonQuery();
                }
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Post";
                    command.ExecuteNonQuery();
                }
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Tag";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
