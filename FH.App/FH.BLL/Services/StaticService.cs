using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FH.BLL.Interfaces;
using FH.BLL.VMs;
using FH.DAL.EF.Interfaces;
using FH.Models.EnumModels;
using FH.Models.StaticModels;

namespace FH.BLL.Services
{
    public class StaticService: IStaticService
    {
        private IUnitOfWork _db { get; set; }
        //private readonly 
        public StaticService(IUnitOfWork uow)
        {
            _db = uow;
        }

        public List<Sex> GetSexes()
        {
            var items = _db.Sexes.GetAll().ToList();
            if (items == null)
            {
                throw new Exception("Sexes not found");
            }
            return items;

        }
        public List<CompanySpecification> GetSpecifications()
        {
            var items = _db.CompanySpecifications.GetAll().ToList();
            if (items == null)
            {
                throw new Exception("Specifications not found");
            }
            return items;

        }

        public List<Icon> GetIcons()
        {
            var items = _db.Icons.GetAll().ToList();
            if (items == null)
            {
                throw new Exception("Icons not found");
            }
            return items;

        }
    }
}
