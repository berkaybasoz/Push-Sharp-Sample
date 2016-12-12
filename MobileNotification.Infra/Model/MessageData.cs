using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNotification.Infra.Model
{
    public class MessageData
    {
        public string Message { get; set; }
        public string Title { get; set; }

 
        public MessageData(string msg="",string title="")
        {
            Message = msg;
            Title = title;
        }
    }
}
