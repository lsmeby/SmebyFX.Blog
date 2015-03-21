using System.Collections.Generic;
using SmebyFX_blog.Models;

namespace SmebyFX_blog.Web.ViewModels
{
    public class PostsWithTitleViewModel
    {
        public string Title { get; set; }
        public IEnumerable<Post> Posts { get; set; } 
    }
}