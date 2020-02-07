﻿using System;
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

        public async Task<CompanyPageVM> GetCompanyPageAsync(int id)
        {
            try
            {
                var Company = await _db.Companys.GetByIdAsync(id);
                if (Company == null)
                {
                    throw new Exception("Company not found");
                }
                return new CompanyPageVM(Company);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<CompanyPageVM> CreateCompanyAsync(CreateCompanyVM Company, string userId)
        {
            try 
            { 
                var fileId = await _fileService.CreateFileDbAsync(Company.LogoFile);
                var newCompany = new Company()
                {
                    Name = Company.Name,
                    ContactInfo = Company.ContactInfo,
                    Vk = Company.Vk,
                    Facebook = Company.Facebook,
                    Instagram = Company.Instagram,
                    Site = Company.Site,
                    Describe = Company.Describe,
                    SpecificationId = Company.SpecificationId,
                    FileId = fileId,
                    AdminId = userId
                };
                return new CompanyPageVM(await _db.Companys.CreateAsync(newCompany));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteCompanyAsync(int id)
        {
            try
            {
                await _db.Companys.DeleteAsync(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
