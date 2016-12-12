using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNotification.WinForm.Model.DataReader
{
    public interface IPushMessageReader<T>
    {
        void Start();
        IEnumerable<T> Read(long timeStamp);
        void Stop();
      
    }

}
