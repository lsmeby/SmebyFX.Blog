using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SmebyFX_blog.Post.Data;
using SmebyFX_blog.Post.Domain;

namespace SmebyFX_blog.Post.IntegrationTest.Data
{
    [TestFixture]
    public class TagDaoTest : BaseDaoTest
    {
        private readonly TagDao _tagDao = new TagDao();

        [Test]
        public void ShouldBeAbleToAddTag()
        {
            //arrange
            var tag = new Tag
            {
                Title = "Title",
                UrlSlug = "UrlSlug"
            };

            //act
            var id = _tagDao.Add(tag);

            //assert
            id.Should().BeGreaterThan(0);
        }

        [Test]
        public void ShouldBeAbleToAddTagWithoutOptionalProperties()
        {
            var tag = new Tag
            {
                Title = "Title"
            };

            //act
            var id = _tagDao.Add(tag);

            //assert
            id.Should().BeGreaterThan(0);
        }

        [Test]
        public void ShouldBeAbleToGetAllTags()
        {
            //arrange
            var tag = new Tag
            {
                Title = "Title"
            };

            //act
            for(var i = 0; i < 10; i++)
            {
                _tagDao.Add(tag);
            }

            var tags = _tagDao.GetTags();

            //assert
            tags.Count.Should().Be(10);
        }

        [Test]
        public void ShouldBeAbleToGetTagByUrlSlug()
        {
            //arrange
            var tag1 = new Tag
            {
                Title = "Title",
                UrlSlug = "Title"
            };
            var tag2 = new Tag
            {
                Title = "Title2",
                UrlSlug = "Title2"
            };

            //act
            _tagDao.Add(tag1);
            _tagDao.Add(tag2);
            var newTag = _tagDao.GetTag("Title");

            //assert
            newTag.Should().NotBeNull();
            newTag.Title.Should().Be("Title");
        }

        [Test]
        public void GetTagByTitleWithInvalidTitleShouldReturnNull()
        {
            //arrange
            var tag = new Tag
            {
                Title = "Title"
            };

            //act
            _tagDao.Add(tag);
            var newTag = _tagDao.GetTag("Wrong title");

            //assert
            newTag.Should().BeNull();
        }

        [Test]
        public void ShouldBeAbleToUpdateTag()
        {
            //arrange
            var tag = new Tag
            {
                Title = "Title"
            };

            //act
            tag.Id = _tagDao.Add(tag);
            tag.Title = "NewTitle";
            tag.UrlSlug = "UrlSlug";
            _tagDao.Update(tag);
            var updatedTag = _tagDao.GetTags().First();

            //assert
            updatedTag.Title.Should().Be(tag.Title);
            updatedTag.UrlSlug.Should().Be(tag.UrlSlug);
        }

        [Test]
        public void ShouldBeAbleToDeleteTag()
        {
            //arrange
            var tag = new Tag
            {
                Title = "Title"
            };

            //act
            var id = _tagDao.Add(tag);
            _tagDao.Delete(id);
            var tags = _tagDao.GetTags();

            //assert
            tags.Should().BeEmpty();
        }

        [Test]
        public void ShouldBeAbleToGetTagsByPost()
        {
            //arrange
            var postDao = new PostDao();
            var postTagDao = new PostTagDao();

            var post = new Domain.Post
            {
                Title = "Title"
            };
            var tag = new Tag
            {
                Title = "Title"
            };

            //act
            var postId = postDao.Add(post);

            var tagId1 = _tagDao.Add(tag);
            var tagId2 = _tagDao.Add(tag);
            _tagDao.Add(tag);
            _tagDao.Add(tag);

            postTagDao.AddTagToPost(postId, tagId1);
            postTagDao.AddTagToPost(postId, tagId2);

            var tags = _tagDao.GetTagsForPost(postId);

            //assert
            tags.Count.Should().Be(2);
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
