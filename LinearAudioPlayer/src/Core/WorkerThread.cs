using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace FINALSTREAM.LinearAudioPlayer.Core
{
    /// <summary>
    /// バックグラウンドでずーっと動き続けるスレッド
    /// </summary>
    public class WorkerThread
    {
        private Thread workerthread = null;
        private object mutex = new object();
        private Queue<Action> taskQueue = null;
        private bool isTaskComplete = false;
        
        public WorkerThread()
        {
            workerthread  = new Thread(WorkerProc);
            taskQueue = new Queue<Action>();
            workerthread.Start();
        }

        public bool IsBusy()
        {
            bool result = false;
            lock (mutex)
            {
                if (taskQueue.Count > 0 && isTaskComplete)
                {
                    result = true;
                }

            }
            return result;
        }

        /// <summary>
        /// 別スレッドで実行するタスクとしてキューイングする
        /// </summary>
        /// <param name="a"></param>
        /// <param name="arg"></param>
        public void EnqueueTask(Action a)
        {
            lock (mutex)
            {
                taskQueue.Enqueue(a);
                Debug.WriteLine("Enque Task:" + DateTime.Now.ToString("yyyy/MM/dd hh:mmm:ss:fff"));
            }
        }

        void WorkerProc()
        {
            //try
            //{
                while (true)
                {
                    
                        if (taskQueue.Count > 0)
                        {
                            // 無限ループ
                            lock (mutex)
                            {
                                isTaskComplete = false;

                                var action = taskQueue.Dequeue();
                                action();
                                Debug.WriteLine("Complete Task:" + DateTime.Now.ToString("yyyy/MM/dd hh:mmm:ss:fff"));
                                isTaskComplete = true;
                            }

                        }

                    Thread.Sleep(10);
                }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }


        public void Kill()
        {
            workerthread.Abort();
        }
    }
}
