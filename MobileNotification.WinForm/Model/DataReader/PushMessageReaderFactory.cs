using MobileNotification.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNotification.WinForm.Model.DataReader
{
   public class PushMessageReaderFactory
    { 
        public static IPushMessageReader<MusteriBildirimMobil> Create(PushMessageReaderType type)
        {
            switch (type)
            {
                case PushMessageReaderType.PerCall:
                    return new EFPushMessageReaderPerCall();
                case PushMessageReaderType.Existing:
                    return new EFPushMessageReader();
                default:
                    throw new NotSupportedException($"{type.ToString()} push message reader type not supported"); 
            }
        }
    }
}
