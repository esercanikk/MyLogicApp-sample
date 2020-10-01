using MyLogicApp_sample.DAL;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MyLogicApp_sample.BLL.Repository
{
    public class RepositoryBase<T, TId> : IDisposable where T : class
    {
        protected internal static MyContext dbContext;
        public static bool IsProxy { get; set; } = true;
        public RepositoryBase()
        {
            if (dbContext != null && dbContext.Database.Connection.State != ConnectionState.Open)
            {
                dbContext = new MyContext();
                dbContext.Database.Connection.Open();
            }
            dbContext = dbContext ?? new MyContext();
        }
        public RepositoryBase(MyContext context)
        {
            dbContext = context;
        }
        public virtual List<T> GetAll()
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                dbContext.Configuration.ProxyCreationEnabled = IsProxy;
                dbContext.Configuration.LazyLoadingEnabled = IsProxy;
                return dbContext.Set<T>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual List<T> GetAll(Func<T, bool> predicate)
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                dbContext.Configuration.ProxyCreationEnabled = IsProxy;
                dbContext.Configuration.LazyLoadingEnabled = IsProxy;
                return dbContext.Set<T>().AsQueryable().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual List<T> GetAllQueryable(Func<T, bool> predicate)
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                dbContext.Configuration.ProxyCreationEnabled = IsProxy;
                dbContext.Configuration.LazyLoadingEnabled = IsProxy;
                return dbContext.Set<T>().AsQueryable().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task<List<T>> GetAllAsync()
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                return await dbContext.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual T GetByID(TId id)
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                return dbContext.Set<T>().Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task<T> GetByIDAsync(TId id)
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                return await dbContext.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual int Insert(T entity)
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                dbContext.Set<T>().Add(entity);
                return dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task<int> InsertAsync(T entity)
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                dbContext.Set<T>().Add(entity);
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual int Delete(T entity)
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                dbContext.Set<T>().Remove(entity);
                return dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task<int> DeleteAsync(T entity)
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                dbContext.Set<T>().Remove(entity);
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual int DeleteByID(TId id)
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                var entity = dbContext.Set<T>().Find(id);
                dbContext.Set<T>().Remove(entity);
                return dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual int Update()
        {
            try
            {
                return dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task<int> UpdateAsync()
        {
            try
            {
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateDB(T model, TId id)
        {
            var entity = GetByID(id);
            dbContext.Entry(entity).CurrentValues.SetValues(model);
            dbContext.Entry(entity).State = EntityState.Modified;
            return dbContext.SaveChanges() != 0;
        }
        public virtual IQueryable<T> Queryable()
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                return dbContext.Set<T>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual ParallelQuery<T> Parallel()
        {
            try
            {
                dbContext = new MyContext();
                return dbContext.Set<T>().AsParallel();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Dispose()
        {
            dbContext.Dispose();
            GC.SuppressFinalize(this);
            dbContext = null;
        }
        public void Dispose(bool disposing = true)
        {
            if (disposing)
            {
                this.Dispose();
            }
        }
    }
}
