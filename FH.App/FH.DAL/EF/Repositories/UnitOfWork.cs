using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FH.DAL.DataContext;
using FH.DAL.EF.Interfaces;
using FH.Models.EnumModels;
using FH.Models.Models;
using FH.Models.StaticModels;
using Microsoft.EntityFrameworkCore;

namespace FH.DAL.EF.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        public UnitOfWork(DbContextOptions options)
        {
            _db = new EfDbContext(options);
        }

        private readonly EfDbContext _db;
        private IFullRepository<UserProfile> _userProfiles;
        private IFullRepository<FileModel> _fileModels;
        private IFullRepository<CuisineUser> _cuisineUsers;
        private IFullRepository<Company> _companys;
        private IFullRepository<Location> _locations;
        private IFullRepository<Feedback> _feedbacks;
        private IFullRepository<Manager> _managers;
        private IFullRepository<Menu> _menus;
        private IFullRepository<MenuItem> _menuItems;
        private IFullRepository<Subscription> _subscriptions;
        private IFullRepository<Order> _orders;
        private IFullRepository<OrderItem> _orderItems;
        private IRepository<Sex> _sexes;
        private IRepository<Cuisine> _cuisines;
        private IRepository<CompanySpecification> _companyspecifications;
        private IRepository<SubscriptionType> _subscriptionTypes;

        public IFullRepository<UserProfile> UserProfiles => _userProfiles ?? (_userProfiles = new FullRepository<UserProfile>(_db));
        public IFullRepository<FileModel> FileModels => _fileModels ?? (_fileModels = new FullRepository<FileModel>(_db));
        public IFullRepository<CuisineUser> CuisineUsers => _cuisineUsers ?? (_cuisineUsers = new FullRepository<CuisineUser>(_db));
        public IFullRepository<Company> Companys => _companys ?? (_companys = new FullRepository<Company>(_db));
        public IFullRepository<Location> Locations => _locations ?? (_locations = new FullRepository<Location>(_db));
        public IFullRepository<Feedback> Feedbacks => _feedbacks ?? (_feedbacks = new FullRepository<Feedback>(_db));
        public IFullRepository<Manager> Managers => _managers ?? (_managers = new FullRepository<Manager>(_db));
        public IFullRepository<Menu> Menus => _menus ?? (_menus = new FullRepository<Menu>(_db));
        public IFullRepository<Subscription> Subscriptions => _subscriptions ?? (_subscriptions = new FullRepository<Subscription>(_db));
        public IFullRepository<MenuItem> MenuItems => _menuItems ?? (_menuItems = new FullRepository<MenuItem>(_db));
        public IFullRepository<Order> Orders => _orders ?? (_orders = new FullRepository<Order>(_db));
        public IFullRepository<OrderItem> OrderItems => _orderItems ?? (_orderItems = new FullRepository<OrderItem>(_db));
        public IRepository<Sex> Sexes => _sexes ?? (_sexes = new Repository<Sex>(_db));
        public IRepository<Cuisine> Cuisines => _cuisines ?? (_cuisines = new Repository<Cuisine>(_db));
        public IRepository<CompanySpecification> CompanySpecifications => _companyspecifications ?? (_companyspecifications = new Repository<CompanySpecification>(_db));
        public IRepository<SubscriptionType> SubscriptionTypes => _subscriptionTypes ?? (_subscriptionTypes = new Repository<SubscriptionType>(_db));

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (this._disposed)
                return;
            if (disposing)
            {
                _db.Dispose();
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
