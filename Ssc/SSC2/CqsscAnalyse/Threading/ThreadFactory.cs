using System.Threading;

namespace CqsscAnalyse.Threading
{
    public static class ThreadFactory
    {
        static ThreadFactory()
        {
            ThreadPool.SetMaxThreads(150, 20);
        }

        public static bool AddTask(ThreadProxy param)
        {
            return ThreadPool.QueueUserWorkItem(new WaitCallback(DoWork), param);
        }

        static void DoWork(object param)
        {
            ThreadProxy task = param as ThreadProxy;
            task.DoWork();
        }
    }
}
