using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoScalping.Controller;
using AutoScalping.Models;
using AutoScalping.Threading;

namespace AutoScalping
{
    public partial class fmsscMainWin : Form
    {
        private bool isQueryingIssue = false;
        private int errorQueryIssueCount = 0;

        private static List<BettingSolutinModel> currentBetModels = new List<BettingSolutinModel>();

        private static List<BettingSolutinModel> bettedModels = new List<BettingSolutinModel>();

        public fmsscMainWin()
        {
            InitializeComponent();
            this.Text += " -v" + GloableParams.AppCurrentVersion.ToString();
        }

        private void fmsscMainWin_Load(object sender, EventArgs e)
        {
            this.txtJiangBeishu.Text = GloableParams.DefaultJiangMultBet.ToString();
            this.txtQcwBeishu.Text = GloableParams.DefaultQcwMultBet.ToString();
            this.txtJiangPersent.Text = GloableParams.DefaultJiangPersent.ToString();
            this.txtQcwPersent.Text = GloableParams.DefaultQcwPersent.ToString();
            this.cboxSln.SelectedIndex = 0;

            //this.txtIssue.BeginInvoke(
            //    new MethodInvoker(delegate() {
            //                                     this.RequeryIssue();
            //    }
            //        ));
            
            //this.lbJiang.DataSource = new JiangComController().GetCurrentIssue();
            //this.lbJiang.DisplayMember = "type";
            //this.lbJiang.ValueMember = "termNo";
            //this.RequeryIssue();
            //for (int i = 1; i < 1000; i++)
            //{
            //    for (int j = 1; j < 1000; j++)
            //    {
            //        if (1.85*i == 1.92*j)
            //        {
            //            MessageBox.Show("i=" + i + "  j=" + j);
            //            break;
            //        }
            //    }
            //}

            //bettedModels.Add(new BettingSolutinModel());
            //bettedModels.Add(new BettingSolutinModel());
            //bettedModels.Add(new BettingSolutinModel());
            //bettedModels.Add(new BettingSolutinModel());
            //this.dgProcessed.BeginInvoke(new MethodInvoker(delegate ()
            //{
            //    this.dgProcessed.AutoGenerateColumns = false;
            //    this.dgProcessed.DataError += delegate (object sender2, DataGridViewDataErrorEventArgs e2) { };
            //    this.dgProcessed.DataSource = new BindingList<BettingSolutinModel>(bettedModels.GetRange(0, bettedModels.Count));
            //}));


        }

        private void tmIssue_Tick(object sender, EventArgs e)
        {
            this.lblCounter.BeginInvoke(
                new MethodInvoker(UpdateIssue));
        }

        private void UpdateIssue()
        {
            int counter;
            if (!string.IsNullOrEmpty(this.lblCounter.Text.Trim()))
            {
                counter = int.Parse(this.lblCounter.Text.Trim().Replace("-", "0"));
                counter--;
                if (counter >= 0)
                {
                    this.lblCounter.Text = counter.ToString().PadLeft(3, '0');
                }
                if (counter <= 0)
                {
                    if (!isQueryingIssue)
                        RequeryIssue();
                }
                else
                {
                    if (counter < 60)
                    {
                        this.lblWarning.Text = "当前期剩余操作不足一分钟，请勿刷单，风险自负！";
                        this.lblWarning.ForeColor = counter%2 == 0 ? Color.Red : Color.Yellow;
                    }
                    else
                    {
                        this.lblWarning.Text = "还有空余时间刷单，请尽快操作！";
                        this.lblWarning.ForeColor = Color.Green;
                    }
                }
            }
            else if (!isQueryingIssue)
            {
                RequeryIssue();
            }
        }

        private void RequeryIssue()
        {
            isQueryingIssue = true;
            try
            {
                bool isOK = false;
                var issue = BaseController.GetCurrentCqsscIssue();
                if (issue == null || issue.termNo == "")
                {
                    errorQueryIssueCount ++;
                    if (errorQueryIssueCount < 5)
                    {
                        MessageBox.Show("获取期号错误或者网络超时，请关检查网络后重试，或者重启程序！", "错误", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    errorQueryIssueCount = 0;
                    isOK = true;
                }
                if (!isOK)
                {
                    this.lblStopTime.Text = "";
                    this.lblCounter.Text = "";
                    this.txtIssue.Text = "";
                    this.lblWarning.Text = "当前不可投注，注意风险！";
                    this.lblWarning.ForeColor = Color.Red;
                    return;
                }
                this.lblStopTime.Text = "本期停售：" + issue.stopSaleTime;
                this.txtIssue.Text = issue.termNo;
                int sndLeft = ((int) (DateTime.Parse(issue.stopSaleTime) - DateTime.Now).TotalSeconds) - 1;
                if (sndLeft <= 0)
                {
                    this.lblWarning.Text = "当前已结停售，不要刷单！新期查询中....";
                    this.lblWarning.ForeColor = Color.Red;
                    return;
                }
                else
                {
                    this.lblCounter.Text = sndLeft.ToString().PadLeft(3, '0');
                }
            }
            catch (Exception ex)
            {
                errorQueryIssueCount++;
            }
            finally
            {
                isQueryingIssue = false;
            }
        }

        private void btnJiang_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            ofDialog.Filter = "CSV文件(*.csv) | *.csv";
            if (ofDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = ofDialog.FileName;
                GloableParams.JiangAccountFromFile = BaseController.ReaderAccountFile(fileName,
                    GloableConstants.AccountBelongs.Jiang);

                this.lbJiang.DataSource = GloableParams.JiangAccountFromFile;
                this.lbJiang.DisplayMember = "LoginName";
                this.lbJiang.ValueMember = "Password";
            }
        }

        private void btnQcw_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            ofDialog.Filter = "CSV文件(*.csv) | *.csv";
            if (ofDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = ofDialog.FileName;
                GloableParams.QcwAccountFromFile = BaseController.ReaderAccountFile(fileName,
                    GloableConstants.AccountBelongs.Qcw);

                this.lbQcw.DataSource = GloableParams.QcwAccountFromFile;
                this.lbQcw.DisplayMember = "LoginName";
                this.lbQcw.ValueMember = "Password";
            }
        }

        private void lbJiang_Click(object sender, EventArgs e)
        {
            if (lbJiang.SelectedItem != null)
            {
                //AccountResponse account = (AccountResponse) lbJiang.SelectedItem;
                //JiangComController.Login(account.LoginName,account.Password);
                //MessageBox.Show(GloableParams.CurrentAccount.RedBagBalance.ToString());
            }
        }


        private void btnCreateSln_Click(object sender, EventArgs e)
        {
            if (this.lbJiang.Items.Count == 0 || this.lbQcw.Items.Count == 0)
            {
                MessageBox.Show("左侧账号不足够同时在2个网站投注，请导入账号重试！", "提示", MessageBoxButtons.OK);
                return;
            }
            else if (this.cboxSln.SelectedIndex == 1 && this.lbQcw.Items.Count < 3)
            {
                MessageBox.Show("方案2必须保证左侧趣彩网至少有3个账号才行，\r\n请选择方案一或者导入账号重试！", "提示", MessageBoxButtons.OK);
                return;
            }
            //this.btnCreateSln.BeginInvoke(
            //    new MethodInvoker(CreateSln)
            //    );
            currentBetModels = new List<BettingSolutinModel>();
            this.btnCreateSln.Enabled = false;
            this.btnBet.Enabled = false;
            this.lblInfo.Text = "方案生成中,请稍候....";
            ThreadFactory.AddTask(new ThreadProxy(CreateSln, this.cboxSln.SelectedIndex));
        }

        private void UpdateInfo(string message)
        {
            this.lblInfo.BeginInvoke(new MethodInvoker(delegate()
            {
                this.lblInfo.Text = message;
            }));
        }

        private void CreateSln(object slnInex)
        {
            int selectedSolutionIndex = (int) slnInex;
            this.dgSolution.BeginInvoke(new MethodInvoker(delegate()
            {
                this.dgSolution.AutoGenerateColumns = false;
                this.dgSolution.Rows.Clear();
                this.dgSolution.DataSource = null;
            }));

            // btnCreateSln.inv
            int jiangAccountCount = 1;
            int qcwAccountCount = 1;
            //如果是方案2，也就是趣彩网复选，A：01234，B：023，C：14  大奖网：56789，那么趣彩网得2 - 3个账号，防止账号过多，此时大奖网只要1 - 2个账号
            if (selectedSolutionIndex == 1)
            {
                Random rndTmp = new Random(DateTime.Now.Millisecond);
                jiangAccountCount = rndTmp.Next(1, 3);
                qcwAccountCount = 3;
            }
            else if (selectedSolutionIndex == 0)
            {
                Random rndTmp = new Random(DateTime.Now.Millisecond);
                jiangAccountCount = rndTmp.Next(1, 4);
                System.Threading.Thread.Sleep(100);
                rndTmp = new Random(DateTime.Now.Millisecond);
                qcwAccountCount = rndTmp.Next(1, 4);
            }

            Random rnd = new Random(DateTime.Now.Millisecond);
            List<AccountResponse> Jaccounts = new List<AccountResponse>();
            List<AccountResponse> Qaccounts = new List<AccountResponse>();
            for (int i = 0; i < jiangAccountCount; i++)
            {
                if (GloableParams.JiangAccountFromFile == null || GloableParams.JiangAccountFromFile.Count == 0)
                    break;
                int index = rnd.Next(0, GloableParams.JiangAccountFromFile.Count);
                var acctmp = GloableParams.JiangAccountFromFile[index];
                try
                {
                    UpdateInfo(string.Format("正在登录并查询{0}账号【{1}】信息....", acctmp.ComeFromStr, acctmp.LoginName));
                    JiangComController.Login(ref acctmp);
                    if (acctmp.status == true)
                    {
                        if (acctmp.RedBagBalance < 2*(GloableParams.DefaultJiangPersent/100))
                        {
                            i--;
                            acctmp.HasError = true;
                            acctmp.ErrorMessage = "红包额度过低";
                            GloableParams.JiangErrorAccountFromFile.Remove(
                                GloableParams.JiangErrorAccountFromFile.FirstOrDefault(
                                    a => a.LoginName == acctmp.LoginName));
                            GloableParams.JiangErrorAccountFromFile.Add(acctmp);
                        }
                        else if (acctmp.MyTotoalMoeny < (2- 2 * (GloableParams.DefaultJiangPersent/100)))
                        {
                            i--;
                            acctmp.HasError = true;
                            acctmp.ErrorMessage = "账户额度过低";
                            GloableParams.JiangErrorAccountFromFile.Remove(
                                GloableParams.JiangErrorAccountFromFile.FirstOrDefault(
                                    a => a.LoginName == acctmp.LoginName));
                            GloableParams.JiangErrorAccountFromFile.Add(acctmp);
                        }
                        else
                        {
                            Jaccounts.Add(acctmp);
                        }

                    }
                    else
                    {
                        i--;
                        acctmp.HasError = true;
                        acctmp.ErrorMessage = string.IsNullOrEmpty(acctmp.ErrorMessage)
                            ? acctmp.message
                            : acctmp.ErrorMessage;
                        GloableParams.JiangErrorAccountFromFile.Remove(
                            GloableParams.JiangErrorAccountFromFile.FirstOrDefault(
                                a => a.LoginName == acctmp.LoginName));
                        GloableParams.JiangErrorAccountFromFile.Add(acctmp);
                    }
                }
                catch (Exception ex)
                {
                    UpdateInfo(string.Format("{0}账号【{1}】 查询出错：{2}", acctmp.ComeFromStr, acctmp.LoginName, ex.Message));
                    i--;
                    acctmp.HasError = true;
                    acctmp.ErrorMessage = ex.Message;
                    GloableParams.JiangErrorAccountFromFile.Remove(
                        GloableParams.JiangErrorAccountFromFile.FirstOrDefault(
                            a => a.LoginName == acctmp.LoginName));
                    GloableParams.JiangErrorAccountFromFile.Add(acctmp);
                }
                finally
                {
                    //GloableParams.JiangAccountFromFile.Remove(
                    //    GloableParams.JiangAccountFromFile.FirstOrDefault(
                    //        a => a.LoginName == acctmp.LoginName));
                }
            }
            ///
            for (int i = 0; i < qcwAccountCount; i++)
            {
                if (GloableParams.QcwAccountFromFile == null || GloableParams.QcwAccountFromFile.Count == 0)
                    break;
                int index = rnd.Next(0, GloableParams.QcwAccountFromFile.Count);
                var acctmp = GloableParams.QcwAccountFromFile[index];
                try
                {
                    UpdateInfo(string.Format("正在登录并查询{0}账号【{1}】信息....", acctmp.ComeFromStr, acctmp.LoginName));
                    QcwComController.Login(ref acctmp);
                    if (acctmp.status == true)
                    {
                        if (acctmp.RedBagBalance < 2 * (GloableParams.DefaultQcwPersent / 100))
                        {
                            i--;
                            acctmp.HasError = true;
                            acctmp.ErrorMessage = "红包额度过低";
                            GloableParams.QcwErrorAccountFromFile.Remove(
                                GloableParams.QcwErrorAccountFromFile.FirstOrDefault(
                                    a => a.LoginName == acctmp.LoginName));
                            GloableParams.QcwErrorAccountFromFile.Add(acctmp);
                        }
                        else if (acctmp.MyTotoalMoeny < (2 - 2 * (GloableParams.DefaultQcwPersent / 100)))
                        {
                            i--;
                            acctmp.HasError = true;
                            acctmp.ErrorMessage = "账户额度过低";
                            GloableParams.QcwErrorAccountFromFile.Remove(
                                GloableParams.QcwErrorAccountFromFile.FirstOrDefault(
                                    a => a.LoginName == acctmp.LoginName));
                            GloableParams.QcwErrorAccountFromFile.Add(acctmp);
                        }
                        else
                        {
                            Qaccounts.Add(acctmp);
                        }
                    }
                    else
                    {
                        i--;
                        acctmp.HasError = true;
                        acctmp.ErrorMessage = acctmp.ErrorMessage = string.IsNullOrEmpty(acctmp.ErrorMessage)
                            ? acctmp.message
                            : acctmp.ErrorMessage;
                        GloableParams.QcwErrorAccountFromFile.Remove(
                            GloableParams.QcwErrorAccountFromFile.FirstOrDefault(
                                a => a.LoginName == acctmp.LoginName));
                        GloableParams.QcwErrorAccountFromFile.Add(acctmp);
                    }
                }
                catch (Exception ex)
                {
                    UpdateInfo(string.Format("{0}账号【{1}】 查询出错：{2}", acctmp.ComeFromStr, acctmp.LoginName, ex.Message));
                    i--;
                    acctmp.HasError = true;
                    acctmp.ErrorMessage = ex.Message;
                    GloableParams.QcwErrorAccountFromFile.Remove(
                        GloableParams.QcwErrorAccountFromFile.FirstOrDefault(
                            a => a.LoginName == acctmp.LoginName));
                    GloableParams.QcwErrorAccountFromFile.Add(acctmp);
                }
                finally
                {
                    //GloableParams.QcwAccountFromFile.Remove(
                    //    GloableParams.QcwAccountFromFile.FirstOrDefault(
                    //        a => a.LoginName == acctmp.LoginName));
                }
            }
            //end
            UpdateInfo("方案生成完毕！请注意将方案投注时候所有数据不会变更，\r\n倍数，倍数，倍数,不变！\r\n变的只有当前期号");
            //生成方案并绑定
            List<BettingSolutinModel> betModels = new List<BettingSolutinModel>();
            if (selectedSolutionIndex == 1 && Qaccounts.Count > 1)
            {
                betModels = BettingSolutionController2.BuildingBetModels(ref Jaccounts, ref Qaccounts);
            }
            else
            {
                betModels = BettingSolutionController1.BuildingBetModels(ref Jaccounts, ref Qaccounts);
            }
            //betModels = BettingSolutionController0Test.BuildingBetModels(ref Jaccounts, ref Qaccounts);
            currentBetModels = betModels;
            this.dgSolution.BeginInvoke(new MethodInvoker(delegate()
            {
                this.dgSolution.AutoGenerateColumns = false;
                this.dgSolution.DataSource = new BindingList<BettingSolutinModel>(betModels.GetRange(0, betModels.Count));
            }));
            //显示不合格账号
            List<AccountResponse> newListTmp = new List<AccountResponse>();
            newListTmp.AddRange(GloableParams.QcwErrorAccountFromFile);
            newListTmp.AddRange(GloableParams.JiangErrorAccountFromFile);
            this.dgBadAccount.BeginInvoke(new MethodInvoker(delegate()
            {
                this.dgBadAccount.AutoGenerateColumns = false;
                this.dgBadAccount.DataSource = new BindingList<AccountResponse>(newListTmp.GetRange(0, newListTmp.Count));
            }));
            //更新account list
            this.lbJiang.BeginInvoke(new MethodInvoker(delegate()
            {
                this.lbJiang.DataSource = null;
                this.lbJiang.Items.Clear();
                this.lbJiang.DataSource = GloableParams.JiangAccountFromFile;
                this.lbJiang.DisplayMember = "LoginName";
                this.lbJiang.ValueMember = "Password";
            }));
            this.lbQcw.BeginInvoke(new MethodInvoker(delegate()
            {
                this.lbQcw.DataSource = null;
                this.lbQcw.Items.Clear();
                this.lbQcw.DataSource = GloableParams.QcwAccountFromFile;
                this.lbQcw.DisplayMember = "LoginName";
                this.lbQcw.ValueMember = "Password";
            }));

            this.btnCreateSln.BeginInvoke(new MethodInvoker(delegate()
            {
                this.btnCreateSln.Enabled = true;
            }));

            this.btnBet.BeginInvoke(new MethodInvoker(delegate()
            {
                this.btnBet.Enabled = true;
            }));
            //
        }

        private void txtJiangBeishu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b') //这是允许输入退格键  
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9')) //这是允许输入0-9数字  
                {
                    e.Handled = true;
                }
            }
        }

        private void txtQcwBeishu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b') //这是允许输入退格键  
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9')) //这是允许输入0-9数字  
                {
                    e.Handled = true;
                }
            }
        }

        private void txtQcwBeishu_Leave(object sender, EventArgs e)
        {
            int.TryParse(this.txtQcwBeishu.Text.Trim(), out GloableParams.DefaultQcwMultBet);
        }

        private void txtJiangBeishu_Leave(object sender, EventArgs e)
        {
            int.TryParse(this.txtJiangBeishu.Text.Trim(), out GloableParams.DefaultJiangMultBet);
        }

        private void btnBet_Click(object sender, EventArgs e)
        {
            if (currentBetModels == null || currentBetModels.Count == 0)
            {
                MessageBox.Show("没有投注方案，不要瞎按，按下去都是钱啊！", "提示", MessageBoxButtons.OK);
                return;
            }
            if (currentBetModels == null || currentBetModels.Count == 1)
            {
                MessageBox.Show("必须同时又2个网站的方案才可投注，请重新生成方案！", "提示", MessageBoxButtons.OK);
                return;
            }
            bool isOK = true;
            string mssage = "当前方案不可投注：\r\n";
            foreach (BettingSolutinModel m in currentBetModels)
            {
                if (m.AccountMoney < m.TotalReallyPayMoney)
                {
                    mssage += String.Format("{0}账号【{1}】没有足够的余额！", m.BelongsStr, m.LoginName);
                    isOK = false;
                    break;
                }
                if (m.MaxRedTheory > m.MaxRedUse)
                {
                    mssage += "红包余额不足，此单理论可以使用" + m.MaxRedTheory + "，实际账户只有" + m.MaxRedUse + "！";
                    isOK = false;
                    break;
                }
            }
            if (!isOK)
            {
                MessageBox.Show(mssage, "提示", MessageBoxButtons.OK);
                return;
            }
            if (!ValidConfig())
            {
                return;
            }
            string currentIssue = this.txtIssue.Text.Trim();
            if (string.IsNullOrEmpty(currentIssue) || currentIssue.Length != 12)
            {
                MessageBox.Show("警告：期号错误或者在更行期次中，不要投注！\r\n等下期！等下期！等下期！", "警告", MessageBoxButtons.OK);
                return;
            }
            int sndLeft = 0;
            int.TryParse(this.lblCounter.Text.Trim().Replace('-', '0'), out sndLeft);
            if (sndLeft < 60)
            {
                if (
                    MessageBox.Show("警告：投注剩余时间不足1分钟，\r\n如果出现错误没有时间及时手动补单，建议不要投注！\r\n等下期！等下期！等下期！\r\n\r\n 是否继续投注？", "警告",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.btnCreateSln.Enabled = false;
                    this.btnBet.Enabled = false;
                    this.lblInfo.Text = "方案投注中,请稍候....";
                    ThreadFactory.AddTask(new ThreadProxy(BettingSln, currentIssue));
                }
            }
            else
            {
                this.btnCreateSln.Enabled = false;
                this.btnBet.Enabled = false;
                this.lblInfo.Text = "方案投注中,请稍候....";
                ThreadFactory.AddTask(new ThreadProxy(BettingSln, currentIssue));
            }
        }

        private void BettingSln(object currentIssue)
        {
            for (int i = 0; i < currentBetModels.Count; i++)
            {
                var model = currentBetModels[i];
                try
                {
                    model.CurrentIssueStr = currentIssue.ToString();
                    string token = model.CurrentToken;
                    UpdateInfo(string.Format("正在投注{0}账号【{1}】的方案【{2}】...", model.BelongsStr, model.LoginName,
                        model.BettingNumStr));
                    if (model.Belongs == GloableConstants.AccountBelongs.Jiang)
                    {
                        JiangComController.BettingYiXing(ref model, token);
                    }
                    if (model.Belongs == GloableConstants.AccountBelongs.Qcw)
                    {
                        QcwComController.BettingYiXing(ref model, token);
                    }
                    UpdateInfo(string.Format("{0}账号【{1}】的方案投注完成，结果：{2}.", model.BelongsStr, model.LoginName,model.IsBettingSuccMsg));
                }
                catch (Exception ex)
                {
                    if (!model.IsBettingSucc && model.ResponseCode != 0)
                    {
                        model.IsBettingSucc = false;
                        model.MyMessage = ex.Message;
                    }
                }
                finally
                {
                    bettedModels.Insert(0, model);
                    //显示投注历史
                    this.dgProcessed.BeginInvoke(new MethodInvoker(delegate()
                    {
                        this.dgProcessed.AutoGenerateColumns = false;
                        this.dgProcessed.DataSource =
                            new BindingList<BettingSolutinModel>(bettedModels.GetRange(0, bettedModels.Count));
                    }));
                }
            }
            //end
            UpdateInfo("投注完毕！请注意核查网数据，\r\n\r\n如何下方有显示投注失败的，请手动去网站补单！");

            this.btnCreateSln.BeginInvoke(new MethodInvoker(delegate()
            {
                this.btnCreateSln.Enabled = true;
            }));

            this.btnBet.BeginInvoke(new MethodInvoker(delegate()
            {
                this.btnBet.Enabled = true;
            }));
        }


        private void cboxSln_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboxSln.SelectedIndex == 1)
            {
                if (this.lbQcw.Items.Count < 3)
                {
                    this.cboxSln.SelectedIndex = 0;
                    return;
                }
                GloableParams.DefaultJiangMultBet = GloableParams.DefaultQcwMultBet*2;
                this.txtJiangBeishu.Text = GloableParams.DefaultJiangMultBet.ToString();
            }


        }

        private bool ValidConfig()
        {
            int jBeishu = 0;
            int jPersent = 0;
            int qBeishu = 0;
            int qPresent = 0;
            if (!int.TryParse(this.txtJiangBeishu.Text, out jBeishu) & jBeishu > 0)
            {
                MessageBox.Show("配置错误：大奖网倍数只能够为大于0的数字");
                return false;
            }
            if (!int.TryParse(this.txtJiangPersent.Text, out jPersent) && jPersent > 0)
            {
                MessageBox.Show("配置错误：大奖网红包使用率只能够为大于0的数字");
                return false;
            }
            if (!int.TryParse(this.txtQcwBeishu.Text, out qBeishu) & qBeishu > 0)
            {
                MessageBox.Show("配置错误：趣彩网倍数只能够为大于0的数字");
                return false;
            }
            if (!int.TryParse(this.txtQcwPersent.Text, out qPresent) && qPresent > 0)
            {
                MessageBox.Show("配置错误：趣彩网红包使用率只能够为大于0的数字");
                return false;
            }
            GloableParams.DefaultJiangMultBet = jBeishu;
            GloableParams.DefaultJiangPersent = jPersent;
            GloableParams.DefaultQcwMultBet = qBeishu;
            GloableParams.DefaultQcwPersent = qPresent;
            return true;

        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            if(!ValidConfig())
                return;
        }

        private void txtJiangPersent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b') //这是允许输入退格键  
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9')) //这是允许输入0-9数字  
                {
                    e.Handled = true;
                }
            }
        }

        private void txtQcwPersent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b') //这是允许输入退格键  
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9')) //这是允许输入0-9数字  
                {
                    e.Handled = true;
                }
            }
        }

        private void dgBadAccount_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void dgSolution_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                
            }
        }
    }
}
