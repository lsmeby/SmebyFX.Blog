using System.ComponentModel.DataAnnotations;

namespace SmebyFX_blog.Web.ViewModels
{
    public class TagViewModel
    {
        public int? TagId { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string TagTitle { get; set; }

        [RegularExpression("^[A-Za-z-_]+$", ErrorMessage = "The url slug can only contain enlish letters, numbers, dashes and underscores.")]
        [Display(Name = "Url slug")]
        public string TagUrlSlug { get; set; }
    }
}