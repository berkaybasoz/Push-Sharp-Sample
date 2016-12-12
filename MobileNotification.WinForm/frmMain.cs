
using MobileNotification.Infra.Model;
using MobileNotification.Model;
using PushSharp.Core;
using PushSharp.Google;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MobileNotification.DAL.Repo;
using MobileNotification.DAL.UnitOfWork;
using MobileNotification.DAL.Context;
using MobileNotification.DAL.Model;
using System.Threading;
using MobileNotification.WinForm.Model;
using MobileNotification.WinForm.Model.DataReader;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using PushSharp.Apple;

namespace MobileNotification.WinForm
{

    public partial class frmMain : BaseForm
    {
        public const string SUCCESS_MSG_COUNT_STR = "Giden Mesaj Sayısı = {0}";
        public const string ERROR_MSG_COUNT_STR = "Hatalı Mesaj Sayısı = {0}";
        public const string SUBSCRIBER_COUNT_STR = "Abone Sayısı = {0}";

        private int successMsgCount;
        private int errorMsgCount;
        private int subscriberCount;
        private bool isStarted;

        private MyQueue<IMessage> _messageQueue;
        private MessageSenderDictionary _senders;
        private QueueLoadWorker _queueLoader;
        private IPushMessageReader<MusteriBildirimMobil> _messageReader;
        private PushMessageReaderType readerType = PushMessageReaderType.PerCall;
        //IRepository<PushMessage> _pushRepositoryRead;
        //PushContext _dbContextRead;
        //IUnitOfWork _uowRead;

        //IRepository<PushMessage> _pushRepositoryWrite;
        //PushContext _dbContextWrite;
        //IUnitOfWork _uowWrite;

        public frmMain()
        {
            InitializeComponent();
        }

        #region Properties
        private int SuccessMsgCount
        {
            get { return successMsgCount; }
            set
            {
                successMsgCount = value;
                SetText(tssSuccessMsg, String.Format(SUCCESS_MSG_COUNT_STR, value));
            }
        }

        private int ErrorMsgCount
        {
            get { return errorMsgCount; }
            set
            {
                errorMsgCount = value;
                SetText(tssErrorMsg, String.Format(ERROR_MSG_COUNT_STR, value));
            }
        }


        private int SubscriberCount
        {
            get { return subscriberCount; }
            set
            {
                subscriberCount = value;
                SetText(tssUserCount, String.Format(SUBSCRIBER_COUNT_STR, value));
            }
        }

        private bool IsStarted
        {
            get { return isStarted; }
            set
            {
                isStarted = value;
                if (isStarted)
                {
                    SetEnabled(tssStart, false);
                    SetEnabled(tssStop, true);
                }
                else
                {
                    SetEnabled(tssStart, true);
                    SetEnabled(tssStop, false);
                }

            }
        }
        #endregion

        #region Form Events
        private void Form1_Load(object sender, EventArgs e)
        {
            Run(() =>
            {
                //    _dbContextRead = new PushContext();
                //    _uowRead = new EFUnitOfWork(_dbContextRead);
                //    _pushRepositoryRead = _uowRead.GetRepository<PushMessage>();


                OnException += FrmMain_OnException;
                FormClosing += FrmMain_FormClosing;
                WriteInfo($"Bildirim gönderme max hata sayısı {Settings.Instance.AppMaxErrorCount} olarak tanımlıdır.");
                ResetTexts();
                
                //Burasi calisiyor
                //APNS_Send_Single();
            });
        }

       
        public void APNS_Send_Single()
        {
            var succeeded = 0;
            var failed = 0;
            var attempted = 0;

            var config = new ApnsConfiguration(
                ApnsConfiguration.ApnsServerEnvironment.Production, 
                Settings.Instance.ApnsCertificateFile, 
                Settings.Instance.ApnsCertificatePassword);
            var broker = new ApnsServiceBroker(config);
            broker.OnNotificationFailed += (notification, exception) => {
                failed++;
            };
            broker.OnNotificationSucceeded += (notification) => {
                succeeded++;
            };
            broker.Start();

            foreach (var dt in Settings.Instance.ApnsDeviceTokens)
            {
                attempted++;
                broker.QueueNotification(new ApnsNotification
                {
                    DeviceToken = dt,
                    Payload = Newtonsoft.Json.Linq.JObject.Parse("{ \"aps\" : { \"alert\" : \"Hello PushSharp!\" } }")
                });
            }

            //broker.Stop();

          
        }
         
        private void FrmMain_OnException(Action action, Model.Events.EventArgs<Exception> arg)
        {
            Run(() =>
            {
                String fullMethodName = GetMethodName(action);
                Exception ex = arg.Item;

                WriteInfo($"{fullMethodName} methodunda hata oluştu :{ex.ToString()}");
            });
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //_dbContextRead = null;
            //_uowRead.Dispose();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Run(() =>
            {
                if (_senders != null)
                {
                    string title = txtTitle.Text.Trim();
                    string msg = txtMessage.Text.Trim();

                    _senders.Send(new GcmMessage(msg, title, Settings.Instance.GcmRegistrationIds.FirstOrDefault()));

                }
                else
                {
                    WriteInfo("Mesaj göndermeden önce uygulamayı başlatmanız gerekiyor.");
                }
            });
        }

        private void tssStartMenu_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void tssStopMenu_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void temizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clear();
        }

        #endregion

        #region Business Layer Events

        private void _queueLoader_OnError(QueueLoadWorker arg1, Exception arg2)
        {
            Run(() =>
            {
                WriteError(arg2);
            });
        }

        private void _queueLoader_onReading(System.Collections.Generic.IEnumerable<MusteriBildirimMobil> msgs)
        {
            Run(() =>
            {
                changeStatus(msgs.Select(s => s.Id).ToList(), SentStatus.CollectingForSend);

            });
        }

        private void _messageQueue_OnException(object arg1, QueueEventArgs<IMessage> arg2)
        {
            Run(() =>
            {
                WriteError($"Messeque error : Desc={arg2.Exception.ToString()}");


                changeStatus(arg2.Entry.ID, SentStatus.Error);

                //_uowRead.SaveChanges();
            });

        }

        private void _messageQueue_OnDequeue(object arg1, QueueEventArgs<IMessage> arg2)
        {
            Run(() =>
            {
                if (_senders != null)
                {

                    //string title = txtTitle.Text.Trim();
                    //string msg = txtMessage.Text.Trim();

                    //_senders.Send(new GcmMessage(msg, title, Settings.Instance.GcmRegistrationIds.ToList()));

                    _senders.Send(arg2.Entry);
                }
                else
                {
                    WriteInfo("Mesaj göndermeden önce uygulamayı başlatmanız gerekiyor.");
                }
            });
        }

        private void BaseMessageSender_OnSent(object sender, ItemEventArgs<IMessage> arg)
        {
            Run(() =>
            {
                try
                {
                    changeStatus(arg.Item.ID, SentStatus.Sent);
                    //_uowRead.SaveChanges();
                }
                catch (Exception ex)
                {
                    WriteError(ex);
                }
            });
        }

        private void BaseMessageSender_OnSucceed(object sender, ItemEventArgs<IMessage> arg)
        {
            Run(() =>
            {
                ++SuccessMsgCount;
                IMessageSender<IMessage> tmpSender = (IMessageSender<IMessage>)sender;
                IMessage msg = arg.Item;
                // Console.WriteLine("GCM Notification Sent!"); 
                WriteInfo($"{tmpSender.Name} sent : {msg.Message}");
            });
        }

        private void BaseMessageSender_OnException(object sender, ItemEventArgs<Exception> arg)
        {
            Run(() =>
            {
                ++ErrorMsgCount;

                if (ErrorMsgCount >= Settings.Instance.AppMaxErrorCount)
                {
                    WriteError($"Uygulamada tanımlı max hata sayısı { Settings.Instance.AppMaxErrorCount} aşıldığında gönderim durduruluyor");
                    Stop();
                    return;
                }

                IMessageSender<IMessage> tmpSender = (IMessageSender<IMessage>)sender;

                Exception ex = arg.Item;

                // See what kind of exception it was to further diagnose
                if (ex is GcmNotificationException)
                {
                    var notificationException = (GcmNotificationException)ex;

                    // Deal with the failed notification
                    var gcmNotification = notificationException.Notification;
                    var description = notificationException.Description;

                    WriteError($"{tmpSender.Name} Notification Failed: ID={gcmNotification.MessageId}, Desc={description}, Ex:{ex.ToString()}");


                }
                else if (ex is ApnsNotificationException)
                {
                    var notificationException = (ApnsNotificationException)ex;

                    // Deal with the failed notification
                    var appnNotification = notificationException.Notification;
                    var description = notificationException.ErrorStatusCode;

                    WriteError($"{tmpSender.Name} Notification Failed: ID={description  }, Desc={appnNotification.Payload}, Ex:{notificationException.ToString()}");


                }
                else if (ex is GcmMulticastResultException)
                {
                    var multicastException = (GcmMulticastResultException)ex;

                    foreach (var succeededNotification in multicastException.Succeeded)
                    {
                        WriteError($"{tmpSender.Name} Notification Failed: ID={succeededNotification.MessageId}");
                    }

                    foreach (var failedKvp in multicastException.Failed)
                    {
                        var n = failedKvp.Key;
                        var e = failedKvp.Value;

                        WriteError($"{tmpSender.Name} Notification Failed: ID={n.MessageId}, Desc={e.ToString()}");
                    }

                }
                else if (ex is DeviceSubscriptionExpiredException)
                {
                    var expiredException = (DeviceSubscriptionExpiredException)ex;

                    var oldId = expiredException.OldSubscriptionId;
                    var newId = expiredException.NewSubscriptionId;

                    WriteError($"Device RegistrationId Expired: {oldId}, Ex:{expiredException.ToString()}");

                    if (!string.IsNullOrWhiteSpace(newId))
                    {
                        // If this value isn't null, our subscription changed and we should update our database
                        WriteError($"Device RegistrationId Changed To: {newId}");
                    }
                }
                else if (ex is RetryAfterException)
                {
                    var retryException = (RetryAfterException)ex;
                    // If you get rate limited, you should stop sending messages until after the RetryAfterUtc date
                    WriteError($"{tmpSender.Name} Rate Limited, don't send more until after {retryException.RetryAfterUtc}, Ex:{retryException.ToString()}");
                }
                else
                {
                    WriteError($"{tmpSender.Name} Notification Failed for some unknown reason, Ex=" + ex == null ? "" : ex.ToString());
                }


            });
        }

        #endregion

        #region Start / Stop

        private void Start()
        {
            Run(() =>
            {
                WriteInfo("Bildirim gönderici başlatılıyor...");

                ResetTexts();

                _senders = new MessageSenderDictionary();

                if (cbxAndroid.Checked)
                {
                    IMessageSender<IMessage> gcmMsgSender = new GcmMessageSender();

                    if (gcmMsgSender is AbsMessageSender<IMessage>)
                    {
                        ((AbsMessageSender<IMessage>)gcmMsgSender).OnException += BaseMessageSender_OnException;
                        ((AbsMessageSender<IMessage>)gcmMsgSender).OnSucceed += BaseMessageSender_OnSucceed;
                        ((AbsMessageSender<IMessage>)gcmMsgSender).OnSent += BaseMessageSender_OnSent;
                    }
                    _senders.Add(gcmMsgSender.DeviceType, gcmMsgSender);
                }

                if (cbxAppleIOS.Checked)
                {
                    IMessageSender<IMessage> iosMsgSender = new IOSMessageSender();
                    if (iosMsgSender is AbsMessageSender<IMessage>)
                    {
                        ((AbsMessageSender<IMessage>)iosMsgSender).OnException += BaseMessageSender_OnException;
                        ((AbsMessageSender<IMessage>)iosMsgSender).OnSucceed += BaseMessageSender_OnSucceed;
                        ((AbsMessageSender<IMessage>)iosMsgSender).OnSent += BaseMessageSender_OnSent;
                    }
                    _senders.Add(iosMsgSender.DeviceType, iosMsgSender); 
                }
              
                 _senders.Start();

                _messageQueue = new MessageQueue();

                _messageQueue.OnException += _messageQueue_OnException;

                _messageQueue.OnDequeue += _messageQueue_OnDequeue;

                //TestContainerFromConfig();

                //TestContainerFromCode();

                _messageReader = PushMessageReaderFactory.Create(readerType); //_messageReader = new EFPushMessageReader(_pushRepositoryRead);

                _queueLoader = new QueueLoadWorker(_messageQueue, _messageReader);

                _queueLoader.OnReading += _queueLoader_onReading;

                _queueLoader.OnError += _queueLoader_OnError;

                _queueLoader.Start();

                IsStarted = true;

                WriteInfo("Bildirim gönderici başlatıldı.");
            });
        }

        private void Stop()
        {
            Run(() =>
            {
                WriteInfo("Bildirim gönderici durduruluyor...");

                _senders.Stop();

                if (_queueLoader != null)
                    _queueLoader.Stop();

                IsStarted = false;

                WriteInfo("Bildirim gönderici durduruldu.");
            });
        }

        private void Clear()
        {
            ScreenClear();
        }

        #endregion

        public void changeStatus(int id, SentStatus status, string err = "")
        {
            changeStatus(new List<int> { id }, status, err);
        }

        public void changeStatus(List<int> list, SentStatus status, string err = "")
        {
            Run(() =>
            {
                using (PushContext context = ContextFactory<PushContext>.Create())
                {
                    IUnitOfWork uow = new EFUnitOfWork(context);
                    IRepository<MusteriBildirimMobil> repository = uow.GetRepository<MusteriBildirimMobil>();
                    foreach (var id in list)
                    {
                        MusteriBildirimMobil pm = repository.GetById(id);
                        if (pm != null)
                        {
                            pm.GonderimDurumu = (short)status;
                            pm.Hata = err;
                            repository.Update(pm);
                        }
                    }
                    uow.SaveChanges();
                }
            });
        }

        private void ResetTexts()
        {
            ErrorMsgCount = 0;
            SuccessMsgCount = 0;
            SubscriberCount = 0;
            IsStarted = false;
        }
         
        public void ScreenWrite(string msg)
        {
            BeginInvoke(lbxInfo, () =>
            {
                if (lbxInfo.Items.Count > 100)
                {
                    lbxInfo.Items.Clear();
                }

                lbxInfo.Items.Insert(0, msg);
            });
        }
        public void ScreenClear( )
        {
            Run(() =>
            {
                BeginInvoke(lbxInfo, () =>
                {
                    lbxInfo.Items.Clear();
                });
            });
        }

        public void WriteInfo(string msg)
        {
            msg = string.Format("{0} {1}", DateTime.Now.ToString("HH:mm.ss.ffffff"), msg);
            ScreenWrite("Bilgi: " + msg);
        }

        public void WriteError(string msg)
        {
            msg = string.Format("{0} {1}", DateTime.Now.ToString("HH:mm.ss.ffffff"), msg);
            ScreenWrite("Hata:  " + msg);
        }

        public void WriteError(Exception ex)
        {
            string errMsg = ex != null ? ex.ToString() : "";
            WriteError(errMsg);

        }

        private void TestContainerFromCode()
        {
            var container = new UnityContainer();



            container.RegisterType<IRepository<MusteriBildirimMobil>, EFRepository<MusteriBildirimMobil>>();

            container.RegisterType<IPushMessageReader<MusteriBildirimMobil>, EFPushMessageReader>("0", new InjectionProperty("Sil", 3));
            container.RegisterType<IPushMessageReader<MusteriBildirimMobil>, EFPushMessageReader>("0",
                new InjectionProperty("Repository",
                new EFRepository<MusteriBildirimMobil>(new PushContext())));
            container.RegisterType<IPushMessageReader<MusteriBildirimMobil>, EFPushMessageReaderPerCall>("1");


            IPushMessageReader<MusteriBildirimMobil> tmp = container.Resolve<IPushMessageReader<MusteriBildirimMobil>>("0");
        }

        private void TestContainerFromConfig()
        {
            IUnityContainer container = new UnityContainer();
            //UnityConfigurationSection unityConfig = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            //unityConfig.Configure(container);
            container.LoadConfiguration();
            IRepository<MusteriBildirimMobil> tmp = container.Resolve<IRepository<MusteriBildirimMobil>>("0");
        }

        private void cbxAndroid_CheckedChanged(object sender, EventArgs e)
        {

        }
    }


}
