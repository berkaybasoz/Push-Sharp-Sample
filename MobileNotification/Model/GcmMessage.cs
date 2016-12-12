using MobileNotification.Infra.Model;
using MobileNotification.Model;

namespace MobileNotification.Model
{
    public class GcmMessage : IMessage
    {
        public int ID { get; set; }

        public DeviceType DeviceType { get; set; }

        public string RegistrationId { get; set; }

        public string Message { get; set; }

        public string Title { get; set; }

        public GcmMessage(string msg = "", string title = "", string registrationId = "")
        {
            DeviceType = DeviceType.Android;
            Message = msg;
            Title = title;
            RegistrationId = registrationId;
        }

    }
}
