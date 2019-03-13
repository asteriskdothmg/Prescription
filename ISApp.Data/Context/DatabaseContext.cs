using ISApp.Business.Common.Objects;
using ISApp.Core.Logging;
using ISApp.Core.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISApp.Data.Context
{
    public class DatabaseContext : DbContext, IDbContext
    {
        private static readonly object Lock = new object();
        private static string connectionStringName = Globals.ConnectionStringName;

        public DatabaseContext()
            : base(connectionStringName)
        {

        }

        public DatabaseContext(string nameOrConnectionString, ILogger logger)
            : base(nameOrConnectionString)
        {
            if (logger != null)
            {
                Database.Log = logger.Log;
            }            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public static DatabaseContext Create()
        {
            return new DatabaseContext(nameOrConnectionString: connectionStringName, logger: null);
        }

        #region DbSets
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<ApplicationKey>  ApplicationKey { get; set; }
        #endregion DbSets

        #region IDbContext
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void SetAsAdded<TEntity>(TEntity entity) where TEntity : class
        {
            UpdateEntityState(entity, EntityState.Added);
        }

        public void SetAsModified<TEntity>(TEntity entity) where TEntity : class
        {
            UpdateEntityState(entity, EntityState.Modified);
        }

        public void SetAsDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            UpdateEntityState(entity, EntityState.Deleted);
        }

        public int Commit()
        {
            try
            {
                var saveChanges = SaveChanges();
                return saveChanges;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                var saveChangesAsync = await SaveChangesAsync();
                return saveChangesAsync;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void UpdateEntityState<TEntity>(TEntity entity, EntityState entityState) where TEntity : class
        {
            var dbEntityEntry = GetDbEntityEntrySafely(entity);
            dbEntityEntry.State = entityState;
        }

        private DbEntityEntry GetDbEntityEntrySafely<TEntity>(TEntity entity) where TEntity : class
        {
            var dbEntityEntry = Entry<TEntity>(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                Set<TEntity>().Attach(entity);
            }
            return dbEntityEntry;
        }
        #endregion IDbContext
    }
}
