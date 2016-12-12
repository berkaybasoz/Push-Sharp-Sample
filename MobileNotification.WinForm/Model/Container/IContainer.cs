using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNotification.WinForm.Model.Container
{
    public interface IContainer<T, K>
    {
        void Register(T t, K k);
        void Unregister(T t, K k);

        K Resolve(T t);
        K Resolve(T t, string key);

    }

    public class UnityContainer<T, K> : IContainer<T, K>
    {
        UnityContainer myContainer;

        public UnityContainer()
        {
            myContainer = new UnityContainer();
        }

        public void Register(T t, K k)
        {
            throw new NotImplementedException();
        }

        public K Resolve(T t)
        {
            throw new NotImplementedException();
        }

        public K Resolve(T t, string key)
        {
            throw new NotImplementedException();
        }

        public void Unregister(T t, K k)
        {
            throw new NotImplementedException();
        }
    }

    public class ItemFactory
    {

    }
}
