using ErpMongoLog;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ErpMongoLog.App_Code
{
    /// <summary>
    /// Biz 的摘要说明
    /// </summary>
    public static class Biz
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(Biz));
        private static System.Timers.Timer timer = new System.Timers.Timer(60000);
        static Biz()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            timer.Elapsed += OnTimedEvent;
            timer.Enabled = false;

            //
        }

        public static void Start()
        {
            //new MongoLogUtil().SaveTodayLog();
            //return;
            timer.Enabled = true;
            log.Info("Biz started at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            log.Info("Timer triggered at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            if (DateTime.Now.Hour >= 21 || DateTime.Now.Hour <= 9)
            {

                if (DateTime.Now.Minute % 60 == 0)
                {
                    new MongoLogUtil().SaveTodayLog();
                }
            }
            else
            {
                if (DateTime.Now.Minute % 10 == 0)
                {
                    new MongoLogUtil().SaveTodayLog();
                }
            }
        }
    }
}