using System;

namespace MobileNotification.WinForm.Model
{
    public class ConvertHelper
    {
        public const string DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss.ffffff";

        public static string ToString(DateTime? datetime)
        {
            if (datetime.HasValue)
            {
                return datetime.Value.ToString(DATETIME_FORMAT);
            }
            else
            {
                return "";
            }
        }

        public static long ToTimeStamp(byte[] bytes)
        {
            return ToEFTimeStamp(bytes);
            //return BitConverter.ToInt64(bytes, 0);

            // return BitConverter.ToInt64(bytes.Reverse().ToArray(), 0);
        }

        private static Int64 ToEFTimeStamp(byte[] dbVersion)
        {
            Int64 version = 0;
            if (BitConverter.IsLittleEndian)
            {
                byte[] clone = dbVersion.Clone() as byte[];
                Array.Reverse(clone);
                version = BitConverter.ToInt64(clone, 0);
            }
            else
            {
                version = BitConverter.ToInt64(dbVersion, 0);
            }
            return version;

        }
        public static string ToString(object obj)
        {
            if (obj == null)
                return "";
            else return obj.ToString();
        }
    }
}
