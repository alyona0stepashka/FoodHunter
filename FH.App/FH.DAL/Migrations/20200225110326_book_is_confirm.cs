using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FH.DAL.Migrations
{
    public partial class book_is_confirm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableBooks_AspNetUsers_ClientId",
                table: "TableBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_TableBooks_TableBooks_TableBookId",
                table: "TableBooks");

            migrationBuilder.DropIndex(
                name: "IX_TableBooks_TableBookId",
                table: "TableBooks");

            migrationBuilder.DropColumn(
                name: "TableBookId",
                table: "TableBooks");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "TableBooks",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BookTime",
                table: "TableBooks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirm",
                table: "TableBooks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_TableBooks_UserProfiles_ClientId",
                table: "TableBooks",
                column: "ClientId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableBooks_UserProfiles_ClientId",
                table: "TableBooks");

            migrationBuilder.DropColumn(
                name: "BookTime",
                table: "TableBooks");

            migrationBuilder.DropColumn(
                name: "IsConfirm",
                table: "TableBooks");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "TableBooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TableBookId",
                table: "TableBooks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TableBooks_TableBookId",
                table: "TableBooks",
                column: "TableBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_TableBooks_AspNetUsers_ClientId",
                table: "TableBooks",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TableBooks_TableBooks_TableBookId",
                table: "TableBooks",
                column: "TableBookId",
                principalTable: "TableBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
