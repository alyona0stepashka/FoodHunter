using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using FH.Models;

namespace FH.DAL.EF.Interfaces
{
    interface IFullRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        List<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate);
    }
}