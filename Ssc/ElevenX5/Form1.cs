using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SscCommon.Buz;
using SscCommon.Models;
using SscCommon.SscUtil;

namespace ElevenX5
{
    public partial class Form1 : Form
    {

        protected List<SscModel> Models { get; set; }
        protected List<int> DanList { get; set; }
        protected List<int> TuoList { get; set; }
        protected List<List<int>> CombinedModels { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        #region

        private void InitControls()
        {
            this.listView1.Columns.Add("序号", 40, HorizontalAlignment.Center);
            this.listView1.Columns.Add("期号", 90, HorizontalAlignment.Center);
            this.listView1.Columns.Add("开奖号码", 160, HorizontalAlignment.Left);
            this.listView1.GridLines = true;
            this.listView1.View = System.Windows.Forms.View.Details;  //这命令比较重要，否则不能显示。

            this.listView2.Columns.Add("序号", 40, HorizontalAlignment.Center);
            this.listView2.Columns.Add("胆拖组合号码", 160, HorizontalAlignment.Left);
            this.listView2.GridLines = true;
            this.listView2.View = System.Windows.Forms.View.Details;  //这命令比较重要，否则不能显示。
        }
        #endregion

        private void ModelsSort()
        {
            Models =  Models.OrderBy(s => s.IssueNo).ToList();
        }
        private void FillView()
        {
            ModelsSort();
            listView1.Items.Clear();
            if (!Models.Any())
                return;
            int index = 1;
            foreach (var model in Models)
            {
                var item = new ListViewItem();
                item.Text = index + "";
                item.SubItems.Add(model.IssueNo.ToString());
                item.SubItems.Add(model.BetNo.ToSpliteString());
                this.listView1.Items.Add(item);
                index++;
            }
        }

        private void FillCombinedView()
        {
            listView2.Items.Clear();
            if (!CombinedModels.Any())
                return;
            int index = 1;
            foreach (var model in CombinedModels)
            {
                var item = new ListViewItem();
                item.Text = index + "";
                item.SubItems.Add(model.ToSpliteString());
                this.listView2.Items.Add(item);
                index++;
            }
        }

        private void LoadData()
        {
            Models = ElevenX5Buz.GetModelFromFile();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.InitControls();
            LoadData();
            FillView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var issue = this.textBox1.Text.Trim();
            var no1 = this.textBox2.Text.Trim();
            var no2 = this.textBox3.Text.Trim();
            var no3 = this.textBox4.Text.Trim();
            var no4 = this.textBox5.Text.Trim();
            var no5 = this.textBox6.Text.Trim();
            if (!CheckIssue(issue)
                || !CheckBetNo(no1)
                || !CheckBetNo(no2)
                || !CheckBetNo(no3)
                || !CheckBetNo(no4)
                || !CheckBetNo(no5)
                )
            {
                MessageBox.Show("输入的开奖期号或开奖号码不正确，请检查！", "提示", MessageBoxButtons.OK);
                return;
            }
            var model = new SscModel()
            {
                IssueNo = issue.ToLong(),
                BetNo = new List<int>()
                {
                    no1.ToInt(),no2.ToInt(),no3.ToInt(),no4.ToInt(),no5.ToInt()
                }
            };
            if (model.BetNo.Distinct().Count() != 5)
            {
                MessageBox.Show("输入开奖号码有重复，请检查！", "提示", MessageBoxButtons.OK);
                return;
            }
            if (Models.Count == 0)
                Models.Add(model);
            else
            {
                var replacemodel = Models.FirstOrDefault(x => x.IssueNo == model.IssueNo);
                if (replacemodel != null)
                {
                    replacemodel.IssueNo = model.IssueNo;
                    replacemodel.BetNo = model.BetNo;
                }
                else if (Models.Count >= 20)
                {
                    Models.RemoveAt(0);
                    Models.Add(model);
                }
                else
                {
                    Models.Add(model);
                }
            }
            ElevenX5Buz.SaveModelToFile(Models);
            FillView();
        }

        private bool CheckIssue(string issue)
        {
            if (String.IsNullOrWhiteSpace(issue))
            {
                return false;
            }
            return true;
        }

        private bool CheckBetNo(string num)
        {
            if (String.IsNullOrWhiteSpace(num))
            {
                return false;
            }
            int no = 0;
            if (!int.TryParse(num, out no))
            {
                return false;
            }
            if (!no.IsValidSscNumber())
            {
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dan1 = this.textBox7.Text.Trim();
            var dan2 = this.textBox8.Text.Trim();

            var tuo1 = this.textBox9.Text.Trim();
            var tuo2 = this.textBox10.Text.Trim();
            var tuo3 = this.textBox11.Text.Trim();
            var tuo4 = this.textBox12.Text.Trim();

            if (!CheckBetNo(dan1)
                || !CheckBetNo(dan2)
                || !CheckBetNo(tuo1)
                || !CheckBetNo(tuo2)
                || !CheckBetNo(tuo3)
                || !CheckBetNo(tuo4)
                )
            {
                MessageBox.Show("输入胆码拖码不正确，请检查！", "提示", MessageBoxButtons.OK);
                return;
            }
            var all = new List<int> {dan1.ToInt(),dan2.ToInt(),tuo1.ToInt(),tuo2.ToInt(),tuo3.ToInt(),tuo4.ToInt()};
            if (all.Distinct().ToList().Count < 6)
            {
                MessageBox.Show("输入胆码拖码有重复，请检查！", "提示", MessageBoxButtons.OK);
                return;
            }

            DanList = new List<int>() {dan1.ToInt(),dan2.ToInt()};
            TuoList = new List<int>() { tuo1.ToInt(), tuo2.ToInt(), tuo3.ToInt(), tuo4.ToInt() };

            CombinedModels = SscCombineUtil.CombineBet(DanList, TuoList);
            FillCombinedView();

        }
    }
}
