using ISApp.Core.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using EntityFramework.BulkInsert.Extensions;
using System.Linq.Expressions;
using System.Linq;
using ISApp.Data.Context;
using System.Data.SqlClient;
using System.Data;

namespace ISApp.Data.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Declarations and Constructors
        private readonly DbContext _dbContext;
        private readonly IDbContext _context;
        private readonly IDbSet<TEntity> _dbEntitySet;
        private bool _disposed;

        public BaseRepository(IDbContext context)
        {
            _context = context;
            _dbEntitySet = _context.Set<TEntity>();
            _dbContext = (_context as System.Data.Entity.DbContext);
        }
        #endregion Declarations and Constructors

        #region IRepository Interface Implementations (Basic Database Methods)
        public async Task<List<TEntity>> GetAllListAsync()
        {
            return await _dbEntitySet.ToListAsync();
        }

        public List<TEntity> GetAllList()
        {
            System.Linq.IQueryable<TEntity> queryable = GetAllQueryable();
            List<TEntity> list = new List<TEntity>(queryable);
            return list;
        }

        public System.Linq.IQueryable<TEntity> GetAllQueryable()
        {
            return _dbEntitySet;
        }

        public void Insert(TEntity entity)
        {
            _context.SetAsAdded(entity);
        }

        public void Update(TEntity entity)
        {
            _context.SetAsModified(entity);
        }

        public void BulkInsert(IList<TEntity> list)
        {
            _dbContext.BulkInsert(list);
        }

        public void Delete(TEntity entity)
        {
            _context.SetAsDeleted(entity);
        }

        public void BulkDelete(Expression<Func<TEntity, bool>> filter)
        {
            var dbSet = _dbContext.Set<TEntity>();
            dbSet.RemoveRange(GetAllQueryable().Where(filter));
        }

        public async Task<List<TEntity>> SearchListAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbEntitySet.Where(filter).ToListAsync();
        }

        public List<TEntity> SearchList(Expression<Func<TEntity, bool>> filter)
        {
            return _dbEntitySet.Where(filter).ToList();
        }

        public IQueryable<TEntity> SearchQueryable(Expression<Func<TEntity, bool>> filter)
        {
            return _dbEntitySet.Where(filter);
        }

        public int ExecuteNonQueryInt(string commandString, bool isStoredProc = false, params object[] param)
        {
            int result = 0;

            try
            {
                using (SqlConnection con = new SqlConnection((_context as System.Data.Entity.DbContext).Database.Connection.ConnectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = commandString;
                        cmd.CommandType = isStoredProc ? CommandType.StoredProcedure : CommandType.Text;

                        foreach (var parm in param)
                        {
                            cmd.Parameters.Add(parm);
                        }

                        result = cmd.ExecuteNonQuery();
                    }

                    con.Close();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public DataTable ExecuteNonQueryDataTable(string commandString, bool isStoredProc = false, params object[] param)
        {
            DataTable result = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection((_context as System.Data.Entity.DbContext).Database.Connection.ConnectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = commandString;
                        cmd.CommandType = isStoredProc ? CommandType.StoredProcedure : CommandType.Text;

                        foreach (var parm in param)
                        {
                            cmd.Parameters.Add(parm);
                        }

                        using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                        {
                            result.Load(rdr);
                        }
                    }

                    con.Close();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public object ExecuteScalar(string commandString, bool isStoredProc = false, params object[] param)
        {
            object result = null;

            try
            {
                using (SqlConnection con = new SqlConnection((_context as System.Data.Entity.DbContext).Database.Connection.ConnectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = commandString;
                        cmd.CommandType = isStoredProc ? CommandType.StoredProcedure : CommandType.Text;

                        foreach (var parm in param)
                        {
                            cmd.Parameters.Add(parm);
                        }

                        result = cmd.ExecuteScalar();
                    }

                    con.Close();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        #endregion IRepository Interface Implementations (Basic Database Methods)

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
                _context.Dispose();
            }
            _disposed = true;
        }
        #endregion Dispose
    }
}
