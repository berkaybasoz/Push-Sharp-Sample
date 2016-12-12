using MobileNotification.Infra.Model;
using MobileNotification.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNotification.WinForm.Model.Message
{
    public class MessageFactory
    {
        public static IMessage Create(short deviceType)
        {
            IMessage msg = null;
            switch (deviceType)
            {
                case (short)DeviceType.Android:
                    msg = new GcmMessage();
                    break;
                case (short)DeviceType.AppleIOS:
                    msg = new IOSMessage();
                    break;
                default:
                    throw new NotSupportedException(String.Format("{0} device type not supperted", deviceType));
            }

            return msg;
        }
    }
}
