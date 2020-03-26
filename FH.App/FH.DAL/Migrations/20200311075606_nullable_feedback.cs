using Microsoft.EntityFrameworkCore.Migrations;

namespace FH.DAL.Migrations
{
    public partial class nullable_feedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FeedbackId",
                table: "OrderUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderUsers_FeedbackId",
                table: "OrderUsers",
                column: "FeedbackId",
                unique: true,
                filter: "[FeedbackId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderUsers_Feedbacks_FeedbackId",
                table: "OrderUsers",
                column: "FeedbackId",
                principalTable: "Feedbacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderUsers_Feedbacks_FeedbackId",
                table: "OrderUsers");

            migrationBuilder.DropIndex(
                name: "IX_OrderUsers_FeedbackId",
                table: "OrderUsers");

            migrationBuilder.DropColumn(
                name: "FeedbackId",
                table: "OrderUsers");
        }
    }
}
