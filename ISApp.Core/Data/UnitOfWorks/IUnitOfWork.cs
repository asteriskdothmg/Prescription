using ISApp.Core.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISApp.Core.Data.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        int Commit();
        Task<int> CommitAsync();
        void Dispose(bool disposing);
    }
}
