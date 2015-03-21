//using System.Collections.Generic;
//using FluentAssertions;
//using Moq;
//using NUnit.Framework;
//using SmebyFX_blog.Post.Data;
//using SmebyFX_blog.Post.Domain;
//
//namespace SmebyFX_blog.Post.UnitTest.Services
//{
//    [TestFixture]
//    public class PostServiceTest
//    {
//        private readonly Mock<PostDao> _postDaoMock = new Mock<PostDao>();
//        private readonly Mock<TagDao> _tagDaoMock = new Mock<TagDao>();
//        private PostService _postService;
//
//        [SetUp]
//        public void Setup()
//        {
//            _postService = new PostService(_postDaoMock.Object, _tagDaoMock.Object);
//        }
//
//        [Test]
//        public void ShouldBeAbleToAttachTagsToPosts()
//        {
//            //arrange
//            var posts = new List<Domain.Post>()
//            {
//                new Domain.Post {Title = "Title"},
//                new Domain.Post {Title = "Title"}
//            };
//            var tags = new List<Tag>
//            {
//                new Tag {Title = "Title"},
//                new Tag {Title = "Title"}
//            };
//            _postDaoMock.Setup(p => p.GetBlogPosts()).Returns(posts);
//            _tagDaoMock.Setup(t => t.GetTagsForPost(It.IsAny<int>())).Returns(tags);
//            
//            //act
//            var returnedPosts = _postService.GetPosts();
//
//            returnedPosts.Count.Should().Be(2);
//            returnedPosts.ForEach(post => post.Tags.Count.Should().Be(2));
//        }
//
//        [Test]
//        public void GetPostsShouldThrowExceptionIfTagDoesNotExist()
//        {
//            //arrange
//            _tagDaoMock.Setup(t => t.GetTag(It.IsAny<string>())).Returns(default(Tag));
//
//            //assert
//            Assert.That(() => _postService.GetPosts("Title"), Throws.ArgumentException);
//        }
//    }
//}
