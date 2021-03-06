﻿using System;
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
        IRepository<UserProfile> UserProfiles { get; }
        IRepository<FileModel> FileModels { get; } 
        IRepository<Company> Companys { get; }
        IRepository<Location> Locations { get; }
        IRepository<Feedback> Feedbacks { get; }
        IRepository<Manager> Managers { get; }
        IRepository<Menu> Menus { get; }
        IRepository<MenuItem> MenuItems { get; } 
        IRepository<Order> Orders { get; }
        IRepository<OrderItem> OrderItems { get; }
        IRepository<OrderUser> OrderUsers { get; }
        IRepository<Sex> Sexes { get; }
        IRepository<Icon> Icons { get; } 
        IRepository<ManagerCall> ManagerCalls { get; }
        IRepository<CompanySpecification> CompanySpecifications { get; } 
        IRepository<Table> Tables { get; }
        IRepository<TableBook> TableBooks { get; }

        Task SaveAsync();
    }
}
