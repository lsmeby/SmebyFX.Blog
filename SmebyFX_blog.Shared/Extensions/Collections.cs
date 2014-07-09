using System.Collections.Generic;
using System.Linq;

namespace SmebyFX_blog.Shared.Extensions
{
    public static class Collections
    {
        public static List<T> Materialize<T>(this IEnumerable<T> enumerable)
        {
            var e = enumerable as List<T>;
            return e ?? enumerable.ToList();
        }
    }
}
