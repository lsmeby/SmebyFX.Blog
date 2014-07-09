namespace SmebyFX_blog.Data.Tables
{
    public class PostTag
    {
        public static string Name { get { return "PostTag"; } }

        public class Columns
        {
            public static string PostId { get { return "PostId"; } }
            public static string TagId { get { return "TagId"; } }
        }

        public class Indices
        {
            public static string PostTagIndex { get { return "PostTagIndex"; } }
        }

        public class ForeignKeys
        {
            public static string ToPost { get { return "FK_PostTag_Post"; } }
            public static string ToTag { get { return "FK_PostTag_Tag"; } }
        }
    }
}
