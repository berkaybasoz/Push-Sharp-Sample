using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MobileNotification.Infra.Model
{
    public class ItemEventArgs<T> : EventArgs  
    {
        public T Item { get; protected set; }

        public ItemEventArgs(T item)
        {
            Item = item;
        }

        public static implicit operator ItemEventArgs<T>(T item)
        {
            return new ItemEventArgs<T>(item);
        }
    }
}
