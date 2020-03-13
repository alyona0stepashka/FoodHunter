using System.Linq;
using FH.Models.EnumModels;
using FH.Models.Models;
using FH.Models.StaticModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FH.DAL.DataContext
{
    public class EfDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<FileModel> FileModels { get; set; }
        public DbSet<Sex> Sexes { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<CuisineUser> CuisineUsers { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<CompanySpecification> CompanySpecifications { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderUser> OrderUsers { get; set; }
        public DbSet<Icon> Icons { get; set; }
        public DbSet<ManagerCall> ManagerCalls { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<TableBook> TableBooks { get; set; }


        public EfDbContext(DbContextOptions options)
            : base(options)
        {
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //            {
            //                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //            }

            modelBuilder.Entity<Company>().HasOne(m => m.Specification).WithMany(m => m.Companies).HasForeignKey(m=>m.SpecificationId)
                .OnDelete(DeleteBehavior.Restrict); 
            modelBuilder.Entity<Company>().HasOne(m => m.File).WithOne(m => m.Company)
                .OnDelete(DeleteBehavior.Restrict);
            //            modelBuilder.Entity<Company>().HasOne(m => m.Admin).WithOne(m => m.Company)
            //                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feedback>().HasOne(m => m.Location).WithMany(m => m.Feedbacks).HasForeignKey(m => m.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Feedback>().HasOne(m => m.MenuItem).WithMany(m => m.Feedbacks).HasForeignKey(m => m.MenuItemId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Feedback>().HasOne(m => m.UserProfile).WithMany(m => m.Feedbacks).HasForeignKey(m => m.UserProfileId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<FileModel>().HasOne(m => m.Feedback).WithMany(m => m.Photos).HasForeignKey(m => m.FeedbackId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<FileModel>().HasOne(m => m.Location).WithMany(m => m.PhotoAlbum).HasForeignKey(m => m.LocationId)
                .OnDelete(DeleteBehavior.Restrict); 


//            modelBuilder.Entity<Location>().HasOne(m => m.Admin).WithMany(m => m.Location)
//                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Location>().HasOne(m => m.Company).WithMany(m => m.Locations).HasForeignKey(m => m.CompanyId)
                .OnDelete(DeleteBehavior.Restrict); 



            modelBuilder.Entity<Manager>().HasOne(m => m.Location).WithMany(m => m.Managers).HasForeignKey(m => m.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Manager>().HasOne(m => m.UserProfile).WithOne(m => m.Manager)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ManagerCall>().HasOne(m => m.Order).WithMany(m => m.ManagerCalls).HasForeignKey(m => m.OrderId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Menu>().HasOne(m => m.Location).WithMany(m => m.Menus).HasForeignKey(m => m.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Menu>().HasOne(m => m.Icon).WithMany(m => m.Menus).HasForeignKey(m => m.IconId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<MenuItem>().HasOne(m => m.Menu).WithMany(m => m.MenuItems).HasForeignKey(m => m.MenuId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MenuItem>().HasOne(m => m.FileModel).WithMany(m => m.MenuItems).HasForeignKey(m => m.FileModelId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Order>().HasOne(m => m.Manager).WithMany(m => m.Orders).HasForeignKey(m => m.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Order>().HasOne(m => m.Table).WithMany(m => m.Orders).HasForeignKey(m => m.TableId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Order>().HasOne(m => m.Location).WithMany(m => m.Orders).HasForeignKey(m => m.LocationId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<OrderItem>().HasOne(m => m.Order).WithMany(m => m.OrderItems).HasForeignKey(m => m.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OrderItem>().HasOne(m => m.MenuItem).WithMany(m => m.OrderItems).HasForeignKey(m => m.MenuItemId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OrderItem>().HasOne(m => m.User).WithMany(m => m.OrderItems).HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Subscription>().HasOne(m => m.SubscriptionType).WithMany(m => m.Subscriptions).HasForeignKey(m => m.SubscriptionTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Subscription>().HasOne(m => m.Location).WithMany(m => m.Subscriptions).HasForeignKey(m => m.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
            //            modelBuilder.Entity<Subscription>().HasOne(m => m.User).WithMany(m => m.Subscriptions)
            //                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Table>().HasOne(m => m.Location).WithMany(m => m.Tables).HasForeignKey(m => m.LocationId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<TableBook>().HasOne(m => m.Table).WithMany(m => m.TableBooks).HasForeignKey(m => m.TableId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TableBook>().HasOne(m => m.Client).WithMany(m => m.TableBooks).HasForeignKey(m => m.ClientId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<UserProfile>().HasOne(m => m.File).WithOne(m => m.UserProfile)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserProfile>().HasOne(m => m.Sex).WithMany(m => m.Users).HasForeignKey(m => m.SexId)
                .OnDelete(DeleteBehavior.Restrict);
            //            modelBuilder.Entity<UserProfile>().HasOne(m => m.User).WithMany(m => m.OrderItems)
            //                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<CuisineUser>().HasOne(m => m.Cuisine).WithMany(m => m.CuisineUsers).HasForeignKey(m => m.CuisineId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CuisineUser>().HasOne(m => m.UserProfile).WithMany(m => m.CuisineUsers).HasForeignKey(m => m.UserProfileId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<OrderUser>().HasOne(m => m.Order).WithMany(m => m.OrderUsers).HasForeignKey(m => m.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OrderUser>().HasOne(m => m.UserProfile).WithMany(m => m.OrderUsers).HasForeignKey(m => m.UserProfileId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
