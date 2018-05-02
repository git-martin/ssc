using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CqsscAnalyse.Common;
using CqsscAnalyse.Model;

namespace CqsscAnalyse
{
    public partial class MoniFrm : Form
    {

        public List<IssueModel> Data { get; set; }

        private Dictionary<int, List<int>>  dicBet = new Dictionary<int, List<int>>
            {
                {0,new List<int>() },
                {1,new List<int>() {2,3,4,7,9} },
                {2,new List<int>() {1,3,4,7,9} },
                {3,new List<int>() {6,9,2,1,8,7} },
                {4,new List<int>() {5,6,1,7,8,9} },
                {5,new List<int>() {6,4,7,8} },
                {6,new List<int>() {3,9,4,5,8} },
                {7,new List<int>() {5,8,9,4,1,0} },
                {8,new List<int>() {5,7,3,4,1} },
                {9,new List<int>() {3,6,1,4,7} }
            };
        public MoniFrm()
        {
            InitializeComponent();
        }

        private void MoniFrm_Load(object sender, EventArgs e)
        {
            InitLv(this.lvToday);
            InitLv(this.lvYestoday);
            InitLv(this.lvBefore);

            MockBet2(this.lvToday,linkLabel1,1);
            MockBet2(this.lvYestoday, linkLabel2, 2);
            MockBet2(this.lvBefore, linkLabel3, 3);

        }

        private void InitLv(ListView lv)
        {
            lv.Columns.Add("", 28, HorizontalAlignment.Center);
            lv.Columns.Add("期号", 80, HorizontalAlignment.Center);
            lv.Columns.Add("开奖号码", 68, HorizontalAlignment.Center);
            lv.Columns.Add("二星投注", 100, HorizontalAlignment.Left);
            lv.Columns.Add("投￥", 50, HorizontalAlignment.Center);
            lv.Columns.Add("中￥", 50, HorizontalAlignment.Center);
            lv.GridLines = true;
            lv.View = System.Windows.Forms.View.Details;  //这命令比较重要，否则不能显示。
        }

        private void MockBet(ListView lv, LinkLabel lnkLable, int dayIndex)
        {
            lv.Items.Clear();
            if(Data == null)
                Data =new List<IssueModel>();
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
                lnkLable.Text = @"无数据！请先获取数据";
                return;
            }
            var totalBetItem = 0;
            var totalBetItemZhong = 0;
            var totalBetItemPrice = 0;
            var totleBetItemGain = 0;
            for (int i = 0; i < data.Count; i++)
            {
                var preIndex = i + 1;
                if (preIndex > data.Count - 1)
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
                    lv.Items.Add(item1);
                    break;
                }
                var preGe = data[preIndex].Ge;
                var preShi = data[preIndex].Shi;

                if (preGe == 0 || preShi == 0 || preGe == preShi)// || Math.Abs(preShi-preGe)>4)
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
                   lv.Items.Add(item1);
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
                var betMoeny = betItem * 2;
                var gain = 0;
                if (isZhong)
                {
                    gain = 195;
                    totalBetItemZhong++;
                }
                totalBetItem++;
                totalBetItemPrice += betMoeny;
                totleBetItemGain += gain;

                var item = new ListViewItem();
                item.UseItemStyleForSubItems = false;
                item.Text = i + "";
                item.SubItems.Add(data[i].Issue.ToString());
                item.SubItems.Add(data[i].OpenCode);
                item.SubItems.Add(betShi.IntListToString() + "," + betGe.IntListToString());
                item.SubItems.Add(betMoeny + "");
                item.SubItems.Add(isZhong ? "中195" : "");
                item.SubItems[0].ForeColor = Color.Gray;
                //item.SubItems[1].ForeColor = Color.Blue;
                if (isZhong)
                {
                    item.SubItems[3].ForeColor = Color.Red;
                    item.SubItems[5].ForeColor = Color.Red;
                }

                lv.Items.Add(item);
            }
            lnkLable.Text =
                $"总投注金额：{totalBetItemPrice},投注次数：{totalBetItem},中奖次数{totalBetItemZhong}，中奖金额：{totleBetItemGain}";
        }

        private void MockBet2(ListView lv, LinkLabel lnkLable, int dayIndex)
        {
            lv.Items.Clear();
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
                lnkLable.Text = @"无数据！请先获取数据";
                return;
            }
            var totalBetItem = 0;
            var totalBetItemZhong = 0;
            var totalBetItemPrice = 0;
            var totleBetItemGain = 0;
            for (int i = 0; i < data.Count; i++)
            {
                var preIndex = i + 1;
                if (preIndex > data.Count - 1)
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
                    lv.Items.Add(item1);
                    break;
                }
                var preGe = data[preIndex].Ge;
                var preShi = data[preIndex].Shi;
                var preBai = data[preIndex].Bai;

                if (preGe == 0 || preShi == 0 || preBai==0)// || Math.Abs(preShi-preGe)>4)
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
                    lv.Items.Add(item1);
                    continue;
                }

                var betGe = dicBet[preGe];
                var betShi = dicBet[preShi];
                var betBai = dicBet[preBai];
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
                var currBai = data[currIndex].Bai;

                var isZhong = betGe.Contains(currGe) && betShi.Contains(currShi)&&betBai.Contains(currBai);

                var betItem = 0;


                foreach (var ge in betGe)
                {
                    foreach (var shi in betShi)
                    {
                        foreach (var bai in betBai)
                        {
                            //if (ge == shi || ge ==bai || shi == bai)
                            //    continue;
                            //else
                                betItem++;
                        }
                    }
                }
                var betMoeny = betItem * 2;
                var gain = 0;
                if (isZhong)
                {
                    gain = 1950;
                    totalBetItemZhong++;
                }
                totalBetItem++;
                totalBetItemPrice += betMoeny;
                totleBetItemGain += gain;

                var item = new ListViewItem();
                item.UseItemStyleForSubItems = false;
                item.Text = i + "";
                item.SubItems.Add(data[i].Issue.ToString());
                item.SubItems.Add(data[i].OpenCode);
                item.SubItems.Add(betShi.IntListToString() + "," + betGe.IntListToString() + "," + betBai.IntListToString());
                item.SubItems.Add(betMoeny + "");
                item.SubItems.Add(isZhong ? "中1950" : "");
                item.SubItems[0].ForeColor = Color.Gray;
                //item.SubItems[1].ForeColor = Color.Blue;
                if (isZhong)
                {
                    item.SubItems[3].ForeColor = Color.Red;
                    item.SubItems[5].ForeColor = Color.Red;
                }

                lv.Items.Add(item);
            }
            lnkLable.Text =
                $"总投注金额：{totalBetItemPrice},投注次数：{totalBetItem},中奖次数{totalBetItemZhong}，中奖金额：{totleBetItemGain}";
        }
    }
}
