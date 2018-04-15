namespace AutoScalping
{
    partial class fmsscMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.wb1 = new System.Windows.Forms.WebBrowser();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenuLoadAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolMenuSeeAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolMenuDelAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.wb1);
            this.panel1.Location = new System.Drawing.Point(12, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(918, 560);
            this.panel1.TabIndex = 0;
            // 
            // wb1
            // 
            this.wb1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wb1.Location = new System.Drawing.Point(0, 0);
            this.wb1.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb1.Name = "wb1";
            this.wb1.ScriptErrorsSuppressed = true;
            this.wb1.Size = new System.Drawing.Size(918, 560);
            this.wb1.TabIndex = 0;
            this.wb1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wb1_DocumentCompleted);
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(952, 58);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(209, 244);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(952, 309);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(209, 274);
            this.panel4.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAccount,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1173, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuAccount
            // 
            this.menuAccount.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolMenuLoadAccount,
            this.toolStripSeparator1,
            this.toolMenuSeeAccount,
            this.toolStripSeparator2,
            this.toolMenuDelAccount});
            this.menuAccount.Name = "menuAccount";
            this.menuAccount.Size = new System.Drawing.Size(68, 21);
            this.menuAccount.Text = "账号管理";
            // 
            // toolMenuLoadAccount
            // 
            this.toolMenuLoadAccount.Name = "toolMenuLoadAccount";
            this.toolMenuLoadAccount.Size = new System.Drawing.Size(172, 22);
            this.toolMenuLoadAccount.Text = "从Excel导入账号";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
            // 
            // toolMenuSeeAccount
            // 
            this.toolMenuSeeAccount.Name = "toolMenuSeeAccount";
            this.toolMenuSeeAccount.Size = new System.Drawing.Size(172, 22);
            this.toolMenuSeeAccount.Text = "查看已载入的账号";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(169, 6);
            // 
            // toolMenuDelAccount
            // 
            this.toolMenuDelAccount.Name = "toolMenuDelAccount";
            this.toolMenuDelAccount.Size = new System.Drawing.Size(172, 22);
            this.toolMenuDelAccount.Text = "清除已导入的账号";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(136, 21);
            this.toolStripMenuItem2.Text = "toolStripMenuItem2";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(136, 21);
            this.toolStripMenuItem3.Text = "toolStripMenuItem3";
            // 
            // fmsscMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 600);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "fmsscMain";
            this.Text = "fmsscMain";
            this.Load += new System.EventHandler(this.fmsscMain_Load);
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.WebBrowser wb1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuAccount;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolMenuLoadAccount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolMenuSeeAccount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolMenuDelAccount;
    }
}