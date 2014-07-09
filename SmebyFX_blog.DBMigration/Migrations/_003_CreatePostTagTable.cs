using FluentMigrator;
using SmebyFX_blog.Data.Tables;

namespace SmebyFX_blog.DBMigration.Migrations
{
    [Migration(201406091609)]
    public class _003_CreatePostTagTable : Migration
    {
        public override void Up()
        {
            Create.Table(PostTag.Name)
                .WithColumn(PostTag.Columns.PostId).AsInt32().NotNullable()
                .WithColumn(PostTag.Columns.TagId).AsInt32().NotNullable();

            Create.Index(PostTag.Indices.PostTagIndex)
                .OnTable(PostTag.Name)
                .OnColumn(PostTag.Columns.PostId).Unique()
                .OnColumn(PostTag.Columns.TagId).Unique();

            Create.ForeignKey(PostTag.ForeignKeys.ToPost)
                .FromTable(PostTag.Name)
                .ForeignColumn(PostTag.Columns.PostId)
                .ToTable(Post.Name)
                .PrimaryColumn(Post.Columns.Id);

            Create.ForeignKey(PostTag.ForeignKeys.ToTag)
                .FromTable(PostTag.Name)
                .ForeignColumn(PostTag.Columns.TagId)
                .ToTable(Tag.Name)
                .PrimaryColumn(Tag.Columns.Id);
        }

        public override void Down()
        {
            Delete.Table(PostTag.Name);
        }
    }
}
