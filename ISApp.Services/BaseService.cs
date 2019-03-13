using ISApp.Core.Data;
using ISApp.Core.Data.Repositories;
using ISApp.Core.Data.UnitOfWorks;
using ISApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ISApp.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        #region Declarations and Constructors
        public IUnitOfWork _unitOfWork { get; private set; }
        private readonly IRepository<TEntity> _repository;
        private bool _disposed;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.Repository<TEntity>();
        }
        #endregion Declarations and Constructors
        
        #region IService Interface Implementations (Basic Database Methods)
        public List<TEntity> GetAllList()
        {
            return _repository.GetAllList();
        }

        public Task<List<TEntity>> GetAllListAsync()
        {
            return _repository.GetAllListAsync();
        }

        public System.Linq.IQueryable<TEntity> GetAllQueryable()
        {
            return _repository.GetAllQueryable();
        }

        public TEntity Insert(TEntity entity)
        {
            _repository.Insert(entity);
            _unitOfWork.Commit();

            return entity;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            _repository.Insert(entity);
            await _unitOfWork.CommitAsync();

            return entity;
        }

        public bool BulkInsert(IList<TEntity> list)
        {
            try
            {
                _repository.BulkInsert(list);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(TEntity entity)
        {
            try
            {
                _repository.Update(entity);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
                _repository.Update(entity);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(TEntity entity)
        {
            try
            {
                _repository.Delete(entity);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                _repository.Delete(entity);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool BulkDelete(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                _repository.BulkDelete(filter);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<List<TEntity>> SearchListAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            return _repository.SearchListAsync(filter);
        }

        public List<TEntity> SearchList(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            return _repository.SearchList(filter);
        }

        public System.Linq.IQueryable<TEntity> SearchQueryable(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            return _repository.SearchQueryable(filter);
        }


        public int ExecuteNonQueryInt(string commandString, bool isStoredProc = false, params object[] param)
        {
            return _repository.ExecuteNonQueryInt(commandString, isStoredProc, param);
        }

        public System.Data.DataTable ExecuteNonQueryDataTable(string commandString, bool isStoredProc = false, params object[] param)
        {
            return _repository.ExecuteNonQueryDataTable(commandString, isStoredProc, param);
        }

        public object ExecuteScalar(string commandString, bool isStoredProc = false, params object[] param)
        {
            return _repository.ExecuteScalar(commandString, isStoredProc, param);
        }
        #endregion IService Interface Implementations (Basic Database Methods)

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _unitOfWork.Dispose();
            }
            _disposed = true;
        }        
        #endregion Dispose                
    }
}
