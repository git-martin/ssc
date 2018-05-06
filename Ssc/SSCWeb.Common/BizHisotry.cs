using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using SSCWeb.Common.Data;
using SSCWeb.Common.Models;

namespace SSCWeb.Common
{
    public static class BizHisotry
    {
        public static List<IssueModel> HisotryCalculateIssues { get; set; }
        static BizHisotry()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            HisotryCalculateIssues = new List<IssueModel>();
            //
        }
        public static void SyncData(int days)
        {
            try
            {
                List<string> dayIssueList = GlobalConstants.DayIssueList();
                for (int i = 0; i <= days; i++)
                {
                    DateTime day = DateTime.Now.AddDays(0 - i);
                    bool allSync = true;
                    List<string> lostIssues = new List<string>();
                    List<string> notSyncIssues = new List<string>();
                    foreach (string issue in dayIssueList)
                    {
                        string issueTmp = day.ToString("yyyyMMdd") + issue;
                        var m = HisotryCalculateIssues.Find(a => a.StrIssue == issueTmp);
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
                                HisotryCalculateIssues.Add(m500);
                            }
                            else if (notSyncIssues.Contains(m500.StrIssue))
                            {
                                //do update both in DB and _currentCalculateIssues
                                m500.sync = 1;
                                int exchangeIndex = HisotryCalculateIssues.FindIndex(a => a.StrIssue == m500.StrIssue);
                                HisotryCalculateIssues[exchangeIndex] = m500;
                            }
                        }
                    }
                }
                HisotryCalculateIssues = HisotryCalculateIssues.OrderByDescending(x => x.Issue).ToList();
            }
            catch (Exception ex)
            {
                BizBase.log.Error("SyncHistoryData Exception", ex);
            }
        }

        public static void SyncTodayData()
        {
            try
            {
                List<string> dayIssueList = GlobalConstants.DayIssueList();
                for (int i = 0; i <= 0; i++)
                {
                    DateTime day = DateTime.Now.AddDays(0 - i);
                    bool allSync = true;
                    List<string> lostIssues = new List<string>();
                    List<string> notSyncIssues = new List<string>();
                    foreach (string issue in dayIssueList)
                    {
                        string issueTmp = day.ToString("yyyyMMdd") + issue;
                        var m = HisotryCalculateIssues.Find(a => a.StrIssue == issueTmp);
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
                        List<IssueModel> models500 = Biz.CurrentCalculateIssues;
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
                                HisotryCalculateIssues.Add(m500);
                            }
                            else if (notSyncIssues.Contains(m500.StrIssue))
                            {
                                //do update both in DB and _currentCalculateIssues
                                m500.sync = 1;
                                int exchangeIndex = HisotryCalculateIssues.FindIndex(a => a.StrIssue == m500.StrIssue);
                                HisotryCalculateIssues[exchangeIndex] = m500;
                            }
                        }
                    }
                }
                HisotryCalculateIssues = HisotryCalculateIssues.OrderByDescending(x => x.Issue).ToList();
            }
            catch (Exception ex)
            {
                BizBase.log.Error("SyncHistoryTodayData Exception", ex);
            }
        }
        public static List<MoniModel> MoniBet(int dayIndex, List<IssueModel> allData)
        {
            var result = new List<MoniModel>();
            if (allData == null)
                allData = new List<IssueModel>();
            List<IssueModel> data  = allData.FindAll(x => x.StrIssue.StartsWith(DateTime.Now.AddDays(0-dayIndex).ToString("yyyyMMdd")));
            if (data.Count == 0)
            {
                return result;
            }
            if (!data[0].Issue.ToString().EndsWith("120"))
            {
                var issue = data[0].Issue + 1;
                data.Insert(0, new IssueModel()
                {
                    OpenCode = "",
                    StrIssue = issue + "",
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
                if (BizBase.NotBetHistory(data,preIndex))
                {
                    result.Add(new MoniModel()
                    {
                        Issue = data[i].Issue,
                        OpenCode = data[i].OpenCode,
                        Start = 2,
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
    }
}