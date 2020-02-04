using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FH.DAL.DataContext;
using FH.DAL.EF.Interfaces;
using FH.Models;

namespace FH.DAL.EF.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly EfDbContext _db;

        public Repository(EfDbContext context)
        {
            _db = context;
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _db.Set<TEntity>().FindAsync(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _db.Set<TEntity>() /*.AsNoTracking()*/;
        }

        public virtual TEntity Update(TEntity entity)
        {
            _db.Set<TEntity>().Update(entity);
            return entity;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _db.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _db.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public virtual async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
