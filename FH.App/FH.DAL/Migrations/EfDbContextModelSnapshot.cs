﻿// <auto-generated />
using System;
using FH.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FH.DAL.Migrations
{
    [DbContext(typeof(EfDbContext))]
    partial class EfDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FH.Models.EnumModels.Icon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Icons");
                });

            modelBuilder.Entity("FH.Models.EnumModels.Sex", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Sexes");
                });

            modelBuilder.Entity("FH.Models.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdminId");

                    b.Property<string>("ContactInfo");

                    b.Property<string>("Describe");

                    b.Property<string>("Facebook");

                    b.Property<int?>("FileId");

                    b.Property<string>("Instagram");

                    b.Property<string>("Name");

                    b.Property<string>("Site");

                    b.Property<int?>("SpecificationId");

                    b.Property<string>("Vk");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("FileId")
                        .IsUnique()
                        .HasFilter("[FileId] IS NOT NULL");

                    b.HasIndex("SpecificationId");

                    b.ToTable("Companys");
                });

            modelBuilder.Entity("FH.Models.Models.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment");

                    b.Property<int?>("LocationId");

                    b.Property<int?>("MenuItemId");

                    b.Property<int?>("OrderUserId");

                    b.Property<int>("Stars");

                    b.Property<int>("UserProfileId");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("MenuItemId");

                    b.HasIndex("OrderUserId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("FH.Models.Models.FileModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Extension");

                    b.Property<int?>("FeedbackId");

                    b.Property<int?>("LocationId");

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.HasKey("Id");

                    b.HasIndex("FeedbackId");

                    b.HasIndex("LocationId");

                    b.ToTable("FileModels");
                });

            modelBuilder.Entity("FH.Models.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("AdminId");

                    b.Property<int?>("CompanyId");

                    b.Property<string>("Latitude");

                    b.Property<string>("Longitude");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("FH.Models.Models.Manager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("LocationId");

                    b.Property<int?>("UserProfileId");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("UserProfileId")
                        .IsUnique()
                        .HasFilter("[UserProfileId] IS NOT NULL");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("FH.Models.Models.ManagerCall", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CallTime");

                    b.Property<string>("Comment");

                    b.Property<bool>("IsActive");

                    b.Property<int?>("OrderId");

                    b.Property<int?>("UserProfileId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("ManagerCalls");
                });

            modelBuilder.Entity("FH.Models.Models.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("IconId");

                    b.Property<string>("Info");

                    b.Property<int?>("LocationId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("IconId");

                    b.HasIndex("LocationId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("FH.Models.Models.MenuItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FileModelId");

                    b.Property<string>("Info");

                    b.Property<bool>("IsActive");

                    b.Property<int?>("MenuId");

                    b.Property<string>("Note");

                    b.Property<decimal>("Price");

                    b.Property<decimal?>("PriceWithSales");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("FileModelId");

                    b.HasIndex("MenuId");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("FH.Models.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EndDate");

                    b.Property<bool>("IsActive");

                    b.Property<int?>("LocationId");

                    b.Property<int?>("ManagerId");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Status");

                    b.Property<int?>("TableId");

                    b.Property<Guid>("WelcomeCode")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("ManagerId");

                    b.HasIndex("TableId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("FH.Models.Models.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Count");

                    b.Property<int?>("MenuItemId");

                    b.Property<int?>("OrderId");

                    b.Property<decimal>("PricePerItem");

                    b.Property<string>("Status");

                    b.Property<string>("Title");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("MenuItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("FH.Models.Models.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BuyDate");

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("LocationId");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("SubscriptionTypeId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("SubscriptionTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("FH.Models.Models.Table", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Info");

                    b.Property<int?>("LocationId");

                    b.Property<int>("Number");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("FH.Models.Models.TableBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BookTime");

                    b.Property<int?>("ClientId");

                    b.Property<string>("Comment");

                    b.Property<DateTime>("EndTime");

                    b.Property<bool>("IsActive");

                    b.Property<bool?>("IsConfirm");

                    b.Property<DateTime>("StartTime");

                    b.Property<int?>("TableId");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("TableId");

                    b.ToTable("TableBooks");
                });

            modelBuilder.Entity("FH.Models.Models.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateBirth");

                    b.Property<DateTime>("DateLastOnline");

                    b.Property<int?>("FileId");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<int?>("SexId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("FileId")
                        .IsUnique()
                        .HasFilter("[FileId] IS NOT NULL");

                    b.HasIndex("SexId");

                    b.HasIndex("UserId");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("FH.Models.StaticModels.CompanySpecification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalInfo");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("CompanySpecifications");
                });

            modelBuilder.Entity("FH.Models.StaticModels.Cuisine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Cuisines");
                });

            modelBuilder.Entity("FH.Models.StaticModels.CuisineUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CuisineId");

                    b.Property<int>("UserProfileId");

                    b.HasKey("Id");

                    b.HasIndex("CuisineId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("CuisineUsers");
                });

            modelBuilder.Entity("FH.Models.StaticModels.OrderUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrderId");

                    b.Property<int>("UserProfileId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("OrderUsers");
                });

            modelBuilder.Entity("FH.Models.StaticModels.SubscriptionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Info");

                    b.Property<decimal>("PricePerMonth");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("SubscriptionTypes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FH.Models.Models.Company", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId");

                    b.HasOne("FH.Models.Models.FileModel", "File")
                        .WithOne("Company")
                        .HasForeignKey("FH.Models.Models.Company", "FileId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FH.Models.StaticModels.CompanySpecification", "Specification")
                        .WithMany("Companies")
                        .HasForeignKey("SpecificationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FH.Models.Models.Feedback", b =>
                {
                    b.HasOne("FH.Models.Models.Location", "Location")
                        .WithMany("Feedbacks")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FH.Models.Models.MenuItem", "MenuItem")
                        .WithMany("Feedbacks")
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FH.Models.StaticModels.OrderUser", "OrderUser")
                        .WithMany()
                        .HasForeignKey("OrderUserId");

                    b.HasOne("FH.Models.Models.UserProfile", "UserProfile")
                        .WithMany("Feedbacks")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FH.Models.Models.FileModel", b =>
                {
                    b.HasOne("FH.Models.Models.Feedback", "Feedback")
                        .WithMany("Photos")
                        .HasForeignKey("FeedbackId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FH.Models.Models.Location", "Location")
                        .WithMany("PhotoAlbum")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FH.Models.Models.Location", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId");

                    b.HasOne("FH.Models.Models.Company", "Company")
                        .WithMany("Locations")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FH.Models.Models.Manager", b =>
                {
                    b.HasOne("FH.Models.Models.Location", "Location")
                        .WithMany("Managers")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FH.Models.Models.UserProfile", "UserProfile")
                        .WithOne("Manager")
                        .HasForeignKey("FH.Models.Models.Manager", "UserProfileId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FH.Models.Models.ManagerCall", b =>
                {
                    b.HasOne("FH.Models.Models.Order", "Order")
                        .WithMany("ManagerCalls")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FH.Models.Models.UserProfile")
                        .WithMany("ManagerCalls")
                        .HasForeignKey("UserProfileId");
                });

            modelBuilder.Entity("FH.Models.Models.Menu", b =>
                {
                    b.HasOne("FH.Models.EnumModels.Icon", "Icon")
                        .WithMany("Menus")
                        .HasForeignKey("IconId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FH.Models.Models.Location", "Location")
                        .WithMany("Menus")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FH.Models.Models.MenuItem", b =>
                {
                    b.HasOne("FH.Models.Models.FileModel", "FileModel")
                        .WithMany("MenuItems")
                        .HasForeignKey("FileModelId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FH.Models.Models.Menu", "Menu")
                        .WithMany("MenuItems")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FH.Models.Models.Order", b =>
                {
                    b.HasOne("FH.Models.Models.Location", "Location")
                        .WithMany("Orders")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FH.Models.Models.Manager", "Manager")
                        .WithMany("Orders")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FH.Models.Models.Table", "Table")
                        .WithMany("Orders")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FH.Models.Models.OrderItem", b =>
                {
                    b.HasOne("FH.Models.Models.MenuItem", "MenuItem")
                        .WithMany("OrderItems")
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FH.Models.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FH.Models.Models.UserProfile", "User")
                        .WithMany("OrderItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FH.Models.Models.Subscription", b =>
                {
                    b.HasOne("FH.Models.Models.Location", "Location")
                        .WithMany("Subscriptions")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FH.Models.StaticModels.SubscriptionType", "SubscriptionType")
                        .WithMany("Subscriptions")
                        .HasForeignKey("SubscriptionTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("FH.Models.Models.Table", b =>
                {
                    b.HasOne("FH.Models.Models.Location", "Location")
                        .WithMany("Tables")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FH.Models.Models.TableBook", b =>
                {
                    b.HasOne("FH.Models.Models.UserProfile", "Client")
                        .WithMany("TableBooks")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FH.Models.Models.Table", "Table")
                        .WithMany("TableBooks")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FH.Models.Models.UserProfile", b =>
                {
                    b.HasOne("FH.Models.Models.FileModel", "File")
                        .WithOne("UserProfile")
                        .HasForeignKey("FH.Models.Models.UserProfile", "FileId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FH.Models.EnumModels.Sex", "Sex")
                        .WithMany("Users")
                        .HasForeignKey("SexId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FH.Models.StaticModels.CuisineUser", b =>
                {
                    b.HasOne("FH.Models.StaticModels.Cuisine", "Cuisine")
                        .WithMany("CuisineUsers")
                        .HasForeignKey("CuisineId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FH.Models.Models.UserProfile", "UserProfile")
                        .WithMany("CuisineUsers")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FH.Models.StaticModels.OrderUser", b =>
                {
                    b.HasOne("FH.Models.Models.Order", "Order")
                        .WithMany("OrderUsers")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FH.Models.Models.UserProfile", "UserProfile")
                        .WithMany("OrderUsers")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
