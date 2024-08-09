using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class FixCommentOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a6ea0768-3108-43c7-8955-a57af51d997b", null, "User", "USER" },
                    { "b2214e33-4ffb-431a-adfc-2daef7086e2d", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6ea0768-3108-43c7-8955-a57af51d997b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2214e33-4ffb-431a-adfc-2daef7086e2d");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");

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
    }
}
