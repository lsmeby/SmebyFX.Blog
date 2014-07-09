using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using SmebyFX_blog.Post.Services;
using SmebyFX_blog.Web.ViewModels;

namespace SmebyFX_blog.Web.Controllers
{
    [RoutePrefix("Blog")]
    public class BlogPostController : Controller
    {
        private const int PostsPerPage = 5;
        private readonly CultureInfo _culture = new CultureInfo("en-US");

        [Route("~/")]
        [Route]
        [Route("Posts")]
        public ActionResult NewBlogPosts()
        {
            var service = new PostService();
            return View("BlogPosts",
                new PostsWithTitleViewModel
                {
                    Title = "New blog posts",
                    Posts = service.GetPosts().Take(PostsPerPage)
                });
        }

        [Route("Tags/{tag}")]
        public ActionResult BlogPostsByTag(string tag)
        {
            var service = new PostService();
            return View("BlogPosts",
                new PostsWithTitleViewModel
                {
                    Title = string.Format("Posts tagget with \"{0}\"", service.GetTag(tag).Title),
                    Posts = service.GetPosts(tag)
                });
        }

        [Route("{year}/{month}/{day}/{title}")]
        public ActionResult BlogPost(int year, int month, int day, string title)
        {
            var service = new PostService();
            var post = service.GetPost(title, new DateTime(year, month, day));
            return View(post);
        }

        [Route("{year}/{month}/{day}")]
        public ActionResult BlogPostsByDay(int year, int month, int day)
        {
            var date = new DateTime(year, month, day);
            var service = new PostService();
            var posts = service.GetPosts(date);
            return View("BlogPosts",
                new PostsWithTitleViewModel
                {
                    Title = string.Format("Posts from {0}", date.ToString("D", _culture)),
                    Posts = posts
                });
        }

        [Route("{year}/{month}")]
        public ActionResult BlogPostsByMonth(int year, int month)
        {
            var service = new PostService();
            var posts = service.GetPosts(year, month);
            return View("BlogPosts",
                new PostsWithTitleViewModel
                {
                    Title = string.Format("Posts from {0}", new DateTime(year, month, 1).ToString("Y", _culture)),
                    Posts = posts
                });
        }

        [Route("{year}")]
        public ActionResult BlogPostsByYear(int year)
        {
            var service = new PostService();
            var posts = service.GetPosts(year);
            return View("BlogPosts",
                new PostsWithTitleViewModel
                {
                    Title = string.Format("Posts from {0}", year),
                    Posts = posts
                });
        }
    }
}