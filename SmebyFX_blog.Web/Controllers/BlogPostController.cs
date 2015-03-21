using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using SmebyFX_blog.Core.Services;
using SmebyFX_blog.Models;
using SmebyFX_blog.Web.ViewModels;

namespace SmebyFX_blog.Web.Controllers
{
    [RoutePrefix("Blog")]
    public class BlogPostController : Controller
    {
        private readonly PostService _postService;
        private const int PostsPerPage = 5;
        private readonly CultureInfo _culture = new CultureInfo("en-US");

        public BlogPostController(PostService postService)
        {
            _postService = postService;
        }

        [Route("~/")]
        [Route]
        [Route("Posts")]
        public ActionResult NewBlogPosts()
        {
            return View("BlogPosts",
                new PostsWithTitleViewModel
                {
                    Title = "New blog posts",
                    Posts = _postService.GetPosts()
                });
        }

        [Route("Tags/{tag}")]
        public ActionResult BlogPostsByTag(string tag)
        {
            return View("BlogPosts",
                new PostsWithTitleViewModel
                {
                    Title = string.Format("Posts tagget with \"{0}\"", tag),
                    Posts = _postService.GetPosts(tag)
                });
        }

        [Route("{year}/{month}/{day}/{title}")]
        public ActionResult BlogPost(int year, int month, int day, string title)
        {
            //var service = new PostService();
            var post = new Post();//service.GetPost(title, new DateTime(year, month, day));
            return View(post);
        }

        [Route("{year}/{month}/{day}")]
        public ActionResult BlogPostsByDay(int year, int month, int day)
        {
            var date = new DateTime(year, month, day);
            //var service = new PostService();
            //var posts = service.GetPosts(date);
            return View("BlogPosts",
                new PostsWithTitleViewModel
                {
                    Title = string.Format("Posts from {0}", date.ToString("D", _culture)),
                    Posts = Enumerable.Empty<Post>()
                });
        }

        [Route("{year}/{month}")]
        public ActionResult BlogPostsByMonth(int year, int month)
        {
            //var service = new PostService();
            //var posts = service.GetPosts(year, month);
            return View("BlogPosts",
                new PostsWithTitleViewModel
                {
                    Title = string.Format("Posts from {0}", new DateTime(year, month, 1).ToString("Y", _culture)),
                    Posts = Enumerable.Empty<Post>()
                });
        }

        [Route("{year}")]
        public ActionResult BlogPostsByYear(int year)
        {
            //var service = new PostService();
            //var posts = service.GetPosts(year);
            return View("BlogPosts",
                new PostsWithTitleViewModel
                {
                    Title = string.Format("Posts from {0}", year),
                    Posts = Enumerable.Empty<Post>()
                });
        }
    }
}