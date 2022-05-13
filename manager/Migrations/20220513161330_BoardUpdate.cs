using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace manager.Migrations
{
    public partial class BoardUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Board_AspNetUsers_ManagerUserId",
                table: "Board");

            migrationBuilder.RenameColumn(
                name: "ManagerUserId",
                table: "Board",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Board_ManagerUserId",
                table: "Board",
                newName: "IX_Board_ApplicationUserId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Board",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Board_AspNetUsers_ApplicationUserId",
                table: "Board",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Board_AspNetUsers_ApplicationUserId",
                table: "Board");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Board",
                newName: "ManagerUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Board_ApplicationUserId",
                table: "Board",
                newName: "IX_Board_ManagerUserId");

            migrationBuilder.UpdateData(
                table: "Board",
                keyColumn: "Name",
                keyValue: null,
                column: "Name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Board",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Board_AspNetUsers_ManagerUserId",
                table: "Board",
                column: "ManagerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
