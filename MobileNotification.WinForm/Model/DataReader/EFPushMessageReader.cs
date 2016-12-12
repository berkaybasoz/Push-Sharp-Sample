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
    public class EFPushMessageReader : IPushMessageReader<MusteriBildirimMobil>
    {
        public IRepository<MusteriBildirimMobil> Repository { get; set; }

        public int Sil { get; set; }

        public IEnumerable<MusteriBildirimMobil> Read(long timeStamp)
        {
            return Repository.GetAll(w => w.GonderimDurumu == (int)MusteriBildirimMobilDurum.Waiting).ToList();
        }

        public void Start()
        {

        }

        public void Stop()
        {

        }
    }
}
