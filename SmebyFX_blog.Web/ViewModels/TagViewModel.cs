using System.ComponentModel.DataAnnotations;

namespace SmebyFX_blog.Web.ViewModels
{
    public class TagViewModel
    {
        public int? TagId { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string TagTitle { get; set; }

        [Display(Name = "Url slug")]
        public string TagUrlSlug { get; set; }
    }
}