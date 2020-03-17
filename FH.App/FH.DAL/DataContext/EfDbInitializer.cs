using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FH.DAL.EF.Interfaces;
using FH.Models.EnumModels;
using FH.Models.Models;
using FH.Models.StaticModels;
using Microsoft.AspNetCore.Identity;

namespace FH.DAL.DataContext
{
    public class EfDbInitializer
    {
        public static async Task InitializeAsync(IUnitOfWork db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //------------------------
            //var id1 = 0;
            if (!db.CompanySpecifications.GetAll().Any())
            {
                var list = new List<CompanySpecification>()
                {
                    new CompanySpecification() {Value = "Not defined"},
                    new CompanySpecification() {Value = "Restaurant"},
                    new CompanySpecification() {Value = "Bar"},
                    new CompanySpecification() {Value = "Cafe"},
                    new CompanySpecification() {Value = "Bistro"},
                    new CompanySpecification() {Value = "Teahouse"},
                    new CompanySpecification() {Value = "Kebab house"},
                    new CompanySpecification() {Value = "Dumpling house"},
                    new CompanySpecification() {Value = "Noodle bar"},
                    new CompanySpecification() {Value = "Pizzeria"},
                    new CompanySpecification() {Value = "Coffee Shop"},
                    new CompanySpecification() {Value = "Cooking stores"},
                    new CompanySpecification() {Value = "Eatery"}
                };
                foreach (var item in list)
                {
                    await db.CompanySpecifications.CreateAsync(item);
                    //id1 = item.Id;
                }
            }

            //------------------------
            //var id2 = 0;
            if (!db.Cuisines.GetAll().Any())
            {
                var list = new List<Cuisine>()
                {
                    new Cuisine() {Value = "Not defined"},
                    new Cuisine() {Value = "Belarusian"},
                    new Cuisine() {Value = "Russian"},
                    new Cuisine() {Value = "Chinese"},
                    new Cuisine() {Value = "Greek"},
                    new Cuisine() {Value = "Azerbaijani"},
                    new Cuisine() {Value = "Brazilian"},
                    new Cuisine() {Value = "French"},
                    new Cuisine() {Value = "German"},
                    new Cuisine() {Value = "Indian"},
                    new Cuisine() {Value = "Jewish"},
                    new Cuisine() {Value = "Ukrainian"},
                    new Cuisine() {Value = "Italian"},
                    new Cuisine() {Value = "Armenian"}
                };
                foreach (var item in list)
                {
                    await db.Cuisines.CreateAsync(item);
                    //id2 = item.Id;
                }
            }

            //------------------------
            var id3 = 0;
            if (!db.Sexes.GetAll().Any())
            {
                var list = new List<Sex>()
                {
                    new Sex() {Value = "Not defined"},
                    new Sex() {Value = "Male"},
                    new Sex() {Value = "Female"},
                    new Sex() {Value = "Other"}
                };
                foreach (var item in list)
                {
                    await db.Sexes.CreateAsync(item);
                    id3 = item.Id;
                }
            }

            //------------------------
            //var id4 = 0;
            if (!db.SubscriptionTypes.GetAll().Any())
            {
                var list = new List<SubscriptionType>()
                {
                    new SubscriptionType()
                        {Title = "Free Trial", PricePerMonth = 0, Info = "Free trial month with Gold level"},
                    new SubscriptionType()
                        {Title = "Silver", PricePerMonth = 25, Info = "Take orders, 3 menu, 15 menu items"},
                    new SubscriptionType()
                        {Title = "Gold", PricePerMonth = 50, Info = "Silver + unlimited menu, reports"}
                };
                foreach (var item in list)
                {
                    await db.SubscriptionTypes.CreateAsync(item);
                    //id4 = item.Id;
                }
            }

            //------------------------
            //var id5 = 0;
            if (!db.Icons.GetAll().Any())
            {
                var list = new List<Icon>()
                {
                    new Icon() {Value="fas fa-star", Description = "Main"},
                    new Icon() {Value="fas fa-baby-carriage", Description = "For child"},
                    new Icon() {Value="fab fa-stripe-s", Description = "Special"},
                    new Icon() {Value="fas fa-wine-bottle", Description = "Contains alcohol"},
                    new Icon() {Value="fas fa-leaf", Description = "Vegan"}, 
                    new Icon() {Value="fas fa-pepper-hot", Description = "Spicy"} 
                };
                foreach (var item in list)
                {
                    await db.Icons.CreateAsync(item);
                    //id5 = item.Id;
                }
            }

            //-----------------------
            if (roleManager.Roles.ToList().Count == 0)
            {
                roleManager.CreateAsync(new IdentityRole("manager")).Wait();
                roleManager.CreateAsync(new IdentityRole("user")).Wait();
            }

            //---------------------
            if (!userManager.Users.Any())
            {
                var newUser = new IdentityUser()
                {
                    Email = "user1@mail.ru",
                    UserName = "user1@mail.ru",
                    EmailConfirmed = true
                };
                var newUser2 = new IdentityUser
                {
                    Email = "user2@mail.ru",
                    UserName = "user2@mail.ru",
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(newUser, "parol01");
                userManager.AddToRoleAsync(newUser, "manager").Wait();
                await userManager.CreateAsync(newUser2, "parol01");
                userManager.AddToRoleAsync(newUser2, "user").Wait();
                var userProfile = new UserProfile()
                {
                    FirstName = "User",
                    LastName = "Userovich",
                    DateBirth = new DateTime(1994, 10, 4),
                    DateLastOnline = DateTime.Now,
                    SexId = id3,
                    //FileId = fileId,
                    UserId = newUser.Id
                };
                var userProfile2 = new UserProfile()
                {
                    FirstName = "Ivan",
                    LastName = "Ivanovich",
                    DateBirth = new DateTime(1994, 10, 4),
                    DateLastOnline = DateTime.Now,
                    SexId = id3,
                    //FileId = fileId,
                    UserId = newUser2.Id
                };
                await db.UserProfiles.CreateAsync(userProfile);
                await db.UserProfiles.CreateAsync(userProfile2);
            }
        }


    }
}
