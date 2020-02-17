using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FH.BLL.Interfaces;
using FH.BLL.VMs;
using FH.DAL.EF.Interfaces;
using FH.Models.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace FH.BLL.Services
{
    public class CompanyService: ICompanyService
    {
        private IUnitOfWork _db { get; set; }

        private readonly IFileService _fileService;
        //private readonly 
        public CompanyService(IUnitOfWork uow, IFileService fileService)
        {
            _db = uow;
            _fileService = fileService;
        }

        public List<CompanyPageVM> GetAllCompanies()
        {
            var companies = _db.Companys.GetAll().ToList();
            if (companies == null)
            {
                throw new Exception("Company not found");
            }
            return companies.Select(m=> new CompanyPageVM(m)).ToList();
        }

        public async Task<CompanyPageVM> GetCompanyPageAsync(int id)
        {
                var company = await _db.Companys.GetByIdAsync(id);
                if (company == null)
                {
                    throw new Exception("Company not found");
                }
                return new CompanyPageVM(company);
        }

        public async Task<CompanyPageVM> CreateCompanyAsync(CreateCompanyVM company, string userId)
        {
                var fileId = await _fileService.CreateFileDbAsync(company.LogoFile);
                var newCompany = new Company()
                {
                    Name = company.Name,
                    ContactInfo = company.ContactInfo,
                    Vk = company.Vk,
                    Facebook = company.Facebook,
                    Instagram = company.Instagram,
                    Site = company.Site,
                    Describe = company.Describe,
                    SpecificationId = company.SpecificationId,
                    FileId = fileId,
                    AdminId = userId
                };
                var model = await _db.Companys.CreateAsync(newCompany);
                return new CompanyPageVM(model);
        }

        public async Task DeleteCompanyAsync(int id)
        {
                await _db.Companys.DeleteAsync(id);
        }
    }
}
