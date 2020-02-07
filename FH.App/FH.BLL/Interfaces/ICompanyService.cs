using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FH.BLL.VMs;

namespace FH.BLL.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyPageVM> GetCompanyPageAsync(int id);
        Task<CompanyPageVM> CreateCompanyAsync(CreateCompanyVM Company, string userId);
        Task DeleteCompanyAsync(int id);
    }
}
