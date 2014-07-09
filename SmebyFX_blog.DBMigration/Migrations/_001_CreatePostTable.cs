using FluentMigrator;
using SmebyFX_blog.Data.Tables;
using SmebyFX_blog.DBMigration.Extensions;

namespace SmebyFX_blog.DBMigration
{
    [Migration(201406091553)]
    public class _001_CreatePostTable : Migration
    {
        public override void Up()
        {
            Create.Table(Post.Name)
                .WithIdColumn()
                .WithColumn(Post.Columns.Title).AsString(255).NotNullable()
                .WithColumn(Post.Columns.Description).AsString(1023).Nullable()
                .WithColumn(Post.Columns.Content).AsString(int.MaxValue).Nullable()
                .WithColumn(Post.Columns.UrlSlug).AsString(255).NotNullable()
                .WithColumn(Post.Columns.Published).AsDateTime().NotNullable()
                .WithColumn(Post.Columns.Modified).AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table(Post.Name);
        }
    }
}