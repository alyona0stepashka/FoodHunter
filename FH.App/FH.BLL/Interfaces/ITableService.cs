using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FH.BLL.Infrastructure;
using FH.BLL.VMs;

namespace FH.BLL.Interfaces
{
    public interface ITableService
    {
        List<TableTabVM> GetAllTablesByLocation(int id);
        Task<TableTabVM> CreateTableAsync(CreateTableVM table);
        Task<TableTabVM> UpdateTableAsync(CreateTableVM table);
        Task DeleteTableAsync(int id);
    }
}
