using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class CommentOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e99cf46-58e5-4f9c-bf88-bf247f22aba5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6433f1e4-05c8-403d-b147-13aa6b02ea49");

            migrationBuilder.AddColumn<int>(
                name: "User",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserNavigationId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4b31da1b-953b-46bd-acb4-469dcfa21b92", null, "User", "USER" },
                    { "b59692f4-5d16-4ce3-abde-ba061434db3b", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserNavigationId",
                table: "Comments",
                column: "UserNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserNavigationId",
                table: "Comments",
                column: "UserNavigationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserNavigationId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserNavigationId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b31da1b-953b-46bd-acb4-469dcfa21b92");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b59692f4-5d16-4ce3-abde-ba061434db3b");

            migrationBuilder.DropColumn(
                name: "User",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserNavigationId",
                table: "Comments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1e99cf46-58e5-4f9c-bf88-bf247f22aba5", null, "Admin", "ADMIN" },
                    { "6433f1e4-05c8-403d-b147-13aa6b02ea49", null, "User", "USER" }
                });
        }
    }
}
