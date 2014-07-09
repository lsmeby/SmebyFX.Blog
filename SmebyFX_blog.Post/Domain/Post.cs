using System;
using System.Collections.Generic;
using SmebyFX_blog.Data.BaseClasses;

namespace SmebyFX_blog.Post.Domain
{
    public class Post : EnitiyBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string UrlSlug { get; set; }
        public DateTime Published { get; set; }
        public DateTime? Modified { get; set; }
        public IList<Tag> Tags { get; set; } 
    }
}
