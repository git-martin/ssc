using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using SSCWeb.Common.Data;
using SSCWeb.Common.Models;

namespace SSCWeb.Common
{
    public static class Biz
    {
        public static List<IssueModel> CurrentCalculateIssues { get; set; }
        private static System.Timers.Timer timer = new System.Timers.Timer(60000);
        
        static Biz()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            timer.Elapsed += OnTimedEvent;
            timer.Enabled = false;
            CurrentCalculateIssues = new List<IssueModel>();
            //
        }

        public static void Start()
        {
            //new MongoLogUtil().SaveTodayLog();
            //return;
            timer.Enabled = true;
            BizBase.log.Info("Biz started at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            if (Biz.CurrentCalculateIssues == null || Biz.CurrentCalculateIssues.Count == 0)
                SyncData();

        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            BizBase.log.Info("Timer triggered at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            
            int hour = DateTime.Now.Hour;
            int mins = DateTime.Now.Minute;
            if (10> hour && hour >= 2)
            {
                return;
            }
            if (22> hour && hour >= 10)
            {
                if (mins % 2 == 0)
                {
                    SyncData();
                }
            }
            else
            {
                SyncData();
            }
        }
        public static void SyncData()
        {
            try
            {
                //CurrentCalculateIssues = DataController.ReaderDaysIssue(GlobalConstants.ReadDataDays);
                List<string> dayIssueList = GlobalConstants.DayIssueList();
                for (int i = 0; i <= GlobalConstants.ReadDataDays; i++)
                {
                    DateTime day = DateTime.Now.AddDays(0 - i);
                    bool allSync = true;
                    List<string> lostIssues = new List<string>();
                    List<string> notSyncIssues = new List<string>();
                    foreach (string issue in dayIssueList)
                    {
                        string issueTmp = day.ToString("yyyyMMdd") + issue;
                        var m = CurrentCalculateIssues.Find(a => a.StrIssue == issueTmp);
                        if (m == null)
                        {
                            lostIssues.Add(issueTmp);
                            allSync = false;
                        }
                        else if (m.IsSync == false)
                        {
                            notSyncIssues.Add(issueTmp);
                            allSync = false;
                        }
                    }//end find and compare
                    if (allSync == false)
                    {
                        string msg = "";
                        List<IssueModel> models500 = new DataReader().ReaderDataFrom500(day.ToString("yyyyMMdd"), out msg);
                        if (!string.IsNullOrEmpty(msg))
                        {
                            continue;
                        }
                        foreach (var m500 in models500)
                        {
                            if (lostIssues.Contains(m500.StrIssue))
                            {
                                //do insert both in DB and _currentCalculateIssues
                                m500.sync = 1;
                                //DataController.InsertIssueModel2Db(m500);
                                CurrentCalculateIssues.Add(m500);
                            }
                            else if (notSyncIssues.Contains(m500.StrIssue))
                            {
                                //do update both in DB and _currentCalculateIssues
                                m500.sync = 1;
                                //DataController.UpdateIssueModel2Db(m500);
                                int exchangeIndex = CurrentCalculateIssues.FindIndex(a => a.StrIssue == m500.StrIssue);
                                CurrentCalculateIssues[exchangeIndex] = m500;
                            }
                        }
                    }
                }
                CurrentCalculateIssues = CurrentCalculateIssues.OrderByDescending(x => x.Issue).ToList();
            }
            catch (Exception ex)
            {
               BizBase.log.Error("SyncData Exception" ,ex);
            }
        }

        public static  List<string> CheckMissData()
        {
            var result = new List<string>();
            List<string> fullDayIssueList = GlobalConstants.DayIssueList();
            List<string> todayIssueList = new List<string>();
            var maxIssue = CurrentCalculateIssues[0];
            for (int i = 1; i <= GlobalConstants.ReadDataDays; i++)
            {
                DateTime day = DateTime.Now.AddDays(0 - i);
                //AddCheckInfo(msg1);
                bool noLost = true;
                //List<string> lostIssues = new List<string>();
                foreach (string issue in fullDayIssueList)
                {
                    string issueTmp = day.ToString("yyyyMMdd") + issue;
                    var m = CurrentCalculateIssues.Find(a => a.StrIssue == issueTmp);
                    if (m == null)
                    {
                        //lostIssues.Add(issueTmp);
                        //AddCheckInfo(issueTmp + "期数据丢失，请手动收入！");
                        result.Add(issueTmp);
                        noLost = false;
                    }
                } //end find and compare
            }
            return result;
        }
        public static void SaveOneData(string issue,string code)
        {
            try
            {
                IssueModel m = new IssueModel();
                m.StrIssue = issue;
                m.Wan = int.Parse(code.Substring(0, 1));
                m.Qian = int.Parse(code.Substring(1, 1));
                m.Bai = int.Parse(code.Substring(2, 1));
                m.Shi = int.Parse(code.Substring(3, 1));
                m.Ge = int.Parse(code.Substring(4, 1));
                m.OpenCode = m.Wan + "," + m.Qian + "," + m.Bai + "," + m.Shi + "," + m.Ge;
                m.OpenTime = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss");
                m.sync = 0;

                if (DataController.InsertOrUpdateIssueModel2Db(m))
                {
                    string msg = "保存第" + m.StrIssue + "期数据成功";
                    if (!CurrentCalculateIssues.Exists(a => a.StrIssue == m.StrIssue))
                    {
                        CurrentCalculateIssues.Add(m);
                    }
                    else
                    {
                        var index = CurrentCalculateIssues.FindIndex(a => a.StrIssue == m.StrIssue);
                        CurrentCalculateIssues[index] = m;
                    }
                }
                else
                {
                    string msg = "保存第" + m.StrIssue + "期数失败，请重试！";
                }
            }
            catch (Exception ex)
            {
            }
        }
        public static List<MoniModel> MoniBet(int dayIndex, List<IssueModel> allData)
        {
            var result = new List<MoniModel>();
            if (allData == null)
                allData = new List<IssueModel>();
            List<IssueModel> data = null;
            if (dayIndex == 1)
            {
                data = allData.FindAll(x => x.StrIssue.StartsWith(DateTime.Now.ToString("yyyyMMdd")));
            }
            else if (dayIndex == 2)
            {
                data = allData.FindAll(x => x.StrIssue.StartsWith(DateTime.Now.AddDays(-1).ToString("yyyyMMdd")));
            }
            else if (dayIndex == 3)
            {
                data = allData.FindAll(x => x.StrIssue.StartsWith(DateTime.Now.AddDays(-2).ToString("yyyyMMdd")));
            }
            if (data.Count == 0)
            {
                return result;
            }
            if (!data[0].Issue.ToString().EndsWith("120"))
            {
                var issue = data[0].Issue + 1;
                data.Insert(0,new IssueModel()
                {
                    OpenCode = "",
                    StrIssue = issue+"",
                    sync = 0
                });
            }
            for (int i = 0; i < data.Count; i++)
            {
                var preIndex = i + 1;
                if (preIndex > data.Count - 1)
                {
                    result.Add(new MoniModel()
                    {
                        Issue = data[i].Issue,
                        OpenCode = data[i].OpenCode,
                        Start = 2,
                    });
                    break;
                }
               

                if (BizBase.NotBet(data, preIndex))
                {
                    result.Add(new MoniModel()
                    {
                        Issue = data[i].Issue,
                        OpenCode = data[i].OpenCode,
                        Start =2,
                    });
                    continue;
                }
                var preGe = data[preIndex].Ge;
                var preShi = data[preIndex].Shi;
                var betGe = BizBase.DicBet[preGe];
                var betShi = BizBase.DicBet[preShi];
                var currIndex = i;
                var currGe = data[currIndex].Ge;
                var currShi = data[currIndex].Shi;
                var isZhong = betGe.Contains(currGe) && betShi.Contains(currShi);
                var betItem = 0;
                foreach (var ge in betGe)
                {
                    foreach (var shi in betShi)
                    {
                        //if (ge == shi)
                        //    continue;
                        //else
                            betItem++;
                    }
                }
                result.Add(new MoniModel()
                {
                    Issue = data[i].Issue,
                    OpenCode = data[i].OpenCode,
                    Start = 2,
                    BetShi = betShi,
                    BetGe = betGe,
                    BetZhuCount = betItem,
                    IsZhong = isZhong,
                    IsBeted = true
                });
            }
            return result;
        }

        public static NumAppearModel NoAppear(int count, int dayIndex, List<IssueModel> allData)
        {
            var result = new NumAppearModel();
            if (allData == null)
                allData = new List<IssueModel>();
            List<IssueModel> data = null;
            if (dayIndex == 1)
            {
                data = allData.FindAll(x => x.StrIssue.StartsWith(DateTime.Now.ToString("yyyyMMdd")));
            }
            else if (dayIndex == 2)
            {
                data = allData.FindAll(x => x.StrIssue.StartsWith(DateTime.Now.AddDays(-1).ToString("yyyyMMdd")));
            }
            else if (dayIndex == 3)
            {
                data = allData.FindAll(x => x.StrIssue.StartsWith(DateTime.Now.AddDays(-2).ToString("yyyyMMdd")));
            }
            if (data.Count == 0)
            {
                return result;
            }
            var tm = data.Take(count).ToList();
            for (int i = 0; i < count; i++)
            {
                result.Ge.Add(tm.Count(x=>x.Ge==i));
                result.Shi.Add(tm.Count(x => x.Shi == i));
            }
            return result;
        }
    }
}