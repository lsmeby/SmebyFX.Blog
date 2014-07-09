using SmebyFX_blog.Data.BaseClasses;

namespace SmebyFX_blog.Post.Domain
{
    public class Tag : EnitiyBase
    {
        public string Title { get; set; }
        public string UrlSlug { get; set; }
        public int Usages { get; set; }
    }
}
