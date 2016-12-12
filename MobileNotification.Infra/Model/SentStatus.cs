using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNotification.Infra.Model
{
    public enum SentStatus
    {
        Waiting=0,
        CollectingForSend=1,
        Sent=2,
        Error=3 
    }
}
