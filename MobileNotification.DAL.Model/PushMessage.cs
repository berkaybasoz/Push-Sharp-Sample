using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileNotification.DAL.Model
{
    public enum MusteriBildirimMobilDurum
    {
        Waiting = 0,
        CollectingForSend = 1,
        Sent = 2,
        Error = 3
    }

    public class MusteriBildirimMobil
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int MusteriNo { get; set; }

        public short CihazTipi { get; set; }//3 for Android // 4 for Apple

        public string Token { get; set; }

        public string Icerik { get; set; }

        public string Baslik { get; set; }

        public DateTime? EklenmeZamani { get; set; }

        public DateTime? DegismeZamani { get; set; }

        public DateTime? GonderilmeZamani { get; set; }

        /// <summary>
        /// Look MusteriBildirimMobilDurum
        /// </summary>
        public short GonderimDurumu { get; set; }

        public string Hata { get; set; }

        [Timestamp]
        public byte[] TimeStamp { get; set; }

        [NotMapped]
        public long TimeStampDegeri
        {
            get { return ConvertHelper.ToTimeStamp(TimeStamp); }
        }

        /*
        [ForeignKey("Application")]
        public int AppRefID { get; set; }
        public virtual App Application { get; set; }
        */
    }
}