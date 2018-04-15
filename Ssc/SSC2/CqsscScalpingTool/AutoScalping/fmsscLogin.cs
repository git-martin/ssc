using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using AutoScalping.Controller;
using AutoScalping.Http;
using AutoScalping.Models;
using AutoScalping.Util;

namespace AutoScalping
{
    public partial class fmsscLogin : Form
    {
        public fmsscLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "http://app.jiangapps.com/app/PhoneLogin";
            try
            {
                var objUri = new Uri(url);
                WebRequest httpRequest = HttpWebRequest.Create(objUri);
                httpRequest.Method = "POST";

                httpRequest.ContentType = "application/x-www-form-urlencoded";
                #region 添加Post 参数  
                StringBuilder builder = new StringBuilder();
                var dic = new Dictionary<string, string>();
                dic.Add("user", HtmlUtil.UrlEncode("良善"));
                dic.Add("password", "l1231235");
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
                httpRequest.Timeout = 20000;
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

                //GlobalParams.TimeOutTimes = 0;
                this.richTextBox1.Text = responseJson;
                AccountResponse account = JsonHelper.JsonToObject<AccountResponse>(responseJson);
                GloableParams.CurrentAccount = account;
            }
            catch (Exception ex)
            {
                //GlobalParams.TimeOutTimes++;
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string a="12345678910";
            //this.richTextBox1.Text = this.getMd5HashStr(a);

            //return;
            CqsscBettingContent bet = new CqsscBettingContent();

            //bet.GameType = GloableConstants.CqsscGameType.YiXingDX.ToString();
            //bet.CodeList = this.getCodeListNew("-,-,-,-,234", bet.GameType);

            bet.GameType = GloableConstants.CqsscGameType.DXDS;
            bet.CodeList = this.getCodeListNew("4,4;5,4", bet.GameType);
          
            bet.TotalMatchCount = 2;
            bet.IssuseList = this.getIssuseListNew(1, bet.TotalMatchCount, this.textBox1.Text.Trim());
            bet.RedBagMoney = 0.12f;
            
            bet.TotalMoney = 4;

            string content = JsonHelper.ObjectToJson(bet);

            //this.richTextBox1.Text = content;
            //return;

            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add("Action", "104");
            map.Add("SourceCode", "102");
            map.Add("MsgId", "123456");
            
            map.Add("Param",HtmlUtil.UrlEncode(content) );
            map.Add("Sign", HtmlUtil.UrlEncode(this.getMd5HashStr2("caibb123456102104"+content)));

            this.richTextBox1.Text += JsonHelper.ObjectToJson(map) + "\r\n";



            string url = "http://app.jiangapps.com/LotteryService/Service";
            try
            {
                var objUri = new Uri(url);
                WebRequest httpRequest = HttpWebRequest.Create(objUri);
                httpRequest.Method = "POST";

                httpRequest.ContentType = "application/x-www-form-urlencoded";
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

                //GlobalParams.TimeOutTimes = 0;
                this.richTextBox1.Text += responseJson + "\r\n";
                //AccountResponse account = JsonHelper.JsonToObject<AccountResponse>(responseJson);
                //GloableParams.CurrentAccount = account;
            }
            catch (Exception ex)
            {
                //GlobalParams.TimeOutTimes++;
                this.richTextBox1.Text += ex.Message+ "\r\n";
            }

        }

        /// <summary>
        /// MD5(32位加密)
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <returns>MD5加密后的字符串</returns>
        public  string getMd5HashStr(string str)
        {
            string pwd = string.Empty;

            //实例化一个md5对像
            MD5 md5 = MD5.Create();

            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("x");
            }

            return pwd;
        }

        public string getMd5HashStr2(string input)
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

        public string getMd5HashStr3(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);
            StringBuilder builder = new StringBuilder();
            foreach (byte b in res)
            {
                builder.Append(Convert.ToString(b, 16));
            }
            return builder.ToString();
        }




        public String getIssuseListNew(int beishu, int zhushu, String issueStr)
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

        public String getCodeListNew(string numbersWithCommer, String gameType)
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


        public static String getIssuseList(int beishu, int issuseTotalMoney, String issueStr)
        {
            return "[{\\\"Amount\\\":" + beishu + ",\\\"IssuseTotalMoney\\\":" + issuseTotalMoney + ",\\\"IssuseNumber\\\":\\\"" + issueStr + "\\\"}]";
        }

        private void fmsscLogin_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = BaseController.GetCurrentCqsscIssue().termNo;
            this.textBox1.ReadOnly = true;


            //string  s = "{\"RedBagMoney\":0.0,\"UserToken\":\"6DWRXGFyasmkjaGd/LxomgUtPHG6nZ6vg3u9M1todP/cHHooKS2EoZo+wnRCWYbM8ZJRO4/vWJTeKWoYEpwoew0xsInzoV027D0OIav3uq8=\",\"StopAfterBonus\":true,\"CodeList\":\"[{\"IsDan\":false,\"GameType\":\"DXDS\",\"AnteCode\":\"4,2\",\"MatchId\":\"\"}]\",\"GameType\":\"DXDS\",\"Security\":4,\"BalancePassword\":\"\",\"TotalMatchCount\":1,\"IsRepeat\":false,\"StrCode\":\"37\",\"PlayType\":\"\",\"IssuseList\":\"[{\"Amount\":1,\"IssuseNumber\":\"20161227-114\",\"IssuseTotalMoney\":2.0}]\",\"GameCode\":\"CQSSC\",\"TotalMoney\":2}";
            //s = "caibb123456102104" + s;
            return;
            string s = "";


            using (FileStream fs = new FileStream("d:\\test.txt", FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs, Encoding.Default);
                s = sr.ReadToEnd();
                sr.Close();
            }


            this.richTextBox1.Text = this.getMd5HashStr(s);
            this.richTextBox1.Text += "\r\n";
            this.richTextBox1.Text += "\r\n";
            this.richTextBox1.Text += this.getMd5HashStr2(s);
            this.richTextBox1.Text += "\r\n";
            this.richTextBox1.Text += "\r\n";
            this.richTextBox1.Text += this.getMd5HashStr3(s);

        }
    }
}
