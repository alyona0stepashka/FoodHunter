using Microsoft.EntityFrameworkCore.Migrations;

namespace FH.DAL.Migrations
{
    public partial class nullable_feedback3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "OrderUserId",
                table: "Feedbacks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_OrderUserId",
                table: "Feedbacks",
                column: "OrderUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_OrderUsers_OrderUserId",
                table: "Feedbacks",
                column: "OrderUserId",
                principalTable: "OrderUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_OrderUsers_OrderUserId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_OrderUserId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "OrderUserId",
                table: "Feedbacks");

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
    }
}
