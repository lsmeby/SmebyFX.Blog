using System.ComponentModel.DataAnnotations;

namespace SmebyFX_blog.Data.BaseClasses
{
    public abstract class EnitiyBase
    {
        [Key]
        public int Id { get; set; }
    }
}
