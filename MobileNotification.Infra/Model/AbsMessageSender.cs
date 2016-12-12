using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNotification.Infra.Model
{
    public abstract  class AbsMessageSender<T> : IMessageSender<T> where T:IMessage
    {
        public event Action<object, ItemEventArgs<Exception>> OnException;
        public event Action<object, ItemEventArgs<T>> OnSucceed;
        public event Action<object, ItemEventArgs<T>> OnSent;

        public abstract string Name { get; }
        public abstract DeviceType DeviceType { get; }
        public abstract void Start();
        public abstract void Send(T message);
        public abstract void Stop();
        public abstract T Create();

        protected void InvokeError(Exception ex)
        {
            if (OnException != null)
                OnException(this, ex);
        }

        protected void InvokeSucceed(T msg)
        {
            if (OnSucceed != null)
                OnSucceed(this, new ItemEventArgs<T>(msg));//interface oldugundan operator overloading ise yaramiyor
        }

       protected void InvokeSent(T msg)
        {
            if (OnSent != null)
                OnSent(this, new ItemEventArgs<T>(msg));//interface oldugundan operator overloading ise yaramiyor
        } 
    }
}
