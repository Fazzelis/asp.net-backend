using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restApi.Migrations
{
    /// <inheritdoc />
    public partial class edit_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "News");

            migrationBuilder.DropColumn(
                name: "CreatorName",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Events",
                newName: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_News_AuthorId",
                table: "News",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_JwtTokens_UserId",
                table: "JwtTokens",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_AuthorId",
                table: "Events",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_AuthorId",
                table: "Events",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JwtTokens_Users_UserId",
                table: "JwtTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Users_AuthorId",
                table: "News",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_AuthorId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_JwtTokens_Users_UserId",
                table: "JwtTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Users_AuthorId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_AuthorId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_JwtTokens_UserId",
                table: "JwtTokens");

            migrationBuilder.DropIndex(
                name: "IX_Events_AuthorId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Events",
                newName: "CreatorId");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "News",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatorName",
                table: "Events",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
