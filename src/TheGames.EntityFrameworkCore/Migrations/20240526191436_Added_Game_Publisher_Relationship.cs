using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheGames.Migrations
{
    /// <inheritdoc />
    public partial class Added_Game_Publisher_Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "AppPublishers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AppGames_PublisherId",
                table: "AppGames",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppGames_AppPublishers_PublisherId",
                table: "AppGames",
                column: "PublisherId",
                principalTable: "AppPublishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppGames_AppPublishers_PublisherId",
                table: "AppGames");

            migrationBuilder.DropIndex(
                name: "IX_AppGames_PublisherId",
                table: "AppGames");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "AppPublishers");
        }
    }
}
