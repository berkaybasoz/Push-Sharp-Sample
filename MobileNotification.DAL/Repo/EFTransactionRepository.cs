using MobileNotification.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MobileNotification.DAL.Repo
{
    public class EFTransactionRepository<T> : IRepository<T> where T : class
    {
        private readonly PushContext _dbContext;
        private readonly DbSet<T> _dbSet;
        IsolationLevel level = IsolationLevel.Serializable;


        public EFTransactionRepository(PushContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException($"{nameof(dbContext)} can not be null");

            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public K RunWithTransaction<K>(Func<K> func)
        {
            using (_dbContext.Database.BeginTransaction(level))
            {
                return func();
            }
        }

        public void RunWithTransaction (Action  func)
        {
            using (_dbContext.Database.BeginTransaction(level))
            {
                 func();
            }
        }

        public IQueryable<T> GetAll()
        {
            return RunWithTransaction<IQueryable<T>>(() => { return _dbSet;  });
        }

      
        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        { 
                return RunWithTransaction<IQueryable<T>>( ()=> { return _dbSet.Where(predicate); });
             
        }

        public T GetById(int id)
        {
            return RunWithTransaction<T>(() => { return _dbSet.Find(id); });
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return RunWithTransaction<T>(() => { return _dbSet.Find(predicate); }); 
        }

        public void Add(T entity)
        {
            RunWithTransaction (() => { return _dbSet.Add(entity); });
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null)
                return;

           
            RunWithTransaction(() => { Delete(entity); });
        }

        public void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = _dbContext.Entry(entity);

            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            }
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

        }
    }
}
