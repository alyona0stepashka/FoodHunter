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
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<DealerSpecification> DealerSpecifications { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        public EfDbContext(DbContextOptions options)
            : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
