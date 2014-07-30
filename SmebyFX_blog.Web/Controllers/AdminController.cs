using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SmebyFX_blog.Post.Services;
using SmebyFX_blog.Shared.Extensions;
using SmebyFX_blog.Web.Authentication;
using SmebyFX_blog.Web.ViewModels;
using WebGrease.Css.Extensions;
using log4net;

namespace SmebyFX_blog.Web.Controllers
{
    [RoutePrefix("Admin")]
    [Authorize]
    public class AdminController : Controller
    {
        [Route]
        [Route("Dashboard")]
        public ActionResult Dashboard()
        {
            var postService = new PostService();
            ViewBag.TagError = TempData["TagError"];

            return View(new DashboardViewModel
            {
                Posts = postService.GetPosts(),
                Tags = postService.GetTags()
            });
        }

        [Route("Login")]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = await userManager.FindAsync(model.Email, model.Password);
            if (user != null)
            {
                var authenticationManager = HttpContext.GetOwinContext().Authentication;
                authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                authenticationManager.SignIn(await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie));

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                    
                return RedirectToAction("Dashboard");
            }

            ModelState.AddModelError("", "Feil brukernavn eller passord.");
            return View(model);
        }

        [Route("CreateOrEditTag")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEditTag(TagViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var postService = new PostService();
                postService.CreateOrEditTag(viewModel.TagId, viewModel.TagTitle, viewModel.TagUrlSlug);
            }
            else
            {
                TempData["TagError"] = ModelState.Select(s => s.Value.Errors)
                                                 .First(e => e.Count > 0)
                                                 .Select(m => m.ErrorMessage)
                                                 .FirstOrDefault();
            }
            return RedirectToAction("Dashboard");
        }

        [Route("DeleteTag")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTag(int tagId)
        {
            var postService = new PostService();

            if (postService.IsTagInUse(tagId))
            {
                TempData["TagError"] = "Cannot delete tag that's in use";
            }
            else
            {
                postService.DeleteTag(tagId);
            }

            return RedirectToAction("Dashboard");
        }

        [Route("CreatePost")]
        public ActionResult CreatePost()
        {
            var postService = new PostService();
            return View(new CreatePostViewModel
            {
                Tags = postService.GetTags(),
                AssignedTags = new List<int>()
            });
        }

        [Route("CreatePost")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(CreatePostViewModel viewModel)
        {
            var postService = new PostService();

            if (ModelState.IsValid)
            {
                var tagStrings = viewModel.SelectedTags.Split('-');
                var tagIds = new List<int>();

                try
                {
                    tagStrings.ForEach(ts => tagIds.Add(Int32.Parse(ts)));
                }
                catch (Exception e)
                {
                    LogManager.GetLogger(typeof (AdminController)).Error(e);
                    throw;
                }

                postService.CreatePost(new Post.Domain.Post
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Content = viewModel.Content,
                    UrlSlug = viewModel.UrlSlug,
                    Tags = postService.GetTags().Where(t => tagIds.Contains(t.Id)).Materialize()
                });

                return RedirectToAction("Dashboard");
            }

            viewModel.Tags = postService.GetTags();
            viewModel.AssignedTags = new List<int>();

            return View(viewModel);
        }

        [Route("EditPost")]
        public ActionResult EditPost(int postId)
        {
            var postService = new PostService();
            var post = postService.GetPost(postId);

            return View(new CreatePostViewModel
            {
                PostId = post.Id,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                UrlSlug = post.UrlSlug,
                Tags = postService.GetTags(),
                AssignedTags = post.Tags.Select(t => t.Id).Materialize()
            });
        }

        [Route("EditPost")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(CreatePostViewModel viewModel)
        {
            var postService = new PostService();

            if (ModelState.IsValid)
            {
                var tagStrings = viewModel.SelectedTags.Split('-');
                var tagIds = new List<int>();

                try
                {
                    tagStrings.ForEach(ts => tagIds.Add(Int32.Parse(ts)));
                }
                catch (Exception e)
                {
                    LogManager.GetLogger(typeof(AdminController)).Error(e);
                    throw;
                }

                postService.UpdatePost(new Post.Domain.Post
                {
                    Id = viewModel.PostId,
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Content = viewModel.Content,
                    UrlSlug = viewModel.UrlSlug,
                    Tags = postService.GetTags().Where(t => tagIds.Contains(t.Id)).Materialize()
                });

                return RedirectToAction("Dashboard");
            }

            viewModel.Tags = postService.GetTags();
            viewModel.AssignedTags = postService.GetPost(viewModel.PostId).Tags.Select(t => t.Id).Materialize();

            return View(viewModel);
        }

        [Route("DeletePost")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int postId)
        {
            var postService = new PostService();
            postService.DeletePost(postId);

            return RedirectToAction("Dashboard");
        }
    }
}