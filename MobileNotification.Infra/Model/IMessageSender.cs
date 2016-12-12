using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNotification.Infra.Model
{
    public interface IMessageSender<T> where T : IMessage
    {
        event Action<object, ItemEventArgs<Exception>> OnException;
        event Action<object, ItemEventArgs<T>> OnSucceed;

        string Name { get; }
        DeviceType DeviceType { get; }
        
        void Send(T message);
        void Start();
        void Stop();
        T Create();
    }
}
