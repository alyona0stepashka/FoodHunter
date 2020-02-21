using System;
using System.Collections.Generic;
using System.Text;
using FH.BLL.Interfaces;
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
    }
}
