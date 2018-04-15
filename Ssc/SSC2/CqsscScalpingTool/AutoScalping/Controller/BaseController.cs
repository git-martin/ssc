using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using AutoScalping.Models;
using AutoScalping.Util;

namespace AutoScalping.Controller
{
    public class BaseController
    {

        protected static void Login(ref AccountResponse account, GloableConstants.AccountBelongs wherefrom)
        {
            string url = "";
            if (wherefrom == GloableConstants.AccountBelongs.Jiang)
            {
                url = GloableConstants.Jiang.Phone_LoginServiceUrl;
            }else if (wherefrom == GloableConstants.AccountBelongs.Qcw)
            {
                url = GloableConstants.Qcw.Phone_LoginServiceUrl;
            }
            try
            {
                var objUri = new Uri(url);
                HttpWebRequest httpRequest = HttpWebRequest.Create(objUri) as HttpWebRequest;
                httpRequest.Method = "POST";
                httpRequest.Accept = "*/*";
                httpRequest.KeepAlive = true;
                httpRequest.ContentType = "application/x-www-form-urlencoded";
                httpRequest.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 8_0 like Mac OS X) AppleWebKit/600.1.3 (KHTML, like Gecko) Version/8.0 Mobile/12A4345d Safari/600.1.4";
                httpRequest.Headers.Add("Cache-Control", "no-cache");
                httpRequest.Timeout = 10000;
                #region 添加Post 参数  
                StringBuilder builder = new StringBuilder();
                var dic = new Dictionary<string, string>();
                dic.Add("user", account.LoginName);
                dic.Add("password", account.Password);
                int i = 0;
                foreach (var item in dic)
                {
                    if (i > 0)
                        builder.Append("&");
                    builder.AppendFormat("{0}={1}", item.Key, HtmlUtil.UrlEncode(item.Value));
                    i++;
                }
                byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
                httpRequest.ContentLength = data.Length;
                using (Stream reqStream = httpRequest.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                #endregion
                var httpResponse = httpRequest.GetResponse();
                var responseStream = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8);
                var responseJson = responseStream.ReadToEnd();
                responseStream.Close();

                AccountResponse accountTmp = JsonHelper.JsonToObject<AccountResponse>(responseJson);
                accountTmp.LoginName = account.LoginName;
                accountTmp.Password = account.Password;
                accountTmp.ComeFrom = account.ComeFrom;
                account = accountTmp;
                if (account.status == false)
                {
                    account.HasError = true;
                    account.ErrorMessage = account.message;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected static void BettingYiXing(ref BettingSolutinModel bettingData, GloableConstants.AccountBelongs wherefrom,string userToken)
        {
            string url = "";
            if (wherefrom == GloableConstants.AccountBelongs.Jiang)
            {
                url = GloableConstants.Jiang.Phone_CqsscBetServiceUrl;
            }
            else if (wherefrom == GloableConstants.AccountBelongs.Qcw)
            {
                url = GloableConstants.Qcw.Phone_CqsscBetServiceUrl;
            }
            try
            {
                CqsscBettingContent bet = new CqsscBettingContent();
                bet.UserToken = userToken;
                //bet.CodeList = GetCodeListNew("-,-,-,-,234", bet.GameType);
                //bet.GameType = GloableConstants.CqsscGameType.DXDS;
                //bet.CodeList = GetCodeListNew("4,4;5,4", bet.GameType);
                bet.GameType = GloableConstants.CqsscGameType.YiXingDX.ToString();
                bet.CodeList = GetCodeListNew(bettingData.BettingNumStr, bet.GameType);
                bet.TotalMatchCount = bettingData.Zhushu;
                bet.IssuseList = GetIssuseListNew(bettingData.Beishu, bet.TotalMatchCount, bettingData.CurrentIssueStr);
                bet.RedBagMoney = bettingData.MaxRedUse;
                bet.TotalMoney = bettingData.TotalMoney;
                string content = JsonHelper.ObjectToJson(bet);

                Dictionary<string, string> map = new Dictionary<string, string>();
                map.Add("Action", "104");
                map.Add("SourceCode", "102");
                map.Add("MsgId", "123456");
                map.Add("Param", HtmlUtil.UrlEncode(content));
                map.Add("Sign", HtmlUtil.UrlEncode(GetMd5HashStr("caibb123456102104" + content)));

                var objUri = new Uri(url);
                HttpWebRequest httpRequest = HttpWebRequest.Create(objUri) as HttpWebRequest;
                httpRequest.Method = "POST";
                httpRequest.Accept = "*/*";
                httpRequest.KeepAlive = true;
                httpRequest.ContentType = "application/x-www-form-urlencoded";
                httpRequest.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 8_0 like Mac OS X) AppleWebKit/600.1.3 (KHTML, like Gecko) Version/8.0 Mobile/12A4345d Safari/600.1.4";
                httpRequest.Headers.Add("Cache-Control", "no-cache");
                httpRequest.Timeout = 10000;
                #region 添加Post 参数  
                StringBuilder builder = new StringBuilder();
                var dic = map;
                int i = 0;
                foreach (var item in dic)
                {
                    if (i > 0)
                        builder.Append("&");
                    builder.AppendFormat("{0}={1}", item.Key, item.Value);
                    i++;
                }
                byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
                httpRequest.ContentLength = data.Length;
                using (Stream reqStream = httpRequest.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                #endregion
                var httpResponse = httpRequest.GetResponse();
                var responseStream = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8);
                var responseJson = responseStream.ReadToEnd();
                responseStream.Close();
                BettingResponse response = JsonHelper.JsonToObject<BettingResponse>(responseJson);
                if (response.Code == 101)
                {
                    bettingData.IsBettingSucc = true;
                }
                bettingData.ResponseCode = response.Code;
                bettingData.MyMessage = response.Message;
            }
            catch (Exception ex)
            {
                bettingData.IsBettingSucc = false;
                bettingData.MyMessage = "投注异常："+ex.Message;
            }
        }
        protected static string GetMd5HashStr(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        protected static string GetIssuseListNew(int beishu, int zhushu, string issueStr)
        {
            try
            {
                IssueContent content = new IssueContent();
                content.Amount = beishu;
                content.IssuseTotalMoney = beishu * (zhushu * 2);
                content.IssuseNumber = issueStr;
                List<IssueContent> issueList = new List<IssueContent>();
                issueList.Add(content);
                String str = JsonHelper.ObjectToJson(issueList).ToString();
                return str;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return string.Empty;
        }

        protected static String GetCodeListNew(string numbersWithCommer, String gameType)
        {
            try
            {
                CodeContent content = new CodeContent();
                content.GameType = gameType;
                content.AnteCode = numbersWithCommer;
                List<CodeContent> issueList = new List<CodeContent>();
                issueList.Add(content);
                String str = JsonHelper.ObjectToJson(issueList).ToString();
                return str;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return string.Empty;
        }

        public static  PhoneTermInfo GetCurrentCqsscIssue()
        {
            try
            {
                var responseJson = HttpGetUtil.GetResponse(GloableConstants.Jiang.Phone_CurrentIssueServiceUrl);
                return JsonHelper.JsonToObject<List<PhoneTermInfo>>(responseJson).FindLast(a => a.type=="新时时彩");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static  List<AccountResponse> ReaderAccountFile(string dataFilePathWithName, GloableConstants.AccountBelongs flagBelongs)
        {
            List<AccountResponse> lst = new List<AccountResponse>();
            try
            {
                DataTable dt = null;
                if (!File.Exists(dataFilePathWithName))
                {
                    MessageBox.Show("文件不存在", "错误", MessageBoxButtons.OK);
                    return lst;
                }
                TextReader reader = new StreamReader(dataFilePathWithName,Encoding.Default);
                dt = CsvFileReader.Parse(reader, false);
                reader.Close();

                if (dt == null || dt.Rows.Count == 0 || dt.Columns.Count < 2)
                {
                    MessageBox.Show("账号文件中无数据,请重试！", "错误", MessageBoxButtons.OK);
                    return lst;
                }

                foreach (DataRow row in dt.Rows)
                {
                    AccountResponse account = new AccountResponse();
                    account.LoginName = row[0].ToString();
                    account.Password = row[1].ToString();
                    account.ComeFrom = flagBelongs;
                    lst.Add(account);
                }
                return lst;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK);
                return lst;
            }
        }
    }
}
