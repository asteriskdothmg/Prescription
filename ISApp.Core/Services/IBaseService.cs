using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ISApp.Core.Services
{
    public interface IBaseService<TEntity> : IDisposable where TEntity : class
    {
        List<TEntity> GetAllList();
        Task<List<TEntity>> GetAllListAsync();
        IQueryable<TEntity> GetAllQueryable();
        TEntity Insert(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);
        bool BulkInsert(IList<TEntity> list);
        bool Update(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        bool Delete(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        bool BulkDelete(Expression<Func<TEntity, bool>> filter);
        Task<List<TEntity>> SearchListAsync(Expression<Func<TEntity, bool>> filter);
        List<TEntity> SearchList(Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> SearchQueryable(Expression<Func<TEntity, bool>> filter);
        int ExecuteNonQueryInt(string commandString, bool isStoredProc = false, params object[] param);
        DataTable ExecuteNonQueryDataTable(string commandString, bool isStoredProc = false, params object[] param);
        object ExecuteScalar(string commandString, bool isStoredProc = false, params object[] param);
    }
}
