using MobileNotification.Infra.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PushSharp.Core;
using PushSharp.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNotification.Model
{
    public class GcmMessageSender : AbsMessageSender<IMessage>
    {
        private GcmServiceBroker broker;

        public override DeviceType DeviceType
        {
            get
            {
                return DeviceType.Android;
            }
        }

        public override string Name
        {
            get
            {
                return "GcmMessageSender";
            }
        }

        public override IMessage Create()
        {
            return
               new GcmMessage();
        }

        public override void Send(IMessage msg)
        {

            //if (msg.DeviceTypeIds == null || msg.DeviceTypeIds.Contains(((int)DeviceType.Android).ToString()) == false)
            //    return;

            //if (msg is GcmMessage)
            //{
            //GcmMessage gcmMessage = (GcmMessage)msg;
            if (msg.RegistrationId != null)
            {
                 JObject jData = JObject.FromObject(msg); //2016-12-07 berkay  
  
                GcmNotification notification = new GcmNotification
                {
                    RegistrationIds = new List<string> { msg.RegistrationId },
                    Data = jData
                };

                // Queue a notification to send
                //broker.QueueNotification(notification);
                broker.QueueNotification(notification);
                InvokeSent(msg);
            }

            //}

            //        foreach (var regId in Settings.Instance.GcmRegistrationIds)
            //        {
            //            GcmNotification notification = new GcmNotification
            //            {
            //                RegistrationIds = new List<string> {
            //    regId
            //},
            //                Data = JObject.Parse("{ \"somekey\" : \"somevalue\" }")
            //            };
            //            // Queue a notification to send
            //            gcmBroker.QueueNotification(notification);
            //        }
        }


        public override void Start()
        {
            var config = new GcmConfiguration(Settings.Instance.GcmSenderId, Settings.Instance.GcmAuthToken, null);

            // Create a new broker
            broker = new GcmServiceBroker(config);

            // Wire up events
            broker.OnNotificationFailed += (notification, aggregateEx) =>
            {

                aggregateEx.Handle(ex =>
                {

                    InvokeError(ex);

                    // Mark it as handled
                    return false;
                });
            };

            broker.OnNotificationSucceeded += (notification) =>
            {
                InvokeSucceed(new GcmMessage(notification.Data.ToString()));

            };

            // Start the broker
            broker.Start();
        }

        public override void Stop()
        {
            if (broker != null)
            {
                // Stop the broker, wait for it to finish   
                // This isn't done after every message, but after you're
                // done with the broker
                broker.Stop();
            }
        }

    }
}