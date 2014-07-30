using System.Collections.Generic;
using System.Data.SqlClient;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using SmebyFX_blog.Post.Data;
using SmebyFX_blog.Post.Domain;

namespace SmebyFX_blog.Post.IntegrationTest.Data
{
    [TestFixture]
    public class PostTagDaoTest : BaseDaoTest
    {
        private readonly PostDao _postDao = new PostDao();
        private readonly TagDao _tagDao = new TagDao();
        private readonly PostTagDao _postTagDao = new PostTagDao();

        [Test]
        public void ShouldBeAbleToInsertPostTag()
        {
            //arrange
            var post = new Domain.Post
            {
                Title = "Title"
            };
            var tag = new Tag
            {
                Title = "Title"
            };

            //act
            var postId = _postDao.Add(post);
            var tagId = _tagDao.Add(tag);
            _postTagDao.AddTagToPost(postId, tagId);

            var records = new List<int>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM PostTag";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(reader.GetInt32(0));
                        }
                    }
                }
            }

            //assert
            records.Count.Should().Be(1);
        }

        [Test]
        public void ShouldBeAbleToDeletePostTag()
        {
            //arrange
            var post = new Domain.Post
            {
                Title = "Title"
            };
            var tag = new Tag
            {
                Title = "Title"
            };

            //act
            var postId = _postDao.Add(post);
            var tagId = _tagDao.Add(tag);
            _postTagDao.AddTagToPost(postId, tagId);
            _postTagDao.RemoveTagFromPost(postId, tagId);

            var records = new List<int>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM PostTag";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(reader.GetInt32(0));
                        }
                    }
                }
            }

            //assert
            records.Should().BeEmpty();
        }

        [Test]
        public void GetPostCountShouldReturnCorrectNumber()
        {
            //arrange
            var tag1 = new Tag
            {
                Title = "Title"
            };
            var tag2 = new Tag
            {
                Title = "Title"
            };
            var post1 = new Domain.Post
            {
                Title = "Title"
            };
            var post2 = new Domain.Post
            {
                Title = "Title"
            };
            var post3 = new Domain.Post
            {
                Title = "Title"
            };

            //act
            tag1.Id = _tagDao.Add(tag1);
            tag2.Id = _tagDao.Add(tag2);
            post1.Id = _postDao.Add(post1);
            post2.Id = _postDao.Add(post2);
            post3.Id = _postDao.Add(post3);

            _postTagDao.AddTagToPost(post1.Id, tag1.Id);
            _postTagDao.AddTagToPost(post2.Id, tag2.Id);
            _postTagDao.AddTagToPost(post3.Id, tag1.Id);
            _postTagDao.AddTagToPost(post3.Id, tag2.Id);

            var count = _postTagDao.GetPostCount(tag1.Id);

            //assert
            count.Should().Be(2);
        }

        [Test]
        public void ShouldBeAbleToDeleteAllTagsForPost()
        {
            //arrange
            var tag1 = new Tag
            {
                Title = "Title"
            };
            var tag2 = new Tag
            {
                Title = "Title"
            };
            var post1 = new Domain.Post
            {
                Title = "Title"
            };
            var post2 = new Domain.Post
            {
                Title = "Title"
            };
            var post3 = new Domain.Post
            {
                Title = "Title"
            };

            //act
            tag1.Id = _tagDao.Add(tag1);
            tag2.Id = _tagDao.Add(tag2);
            post1.Id = _postDao.Add(post1);
            post2.Id = _postDao.Add(post2);
            post3.Id = _postDao.Add(post3);

            _postTagDao.AddTagToPost(post1.Id, tag1.Id);
            _postTagDao.AddTagToPost(post2.Id, tag2.Id);
            _postTagDao.AddTagToPost(post3.Id, tag1.Id);
            _postTagDao.AddTagToPost(post3.Id, tag2.Id);

            _postTagDao.RemoveAllTagsFromPost(post3.Id);

            var records = new List<int>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM PostTag";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(reader.GetInt32(0));
                        }
                    }
                }
            }

            //assert
            records.Count.Should().Be(2);
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
