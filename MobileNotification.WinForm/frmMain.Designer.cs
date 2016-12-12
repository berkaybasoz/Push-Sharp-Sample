namespace MobileNotification.WinForm
{
    partial class frmMain
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssUserCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssSuccessMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssErrorMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tssStart = new System.Windows.Forms.ToolStripMenuItem();
            this.tssStop = new System.Windows.Forms.ToolStripMenuItem();
            this.tssClear = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbxAppleIOS = new System.Windows.Forms.CheckBox();
            this.cbxAndroid = new System.Windows.Forms.CheckBox();
            this.lbxInfo = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssUserCount,
            this.toolStripStatusLabel1,
            this.tssSuccessMsg,
            this.toolStripStatusLabel2,
            this.tssErrorMsg});
            this.statusStrip1.Location = new System.Drawing.Point(0, 463);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(531, 25);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssUserCount
            // 
            this.tssUserCount.Name = "tssUserCount";
            this.tssUserCount.Size = new System.Drawing.Size(120, 20);
            this.tssUserCount.Text = "Abone Sayısı = 0";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(13, 20);
            this.toolStripStatusLabel1.Text = "|";
            // 
            // tssSuccessMsg
            // 
            this.tssSuccessMsg.Name = "tssSuccessMsg";
            this.tssSuccessMsg.Size = new System.Drawing.Size(158, 20);
            this.tssSuccessMsg.Text = "Giden Mesaj Sayısı = 0";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(13, 20);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // tssErrorMsg
            // 
            this.tssErrorMsg.Name = "tssErrorMsg";
            this.tssErrorMsg.Size = new System.Drawing.Size(159, 20);
            this.tssErrorMsg.Text = "Hatalı Mesaj Sayısı = 0";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssStart,
            this.tssStop,
            this.tssClear});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(531, 28);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tssStart
            // 
            this.tssStart.Name = "tssStart";
            this.tssStart.Size = new System.Drawing.Size(61, 24);
            this.tssStart.Text = "Başlat";
            this.tssStart.Click += new System.EventHandler(this.tssStartMenu_Click);
            // 
            // tssStop
            // 
            this.tssStop.Name = "tssStop";
            this.tssStop.Size = new System.Drawing.Size(67, 24);
            this.tssStop.Text = "Durdur";
            this.tssStop.Click += new System.EventHandler(this.tssStopMenu_Click);
            // 
            // tssClear
            // 
            this.tssClear.Name = "tssClear";
            this.tssClear.Size = new System.Drawing.Size(72, 24);
            this.tssClear.Text = "Temizle";
            this.tssClear.Click += new System.EventHandler(this.temizleToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(531, 435);
            this.tabControl1.TabIndex = 18;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.lbxInfo);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(523, 406);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Mesajlar";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbxAppleIOS);
            this.panel1.Controls.Add(this.cbxAndroid);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(517, 51);
            this.panel1.TabIndex = 18;
            // 
            // cbxAppleIOS
            // 
            this.cbxAppleIOS.AutoSize = true;
            this.cbxAppleIOS.Checked = true;
            this.cbxAppleIOS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxAppleIOS.Location = new System.Drawing.Point(101, 14);
            this.cbxAppleIOS.Name = "cbxAppleIOS";
            this.cbxAppleIOS.Size = new System.Drawing.Size(93, 21);
            this.cbxAppleIOS.TabIndex = 1;
            this.cbxAppleIOS.Text = "Apple IOS";
            this.cbxAppleIOS.UseVisualStyleBackColor = true;
            // 
            // cbxAndroid
            // 
            this.cbxAndroid.AutoSize = true;
            this.cbxAndroid.Checked = true;
            this.cbxAndroid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxAndroid.Location = new System.Drawing.Point(16, 14);
            this.cbxAndroid.Name = "cbxAndroid";
            this.cbxAndroid.Size = new System.Drawing.Size(79, 21);
            this.cbxAndroid.TabIndex = 0;
            this.cbxAndroid.Text = "Android";
            this.cbxAndroid.UseVisualStyleBackColor = true;
            // 
            // lbxInfo
            // 
            this.lbxInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbxInfo.FormattingEnabled = true;
            this.lbxInfo.ItemHeight = 16;
            this.lbxInfo.Location = new System.Drawing.Point(3, 63);
            this.lbxInfo.Name = "lbxInfo";
            this.lbxInfo.Size = new System.Drawing.Size(517, 340);
            this.lbxInfo.TabIndex = 17;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lblMessage);
            this.tabPage3.Controls.Add(this.lblTitle);
            this.tabPage3.Controls.Add(this.txtTitle);
            this.tabPage3.Controls.Add(this.txtMessage);
            this.tabPage3.Controls.Add(this.btnSend);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(489, 406);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "Mesaj Gönder";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(7, 62);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(45, 17);
            this.lblMessage.TabIndex = 19;
            this.lblMessage.Text = "Mesaj";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(7, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(45, 17);
            this.lblTitle.TabIndex = 20;
            this.lblTitle.Text = "Başlık";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(8, 34);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(456, 22);
            this.txtTitle.TabIndex = 18;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(8, 85);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(454, 139);
            this.txtMessage.TabIndex = 17;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(6, 230);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(91, 41);
            this.btnSend.TabIndex = 16;
            this.btnSend.Text = "Gönder";
            this.btnSend.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 488);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "TEB Yatırım Mobil Notifikasyon Gönderici";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
 
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssUserCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tssSuccessMsg;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel tssErrorMsg;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tssStart;
        private System.Windows.Forms.ToolStripMenuItem tssStop;
        private System.Windows.Forms.ToolStripMenuItem tssClear;
  
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListBox lbxInfo;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbxAppleIOS;
        private System.Windows.Forms.CheckBox cbxAndroid;
    }
}

