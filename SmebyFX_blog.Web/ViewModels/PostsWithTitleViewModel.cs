using System.Collections.Generic;

namespace SmebyFX_blog.Web.ViewModels
{
    public class PostsWithTitleViewModel
    {
        public string Title { get; set; }
        public IEnumerable<Post.Domain.Post> Posts { get; set; } 
    }
}