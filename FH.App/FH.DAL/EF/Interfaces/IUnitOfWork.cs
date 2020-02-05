using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FH.Models.EnumModels;
using FH.Models.Models;
using FH.Models.StaticModels;

namespace FH.DAL.EF.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IFullRepository<UserProfile> UserProfiles { get; }
        IFullRepository<FileModel> FileModels { get; }
        IFullRepository<CuisineUser> CuisineUsers { get; }
        IFullRepository<Company> Companys { get; }
        IFullRepository<Location> Locations { get; }
        IFullRepository<Feedback> Feedbacks { get; }
        IFullRepository<Manager> Managers { get; }
        IFullRepository<Menu> Menus { get; }
        IFullRepository<MenuItem> MenuItems { get; }
        IFullRepository<Subscription> Subscriptions { get; }
        IFullRepository<Order> Orders { get; }
        IFullRepository<OrderItem> OrderItems { get; }
        IRepository<Sex> Sexes { get; }
        IRepository<Cuisine> Cuisines { get; }
        IRepository<CompanySpecification> CompanySpecifications { get; }
        IRepository<SubscriptionType> SubscriptionTypes { get; }

        Task SaveAsync();
    }
}
