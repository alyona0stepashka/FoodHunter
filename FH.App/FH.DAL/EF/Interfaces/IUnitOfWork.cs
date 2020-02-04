using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FH.DAL.EF.Interfaces
{
    interface IUnitOfWork : IDisposable
    {
        Task SaveAsync();
    }
}
