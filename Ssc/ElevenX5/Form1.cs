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

        protected List<ElevenX5Model> KaijiangModels { get; set; }
        protected List<ElevenX5Model> CalculateModels { get; set; }
        protected List<int> DanList { get; set; }
        protected List<int> TuoList { get; set; }
        protected List<List<int>> CombinedModels { get; set; }
        protected List<DanTuoModel> DanTuoModels { get; set; }
        public Form1()
        {
            InitializeComponent();
            SetInputStyle();
        }



        private void SetInputStyle()
        {
            var boxs = new List<TextBox>()
            {
                textBox2,
                textBox3,
                textBox4,
                textBox5,
                textBox6,
                textBox7,
                textBox8,
                textBox9,
                textBox10,
                textBox11,
                textBox12
            };
            foreach (var textBox in boxs)
            {
                textBox.ForeColor = Color.Red;
                textBox.Leave += textBox_Leave;
            }

            var boxs2 = new List<TextBox>()
            {
                textBox2,
                textBox3,
                textBox4,
                textBox5,
                textBox6
            };
            foreach (var textbox in boxs2)
            {
                //textbox.TextChanged += Textbox_TextChanged;
            }

            this.label9.Text = @"录入说明:

1.如果期次相同，则会替换以前录入的对
  应期次开奖数据

2.如果序号大于20，则会替换现有的序号
  排序，序号是1的将会替换掉；
> 如现有序号是1....20,你填入21，则序
  号是2的变成1，序号是20的变成19，当
  前录入的序号变成20

3.如果序号相同，则以新的序号为准
> 如以前第17期的序号是6，现在录入第19
  期序号也为6，那么第17期的序号将作废";
        }

        private void Textbox_TextChanged(object sender, EventArgs e)
        {
            var box = sender as TextBox;
            var no = box.Text.Trim();
            if (no.IsValidNumber())
            {
                int a = no.ToInt();
                if (a > 1)
                {
                    SendKeys.Send("{tab}");
                }
            }
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            var box = sender as TextBox;
            var no = box.Text.Trim();
            if (no.IsValidNumber())
            {
                box.Text = no.PadLeft(2,'0');
            }
        }

        private void InitControls()
        {
            this.listView1.Columns.Add("期号", 40, HorizontalAlignment.Center);
            this.listView1.Columns.Add("开奖号码", 120, HorizontalAlignment.Center);
            this.listView1.Columns.Add("序号", 40, HorizontalAlignment.Center);
            this.listView1.GridLines = true;
            this.listView1.View = System.Windows.Forms.View.Details;  //这命令比较重要，否则不能显示。

            this.listView2.Columns.Add("期号", 40, HorizontalAlignment.Center);
            this.listView2.Columns.Add("开奖号码", 120, HorizontalAlignment.Center);
            this.listView2.Columns.Add("序号", 40, HorizontalAlignment.Center);
            this.listView2.GridLines = true;
            this.listView2.View = System.Windows.Forms.View.Details;  //这命令比较重要，否则不能显示。

            this.listView3.Columns.Add("", 30, HorizontalAlignment.Center);
            this.listView3.Columns.Add("不符合的胆拖", 120, HorizontalAlignment.Left);
            this.listView3.Columns.Add("不符期数", 100, HorizontalAlignment.Center);
            this.listView3.GridLines = true;
            this.listView3.View = System.Windows.Forms.View.Details;  //这命令比较重要，否则不能显示。


            // 设置行高
            var imgList = new ImageList();
            // 分别是宽和高
            imgList.ImageSize = new Size(1, 20);
            // 这里设置listView的SmallImageList ,用imgList将其撑大
            listView1.SmallImageList = imgList;
            listView2.SmallImageList = imgList;
            listView3.SmallImageList = imgList;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.InitControls();
            LoadData();
            FillKaijiangView();
            FillCombinedView();
        }

        private void LoadData()
        {
            KaijiangModels = ElevenX5Buz.GetModelFromFile();
            CalculateModels = KaijiangModels.Where(x => x.Index > 0).ToList();
        }

        private void ModelsSort()
        {
            KaijiangModels = KaijiangModels.OrderBy(s => s.IssueNo).ToList();
            CalculateModels = CalculateModels.OrderBy(s => s.Index).ToList();
        }
        private void FillKaijiangView()
        {
            ModelsSort();
            listView1.Items.Clear();
            if (!KaijiangModels.Any())
                return;
            foreach (var model in KaijiangModels)
            {
                var item = new ListViewItem();
                item.UseItemStyleForSubItems = false;
                item.Text = model.IssueNo.ToString().PadLeft(2, '0');
                item.SubItems.Add(model.BetNo.ToSpliteString());
                var index = model.Index == 0 ? "" : model.Index.ToString();
                item.SubItems.Add(index);
             
                item.SubItems[0].ForeColor = Color.Blue;
                item.SubItems[1].ForeColor = Color.Red;
                item.SubItems[2].ForeColor = Color.DarkGreen;
                this.listView1.Items.Add(item);
            }
        }

        private void FillCombinedView()
        {
            listView2.Items.Clear();
            if (!CalculateModels.Any())
                return;
            foreach (var model in CalculateModels)
            {
                var item = new ListViewItem();
                item.UseItemStyleForSubItems = false;
                item.Text = model.IssueNo.ToString().PadLeft(2, '0');
                item.SubItems.Add(model.BetNo.ToSpliteString());
                var index = model.Index == 0 ? "" : model.Index.ToString();
                item.SubItems.Add(index);

                item.SubItems[0].ForeColor = Color.Blue;
                item.SubItems[1].ForeColor = Color.Red;
                item.SubItems[2].ForeColor = Color.DarkGreen;
                this.listView2.Items.Add(item);
            }
        }

        private void FillDanTuoMissingView()
        {
            listView3.Items.Clear();
            if (!DanTuoModels.Any())
                return;
            var matchedModel = DanTuoModels.Where(a => a.MissingCount >= 8).ToList();
            if (!matchedModel.Any())
                return;
            int index = 1;
            foreach (var model in matchedModel)
            {
                var item = new ListViewItem();
                item.UseItemStyleForSubItems = false;
                item.Text = index + "";
                item.SubItems.Add(model.DanTuoNo.ToSpliteString());
                item.SubItems.Add(model.MissingCount.ToString());
                item.SubItems[0].ForeColor = Color.Gray;
                item.SubItems[1].ForeColor = Color.Red;
                item.SubItems[2].ForeColor = Color.OrangeRed;
                item.SubItems[2].Font = new Font(item.SubItems[2].Font.FontFamily,10.5F,FontStyle.Bold);
                this.listView3.Items.Add(item);
                index++;
            }
        }



        //录入开奖结果
        private void button1_Click(object sender, EventArgs e)
        {
            if(!CheckSb())
                return;
            var issue = this.textBox1.Text.Trim();
            var no1 = this.textBox2.Text.Trim();
            var no2 = this.textBox3.Text.Trim();
            var no3 = this.textBox4.Text.Trim();
            var no4 = this.textBox5.Text.Trim();
            var no5 = this.textBox6.Text.Trim();
            var index = this.textBox13.Text.Trim();
            var indexNum = 0;
            if (!issue.IsValidIssue()
                || !no1.IsValidNumber()
                || !no2.IsValidNumber()
                || !no3.IsValidNumber()
                || !no4.IsValidNumber()
                || !no5.IsValidNumber()
                )
            {
                MessageBox.Show("录入的开奖期号或开奖号码不正确，请检查！", "提示", MessageBoxButtons.OK);
                return;
            }
            if (!string.IsNullOrWhiteSpace(index))
            {
                indexNum = index.ToInt();
            }
            var model = new ElevenX5Model()
            {
                IssueNo = issue.ToInt(),
                BetNo = new List<int>()
                {
                    no1.ToInt(),no2.ToInt(),no3.ToInt(),no4.ToInt(),no5.ToInt()
                },
                Index = indexNum
                
            };
            if (model.BetNo.Distinct().Count() != 5)
            {
                MessageBox.Show("录入开奖号码有重复，请检查！", "提示", MessageBoxButtons.OK);
                return;
            }
            if (KaijiangModels.Count == 0)
                KaijiangModels.Add(model);
            else
            {
                var replacemodel = KaijiangModels.FirstOrDefault(x => x.IssueNo == model.IssueNo);
                if (replacemodel != null)
                {
                    replacemodel.IssueNo = model.IssueNo;
                    replacemodel.BetNo = model.BetNo;
                }
                else if (KaijiangModels.Count >= 30)
                {
                    KaijiangModels.RemoveAt(0);
                    KaijiangModels.Add(model);
                }
                else
                {
                    KaijiangModels.Add(model);
                }
            }
            ElevenX5Buz.SaveModelToFile(KaijiangModels);
            FillKaijiangView();

            if (!GetSettingDanTuo())
                return;
            GetCombinedBetNoByDanTuo();
            FillCombinedView();
            GetCombinedDanTuoNo();
            CompareKaijiangWithDanTuo();
            FillDanTuoMissingView();
        }

        //录入胆码和拖码
        private void CalculateMissing()
        {
            if (!CheckSb())
                return;

            if (!GetSettingDanTuo())
                return;
            GetCombinedBetNoByDanTuo();
            FillCombinedView();
            GetCombinedDanTuoNo();
            CompareKaijiangWithDanTuo();
            FillDanTuoMissingView();
        }
        // 根据胆拖生产196注
        private void GetCombinedBetNoByDanTuo()
        {
            CombinedModels = ElevenX5Buz.GetCombinedBetNos(DanList, TuoList);
        }
        //根据UI获取胆拖设置
        private bool GetSettingDanTuo()
        {
            var dan1 = this.textBox7.Text.Trim();
            var dan2 = this.textBox8.Text.Trim();
            var tuo1 = this.textBox9.Text.Trim();
            var tuo2 = this.textBox10.Text.Trim();
            var tuo3 = this.textBox11.Text.Trim();
            var tuo4 = this.textBox12.Text.Trim();

            if (!dan1.IsValidNumber()
                || !dan2.IsValidNumber()
                || !tuo1.IsValidNumber()
                || !tuo2.IsValidNumber()
                || !tuo3.IsValidNumber()
                || !tuo4.IsValidNumber()
                )
            {
                MessageBox.Show("输入胆码拖码不正确，请检查！", "提示", MessageBoxButtons.OK);
                return false;
            }
            var all = new List<int> { dan1.ToInt(), dan2.ToInt(), tuo1.ToInt(), tuo2.ToInt(), tuo3.ToInt(), tuo4.ToInt() };
            if (all.Distinct().ToList().Count < 6)
            {
                MessageBox.Show("输入胆码拖码有重复，请检查！", "提示", MessageBoxButtons.OK);
                return false;
            }
            DanList = new List<int>() { dan1.ToInt(), dan2.ToInt() };
            TuoList = new List<int>() { tuo1.ToInt(), tuo2.ToInt(), tuo3.ToInt(), tuo4.ToInt() };
            return true;
        }
        //根据胆拖获取胆拖组合
        private void GetCombinedDanTuoNo()
        {
            var list = ElevenX5Buz.GetCombinedDanTuoNos(DanList, TuoList);
            DanTuoModels = new List<DanTuoModel>();
            foreach (var item in list)
            {
                DanTuoModels.Add(new DanTuoModel()
                {
                    MissingCount = 0,
                    DanTuoNo = item
                });
            }
        }

        private void CompareKaijiangWithDanTuo()
        {
            foreach (var danTuoModel in DanTuoModels)
            {
                int maxMissing = 0;
                int currentMissing = 0;
                for (int i = 0; i < KaijiangModels.Count; i++)
                {
                    var kaijiangModel = KaijiangModels[i];
                    if (!kaijiangModel.BetNo.ContainsAllNo(danTuoModel.DanTuoNo))
                    {
                        if (maxMissing == 0)
                        {
                            maxMissing++;
                        }
                        currentMissing++;
                        if (currentMissing > maxMissing)
                        {
                            maxMissing++;
                        }
                        danTuoModel.MissingCount += 1;
                    }
                    else
                    {
                        currentMissing = 0;
                    }
                }

                danTuoModel.MissingCount = maxMissing;
            }
            DanTuoModels = DanTuoModels.OrderBy(s => s.MissingCount).ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!CheckSb())
                return;

            if(!GetSettingDanTuo())
            {
                return;
            }

            GetCombinedBetNoByDanTuo();
            FillCombinedView();
            GetCombinedDanTuoNo();
            CompareKaijiangWithDanTuo();
            FillDanTuoMissingView();
        }

        private bool CheckSb()
        {
            var endDate = new DateTime(2018, 4, 18);
            if (DateTime.Now >= endDate)
                return false;
            return true;
        }
    }
}
