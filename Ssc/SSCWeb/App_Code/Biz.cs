using SSCWeb.Common;
using SSCWeb.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSCWeb.Common
{
    public static class Biz
    {
        public static List<IssueModel> _currentCalculateIssues = null;
        public static void SyncData()
        {
            try
            {
                _currentCalculateIssues = DataController.ReaderDaysIssue(GlobalConstants.ReadDataDays);
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
                        var m = _currentCalculateIssues.Find(a => a.StrIssue == issueTmp);
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
                                DataController.InsertIssueModel2Db(m500);
                                _currentCalculateIssues.Add(m500);
                            }
                            else if (notSyncIssues.Contains(m500.StrIssue))
                            {
                                //do update both in DB and _currentCalculateIssues
                                m500.sync = 1;
                                DataController.UpdateIssueModel2Db(m500);
                                int exchangeIndex = _currentCalculateIssues.FindIndex(a => a.StrIssue == m500.StrIssue);
                                _currentCalculateIssues[exchangeIndex] = m500;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public static  List<string> CheckMissData()
        {
            var result = new List<string>();
            List<string> fullDayIssueList = GlobalConstants.DayIssueList();
            List<string> todayIssueList = new List<string>();
            var maxIssue = _currentCalculateIssues[0];
            for (int i = 1; i <= GlobalConstants.ReadDataDays; i++)
            {
                DateTime day = DateTime.Now.AddDays(0 - i);
                //AddCheckInfo(msg1);
                bool noLost = true;
                //List<string> lostIssues = new List<string>();
                foreach (string issue in fullDayIssueList)
                {
                    string issueTmp = day.ToString("yyyyMMdd") + issue;
                    var m = _currentCalculateIssues.Find(a => a.StrIssue == issueTmp);
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
                    if (!_currentCalculateIssues.Exists(a => a.StrIssue == m.StrIssue))
                    {
                        _currentCalculateIssues.Add(m);
                    }
                    else
                    {
                        var index = _currentCalculateIssues.FindIndex(a => a.StrIssue == m.StrIssue);
                        _currentCalculateIssues[index] = m;
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
        private static List<MoniModel> MoniBet(int dayIndex, List<IssueModel> Data )
        {
            var result = new List<MoniModel>();
            if (Data == null)
                Data = new List<IssueModel>();
            List<IssueModel> data = null;
            if (dayIndex == 1)
            {
                data = Data.FindAll(x => x.StrIssue.StartsWith(DateTime.Now.ToString("yyyyMMdd")));
            }
            else if (dayIndex == 2)
            {
                data = Data.FindAll(x => x.StrIssue.StartsWith(DateTime.Now.AddDays(-1).ToString("yyyyMMdd")));
            }
            else if (dayIndex == 3)
            {
                data = Data.FindAll(x => x.StrIssue.StartsWith(DateTime.Now.AddDays(-2).ToString("yyyyMMdd")));
            }
            if (data.Count == 0)
            {
                return result;
            }
            var dicBet = new Dictionary<int, List<int>>
            {
                {0,new List<int>() },
                {1,new List<int>() {2,3,4,7,9} },
                {2,new List<int>() {1,3,7,9} },
                {3,new List<int>() {6,9,2,1,8,7} },
                {4,new List<int>() {5,6,1,7,9} },
                {5,new List<int>() {6,4,7,8} },
                {6,new List<int>() {3,9,4,5,0} },
                {7,new List<int>() {2,5,8,9,4,1,3} },
                {8,new List<int>() {5,7,3,4,1} },
                {9,new List<int>() {3,6,1,4,7} }
            };
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
                var preGe = data[preIndex].Ge;
                var preShi = data[preIndex].Shi;

                if (preGe == 0 || preShi == 0 || preGe == preShi)// || Math.Abs(preShi-preGe)>4)
                {
                    result.Add(new MoniModel()
                    {
                        Issue = data[i].Issue,
                        OpenCode = data[i].OpenCode,
                        Start =2,
                    });
                    continue;
                }
                var betGe = dicBet[preGe];
                var betShi = dicBet[preShi];
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