using Microsoft.EntityFrameworkCore.Migrations;

namespace FH.DAL.Migrations
{
    public partial class locationFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TopFileId",
                table: "Locations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_TopFileId",
                table: "Locations",
                column: "TopFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_FileModels_TopFileId",
                table: "Locations",
                column: "TopFileId",
                principalTable: "FileModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_FileModels_TopFileId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_TopFileId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "TopFileId",
                table: "Locations");
        }
    }
}
