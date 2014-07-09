using FluentMigrator;
using SmebyFX_blog.Data.Tables;
using SmebyFX_blog.DBMigration.Extensions;

namespace SmebyFX_blog.DBMigration.Migrations
{
    [Migration(201406091606)]
    public class _002_CreateTagTable : Migration
    {
        public override void Up()
        {
            Create.Table(Tag.Name)
                .WithIdColumn()
                .WithColumn(Tag.Columns.Title).AsString(255).NotNullable()
                .WithColumn(Tag.Columns.UrlSlug).AsString(255).NotNullable();
        }

        public override void Down()
        {
            Delete.Table(Tag.Name);
        }
    }
}
