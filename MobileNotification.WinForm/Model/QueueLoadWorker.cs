using MobileNotification.DAL.Model;
using MobileNotification.Infra.Model;
using MobileNotification.WinForm.Model.DataReader;
using MobileNotification.WinForm.Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MobileNotification.WinForm.Model
{
    public class QueueLoadWorker
    {
        public event Action<IEnumerable<MusteriBildirimMobil>> OnReading;
        public event Action<QueueLoadWorker, Exception> OnError;

        Task _task;
        CancellationTokenSource tokenSource;
        TaskCreationOptions options;
        MyQueue<IMessage> _messageQueue;
        IPushMessageReader<MusteriBildirimMobil> _reader;

        long timeStamp = 0;
        volatile bool work = true;

        public QueueLoadWorker(MyQueue<IMessage> messageQueue, IPushMessageReader<MusteriBildirimMobil> reader)
        {
            _messageQueue = messageQueue;

            _reader = reader;
        }

        public void Start()
        {
            tokenSource = new CancellationTokenSource();

            options = TaskCreationOptions.LongRunning;

            work = true;

            _task = new Task((s) =>
             {
                 _reader.Start();

                 while (work)
                 {
                     try
                     {

                         List<MusteriBildirimMobil> msgs = _reader.Read(timeStamp).ToList();

                         if (msgs.Count() > 0)
                         {
                             InvokeOnReading(msgs);

                             foreach (MusteriBildirimMobil item in msgs)
                             {
                                 long lastTimeStamp = ConvertHelper.ToTimeStamp(item.TimeStamp);
                                 if (lastTimeStamp > timeStamp)
                                     timeStamp = lastTimeStamp;

                                 IMessage msg = MessageFactory.Create(item.CihazTipi);
                                 MessageBuilder msgBuilder = new MessageBuilder(msg);
                                 _messageQueue.Enqueue(msgBuilder.Build(item));
                             }
                         }

                         Thread.Sleep(5);
                     }
                     catch (Exception ex)
                     {
                         InvokeOnError(ex);
                     }
                 }

             }, null, tokenSource.Token, options);

            _task.Start();
        }

        public void Stop()
        {
            try
            {
                work = false;

                if (tokenSource != null && tokenSource.IsCancellationRequested == false)
                    tokenSource.Cancel();
                 
                if (_reader != null)
                {
                    _reader.Stop();
                }

            }
            catch (Exception ex)
            {
                InvokeOnError(ex);
            }
        }

        public void InvokeOnReading(IEnumerable<MusteriBildirimMobil> messages)
        {
            if (OnReading != null)
                OnReading(messages);
        }
        public void InvokeOnError(Exception arg)
        {
            if (OnError != null)
                OnError(this, arg);
        }
    }

}
