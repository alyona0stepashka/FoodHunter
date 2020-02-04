using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FH.DAL.Migrations
{
    public partial class init05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Locations_LocationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Dealers_DealerId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_AspNetUsers_UserId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LocationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Locations",
                newName: "AdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_UserId",
                table: "Locations",
                newName: "IX_Locations_AdminId");

            migrationBuilder.AlterColumn<int>(
                name: "DealerId",
                table: "Locations",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FeedbackId",
                table: "FileModels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdditionalInfo",
                table: "DealerSpecifications",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WorkBegin = table.Column<DateTime>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    UserProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manager_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Manager_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menu_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BuyDate = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    SubscriptionTypeId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscription_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    MenuId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItem_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Stars = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    LocationId = table.Column<int>(nullable: true),
                    MenuItemId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Feedback_MenuItem_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileModels_FeedbackId",
                table: "FileModels",
                column: "FeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_LocationId",
                table: "Feedback",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_MenuItemId",
                table: "Feedback",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Manager_LocationId",
                table: "Manager",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Manager_UserProfileId",
                table: "Manager",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_LocationId",
                table: "Menu",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_MenuId",
                table: "MenuItem",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_LocationId",
                table: "Subscription",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileModels_Feedback_FeedbackId",
                table: "FileModels",
                column: "FeedbackId",
                principalTable: "Feedback",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_AspNetUsers_AdminId",
                table: "Locations",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Dealers_DealerId",
                table: "Locations",
                column: "DealerId",
                principalTable: "Dealers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileModels_Feedback_FeedbackId",
                table: "FileModels");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_AspNetUsers_AdminId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Dealers_DealerId",
                table: "Locations");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "MenuItem");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_FileModels_FeedbackId",
                table: "FileModels");

            migrationBuilder.DropColumn(
                name: "FeedbackId",
                table: "FileModels");

            migrationBuilder.DropColumn(
                name: "AdditionalInfo",
                table: "DealerSpecifications");

            migrationBuilder.RenameColumn(
                name: "AdminId",
                table: "Locations",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_AdminId",
                table: "Locations",
                newName: "IX_Locations_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "DealerId",
                table: "Locations",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LocationId",
                table: "AspNetUsers",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Locations_LocationId",
                table: "AspNetUsers",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Dealers_DealerId",
                table: "Locations",
                column: "DealerId",
                principalTable: "Dealers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_AspNetUsers_UserId",
                table: "Locations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
