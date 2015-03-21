using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using SmebyFX_blog.Models;

namespace SmebyFX_blog.Web.ViewModels
{
    public class CreatePostViewModel
    {
        public int PostId { get; set; }

        [Required(ErrorMessage = "You must provide a title")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [AllowHtml]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [RegularExpression("^[A-Za-z-_]+$", ErrorMessage = "The url slug can only contain enlish letters, numbers, dashes and underscores")]
        [Display(Name = "Url slug")]
        public string UrlSlug { get; set; }

        [Required(ErrorMessage = "You must provide at least one tag")]
        public string SelectedTags { get; set; }

        // To View
        public List<int> AssignedTags { get; set; } 
    }
}