namespace SmebyFX_blog.Data.Tables
{
    public class Tag
    {
        public static string Name { get { return "Tag"; } }

        public class Columns
        {
            public static string Id { get { return "Id"; } }
            public static string Title { get { return "Title"; } }
            public static string UrlSlug { get { return "UrlSlug"; } }
        }
    }
}
