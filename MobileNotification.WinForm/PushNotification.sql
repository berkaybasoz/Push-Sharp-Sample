  --truncate table [tebdb].[dbo].[PushMessages]
  declare @GcmToken varchar(max)= 'c91G57Yu6Mg:APA91bFqCHlVedtsRiwUKQp_PqoVt6CA5R5XAaPCJiAMPwrKc00m8TCd4Wd57ILZV23r2Wm0ctiudYkeO-m79jXI2KArBaRS0mkAv6TW3Yv7jYYuDmaXSQUWWyqzGawTXKVzqhkYBtEg'

  declare @IosToken varchar(max)= '76121369E7B74FAA82DCCC3ABFAA72B6C6EF96A150FADF4F2DC378F6FCA8F4A5'
  declare @Indx int =(select count(*) from [tebdb].[dbo].[MusteriBildirimMobils] (nolock))
  
  insert into [tebdb].[dbo].[MusteriBildirimMobils]([MusteriNo],[CihazTipi],[Token],[Icerik],[Baslik],[EklenmeZamani],[DegismeZamani],[GonderilmeZamani],[Hata],[GonderimDurumu])
   values( 22208,3,@GcmToken,convert(varchar,@Indx)+' GARAN Fiyat : 9.06','TEB',sysdatetime(),sysdatetime(),sysdatetime(),'',0)

 

 insert into [tebdb].[dbo].[MusteriBildirimMobils]([MusteriNo],[CihazTipi],[Token],[Icerik],[Baslik],[EklenmeZamani],[DegismeZamani],[GonderilmeZamani],[Hata],[GonderimDurumu])
   values( 22208,4,@IosToken,convert(varchar,@Indx)+' GARAN Fiyat : 9.06','TEB',sysdatetime(),sysdatetime(),sysdatetime(),'',0)
   
 /*
 Android json

 {{
  "id": 480,
  "alert": "479 GARAN Fiyat : 9.06",
  "title": "TEB"
}}


IOS json
{{
  "aps": {
    "id": 481,
    "alert": "479 GARAN Fiyat : 9.06",
    "title": "TEB"
  }
}}

 */



