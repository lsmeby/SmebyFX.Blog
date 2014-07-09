using System.Collections.Generic;
using SmebyFX_blog.Post.Domain;

namespace SmebyFX_blog.Web.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Post.Domain.Post> Posts { get; set; }
        public IEnumerable<Tag> Tags { get; set; } 
    }
}