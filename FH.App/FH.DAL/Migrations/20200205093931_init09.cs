using Microsoft.EntityFrameworkCore.Migrations;

namespace FH.DAL.Migrations
{
    public partial class init09 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DealerId",
                table: "FileModels",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "FileModels",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileModels_DealerId",
                table: "FileModels",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_FileModels_LocationId",
                table: "FileModels",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileModels_Dealers_DealerId",
                table: "FileModels",
                column: "DealerId",
                principalTable: "Dealers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FileModels_Locations_LocationId",
                table: "FileModels",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileModels_Dealers_DealerId",
                table: "FileModels");

            migrationBuilder.DropForeignKey(
                name: "FK_FileModels_Locations_LocationId",
                table: "FileModels");

            migrationBuilder.DropIndex(
                name: "IX_FileModels_DealerId",
                table: "FileModels");

            migrationBuilder.DropIndex(
                name: "IX_FileModels_LocationId",
                table: "FileModels");

            migrationBuilder.DropColumn(
                name: "DealerId",
                table: "FileModels");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "FileModels");
        }
    }
}
