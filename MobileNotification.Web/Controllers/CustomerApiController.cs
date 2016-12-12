using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.IO;

namespace MobileNotification.Web.Controllers
{
    public class CustomerApiController : BaseApiController
    {
        public HttpResponseMessage getCustomerChoices()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "asd");
        }
    }
}