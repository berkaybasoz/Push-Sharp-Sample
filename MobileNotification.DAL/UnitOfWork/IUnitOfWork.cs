using System;

namespace MobileNotification.DAL.Repo
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        int SaveChanges();
    }
}
