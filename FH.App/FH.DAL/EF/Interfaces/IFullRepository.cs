using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FH.Models;

namespace FH.DAL.EF.Interfaces
{
    public interface IFullRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate);
    }
}