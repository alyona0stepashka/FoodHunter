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
        private IRepository<UserProfile> _userProfiles;
        private IRepository<FileModel> _fileModels;
        private IRepository<CuisineUser> _cuisineUsers;
        private IRepository<Company> _companys;
        private IRepository<Location> _locations;
        private IRepository<Feedback> _feedbacks;
        private IRepository<Manager> _managers;
        private IRepository<Menu> _menus;
        private IRepository<MenuItem> _menuItems;
        private IRepository<Subscription> _subscriptions;
        private IRepository<Order> _orders;
        private IRepository<OrderUser> _orderUsers;
        private IRepository<OrderItem> _orderItems;
        private IRepository<Sex> _sexes;
        private IRepository<Cuisine> _cuisines;
        private IRepository<ManagerCall> _managerCalls;
        private IRepository<CompanySpecification> _companyspecifications;
        private IRepository<SubscriptionType> _subscriptionTypes;
        private IRepository<Icon> _icons;
        private IRepository<Table> _tables;
        private IRepository<TableBook> _tableBooks;

        public IRepository<UserProfile> UserProfiles => _userProfiles ?? (_userProfiles = new Repository<UserProfile>(_db));
        public IRepository<FileModel> FileModels => _fileModels ?? (_fileModels = new Repository<FileModel>(_db));
        public IRepository<ManagerCall> ManagerCalls => _managerCalls ?? (_managerCalls = new Repository<ManagerCall>(_db));
        public IRepository<CuisineUser> CuisineUsers => _cuisineUsers ?? (_cuisineUsers = new Repository<CuisineUser>(_db));
        public IRepository<Company> Companys => _companys ?? (_companys = new Repository<Company>(_db));
        public IRepository<Location> Locations => _locations ?? (_locations = new Repository<Location>(_db));
        public IRepository<Feedback> Feedbacks => _feedbacks ?? (_feedbacks = new Repository<Feedback>(_db));
        public IRepository<Manager> Managers => _managers ?? (_managers = new Repository<Manager>(_db));
        public IRepository<Menu> Menus => _menus ?? (_menus = new Repository<Menu>(_db));
        public IRepository<Subscription> Subscriptions => _subscriptions ?? (_subscriptions = new Repository<Subscription>(_db));
        public IRepository<MenuItem> MenuItems => _menuItems ?? (_menuItems = new Repository<MenuItem>(_db));
        public IRepository<Order> Orders => _orders ?? (_orders = new Repository<Order>(_db));
        public IRepository<OrderUser> OrderUsers => _orderUsers ?? (_orderUsers = new Repository<OrderUser>(_db));
        public IRepository<OrderItem> OrderItems => _orderItems ?? (_orderItems = new Repository<OrderItem>(_db));
        public IRepository<Sex> Sexes => _sexes ?? (_sexes = new Repository<Sex>(_db));
        public IRepository<Cuisine> Cuisines => _cuisines ?? (_cuisines = new Repository<Cuisine>(_db));
        public IRepository<Icon> Icons => _icons ?? (_icons = new Repository<Icon>(_db));
        public IRepository<Table> Tables => _tables ?? (_tables = new Repository<Table>(_db));
        public IRepository<TableBook> TableBooks => _tableBooks ?? (_tableBooks = new Repository<TableBook>(_db));
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
