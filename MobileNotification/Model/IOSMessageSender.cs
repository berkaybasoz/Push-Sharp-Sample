using MobileNotification.Infra.Model;
using Newtonsoft.Json.Linq;
using PushSharp.Apple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNotification.Model
{
    public class IOSMessageSender : AbsMessageSender<IMessage>
    {
        private ApnsServiceBroker broker;

        public override DeviceType DeviceType
        {
            get
            {
                return DeviceType.AppleIOS;
            }
        }

        public override string Name
        {
            get
            {
                return "IOSMessageSender";
            }
        }

        public override IMessage Create()
        {
            return
               new IOSMessage();
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
                //JObject jData = JObject.FromObject(msg); //Data = JObject.FromObject(message.Data) 

                JObject jData = JObject.FromObject(new
                {
                    aps = new
                    {
                        id = msg.ID,
                        alert = msg.Message,
                        title = msg.Title
                    }
                });

                ApnsNotification notification = new ApnsNotification 
                {
                    DeviceToken = msg.RegistrationId,
                    Payload = jData
                } ;
                 
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

            var config = new ApnsConfiguration(
                ApnsConfiguration.ApnsServerEnvironment.Production,
                Settings.Instance.ApnsCertificateFile,
                Settings.Instance.ApnsCertificatePassword);

            // Create a new broker
            broker = new ApnsServiceBroker(config);

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
                InvokeSucceed(new IOSMessage(notification.Payload.ToString()));

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
