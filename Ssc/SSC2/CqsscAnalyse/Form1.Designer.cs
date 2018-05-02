namespace CqsscAnalyse
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnAnalyse = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Issue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Wan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.opentime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblCount = new System.Windows.Forms.Label();
            this.richTextBox10 = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnSync = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIssue = new System.Windows.Forms.TextBox();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.btnSaveOneIssue = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.tabControlCalculate = new System.Windows.Forms.TabControl();
            this.tabCommon = new System.Windows.Forms.TabPage();
            this.richTextBoxCommon = new System.Windows.Forms.RichTextBox();
            this.tabWan = new System.Windows.Forms.TabPage();
            this.tabQian = new System.Windows.Forms.TabPage();
            this.tabBai = new System.Windows.Forms.TabPage();
            this.richTextBox100 = new System.Windows.Forms.RichTextBox();
            this.tabShi = new System.Windows.Forms.TabPage();
            this.tabGe = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControlCalculate.SuspendLayout();
            this.tabCommon.SuspendLayout();
            this.tabBai.SuspendLayout();
            this.tabShi.SuspendLayout();
            this.tabGe.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAnalyse
            // 
            this.btnAnalyse.BackColor = System.Drawing.Color.Red;
            this.btnAnalyse.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAnalyse.ForeColor = System.Drawing.Color.White;
            this.btnAnalyse.Location = new System.Drawing.Point(832, 489);
            this.btnAnalyse.Name = "btnAnalyse";
            this.btnAnalyse.Size = new System.Drawing.Size(169, 48);
            this.btnAnalyse.TabIndex = 5;
            this.btnAnalyse.Text = "开始分析";
            this.btnAnalyse.UseVisualStyleBackColor = false;
            this.btnAnalyse.Click += new System.EventHandler(this.btnAnalyse_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Issue,
            this.Wan,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.opentime,
            this.Column6,
            this.Column7});
            this.dataGridView1.Location = new System.Drawing.Point(4, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(484, 609);
            this.dataGridView1.TabIndex = 6;
            // 
            // Issue
            // 
            this.Issue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Issue.DataPropertyName = "Issue";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Issue.DefaultCellStyle = dataGridViewCellStyle2;
            this.Issue.HeaderText = "期号";
            this.Issue.MinimumWidth = 100;
            this.Issue.Name = "Issue";
            this.Issue.ReadOnly = true;
            // 
            // Wan
            // 
            this.Wan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Wan.DataPropertyName = "Wan";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Wan.DefaultCellStyle = dataGridViewCellStyle3;
            this.Wan.FillWeight = 20F;
            this.Wan.HeaderText = "万";
            this.Wan.MinimumWidth = 20;
            this.Wan.Name = "Wan";
            this.Wan.ReadOnly = true;
            this.Wan.Width = 42;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Column1.DataPropertyName = "Qian";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column1.FillWeight = 20F;
            this.Column1.HeaderText = "千";
            this.Column1.MinimumWidth = 20;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 42;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Column2.DataPropertyName = "Bai";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column2.FillWeight = 20F;
            this.Column2.HeaderText = "百";
            this.Column2.MinimumWidth = 20;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 42;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Column3.DataPropertyName = "Shi";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column3.FillWeight = 20F;
            this.Column3.HeaderText = "十";
            this.Column3.MinimumWidth = 20;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 42;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Column4.DataPropertyName = "Ge";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column4.FillWeight = 20F;
            this.Column4.HeaderText = "个";
            this.Column4.MinimumWidth = 20;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 42;
            // 
            // opentime
            // 
            this.opentime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.opentime.DataPropertyName = "OpenTime";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.opentime.DefaultCellStyle = dataGridViewCellStyle8;
            this.opentime.HeaderText = "开奖时间";
            this.opentime.MinimumWidth = 140;
            this.opentime.Name = "opentime";
            this.opentime.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "StrIssue";
            this.Column6.HeaderText = "StrIssue";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Visible = false;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "OpenCode";
            this.Column7.HeaderText = "OpenCode";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Visible = false;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCount.ForeColor = System.Drawing.Color.Red;
            this.lblCount.Location = new System.Drawing.Point(503, 573);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 22);
            this.lblCount.TabIndex = 7;
            // 
            // richTextBox10
            // 
            this.richTextBox10.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox10.Location = new System.Drawing.Point(3, 3);
            this.richTextBox10.Name = "richTextBox10";
            this.richTextBox10.Size = new System.Drawing.Size(647, 377);
            this.richTextBox10.TabIndex = 8;
            this.richTextBox10.Text = "";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(647, 377);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            // 
            // btnSync
            // 
            this.btnSync.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSync.Location = new System.Drawing.Point(499, 428);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(135, 37);
            this.btnSync.TabIndex = 10;
            this.btnSync.Text = "①同步开奖数据";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCheck.Location = new System.Drawing.Point(649, 429);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(135, 37);
            this.btnCheck.TabIndex = 11;
            this.btnCheck.Text = "②检查缺失数据";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(498, 489);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "期号（20170212002）";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(498, 528);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "开奖号码（09346）";
            // 
            // txtIssue
            // 
            this.txtIssue.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIssue.Location = new System.Drawing.Point(649, 485);
            this.txtIssue.MaxLength = 11;
            this.txtIssue.Name = "txtIssue";
            this.txtIssue.Size = new System.Drawing.Size(147, 26);
            this.txtIssue.TabIndex = 14;
            this.txtIssue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIssue_KeyPress);
            // 
            // txtNumber
            // 
            this.txtNumber.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNumber.Location = new System.Drawing.Point(649, 525);
            this.txtNumber.MaxLength = 5;
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(147, 26);
            this.txtNumber.TabIndex = 15;
            this.txtNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumber_KeyPress);
            // 
            // btnSaveOneIssue
            // 
            this.btnSaveOneIssue.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveOneIssue.Location = new System.Drawing.Point(649, 566);
            this.btnSaveOneIssue.Name = "btnSaveOneIssue";
            this.btnSaveOneIssue.Size = new System.Drawing.Size(147, 46);
            this.btnSaveOneIssue.TabIndex = 16;
            this.btnSaveOneIssue.Text = "保存";
            this.btnSaveOneIssue.UseVisualStyleBackColor = true;
            this.btnSaveOneIssue.Click += new System.EventHandler(this.btnSaveOneIssue_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(801, 439);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 14);
            this.lblInfo.TabIndex = 17;
            // 
            // tabControlCalculate
            // 
            this.tabControlCalculate.Controls.Add(this.tabCommon);
            this.tabControlCalculate.Controls.Add(this.tabWan);
            this.tabControlCalculate.Controls.Add(this.tabQian);
            this.tabControlCalculate.Controls.Add(this.tabBai);
            this.tabControlCalculate.Controls.Add(this.tabShi);
            this.tabControlCalculate.Controls.Add(this.tabGe);
            this.tabControlCalculate.Controls.Add(this.tabPage1);
            this.tabControlCalculate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControlCalculate.Location = new System.Drawing.Point(496, 3);
            this.tabControlCalculate.Name = "tabControlCalculate";
            this.tabControlCalculate.SelectedIndex = 0;
            this.tabControlCalculate.Size = new System.Drawing.Size(661, 413);
            this.tabControlCalculate.TabIndex = 9;
            // 
            // tabCommon
            // 
            this.tabCommon.Controls.Add(this.richTextBoxCommon);
            this.tabCommon.Location = new System.Drawing.Point(4, 26);
            this.tabCommon.Name = "tabCommon";
            this.tabCommon.Padding = new System.Windows.Forms.Padding(3);
            this.tabCommon.Size = new System.Drawing.Size(653, 383);
            this.tabCommon.TabIndex = 5;
            this.tabCommon.Text = "信息";
            this.tabCommon.UseVisualStyleBackColor = true;
            // 
            // richTextBoxCommon
            // 
            this.richTextBoxCommon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxCommon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxCommon.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxCommon.Name = "richTextBoxCommon";
            this.richTextBoxCommon.Size = new System.Drawing.Size(647, 377);
            this.richTextBoxCommon.TabIndex = 0;
            this.richTextBoxCommon.Text = "";
            // 
            // tabWan
            // 
            this.tabWan.Location = new System.Drawing.Point(4, 26);
            this.tabWan.Name = "tabWan";
            this.tabWan.Padding = new System.Windows.Forms.Padding(3);
            this.tabWan.Size = new System.Drawing.Size(653, 383);
            this.tabWan.TabIndex = 0;
            this.tabWan.Text = "万位结果";
            this.tabWan.UseVisualStyleBackColor = true;
            // 
            // tabQian
            // 
            this.tabQian.Location = new System.Drawing.Point(4, 26);
            this.tabQian.Name = "tabQian";
            this.tabQian.Padding = new System.Windows.Forms.Padding(3);
            this.tabQian.Size = new System.Drawing.Size(653, 383);
            this.tabQian.TabIndex = 1;
            this.tabQian.Text = "千位";
            this.tabQian.UseVisualStyleBackColor = true;
            // 
            // tabBai
            // 
            this.tabBai.Controls.Add(this.richTextBox100);
            this.tabBai.Location = new System.Drawing.Point(4, 26);
            this.tabBai.Name = "tabBai";
            this.tabBai.Padding = new System.Windows.Forms.Padding(3);
            this.tabBai.Size = new System.Drawing.Size(653, 383);
            this.tabBai.TabIndex = 2;
            this.tabBai.Text = "百位";
            this.tabBai.UseVisualStyleBackColor = true;
            // 
            // richTextBox100
            // 
            this.richTextBox100.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox100.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox100.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox100.Location = new System.Drawing.Point(3, 3);
            this.richTextBox100.Name = "richTextBox100";
            this.richTextBox100.Size = new System.Drawing.Size(647, 377);
            this.richTextBox100.TabIndex = 0;
            this.richTextBox100.Text = "";
            // 
            // tabShi
            // 
            this.tabShi.Controls.Add(this.richTextBox10);
            this.tabShi.Location = new System.Drawing.Point(4, 26);
            this.tabShi.Name = "tabShi";
            this.tabShi.Padding = new System.Windows.Forms.Padding(3);
            this.tabShi.Size = new System.Drawing.Size(653, 383);
            this.tabShi.TabIndex = 3;
            this.tabShi.Text = "十位";
            this.tabShi.UseVisualStyleBackColor = true;
            // 
            // tabGe
            // 
            this.tabGe.Controls.Add(this.richTextBox1);
            this.tabGe.Location = new System.Drawing.Point(4, 26);
            this.tabGe.Name = "tabGe";
            this.tabGe.Padding = new System.Windows.Forms.Padding(3);
            this.tabGe.Size = new System.Drawing.Size(653, 383);
            this.tabGe.TabIndex = 4;
            this.tabGe.Text = "个位";
            this.tabGe.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.linkLabel1);
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(653, 383);
            this.tabPage1.TabIndex = 6;
            this.tabPage1.Text = "模拟二星";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(647, 334);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(6, 351);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(0, 16);
            this.linkLabel1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 616);
            this.Controls.Add(this.tabControlCalculate);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnSaveOneIssue);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.txtIssue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnAnalyse);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重庆时时彩分析软件";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControlCalculate.ResumeLayout(false);
            this.tabCommon.ResumeLayout(false);
            this.tabBai.ResumeLayout(false);
            this.tabShi.ResumeLayout(false);
            this.tabGe.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAnalyse;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.RichTextBox richTextBox10;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIssue;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Button btnSaveOneIssue;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TabControl tabControlCalculate;
        private System.Windows.Forms.TabPage tabWan;
        private System.Windows.Forms.TabPage tabQian;
        private System.Windows.Forms.TabPage tabBai;
        private System.Windows.Forms.TabPage tabShi;
        private System.Windows.Forms.TabPage tabGe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Issue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Wan;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn opentime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.TabPage tabCommon;
        private System.Windows.Forms.RichTextBox richTextBoxCommon;
        private System.Windows.Forms.RichTextBox richTextBox100;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

