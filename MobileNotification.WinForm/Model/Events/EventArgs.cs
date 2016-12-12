using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNotification.WinForm.Model.Events
{ 
    public class EventArgs<T> : EventArgs
    {
        public T Item { get; protected set; }

        public EventArgs(T item)
        {
            Item = item;
        }

        public static implicit operator EventArgs<T>(T item)
        {
            return new EventArgs<T>(item);
        }
    }
}
