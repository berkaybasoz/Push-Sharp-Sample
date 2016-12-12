using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNotification.Infra.Model
{
    public class QueueEventArgs<T>
    {
        public T Entry { get; set; }
        public Exception Exception { get; set; }
    }
}
