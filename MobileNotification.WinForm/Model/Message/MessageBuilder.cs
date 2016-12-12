using MobileNotification.DAL.Model;
using MobileNotification.Infra.Model;

namespace MobileNotification.WinForm.Model.Message
{
    internal class MessageBuilder  
    {
        private IMessage _message;

        public MessageBuilder(IMessage message)
        {
            this._message = message; 
        }

        public IMessage Build(MusteriBildirimMobil item)
        {
            _message.ID = item.Id;
            _message.DeviceType = DeviceTypefactory.Create( item.CihazTipi);
            _message.Message = item.Icerik;
            _message.Title = item.Baslik;
            _message.RegistrationId = item.Token; 

            return _message;
        }
    }
}