using Microsoft.EntityFrameworkCore.Migrations;

namespace FH.DAL.Migrations
{
    public partial class fix01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Companys",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companys_AdminId",
                table: "Companys",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companys_AspNetUsers_AdminId",
                table: "Companys",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companys_AspNetUsers_AdminId",
                table: "Companys");

            migrationBuilder.DropIndex(
                name: "IX_Companys_AdminId",
                table: "Companys");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Companys");
        }
    }
}
