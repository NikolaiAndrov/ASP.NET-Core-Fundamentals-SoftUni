using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ForumData.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Title" },
                values: new object[,]
                {
                    { new Guid("8dbfb8f3-62a7-4daa-beae-5ada9c9590d2"), "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.", "My second post" },
                    { new Guid("e9575c84-de6e-47db-be5e-d251390f72bd"), "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", "My first post" },
                    { new Guid("fff2bd53-b4e8-4423-8377-54f80d936375"), "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature.", "My third post" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
