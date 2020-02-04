using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FH.DAL.Migrations
{
    public partial class init06 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Locations_LocationId",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_MenuItem_MenuItemId",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_FileModels_Feedback_FeedbackId",
                table: "FileModels");

            migrationBuilder.DropForeignKey(
                name: "FK_Manager_Locations_LocationId",
                table: "Manager");

            migrationBuilder.DropForeignKey(
                name: "FK_Manager_UserProfiles_UserProfileId",
                table: "Manager");

            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Locations_LocationId",
                table: "Menu");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_Menu_MenuId",
                table: "MenuItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_Locations_LocationId",
                table: "Subscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscription",
                table: "Subscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItem",
                table: "MenuItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menu",
                table: "Menu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manager",
                table: "Manager");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feedback",
                table: "Feedback");

            migrationBuilder.RenameTable(
                name: "Subscription",
                newName: "Subscriptions");

            migrationBuilder.RenameTable(
                name: "MenuItem",
                newName: "MenuItems");

            migrationBuilder.RenameTable(
                name: "Menu",
                newName: "Menus");

            migrationBuilder.RenameTable(
                name: "Manager",
                newName: "Managers");

            migrationBuilder.RenameTable(
                name: "Feedback",
                newName: "Feedbacks");

            migrationBuilder.RenameIndex(
                name: "IX_Subscription_LocationId",
                table: "Subscriptions",
                newName: "IX_Subscriptions_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItem_MenuId",
                table: "MenuItems",
                newName: "IX_MenuItems_MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_Menu_LocationId",
                table: "Menus",
                newName: "IX_Menus_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Manager_UserProfileId",
                table: "Managers",
                newName: "IX_Managers_UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Manager_LocationId",
                table: "Managers",
                newName: "IX_Managers_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedback_MenuItemId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_MenuItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedback_LocationId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItems",
                table: "MenuItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menus",
                table: "Menus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Managers",
                table: "Managers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SubscriptionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    PricePerMonth = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionTypes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Locations_LocationId",
                table: "Feedbacks",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_MenuItems_MenuItemId",
                table: "Feedbacks",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FileModels_Feedbacks_FeedbackId",
                table: "FileModels",
                column: "FeedbackId",
                principalTable: "Feedbacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Locations_LocationId",
                table: "Managers",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_UserProfiles_UserProfileId",
                table: "Managers",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Menus_MenuId",
                table: "MenuItems",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Locations_LocationId",
                table: "Menus",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Locations_LocationId",
                table: "Subscriptions",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Locations_LocationId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_MenuItems_MenuItemId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_FileModels_Feedbacks_FeedbackId",
                table: "FileModels");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Locations_LocationId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_UserProfiles_UserProfileId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Menus_MenuId",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Locations_LocationId",
                table: "Menus");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Locations_LocationId",
                table: "Subscriptions");

            migrationBuilder.DropTable(
                name: "SubscriptionTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menus",
                table: "Menus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItems",
                table: "MenuItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Managers",
                table: "Managers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks");

            migrationBuilder.RenameTable(
                name: "Subscriptions",
                newName: "Subscription");

            migrationBuilder.RenameTable(
                name: "Menus",
                newName: "Menu");

            migrationBuilder.RenameTable(
                name: "MenuItems",
                newName: "MenuItem");

            migrationBuilder.RenameTable(
                name: "Managers",
                newName: "Manager");

            migrationBuilder.RenameTable(
                name: "Feedbacks",
                newName: "Feedback");

            migrationBuilder.RenameIndex(
                name: "IX_Subscriptions_LocationId",
                table: "Subscription",
                newName: "IX_Subscription_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Menus_LocationId",
                table: "Menu",
                newName: "IX_Menu_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItems_MenuId",
                table: "MenuItem",
                newName: "IX_MenuItem_MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_Managers_UserProfileId",
                table: "Manager",
                newName: "IX_Manager_UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Managers_LocationId",
                table: "Manager",
                newName: "IX_Manager_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_MenuItemId",
                table: "Feedback",
                newName: "IX_Feedback_MenuItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_LocationId",
                table: "Feedback",
                newName: "IX_Feedback_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscription",
                table: "Subscription",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menu",
                table: "Menu",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItem",
                table: "MenuItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manager",
                table: "Manager",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feedback",
                table: "Feedback",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Locations_LocationId",
                table: "Feedback",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_MenuItem_MenuItemId",
                table: "Feedback",
                column: "MenuItemId",
                principalTable: "MenuItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FileModels_Feedback_FeedbackId",
                table: "FileModels",
                column: "FeedbackId",
                principalTable: "Feedback",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Manager_Locations_LocationId",
                table: "Manager",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Manager_UserProfiles_UserProfileId",
                table: "Manager",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Locations_LocationId",
                table: "Menu",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_Menu_MenuId",
                table: "MenuItem",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_Locations_LocationId",
                table: "Subscription",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
