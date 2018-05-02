using System;
using System.Collections.Generic;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace SSC
{
    public class DataReader
    {
        public List<IssueModel> ReaderDataFrom500(int daysBefore, out string message)
        {
            message = string.Empty;
            List<IssueModel> result = new List<IssueModel>();
            String urlFm = "http://kaijiang.500.com/static/public/ssc/xml/qihaoxml/{0}.xml";
            int firstDay = daysBefore - 1;
            for (int i = 0; i < daysBefore; i++)
            {
                XmlDocument doc = new XmlDocument();
                DateTime issueDate = DateTime.Now.AddDays(0 - firstDay);
                string url = string.Format(urlFm, issueDate.ToString("yyyyMMdd"));
                try
                {
                    doc.Load(url);
                }
                catch
                {
                    message = "从500万" + url + "获取数据失败！";
                    break;
                }
                XmlElement rootElem = doc.DocumentElement;   //获取根节点  
                XmlNodeList rows = rootElem.GetElementsByTagName("row"); //获取row子节点集合  
                foreach (XmlNode row in rows)
                {
                    IssueModel dm = new IssueModel();
                    dm.StrIssue = ((XmlElement)row).GetAttribute("expect");   //获取期次属性值
                    bool has = false;
                    foreach (IssueModel m in result)
                    {
                        if (m.StrIssue == dm.StrIssue)
                        {
                            has = true;
                            break;
                        }
                    }
                    if (has)
                    {
                        continue;
                    }

                    dm.OpenCode = ((XmlElement)row).GetAttribute("opencode");   //获取开奖号码 1,2,3,4,5
                    dm.OpenTime = ((XmlElement)row).GetAttribute("opentime");
                    if (!string.IsNullOrEmpty(dm.StrIssue) && !string.IsNullOrEmpty(dm.OpenCode))
                    {
                        string[] tm = dm.OpenCode.Split(',');
                        dm.Wan = int.Parse(tm[0]);
                        dm.Qian = int.Parse(tm[1]);
                        dm.Bai = int.Parse(tm[2]);
                        dm.Shi = int.Parse(tm[3]);
                        dm.Ge = int.Parse(tm[4]);
                        result.Add(dm);
                    }
                }
                firstDay--;
            }
            result.Sort(new IssueComparer());
            return result;
        }


        public List<IssueModel> ReaderDataFrom500(string date, out string message)
        {
            message = string.Empty;
            List<IssueModel> result = new List<IssueModel>();
            String urlFm = "http://kaijiang.500.com/static/public/ssc/xml/qihaoxml/{0}.xml";

            XmlDocument doc = new XmlDocument();
            string url = string.Format(urlFm, date);
            try
            {
                doc.Load(url);
            }
            catch
            {
                try
                {
                    System.Threading.Thread.Sleep(1000);
                    doc.Load(url);
                }
                catch
                {
                    message = "从500万获取[" + date + "]日数据失败！";
                }

            }
            XmlElement rootElem = doc.DocumentElement;   //获取根节点  
            XmlNodeList rows = rootElem.GetElementsByTagName("row"); //获取row子节点集合  
            foreach (XmlNode row in rows)
            {
                IssueModel dm = new IssueModel();
                dm.StrIssue = ((XmlElement)row).GetAttribute("expect");   //获取期次属性值
                dm.OpenCode = ((XmlElement)row).GetAttribute("opencode");   //获取开奖号码 1,2,3,4,5
                dm.OpenTime = ((XmlElement)row).GetAttribute("opentime");
                if (!string.IsNullOrEmpty(dm.StrIssue) && !string.IsNullOrEmpty(dm.OpenCode))
                {
                    string[] tm = dm.OpenCode.Split(',');
                    dm.Wan = int.Parse(tm[0]);
                    dm.Qian = int.Parse(tm[1]);
                    dm.Bai = int.Parse(tm[2]);
                    dm.Shi = int.Parse(tm[3]);
                    dm.Ge = int.Parse(tm[4]);
                    result.Add(dm);
                }
            }
            result.Sort(new IssueComparer());
            return result;
        }

        public List<IssueModel> ReaderDataFromSSCCN(int daysBefore, out string message)
        {
            message = string.Empty;
            List<IssueModel> result = new List<IssueModel>();
            String urlFm = "http://data.shishicai.cn/handler/kuaikai/data.ashx";
            int firstDay = daysBefore - 1;
            for (int i = 0; i < daysBefore; i++)
            {
                string postData = String.Format("'lottery':4,'date':{0}", DateTime.Now.AddDays(0 - firstDay).ToString("yyyy-MM-dd"));
                try
                {
                    string response = Utils.HttpPost(urlFm, postData);
                    JObject json = JObject.Parse(response);
                    json.Last.Remove();

                }
                catch (Exception ex)
                {
                    message = "从时时彩网" + urlFm + "获取数据失败！";
                    break;
                }
                XmlDocument doc = new XmlDocument();
                XmlElement rootElem = doc.DocumentElement;   //获取根节点  
                XmlNodeList rows = rootElem.GetElementsByTagName("row"); //获取row子节点集合  
                foreach (XmlNode row in rows)
                {
                    IssueModel dm = new IssueModel();
                    dm.StrIssue = ((XmlElement)row).GetAttribute("expect");   //获取期次属性值
                    bool has = false;
                    foreach (IssueModel m in result)
                    {
                        if (m.StrIssue == dm.StrIssue)
                        {
                            has = true;
                            break;
                        }
                    }
                    if (has)
                    {
                        continue;
                    }

                    dm.OpenCode = ((XmlElement)row).GetAttribute("opencode");   //获取开奖号码 1,2,3,4,5
                    dm.OpenTime = ((XmlElement)row).GetAttribute("opentime");
                    if (!string.IsNullOrEmpty(dm.StrIssue) && !string.IsNullOrEmpty(dm.OpenCode))
                    {
                        string[] tm = dm.OpenCode.Split(',');
                        dm.Wan = int.Parse(tm[0]);
                        dm.Qian = int.Parse(tm[1]);
                        dm.Bai = int.Parse(tm[2]);
                        dm.Shi = int.Parse(tm[3]);
                        dm.Ge = int.Parse(tm[4]);
                        result.Add(dm);
                    }
                }
                firstDay--;
            }
            return result;
        }
    }
}