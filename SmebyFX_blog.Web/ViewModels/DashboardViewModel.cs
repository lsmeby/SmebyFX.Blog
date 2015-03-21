using System.Collections.Generic;
using SmebyFX_blog.Models;

namespace SmebyFX_blog.Web.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}