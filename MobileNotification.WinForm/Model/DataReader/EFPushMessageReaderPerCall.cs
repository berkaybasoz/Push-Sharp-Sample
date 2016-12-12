using MobileNotification.DAL.Context;
using MobileNotification.DAL.Model;
using MobileNotification.DAL.Repo;
using MobileNotification.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNotification.WinForm.Model.DataReader
{
    public class EFPushMessageReaderPerCall : IPushMessageReader<MusteriBildirimMobil>
    { 
        public IEnumerable<MusteriBildirimMobil> Read(long timeStamp)
        {
            using (PushContext context =   ContextFactory<PushContext>.Create())
            {
                IUnitOfWork uow = new EFUnitOfWork(context);
                IRepository<MusteriBildirimMobil> repository = uow.GetRepository<MusteriBildirimMobil>();
                return repository.GetAll(w => w.GonderimDurumu == (int)MusteriBildirimMobilDurum.Waiting).ToList();
            }
        }

        public void Start()
        {

        }

        public void Stop()
        {

        }
    }
}
