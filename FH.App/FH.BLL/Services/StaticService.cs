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
            var sexes = _db.Sexes.GetAll().ToList();
            if (sexes == null)
            {
                throw new Exception("Sexes not found");
            }
            return sexes;

        }
        public List<CompanySpecification> GetSpecifications()
        {
            var specifications = _db.CompanySpecifications.GetAll().ToList();
            if (specifications == null)
            {
                throw new Exception("Specifications not found");
            }
            return specifications;

        }
    }
}
