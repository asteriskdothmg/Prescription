using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ISApp.Core.Data.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity: class
    {
        Task<List<TEntity>> GetAllListAsync();
        List<TEntity> GetAllList();
        IQueryable<TEntity> GetAllQueryable();
        void Insert(TEntity entity);
        void BulkInsert(IList<TEntity> list);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void BulkDelete(Expression<Func<TEntity, bool>> filter);
        Task<List<TEntity>> SearchListAsync(Expression<Func<TEntity, bool>> filter);
        List<TEntity> SearchList(Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> SearchQueryable(Expression<Func<TEntity, bool>> filter);
        int ExecuteNonQueryInt(string commandString, bool isStoredProc = false, params object[] param);
        DataTable ExecuteNonQueryDataTable(string commandString, bool isStoredProc = false, params object[] param);
        object ExecuteScalar(string commandString, bool isStoredProc = false, params object[] param);
    }
}
