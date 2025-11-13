using Microsoft.EntityFrameworkCore.Migrations;
using XUnitBlog.Domain.Entities;

#nullable disable

namespace XUnitBlog.Data.Migrations
{
    /// <inheritdoc />
    public partial class InsertUserAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var table = "users";
            var columns = new[]
            {
                "id",
                "firstname",
                "lastname",
                "email",
                "password",
                "username",
                "role",
                "photo",
            };
            var pass =
                "6b754719e9c20a297ee1a32dd3d42217d5d2d1905df9f72470e7b23bc207d02e9ecd9ef848029468c7c38c421f0c7873f7d0fde90cc953847bac28a3e659c517";
            var data = new object[]
            {
                1,
                "super",
                "admin",
                "admin@localhost.com",
                pass,
                "admin",
                Role.ADMIN.ToString(),
                "",
            };
            migrationBuilder.InsertData(table, columns, data);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) { }
    }
}
