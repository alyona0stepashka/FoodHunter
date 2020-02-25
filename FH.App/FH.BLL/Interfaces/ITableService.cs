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
        List<TableBookVM> GetAllTableBooksByLocation(int id);
        Task<TableTabVM> CreateTableAsync(CreateTableVM table);
        Task<TableBookVM> CreateTableBookAsync(CreateTableBookVM table);
        Task<TableTabVM> UpdateTableAsync(CreateTableVM table);
        Task<TableBookVM> AcceptTableBookAsync(int id);
        Task<TableBookVM> DeclineTableBookAsync(int id);
        Task<TableBookVM> CancelTableBookAsync(int id);
        Task DeleteTableAsync(int id);
    }
}
