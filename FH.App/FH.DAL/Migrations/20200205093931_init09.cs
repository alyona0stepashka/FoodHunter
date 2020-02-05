using Microsoft.EntityFrameworkCore.Migrations;

namespace FH.DAL.Migrations
{
    public partial class init09 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "FileModels",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "FileModels",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileModels_CompanyId",
                table: "FileModels",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FileModels_LocationId",
                table: "FileModels",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileModels_companys_CompanyId",
                table: "FileModels",
                column: "CompanyId",
                principalTable: "Companys",
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
                name: "FK_FileModels_companys_CompanyId",
                table: "FileModels");

            migrationBuilder.DropForeignKey(
                name: "FK_FileModels_Locations_LocationId",
                table: "FileModels");

            migrationBuilder.DropIndex(
                name: "IX_FileModels_CompanyId",
                table: "FileModels");

            migrationBuilder.DropIndex(
                name: "IX_FileModels_LocationId",
                table: "FileModels");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "FileModels");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "FileModels");
        }
    }
}
