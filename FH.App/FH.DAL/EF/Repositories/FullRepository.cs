using FH.DAL.DataContext;
using FH.DAL.EF.Interfaces;
using FH.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace FH.DAL.EF.Repositories
{
    public class FullRepository<TEntity> : Repository<TEntity>, IFullRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly EfDbContext _db;

        public FullRepository(EfDbContext context) : base(context)
        {
            _db = context;
        }

        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return _db.Set<TEntity>().Where(predicate);
        }
    }
}
