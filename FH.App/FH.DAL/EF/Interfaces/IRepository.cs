using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FH.Models;

namespace FH.DAL.EF.Interfaces
{
    interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> CreateAsync(TEntity item);
        TEntity Update(TEntity item);
        Task<TEntity> DeleteAsync(int id);
        List<TEntity> GetAll();
        Task<TEntity> GetByIdAsync(int id);
        Task Save();
    }
}
