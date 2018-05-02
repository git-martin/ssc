using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CqsscAnalyse.Common;
using CqsscAnalyse.Controller;
using CqsscAnalyse.Model;
using CqsscAnalyse.Threading;

namespace CqsscAnalyse
{
    public partial class Form1 : Form
    {

        private static List<IssueModel> _currentCalculateIssues = new List<IssueModel>();
        public Form1()
        {
            InitializeComponent();
        }


        private void UpdateInfo(string message)
        {
            this.lblInfo.BeginInvoke(new MethodInvoker(delegate ()
            {
                this.lblInfo.Text = message;
            }));
        }

        private void btnAnalyse_Click(object sender, EventArgs e)
        {
            if (_currentCalculateIssues == null || _currentCalculateIssues.Count < 10)
            {
                MessageBox.Show("请先同步数据！");
                return;
            }
            btnAnalyse.BeginInvoke(
                new MethodInvoker(
                delegate ()
                {
                    btnAnalyse.Text = "计算中，请勿动........";
                    btnAnalyse.Enabled = false;
                }
             ));
            UpdateInfo("计算中，请勿动........");
            this.tabControlCalculate.SelectedTab = tabGe;
            btnAnalyse.BeginInvoke(
              new MethodInvoker(
              delegate ()
              {
                  this.richTextBox10.Text = string.Empty;
                  this.richTextBox1.Text = string.Empty;
                  this.richTextBox100.Text = string.Empty;
                  int minAnalyseIssues = int.Parse(System.Configuration.ConfigurationManager.AppSettings["minIssueCount"]);
                  int maxAnalyseIssues = int.Parse(System.Configuration.ConfigurationManager.AppSettings["maxIssueCount"]);

                  // List<IssueModel> data = LoadData(GlobalConstants.ReadDataDays);

                  var data = _currentCalculateIssues;
                  string msg = string.Format("基于【{0}】期【{1}】分析，结果如下：", data[0].StrIssue, data[0].OpenCode);
                  this.richTextBox10.Text = msg;
                  this.richTextBox1.Text = msg;
                  this.richTextBox100.Text = msg;
                  for (int i = minAnalyseIssues; i <= maxAnalyseIssues; i++)
                  {
                      AnalyseDataGe(data, i);
                      AnalyseDataShi(data, i);
                      AnalyseDataBai(data, i);
                  }
                  this.btnAnalyse.Enabled = true;
                  this.btnAnalyse.Text = "开始分析";
                  UpdateInfo("百、十、个位分析完成！");

                  MoniBet();
                  MoniFrm fm = new MoniFrm();
                  fm.Data = _currentCalculateIssues;
                  fm.ShowDialog();
              }
           ));
            //AnalyseData(data, 10);
        }

        private void MoniBet()
        {
            this.listView1.Items.Clear();
            var data = _currentCalculateIssues;
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

            var totalBetItem = 0;
            var totalBetItemZhong = 0;
            var totalBetItemPrice = 0;
            var totleBetItemGain = 0;
            for (int i = 0; i < data.Count; i++)
            {
                var preIndex = i + 1;
                if(preIndex>data.Count-1)
                    break;
                var preGe = data[preIndex].Ge;
                var preShi = data[preIndex].Shi;

                if (preGe == 0 || preShi == 0 || preGe==preShi)// || Math.Abs(preShi-preGe)>4)
                {
                    var item1 = new ListViewItem();
                    item1.UseItemStyleForSubItems = false;
                    item1.Text = i + "";
                    item1.SubItems.Add(data[i].Issue.ToString());
                    item1.SubItems.Add(data[i].OpenCode);
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                    item1.SubItems[0].ForeColor = Color.Gray;
                    //item.SubItems[1].ForeColor = Color.Blue;
                    //item.SubItems[2].ForeColor = Color.Red;
                    this.listView1.Items.Add(item1);
                    continue;
                }
                   
                var betGe = dicBet[preGe];
                var betShi = dicBet[preShi];
                // 加上前10期数据
                //var ge10 = GetPre10Num(data, i, 5);
                //var shi10 = GetPre10Num(data, i, 4);
                //foreach (var i1 in ge10)
                //{
                //    if(!betGe.Contains(i1)) betGe.Add(i1);
                //}
                //foreach (var i1 in shi10)
                //{
                //    if (!betShi.Contains(i1)) betShi.Add(i1);
                //}

                var currIndex = i;
                var currGe = data[currIndex].Ge;
                var currShi = data[currIndex].Shi;

                var isZhong = betGe.Contains(currGe) && betShi.Contains(currShi);
               
                var betItem = 0;
             

                foreach (var ge in betGe)
                {
                    foreach (var shi in betShi)
                    {
                        if (ge == shi)
                            continue;
                        else
                            betItem++;
                    }
                }
                var betMoeny = betItem*2;
                var gain = 0;
                if (isZhong)
                {
                    gain = 196;
                    totalBetItemZhong++;
                }
                totalBetItem ++;
                totalBetItemPrice += betMoeny;
                totleBetItemGain += gain;

                var item = new ListViewItem();
                item.UseItemStyleForSubItems = false;
                item.Text = i + "";
                item.SubItems.Add(data[i].Issue.ToString());
                item.SubItems.Add(data[i].OpenCode);
                item.SubItems.Add(betShi.IntListToString()+","+betGe.IntListToString());
                item.SubItems.Add(betMoeny + "");
                item.SubItems.Add(isZhong?"中奖196":"");
                item.SubItems[0].ForeColor = Color.Gray;
                //item.SubItems[1].ForeColor = Color.Blue;
                //item.SubItems[2].ForeColor = Color.Red;
                this.listView1.Items.Add(item);
            }
            this.linkLabel1.Text =
                $"总投注金额：{totalBetItemPrice},投注次数：{totalBetItem},中奖次数{totalBetItemZhong}，中奖金额：{totleBetItemGain}";


        }

        private List<int> GetPre10Num(List<IssueModel> data,int currentIndex, int position)
        {
            var count = data.Count;
            var pre10index = currentIndex + 10;
            if (pre10index > count - 1)
                pre10index = count - 1;
            var result = new List<int>();
            for (int i = currentIndex + 1; i <= pre10index; i++)
            {
                if(i>count-1)
                    break;
                if (position == 5)
                {
                    result.Add(data[i].Ge);
                }else if (position == 4)
                {
                    result.Add(data[i].Shi);
                }
            }
            return result;

        } 

        private void AnalyseDataGe(List<IssueModel> data, int comparedIssueCount)
        {
            bool isdebug = System.Configuration.ConfigurationManager.AppSettings["isDebug"].ToLower() == "true";
            bool isshowDetail = System.Configuration.ConfigurationManager.AppSettings["isShowDetail"].ToLower() == "true";
            bool showCount0 = System.Configuration.ConfigurationManager.AppSettings["isShowCount0"].ToLower() == "true";



            ReadyDataModel rm = GetDataAnalyse.GetModelsReadyGe(data, comparedIssueCount);
            if (data == null || data.Count == 0)
            {
                MessageBox.Show("从网上获取数据失败！", "错误", MessageBoxButtons.OK);
                return;
            }
            if (isshowDetail)
            {
                this.richTextBox1.Text += "\r\n";
                this.richTextBox1.Text += string.Format("************************【个位{0}期分析】************************\r\n", comparedIssueCount);
                //this.richTextBox1.Text += " >>>计算中......\r\n";
            }
            DataBeCompareModel baseModel = rm.BeCmpModel;
            int dxAppear = 0;
            int daCount = 0;
            int dsAppear = 0;
            int danCount = 0;
            int zzAppear = 0;
            int zhiCount = 0;
            int l3Appear = 0;
            int lu0Count = 0;
            int lu1Count = 0;
            int lu2Count = 0;
            List<int> zhishuList = new List<int> { 1, 2, 3, 5, 7 };

            long baseIssue = baseModel.beginCompareIssue;
            long baseend = baseModel.endCompareIssue;
            foreach (DataCompareModel cmp in rm.CmpModels)
            {
                if (isdebug)
                {
                    this.richTextBox1.Text += "==============\r\n";
                    this.richTextBox1.Text += "当前期号：" + baseModel.beginCompareIssue + "\r\n";
                    this.richTextBox1.Text += "历史期号：" + cmp.beginCompareIssue + "\r\n";
                    this.richTextBox1.Text += "当前大小：" + baseModel.daxiaoBase + "\r\n";
                    this.richTextBox1.Text += "历史大小：" + cmp.daxiaoCmp + "\r\n";
                    this.richTextBox1.Text += "当前单双：" + baseModel.danshuangBase + "\r\n";
                    this.richTextBox1.Text += "历史单双：" + cmp.danshuangCmp + "\r\n";
                    this.richTextBox1.Text += "当前质合：" + baseModel.zhiheBase + "\r\n";
                    this.richTextBox1.Text += "历史质合：" + cmp.zhiheCmp + "\r\n";
                    this.richTextBox1.Text += "当前3路：" + baseModel.lu3Base + "\r\n";
                    this.richTextBox1.Text += "历史3路：" + cmp.lu3Cmp + "\r\n";
                }
                long cmpedIssue = cmp.beginCompareIssue;
                long nextIssue = cmp.nextIssueData.Issue;
                if (cmp.daxiaoCmp == baseModel.daxiaoBase)
                {
                    dxAppear++;
                    bool isDa = cmp.nextIssueData.Ge > 4;
                    daCount += isDa ? 1 : 0;
                    if (isshowDetail)
                    {
                        this.richTextBox1.Text += string.Format("大小:{0}与历史{1}后{2}期相同,{3}期出【{4}】\r\n",
                        baseIssue, cmpedIssue, comparedIssueCount, nextIssue, isDa ? "大" : "小");
                    }

                }
                if (cmp.danshuangCmp == baseModel.danshuangBase)
                {
                    dsAppear++;
                    bool isDan = cmp.nextIssueData.Ge % 2 == 1;
                    danCount += isDan ? 1 : 0;
                    if (isshowDetail)
                    {
                        this.richTextBox1.Text += string.Format("单双:{0}与历史{1}后{2}期相同,{3}期出【{4}】\r\n",
                       baseIssue, cmpedIssue, comparedIssueCount, nextIssue, isDan ? "单" : "双");
                    }

                }
                if (cmp.zhiheCmp == baseModel.zhiheBase)
                {
                    zzAppear++;
                    bool isZhi = zhishuList.Contains(cmp.nextIssueData.Ge);
                    zhiCount += isZhi ? 1 : 0;
                    if (isshowDetail)
                    {
                        this.richTextBox1.Text += string.Format("质合:{0}与历史{1}后{2}期相同,{3}期出【{4}】\r\n",
                       baseIssue, cmpedIssue, comparedIssueCount, nextIssue, isZhi ? "质" : "合");
                    }

                }
                if (cmp.lu3Cmp == baseModel.lu3Base)
                {
                    l3Appear++;
                    int rest = cmp.nextIssueData.Ge % 3;
                    if (rest == 0)
                    {
                        lu0Count++;
                    }
                    else if (rest == 1)
                    {
                        lu1Count++;
                    }
                    else
                    {
                        lu2Count++;
                    }
                    if (isshowDetail)
                    {
                        this.richTextBox1.Text += string.Format("路数:{0}与历史{1}后{2}期相同,{3}期出【{4}】\r\n",
                        baseIssue, cmpedIssue, comparedIssueCount, nextIssue, rest);
                    }

                }
            }
            this.richTextBox1.Text += "\r\n";
            this.richTextBox1.Text += string.Format("------------******【个位{0}期汇总】******-------------\r\n", comparedIssueCount);
            if (showCount0)
            {
                this.richTextBox1.Text += string.Format("【{0}-{1}】共【{2}】期历史相同对比，共出现\r\n"
              + " - 大小:【{3}】次相同结果，其中下期出【大{4}次占{5}】;\r\n"
              + " - 单双:【{6}】次相同结果，其中下期出【单{7}次占{8}】;\r\n"
              + " - 质合:【{9}】次相同结果，其中下期出【质{10}次占{11}】;\r\n"
              + " - 路数:【{12}】次相同结果，其中下期出0【{13}次占{14}】,1【{15}次占{16}】,2【{17}次占{18}】;\r\n",
              baseIssue, baseend, comparedIssueCount,
              dxAppear, daCount, (daCount * 1.00 / dxAppear).ToString("0.00%"),
              dsAppear, danCount, (danCount * 1.00 / dsAppear).ToString("0.00%"),
              zzAppear, zhiCount, (zhiCount * 1.00 / zzAppear).ToString("0.00%"),
              l3Appear, lu0Count, (lu0Count * 1.00 / l3Appear).ToString("0.00%"),
              lu1Count, (lu1Count * 1.00 / l3Appear).ToString("0.00%"),
              lu2Count, (lu2Count * 1.00 / l3Appear).ToString("0.00%")
              );
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                if ((dxAppear + dsAppear + zzAppear + l3Appear) > 0)
                {
                    sb.AppendFormat("【{0}-{1}】共【{2}】期历史相同对比，共出现\r\n", baseIssue, baseend, comparedIssueCount);
                }

                if (dxAppear > 0)
                {
                    sb.AppendFormat(" - 大小:【{0}】次相同结果，其中下期出【大{1}次占{2}】;\r\n", dxAppear, daCount, (daCount * 1.00 / dxAppear).ToString("0.00%"));
                }
                if (dsAppear > 0)
                {
                    sb.AppendFormat(" - 单双:【{0}】次相同结果，其中下期出【单{1}次占{2}】;\r\n", dsAppear, danCount, (danCount * 1.00 / dsAppear).ToString("0.00%"));
                }
                if (zzAppear > 0)
                {
                    sb.AppendFormat(" - 质合:【{0}】次相同结果，其中下期出【质{1}次占{2}】;\r\n", zzAppear, zhiCount, (zhiCount * 1.00 / zzAppear).ToString("0.00%"));
                }
                if (l3Appear > 0)
                {
                    sb.AppendFormat(" - 路数:【{0}】次相同结果，其中下期出0【{1}次占{2}】,1【{3}次占{4}】,2【{5}次占{6}】;\r\n",
                        l3Appear,
                        lu0Count, (lu0Count * 1.00 / l3Appear).ToString("0.00%"),
                        lu1Count, (lu1Count * 1.00 / l3Appear).ToString("0.00%"),
                        lu2Count, (lu2Count * 1.00 / l3Appear).ToString("0.00%"));
                }
                this.richTextBox1.Text += sb.ToString();
            }

            //this.richTextBox1.Text += "--------***********---------\r\n";
        }


        private void AnalyseDataShi(List<IssueModel> data, int comparedIssueCount)
        {
            bool isdebug = System.Configuration.ConfigurationManager.AppSettings["isDebug"].ToLower() == "true";
            bool isshowDetail = System.Configuration.ConfigurationManager.AppSettings["isShowDetail"].ToLower() == "true";
            bool showCount0 = System.Configuration.ConfigurationManager.AppSettings["isShowCount0"].ToLower() == "true";


            if (isshowDetail)
            {
                this.richTextBox10.Text += "\r\n";
                this.richTextBox10.Text += string.Format("************************【十位{0}期分析】************************\r\n", comparedIssueCount);
                //this.richTextBox1.Text += " >>>计算中......\r\n";
            }
            ReadyDataModel rm = GetDataAnalyse.GetModelsReadyShi(data, comparedIssueCount);
            if (data == null || data.Count == 0)
            {
                MessageBox.Show("从网上获取数据失败！", "错误", MessageBoxButtons.OK);
                return;
            }
            DataBeCompareModel baseModel = rm.BeCmpModel;
            int dxAppear = 0;
            int daCount = 0;
            int dsAppear = 0;
            int danCount = 0;
            int zzAppear = 0;
            int zhiCount = 0;
            int l3Appear = 0;
            int lu0Count = 0;
            int lu1Count = 0;
            int lu2Count = 0;
            List<int> zhishuList = new List<int> { 1, 2, 3, 5, 7 };

            long baseIssue = baseModel.beginCompareIssue;
            long baseend = baseModel.endCompareIssue;
            foreach (DataCompareModel cmp in rm.CmpModels)
            {
                if (isdebug)
                {
                    this.richTextBox10.Text += "==============\r\n";
                    this.richTextBox10.Text += "当前期号：" + baseModel.beginCompareIssue + "\r\n";
                    this.richTextBox10.Text += "历史期号：" + cmp.beginCompareIssue + "\r\n";
                    this.richTextBox10.Text += "当前大小：" + baseModel.daxiaoBase + "\r\n";
                    this.richTextBox10.Text += "历史大小：" + cmp.daxiaoCmp + "\r\n";
                    this.richTextBox10.Text += "当前单双：" + baseModel.danshuangBase + "\r\n";
                    this.richTextBox10.Text += "历史单双：" + cmp.danshuangCmp + "\r\n";
                    this.richTextBox10.Text += "当前质合：" + baseModel.zhiheBase + "\r\n";
                    this.richTextBox10.Text += "历史质合：" + cmp.zhiheCmp + "\r\n";
                    this.richTextBox10.Text += "当前3路：" + baseModel.lu3Base + "\r\n";
                    this.richTextBox10.Text += "历史3路：" + cmp.lu3Cmp + "\r\n";
                }
                long cmpedIssue = cmp.beginCompareIssue;
                long nextIssue = cmp.nextIssueData.Issue;
                if (cmp.daxiaoCmp == baseModel.daxiaoBase)
                {
                    dxAppear++;
                    bool isDa = cmp.nextIssueData.Shi > 4;
                    daCount += isDa ? 1 : 0;
                    if (isshowDetail)
                    {
                        this.richTextBox10.Text += string.Format("大小:{0}与历史{1}后{2}期相同,{3}期出【{4}】\r\n",
                        baseIssue, cmpedIssue, comparedIssueCount, nextIssue, isDa ? "大" : "小");
                    }

                }
                if (cmp.danshuangCmp == baseModel.danshuangBase)
                {
                    dsAppear++;
                    bool isDan = cmp.nextIssueData.Shi % 2 == 1;
                    danCount += isDan ? 1 : 0;
                    if (isshowDetail)
                    {
                        this.richTextBox10.Text += string.Format("单双:{0}与历史{1}后{2}期相同,{3}期出【{4}】\r\n",
                       baseIssue, cmpedIssue, comparedIssueCount, nextIssue, isDan ? "单" : "双");
                    }

                }
                if (cmp.zhiheCmp == baseModel.zhiheBase)
                {
                    zzAppear++;
                    bool isZhi = zhishuList.Contains(cmp.nextIssueData.Shi);
                    zhiCount += isZhi ? 1 : 0;
                    if (isshowDetail)
                    {
                        this.richTextBox10.Text += string.Format("质合:{0}与历史{1}后{2}期相同,{3}期出【{4}】\r\n",
                       baseIssue, cmpedIssue, comparedIssueCount, nextIssue, isZhi ? "质" : "合");
                    }

                }
                if (cmp.lu3Cmp == baseModel.lu3Base)
                {
                    l3Appear++;
                    int rest = cmp.nextIssueData.Shi % 3;
                    if (rest == 0)
                    {
                        lu0Count++;
                    }
                    else if (rest == 1)
                    {
                        lu1Count++;
                    }
                    else
                    {
                        lu2Count++;
                    }
                    if (isshowDetail)
                    {
                        this.richTextBox10.Text += string.Format("路数:{0}与历史{1}后{2}期相同,{3}期出【{4}】\r\n",
                        baseIssue, cmpedIssue, comparedIssueCount, nextIssue, rest);
                    }

                }
            }
            this.richTextBox10.Text += "\r\n";
            this.richTextBox10.Text += string.Format("------------******【十位{0}期汇总】******-------------\r\n", comparedIssueCount);
            if (showCount0)
            {
                this.richTextBox10.Text += string.Format("【{0}-{1}】共【{2}】期历史相同对比，共出现\r\n"
              + " - 大小:【{3}】次相同结果，其中下期出【大{4}次占{5}】;\r\n"
              + " - 单双:【{6}】次相同结果，其中下期出【单{7}次占{8}】;\r\n"
              + " - 质合:【{9}】次相同结果，其中下期出【质{10}次占{11}】;\r\n"
              + " - 路数:【{12}】次相同结果，其中下期出0【{13}次占{14}】,1【{15}次占{16}】,2【{17}次占{18}】;\r\n",
              baseIssue, baseend, comparedIssueCount,
              dxAppear, daCount, (daCount * 1.00 / dxAppear).ToString("0.00%"),
              dsAppear, danCount, (danCount * 1.00 / dsAppear).ToString("0.00%"),
              zzAppear, zhiCount, (zhiCount * 1.00 / zzAppear).ToString("0.00%"),
              l3Appear, lu0Count, (lu0Count * 1.00 / l3Appear).ToString("0.00%"),
              lu1Count, (lu1Count * 1.00 / l3Appear).ToString("0.00%"),
              lu2Count, (lu2Count * 1.00 / l3Appear).ToString("0.00%")
              );
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                if ((dxAppear + dsAppear + zzAppear + l3Appear) > 0)
                {
                    sb.AppendFormat("【{0}-{1}】共【{2}】期历史相同对比，共出现\r\n", baseIssue, baseend, comparedIssueCount);
                }

                if (dxAppear > 0)
                {
                    sb.AppendFormat(" - 大小:【{0}】次相同结果，其中下期出【大{1}次占{2}】;\r\n", dxAppear, daCount, (daCount * 1.00 / dxAppear).ToString("0.00%"));
                }
                if (dsAppear > 0)
                {
                    sb.AppendFormat(" - 单双:【{0}】次相同结果，其中下期出【单{1}次占{2}】;\r\n", dsAppear, danCount, (danCount * 1.00 / dsAppear).ToString("0.00%"));
                }
                if (zzAppear > 0)
                {
                    sb.AppendFormat(" - 质合:【{0}】次相同结果，其中下期出【质{1}次占{2}】;\r\n", zzAppear, zhiCount, (zhiCount * 1.00 / zzAppear).ToString("0.00%"));
                }
                if (l3Appear > 0)
                {
                    sb.AppendFormat(" - 路数:【{0}】次相同结果，其中下期出0【{1}次占{2}】,1【{3}次占{4}】,2【{5}次占{6}】;\r\n",
                        l3Appear,
                        lu0Count, (lu0Count * 1.00 / l3Appear).ToString("0.00%"),
                        lu1Count, (lu1Count * 1.00 / l3Appear).ToString("0.00%"),
                        lu2Count, (lu2Count * 1.00 / l3Appear).ToString("0.00%"));
                }
                this.richTextBox10.Text += sb.ToString();
            }

            //this.richTextBox1.Text += "--------***********---------\r\n";
        }


        private void AnalyseDataBai(List<IssueModel> data, int comparedIssueCount)
        {
            bool isdebug = System.Configuration.ConfigurationManager.AppSettings["isDebug"].ToLower() == "true";
            bool isshowDetail = System.Configuration.ConfigurationManager.AppSettings["isShowDetail"].ToLower() == "true";
            bool showCount0 = System.Configuration.ConfigurationManager.AppSettings["isShowCount0"].ToLower() == "true";


            if (isshowDetail)
            {
                this.richTextBox100.Text += "\r\n";
                this.richTextBox100.Text += string.Format("************************【百位{0}期分析】************************\r\n", comparedIssueCount);
                //this.richTextBox1.Text += " >>>计算中......\r\n";
            }
            ReadyDataModel rm = GetDataAnalyse.GetModelsReadyBai(data, comparedIssueCount);
            if (data == null || data.Count == 0)
            {
                MessageBox.Show("从网上获取数据失败！", "错误", MessageBoxButtons.OK);
                return;
            }
            DataBeCompareModel baseModel = rm.BeCmpModel;
            int dxAppear = 0;
            int daCount = 0;
            int dsAppear = 0;
            int danCount = 0;
            int zzAppear = 0;
            int zhiCount = 0;
            int l3Appear = 0;
            int lu0Count = 0;
            int lu1Count = 0;
            int lu2Count = 0;
            List<int> zhishuList = new List<int> { 1, 2, 3, 5, 7 };

            long baseIssue = baseModel.beginCompareIssue;
            long baseend = baseModel.endCompareIssue;
            foreach (DataCompareModel cmp in rm.CmpModels)
            {
                if (isdebug)
                {
                    this.richTextBox100.Text += "==============\r\n";
                    this.richTextBox100.Text += "当前期号：" + baseModel.beginCompareIssue + "\r\n";
                    this.richTextBox100.Text += "历史期号：" + cmp.beginCompareIssue + "\r\n";
                    this.richTextBox100.Text += "当前大小：" + baseModel.daxiaoBase + "\r\n";
                    this.richTextBox100.Text += "历史大小：" + cmp.daxiaoCmp + "\r\n";
                    this.richTextBox100.Text += "当前单双：" + baseModel.danshuangBase + "\r\n";
                    this.richTextBox100.Text += "历史单双：" + cmp.danshuangCmp + "\r\n";
                    this.richTextBox100.Text += "当前质合：" + baseModel.zhiheBase + "\r\n";
                    this.richTextBox100.Text += "历史质合：" + cmp.zhiheCmp + "\r\n";
                    this.richTextBox100.Text += "当前3路：" + baseModel.lu3Base + "\r\n";
                    this.richTextBox100.Text += "历史3路：" + cmp.lu3Cmp + "\r\n";
                }
                long cmpedIssue = cmp.beginCompareIssue;
                long nextIssue = cmp.nextIssueData.Issue;
                if (cmp.daxiaoCmp == baseModel.daxiaoBase)
                {
                    dxAppear++;
                    bool isDa = cmp.nextIssueData.Bai > 4;
                    daCount += isDa ? 1 : 0;
                    if (isshowDetail)
                    {
                        this.richTextBox100.Text += string.Format("大小:{0}与历史{1}后{2}期相同,{3}期出【{4}】\r\n",
                        baseIssue, cmpedIssue, comparedIssueCount, nextIssue, isDa ? "大" : "小");
                    }

                }
                if (cmp.danshuangCmp == baseModel.danshuangBase)
                {
                    dsAppear++;
                    bool isDan = cmp.nextIssueData.Bai % 2 == 1;
                    danCount += isDan ? 1 : 0;
                    if (isshowDetail)
                    {
                        this.richTextBox100.Text += string.Format("单双:{0}与历史{1}后{2}期相同,{3}期出【{4}】\r\n",
                       baseIssue, cmpedIssue, comparedIssueCount, nextIssue, isDan ? "单" : "双");
                    }

                }
                if (cmp.zhiheCmp == baseModel.zhiheBase)
                {
                    zzAppear++;
                    bool isZhi = zhishuList.Contains(cmp.nextIssueData.Bai);
                    zhiCount += isZhi ? 1 : 0;
                    if (isshowDetail)
                    {
                        this.richTextBox100.Text += string.Format("质合:{0}与历史{1}后{2}期相同,{3}期出【{4}】\r\n",
                       baseIssue, cmpedIssue, comparedIssueCount, nextIssue, isZhi ? "质" : "合");
                    }

                }
                if (cmp.lu3Cmp == baseModel.lu3Base)
                {
                    l3Appear++;
                    int rest = cmp.nextIssueData.Bai % 3;
                    if (rest == 0)
                    {
                        lu0Count++;
                    }
                    else if (rest == 1)
                    {
                        lu1Count++;
                    }
                    else
                    {
                        lu2Count++;
                    }
                    if (isshowDetail)
                    {
                        this.richTextBox100.Text += string.Format("路数:{0}与历史{1}后{2}期相同,{3}期出【{4}】\r\n",
                        baseIssue, cmpedIssue, comparedIssueCount, nextIssue, rest);
                    }

                }
            }
            this.richTextBox100.Text += "\r\n";
            this.richTextBox100.Text += string.Format("------------******【百位{0}期汇总】******-------------\r\n", comparedIssueCount);
            if (showCount0)
            {
                this.richTextBox100.Text += string.Format("【{0}-{1}】共【{2}】期历史相同对比，共出现\r\n"
              + " - 大小:【{3}】次相同结果，其中下期出【大{4}次占{5}】;\r\n"
              + " - 单双:【{6}】次相同结果，其中下期出【单{7}次占{8}】;\r\n"
              + " - 质合:【{9}】次相同结果，其中下期出【质{10}次占{11}】;\r\n"
              + " - 路数:【{12}】次相同结果，其中下期出0【{13}次占{14}】,1【{15}次占{16}】,2【{17}次占{18}】;\r\n",
              baseIssue, baseend, comparedIssueCount,
              dxAppear, daCount, (daCount * 1.00 / dxAppear).ToString("0.00%"),
              dsAppear, danCount, (danCount * 1.00 / dsAppear).ToString("0.00%"),
              zzAppear, zhiCount, (zhiCount * 1.00 / zzAppear).ToString("0.00%"),
              l3Appear, lu0Count, (lu0Count * 1.00 / l3Appear).ToString("0.00%"),
              lu1Count, (lu1Count * 1.00 / l3Appear).ToString("0.00%"),
              lu2Count, (lu2Count * 1.00 / l3Appear).ToString("0.00%")
              );
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                if ((dxAppear + dsAppear + zzAppear + l3Appear) > 0)
                {
                    sb.AppendFormat("【{0}-{1}】共【{2}】期历史相同对比，共出现\r\n", baseIssue, baseend, comparedIssueCount);
                }

                if (dxAppear > 0)
                {
                    sb.AppendFormat(" - 大小:【{0}】次相同结果，其中下期出【大{1}次占{2}】;\r\n", dxAppear, daCount, (daCount * 1.00 / dxAppear).ToString("0.00%"));
                }
                if (dsAppear > 0)
                {
                    sb.AppendFormat(" - 单双:【{0}】次相同结果，其中下期出【单{1}次占{2}】;\r\n", dsAppear, danCount, (danCount * 1.00 / dsAppear).ToString("0.00%"));
                }
                if (zzAppear > 0)
                {
                    sb.AppendFormat(" - 质合:【{0}】次相同结果，其中下期出【质{1}次占{2}】;\r\n", zzAppear, zhiCount, (zhiCount * 1.00 / zzAppear).ToString("0.00%"));
                }
                if (l3Appear > 0)
                {
                    sb.AppendFormat(" - 路数:【{0}】次相同结果，其中下期出0【{1}次占{2}】,1【{3}次占{4}】,2【{5}次占{6}】;\r\n",
                        l3Appear,
                        lu0Count, (lu0Count * 1.00 / l3Appear).ToString("0.00%"),
                        lu1Count, (lu1Count * 1.00 / l3Appear).ToString("0.00%"),
                        lu2Count, (lu2Count * 1.00 / l3Appear).ToString("0.00%"));
                }
                this.richTextBox100.Text += sb.ToString();
            }

            //this.richTextBox1.Text += "--------***********---------\r\n";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //int readDataDays = int.Parse(System.Configuration.ConfigurationManager.AppSettings["getDataDaysBefore"]);
            //List<DataModel> result = LoadData(readDataDays);

            //new DataSaver().SaveData();


            this.listView1.Columns.Add("", 40, HorizontalAlignment.Center);
            this.listView1.Columns.Add("期号", 120, HorizontalAlignment.Right);
            this.listView1.Columns.Add("开奖号码", 120, HorizontalAlignment.Center);
            this.listView1.Columns.Add("二星投注", 120, HorizontalAlignment.Center);
            this.listView1.Columns.Add("投注金额", 120, HorizontalAlignment.Center);
            this.listView1.Columns.Add("中？", 120, HorizontalAlignment.Center);
            this.listView1.GridLines = true;
            this.listView1.View = System.Windows.Forms.View.Details;  //这命令比较重要，否则不能显示。
        }

        private List<IssueModel> LoadData(int days)
        {
            DataReader reader = new DataReader();
            string message = "";
            List<IssueModel> data = reader.ReaderDataFrom500(days, out message);
            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "错误", MessageBoxButtons.OK);
            }
            if (data == null || data.Count == 0)
            {
                return null;
            }
            List<IssueModel> result = new List<IssueModel>();
            int count = data.Count;
            for (int i = 0; i < count; i++)
            {
                result.Add(data[count - 1 - i]);
            }
            this.dataGridView1.DataSource = result;
            this.lblCount.Text = "计算期数：" + result.Count;
            return result;
        }

        private void txtIssue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b') //这是允许输入退格键  
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9')) //这是允许输入0-9数字  
                {
                    e.Handled = true;
                }
            }
        }

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b') //这是允许输入退格键  
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9')) //这是允许输入0-9数字  
                {
                    e.Handled = true;
                }
            }
        }

        private void btnSaveOneIssue_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtIssue.Text.Trim().Length != 11 || this.txtNumber.Text.Trim().Length != 5)
                {
                    MessageBox.Show("手动录入的数据错误，请检测无误后再保存！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.btnSync.Enabled = false;
                this.btnCheck.Enabled = false;
                this.btnSaveOneIssue.Enabled = false;
                this.btnAnalyse.Enabled = false;
                UpdateInfo("保存手动录入的数据中....");
                ThreadFactory.AddTask(new ThreadProxy(SaveOneData));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveOneData()
        {
            try
            {
                IssueModel m = new IssueModel();
                m.StrIssue = this.txtIssue.Text.Trim();
                string code = this.txtNumber.Text.Trim();
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
                    UpdateInfo(msg);
                    if (!_currentCalculateIssues.Exists(a => a.StrIssue == m.StrIssue))
                    {
                        _currentCalculateIssues.Add(m);
                    }
                    else
                    {
                        var index = _currentCalculateIssues.FindIndex(a => a.StrIssue == m.StrIssue);
                        _currentCalculateIssues[index] = m;
                    }
                    ThreadFactory.AddTask(new ThreadProxy(BindGridData));
                    MessageBox.Show(msg);
                }
                else
                {
                    string msg = "保存第" + m.StrIssue + "期数失败，请重试！";
                    UpdateInfo(msg);
                    MessageBox.Show(msg);
                }
            }
            catch (Exception ex)
            {
                UpdateInfo("保存失败");
                MessageBox.Show("保存失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                this.btnSync.BeginInvoke(new MethodInvoker(delegate ()
                {
                    this.btnSync.Enabled = true;
                }));
                this.btnCheck.BeginInvoke(new MethodInvoker(delegate ()
                {
                    this.btnCheck.Enabled = true;
                }));
                this.btnSaveOneIssue.BeginInvoke(new MethodInvoker(delegate ()
                {
                    this.btnSaveOneIssue.Enabled = true;
                }));
                this.btnAnalyse.BeginInvoke(new MethodInvoker(delegate ()
                {
                    this.btnAnalyse.Enabled = true;
                }));
            }
        }

        private void BindGridData()
        {
            _currentCalculateIssues.Sort(new IssueComparerDesc());

            this.lblCount.BeginInvoke(new MethodInvoker(delegate ()
            {
                this.lblCount.Text = "计算期数：" + _currentCalculateIssues.Count;
            }));

            this.dataGridView1.BeginInvoke(new MethodInvoker(delegate ()
            {
                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = new BindingList<IssueModel>(_currentCalculateIssues.GetRange(0, _currentCalculateIssues.Count));
            }));
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSync.Enabled = false;
                this.btnCheck.Enabled = false;
                this.btnSaveOneIssue.Enabled = false;
                this.btnAnalyse.Enabled = false;
                UpdateInfo("从500.com同步开奖数据中....请勿计算");
                ThreadFactory.AddTask(new ThreadProxy(SyncData));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void SyncData()
        {
            try
            {
                _currentCalculateIssues = DataController.ReaderDaysIssue(GlobalConstants.ReadDataDays);
                List<string> dayIssueList = GlobalConstants.DayIssueList();
                for (int i = 0; i <= GlobalConstants.ReadDataDays; i++)
                {
                    DateTime day = DateTime.Now.AddDays(0 - i);
                    UpdateInfo("同步[" + day.ToString("yyyy-MM-dd") + "]开奖数据中....");
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
                            UpdateInfo(msg);
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
                    UpdateInfo("同步[" + day.ToString("yyyy-MM-dd") + "]开奖数据完成....");
                }
                UpdateInfo("同步全部开奖数据完成！");

                ThreadFactory.AddTask(new ThreadProxy(BindGridData));
                this.btnSync.BeginInvoke(new MethodInvoker(delegate ()
                {
                    this.btnSync.Enabled = true;
                }));
                this.btnCheck.BeginInvoke(new MethodInvoker(delegate ()
                {
                    this.btnCheck.Enabled = true;
                }));
                this.btnSaveOneIssue.BeginInvoke(new MethodInvoker(delegate ()
                {
                    this.btnSaveOneIssue.Enabled = true;
                }));
                this.btnAnalyse.BeginInvoke(new MethodInvoker(delegate ()
                {
                    this.btnAnalyse.Enabled = true;
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (_currentCalculateIssues == null || _currentCalculateIssues.Count == 0)
            {
                MessageBox.Show("请先同步数据在检查遗漏的数据！");
                return;
            }
            this.btnSync.Enabled = false;
            this.btnCheck.Enabled = false;
            this.btnSaveOneIssue.Enabled = false;
            this.btnAnalyse.Enabled = false;
            this.tabControlCalculate.SelectedTab = tabCommon;
            this.richTextBoxCommon.Text = "";
            UpdateInfo("检查历史数据是否缺失....请勿计算");
            ThreadFactory.AddTask(new ThreadProxy(CheckMissData));
        }

        private void CheckMissData()
        {
            List<string> fullDayIssueList = GlobalConstants.DayIssueList();
            List<string> todayIssueList = new List<string>();
            var maxIssue = _currentCalculateIssues[0];
            for (int i = 1; i <= GlobalConstants.ReadDataDays; i++)
            {
                DateTime day = DateTime.Now.AddDays(0 - i);
                string msg1 = "检查[" + day.ToString("yyyy-MM-dd") + "]遗失数据中....";
                UpdateInfo(msg1);
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
                        AddCheckInfo(issueTmp + "期数据丢失，请手动收入！");
                        noLost = false;
                    }
                } //end find and compare
                string msg2 = day.ToString("yyyy-MM-dd") + "]遗失数据检查完成！";
                UpdateInfo(msg2);
                //AddCheckInfo(msg2);
            }
            UpdateInfo("检查遗失数据完成！");
            this.btnSync.BeginInvoke(new MethodInvoker(delegate ()
            {
                this.btnSync.Enabled = true;
            }));
            this.btnCheck.BeginInvoke(new MethodInvoker(delegate ()
            {
                this.btnCheck.Enabled = true;
            }));
            this.btnSaveOneIssue.BeginInvoke(new MethodInvoker(delegate ()
            {
                this.btnSaveOneIssue.Enabled = true;
            }));
            this.btnAnalyse.BeginInvoke(new MethodInvoker(delegate ()
            {
                this.btnAnalyse.Enabled = true;
            }));
        }

        private void AddCheckInfo(string msg)
        {
            this.richTextBoxCommon.BeginInvoke(new MethodInvoker(delegate ()
            {
                this.richTextBoxCommon.Text += msg + "\r\n";
            }));
        }
    }
}
