using MobileNotification.Infra.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNotification.Model
{
    public class IOSMessage : IMessage
    {
        public int ID { get; set; }

        public DeviceType DeviceType { get; set; }

        public string RegistrationId { get; set; }

        public string Message { get; set; }

        public string Title { get; set; }

        public IOSMessage(string msg = "", string title = "", string registrationId = "")
        {
            DeviceType = DeviceType.AppleIOS;
            Message = msg;
            Title = title;
            RegistrationId = registrationId;
        }

    }
}
