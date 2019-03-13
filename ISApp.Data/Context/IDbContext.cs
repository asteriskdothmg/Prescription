using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ISApp.Data.Context
{
    public interface IDbContext : IDisposable
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        void SetAsAdded<TEntity>(TEntity entity) where TEntity : class;
        void SetAsModified<TEntity>(TEntity entity) where TEntity : class;
        void SetAsDeleted<TEntity>(TEntity entity) where TEntity : class;
        int Commit();
        Task<int> CommitAsync();
    }
}
