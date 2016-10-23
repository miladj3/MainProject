using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Context
{
    public interface IUnitOfWork : IDisposable
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        Int32 SaveChanges();
        Task<int> SaveChangesAsync();
        void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class;
        void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class;
        IList<T> GetRows<T>(string sql, params object[] parameters) where T : class;
        void ForceDatabaseInitialize();
        Int32 SaveAllChanges(bool invalidateCacheDependencies = true);
        Task<Int32> SaveAllChangesAsync(bool invalidateCacheDependencies = true);
        void AutoDetectChangesEnabled(bool flag = true);
    }
}
