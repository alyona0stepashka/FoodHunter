using System;
using System.Collections.Generic;
using System.Text;
using FH.BLL.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using FH.BLL.VMs;
using FH.DAL.EF.Interfaces;
using FH.Models.Models;

namespace FH.BLL.Services
{
    public class TableService : ITableService
    {
        private IUnitOfWork _db { get; set; }
        //private readonly 
        public TableService(IUnitOfWork uow)
        {
            _db = uow;
        }

        public List<TableTabVM> GetAllTablesByLocation(int id)
        {
            var tables = _db.Tables.GetAll().Where(m => m.LocationId == id).ToList();
            if (tables == null)
            {
                throw new Exception("Tables not found");
            }
            var ret = tables.Select(m => new TableTabVM(m)).ToList();
            return ret;
        }

        public List<TableBookVM> GetAllTableBooksByLocation(int id)
        {
            var tables = _db.TableBooks.GetAll().Where(m => m.Table.LocationId==id).ToList();
            if (tables == null)
            {
                throw new Exception("Tables not found");
            }
            var ret = tables.Select(m => new TableBookVM(m)).ToList();
            return ret;
        }

        public async Task<TableBookVM> CreateTableBookAsync(CreateTableBookVM table)
        {
            var dbTable = new TableBook
            {
                EndTime = table.EndTime,
                StartTime = table.StartTime,
                BookTime = table.BookTime,
                IsConfirm = null,
                IsActive = table.IsActive,
                TableId = table.TableId,
                Comment = table.Comment,
                ClientId = table.ClientId
            };
            var tableNew = await _db.TableBooks.CreateAsync(dbTable);
            var ret = new TableBookVM(tableNew);
            return ret;
        }

        public async Task<TableTabVM> CreateTableAsync(CreateTableVM table)
        {
            var dbTable = new Table
            { 
                Info = table.Info,
                LocationId = table.LocationId,
                Number = table.Number
            };
            var tableNew = await _db.Tables.CreateAsync(dbTable);
            var ret = new TableTabVM(tableNew);
            return ret;
        }

        public async Task DeleteTableAsync(int id)
        {
            await _db.Tables.DeleteAsync(id);
        }

        public async Task<TableTabVM> UpdateTableAsync(CreateTableVM table)
        {
            var item = await _db.Tables.GetByIdAsync(table.Id);
            item.Number = table.Number;
            item.Info = table.Info;
            item.LocationId = table.LocationId; 
            var newItem = await _db.Tables.UpdateAsync(item);
            return new TableTabVM(newItem);
        }

        public async Task<TableBookVM> AcceptTableBookAsync(int id)
        {
            var item = await _db.TableBooks.GetByIdAsync(id);
            item.IsConfirm = true;
            item.IsActive = true;
            var newItem = await _db.TableBooks.UpdateAsync(item);
            return new TableBookVM(newItem);
        }

        public async Task<TableBookVM> DeclineTableBookAsync(int id)
        {
            var item = await _db.TableBooks.GetByIdAsync(id);
            item.IsConfirm = false;
            item.IsActive = true;
            var newItem = await _db.TableBooks.UpdateAsync(item);
            return new TableBookVM(newItem);
        }

        public async Task<TableBookVM> CancelTableBookAsync(int id)
        {
            var item = await _db.TableBooks.GetByIdAsync(id); 
            item.IsActive = false;
            var newItem = await _db.TableBooks.UpdateAsync(item);
            return new TableBookVM(newItem);
        }
    }
}
