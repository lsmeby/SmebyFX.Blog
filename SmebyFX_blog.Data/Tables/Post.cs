namespace SmebyFX_blog.Data.Tables
{
    public class Post
    {
        public static string Name { get { return "Post"; } }

        public class Columns
        {
            public static string Id { get { return "Id"; } }
            public static string Title { get { return "Title"; } }
            public static string Description { get { return "Description"; } }
            public static string Content { get { return "Content"; } }
            public static string UrlSlug { get { return "UrlSlug"; } }
            public static string Published { get { return "Published"; } }
            public static string Modified { get { return "Modified"; } }
        }
    }
}
