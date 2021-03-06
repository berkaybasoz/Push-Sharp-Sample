﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MobileNotification.Infra.Model
{ 
      
    public class MyQueue<T>  //where T : new()
    {
        public event Action<object, QueueEventArgs<T>> OnDequeue;

        public event Action<object, QueueEventArgs<IEnumerable<T>>> OnBulkDequeue;

        public event Action<object, QueueEventArgs<T>> OnException;

        public event Action<object, QueueEventArgs<IEnumerable<T>>> OnBulkException;

        private CancellationTokenSource tokenSource;

        private BlockingCollection<T> QueueDatas;

        private volatile bool shouldIDequeue = true;

        public volatile bool AbortThreadCancelExceptions = true;

        private volatile object lockObject = new object();

        private int count = 0;

        private Task taskDequeue;

        private QueueCreateOptions createOptions = QueueCreateOptions.LongRunning;

        protected MyQueue()
        {
            Init();
        }

        protected MyQueue(QueueCreateOptions options)
        {
            createOptions = options;
            Init();
        }

        protected void Init()
        {
            QueueDatas = new BlockingCollection<T>(new ConcurrentQueue<T>());

            tokenSource = new CancellationTokenSource();

            Start();
        }

        public void Start()
        {
            shouldIDequeue = true;

            lock (lockObject)
            {
                if (taskDequeue == null || taskDequeue.Status == TaskStatus.RanToCompletion || taskDequeue.Status == TaskStatus.Faulted)
                {
                    if (createOptions == QueueCreateOptions.Default)
                    {
                        taskDequeue = new Task(() =>
                        {
                            Dequeue();
                        }, tokenSource.Token);
                    }
                    else
                    {
                        taskDequeue = new Task((s) =>
                        {
                            Dequeue();
                        }, null, tokenSource.Token, TaskCreationOptions.LongRunning);
                    }


                    taskDequeue.Start();
                }
            }
        }

        public void Pause()
        {
            lock (lockObject)
            {
                shouldIDequeue = false;
            }
        }

        public void Reset()
        {
            lock (lockObject)
            {
                Init();
            }
        }

        public void Stop()
        {
            lock (lockObject)
            {
                shouldIDequeue = false;

                try
                {
                    if (tokenSource != null)
                    {
                        tokenSource.Cancel(false);
                        tokenSource.Dispose();
                        tokenSource = null;
                    }
                }
                finally
                {
                    if (QueueDatas != null)
                    {
                        QueueDatas.Dispose();
                        QueueDatas = null;
                    }
                    if (taskDequeue != null)
                    {
                        try
                        {
                            taskDequeue.Dispose();
                        }
                        catch (InvalidOperationException)
                        {
                            //CoreException qEx = new CoreException(this.GetType().Name + " Queue TaskDequeue Dispose() exception occured", ex);
                            //if (OnException != null)
                            //    OnException(this, new QueueEventArgs<T>() { Exception = qEx });
                        }
                        SetCount(0);
                        taskDequeue = null;
                    }
                }
            }
        }

        public int Count
        {
            get { return count; }
        }

        private void IncrementCount()
        {
            Interlocked.Increment(ref count);
        }

        private void DecrementCount()
        {
            Interlocked.Decrement(ref count);
        }

        private void DecrementCount(int decrease)
        {
            lock (lockObject)
            {
                count = count - decrease;
            }
        }
        private void SetCount(int c)
        {
            lock (lockObject)
            {
                count = c;
            }

        }

        public void Enqueue(T entry)
        {
            Add(entry);
        }

        private void Add(T entry)
        {
            try
            {
                if (QueueDatas != null)
                {
                    QueueDatas.Add(entry);

                    IncrementCount();
                }

            }
            catch (Exception ex)
            {
                Exception qEx = new Exception(this.GetType().Name + " Queue Add() exception occured", ex);
                if (OnException != null)
                    OnException(this, new QueueEventArgs<T>() { Entry = entry, Exception = qEx });
            }
        }

        private void Dequeue()
        {
            //taskDequeue = Task.Factory.StartNew(
            //      () =>
            //      {
            Take();
            //      }, tokenSource.Token);
        }

        public void BulkDequeue(int itemCount = 0)
        {
            if (itemCount <= 0)
            {
                if (QueueDatas != null)
                {
                    itemCount = QueueDatas.Count;
                }
                else if (itemCount < 0)
                {
                    itemCount = 0;
                }
            }

            Take(itemCount);
        }



        private void Take()
        {

            while (shouldIDequeue)
            {
                T entry = default(T);

                try
                {

                    entry = QueueDatas.Take(tokenSource.Token);

                    if (entry != null)
                    {
                        if (OnDequeue != null)
                            OnDequeue(this, new QueueEventArgs<T>() { Entry = entry });
                    }

                    DecrementCount();

                }
                catch (OperationCanceledException)
                {
                    DecrementCount();

                    #region MyRegion
                    //CoreException qEx = new CoreException(this.GetType().Name + " Queue OperationCanceledException() occured while queue is taking items", cex);
                    //if (AbortThreadCancelExceptions)
                    //{
                    //    if (OnOperationCancelledException != null)
                    //        OnOperationCancelledException(this, new QueueEventArgs<T>() { Entry = entry, Exception = qEx });
                    //}
                    //else
                    //{
                    //    if (OnException != null)
                    //        OnException(this, new QueueEventArgs<T>() { Entry = entry, Exception = qEx });
                    //}

                    #endregion
                    Stop();
                }
                catch (Exception ex)
                {
                    if (ex is ArgumentNullException && QueueDatas == null)
                        return;

                    DecrementCount();

                    Exception qEx = new Exception(this.GetType().Name + " Queue Take() exception occured", ex);

                    if (OnException != null)
                        OnException(this, new QueueEventArgs<T>() { Entry = entry, Exception = qEx });

                }

            }
        }

        private void Take(int itemCount)
        {

            List<T> entryList = new List<T>();

            try
            {
                int qCount = QueueDatas.Count;

                for (int i = 0; i < qCount; i++)
                {
                    if (itemCount <= i)
                        break;

                    T entry;
                    if (QueueDatas.TryTake(out entry))
                    {
                        entryList.Add(entry);
                    }
                }

                if (OnBulkDequeue != null)
                    OnBulkDequeue(this, new QueueEventArgs<IEnumerable<T>>() { Entry = entryList });

                if (entryList != null)
                {
                    DecrementCount(entryList.Count);
                }
            }
            catch (OperationCanceledException)
            {

                //CoreException qEx = new CoreException(this.GetType().Name + " Queue OnBulkOperationCancelledException() occured while queue is taking items", cex);
                //if (AbortThreadCancelExceptions)
                //{
                //    if (OnBulkOperationCancelledException != null)
                //        OnBulkOperationCancelledException(this, new QueueEventArgs<IEnumerable<T>>() { Entry = entryList, Exception = qEx });
                //}
                //else
                //{
                //    if (OnBulkException != null)
                //        OnBulkException(this, new QueueEventArgs<IEnumerable<T>>() { Entry = entryList, Exception = qEx });
                //}


                //if (entryList != null)
                //{
                //    DecrementCount(entryList.Count);
                //}
                if (entryList != null)
                {
                    DecrementCount(entryList.Count);
                }

                Stop();

            }
            catch (Exception ex)
            {
                Exception qEx = new Exception(this.GetType().Name + " Queue Take(int) unknown exception occured", ex);

                if (OnBulkException != null)
                    OnBulkException(this, new QueueEventArgs<IEnumerable<T>>() { Entry = entryList, Exception = qEx });

                if (entryList != null)
                {
                    DecrementCount(entryList.Count);
                }
            }


        }

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {

            if (disposing == true)
            {
                ReleaseManagedResources();
            }

            ReleaseUnmangedResources();

        }

        private void ReleaseManagedResources()
        {
            Stop();
        }

        private void ReleaseUnmangedResources()
        {
            Stop();
        }

        ~MyQueue()
        {
            Dispose(false);
        }

        #endregion



    }
}
