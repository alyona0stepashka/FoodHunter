using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.BLL.Interfaces;
using FH.BLL.VMs;
using FH.DAL.EF.Interfaces;

namespace FH.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _db { get; set; }
        //private readonly 
        public UserService(IUnitOfWork uow)
        {
            _db = uow;
        }

        public UserTabVM GetUserTabVM(string userId)
        {
            return new UserTabVM(_db.UserProfiles.GetAll().FirstOrDefault(m => m.UserId == userId));
        }
    }
}
