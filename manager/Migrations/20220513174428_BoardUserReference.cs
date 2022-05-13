using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace manager.Migrations
{
    public partial class BoardUserReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Board_AspNetUsers_ApplicationUserId",
                table: "Board");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Board",
                table: "Board");

            migrationBuilder.RenameTable(
                name: "Board",
                newName: "Boards");

            migrationBuilder.RenameIndex(
                name: "IX_Board_ApplicationUserId",
                table: "Boards",
                newName: "IX_Boards_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boards",
                table: "Boards",
                column: "BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_AspNetUsers_ApplicationUserId",
                table: "Boards",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_AspNetUsers_ApplicationUserId",
                table: "Boards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Boards",
                table: "Boards");

            migrationBuilder.RenameTable(
                name: "Boards",
                newName: "Board");

            migrationBuilder.RenameIndex(
                name: "IX_Boards_ApplicationUserId",
                table: "Board",
                newName: "IX_Board_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Board",
                table: "Board",
                column: "BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Board_AspNetUsers_ApplicationUserId",
                table: "Board",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
