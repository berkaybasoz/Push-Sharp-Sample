using Newtonsoft.Json;

namespace MobileNotification.Infra.Model
{
    public interface IMessage
    {
        [JsonProperty("id")]
        int ID { get; set; }

        //MessageData Data { get; }
        [JsonProperty("alert")]
        string Message { get; set; }

        [JsonProperty("title")]
        string Title { get; set; }

        [JsonIgnore]
        DeviceType DeviceType   { get; set; }//3 for Android // 4 for Apple

        [JsonIgnore]
        string RegistrationId { get; set; }  
    } 
}
