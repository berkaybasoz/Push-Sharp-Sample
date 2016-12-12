using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace MobileNotification.Infra.Model
{
    public class MessageSenderDictionary
    {
        ConcurrentDictionary<string, IMessageSender<IMessage>> senders = new ConcurrentDictionary<string, IMessageSender<IMessage>>();

        public void Add(DeviceType key, IMessageSender<IMessage> value)
        {
            senders.AddOrUpdate(key.ToString(), value, (k, v) => { return value; });
        }

        public bool Remove(DeviceType key, out IMessageSender<IMessage> value)
        { 
            return senders.TryRemove(key.ToString(), out value);
        }

        public void Send(IMessage message)
        {
            IMessageSender<IMessage> sender;
            senders.TryGetValue(message.DeviceType.ToString(), out sender);

            if (sender != null)
                sender.Send(message);

        }

        public void Start()
        {
           senders.Values.ToList().ForEach(f => f.Start());
        }

        public void Stop()
        {
            senders.Values.ToList()
                .ForEach(f => f.Stop());
        }

        public int AndroidDevice
        {
            get
            {
                return ((int)DeviceType.Android);
            }
        }

        public int AppleDevice
        {
            get
            {
                return ((int)DeviceType.AppleIOS);
            }
        }



    }
}
