using System;

namespace MobileNotification.Infra.Model
{
    public interface IAbsMessageSender<T> where T : IMessage
    {
        string Name { get; }

        event Action<object, ItemEventArgs<Exception>> OnException;
        event Action<object, ItemEventArgs<T>> OnSucceed;

        void Send(T message);
        void Start();
        void Stop();
    }
}