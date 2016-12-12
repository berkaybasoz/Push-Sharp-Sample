using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNotification.Infra.Model
{
    public class DeviceTypefactory
    {
        public static DeviceType Create(short type)
        {
            switch (type)
            {
                case ((short)DeviceType.Android):
                    return DeviceType.Android;
                case ((short)DeviceType.AppleIOS):
                    return DeviceType.AppleIOS;
                default:
                    throw new NotSupportedException(String.Format("Device type {0} not supported",type));
            }

        }
    }
}
