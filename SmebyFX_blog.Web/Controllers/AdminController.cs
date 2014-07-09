using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SmebyFX_blog.Data.Tables;
using SmebyFX_blog.Post.Services;
using SmebyFX_blog.Web.Authentication;
using SmebyFX_blog.Web.ViewModels;

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
    }
}