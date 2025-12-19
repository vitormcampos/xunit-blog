using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XUnitBlog.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterTablePostsAddColumnPinned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "pinned",
                table: "posts",
                type: "bool",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pinned",
                table: "posts");
        }
    }
}
