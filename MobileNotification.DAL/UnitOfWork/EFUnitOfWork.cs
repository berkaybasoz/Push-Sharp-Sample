using MobileNotification.DAL.Context;
using MobileNotification.DAL.Initializers;
using MobileNotification.DAL.Repo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNotification.DAL.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private readonly PushContext _dbContext;

        public EFUnitOfWork(PushContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext can not be null");

            Database.SetInitializer<PushContext>(new InitializerIfNotExists());

            _dbContext = dbContext;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new EFRepository<T>(_dbContext);
        }

        public int SaveChanges()
        {
            try
            { 
               return _dbContext.SaveChanges();
            }
            catch (OptimisticConcurrencyException ex)
            {
                //TODO : handle exceptions
                throw;
                //var context = ((IObjectContextAdapter)_dbContext  ).ObjectContext;
                //// Resolve the concurrency conflict by refreshing the 
                //// object context before re-saving changes. 
                //context.Refresh(RefreshMode.ClientWins, orders);

                //// Save changes.
                //context.SaveChanges();
                //Console.WriteLine("OptimisticConcurrencyException "
                //+ "handled and changes saved");
            }
            catch (CommitFailedException ex)
            {
                //TODO : handle exceptions
                return _dbContext.SaveChanges();
            }
            catch (EntityException ex)
            {
                //TODO : handle exceptions
                return _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                //TODO : handle exceptions
                throw;
            }
        }
         
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
