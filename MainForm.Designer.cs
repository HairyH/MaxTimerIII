namespace MaxTimerIII
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.ListBoxHistory = new System.Windows.Forms.ListBox();
            this.ComboBoxInterval = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CheckBoxSendTxt = new System.Windows.Forms.CheckBox();
            this.ButtonHistoryClear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnGetPt = new System.Windows.Forms.Button();
            this.BtnStart = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.TxtClock = new System.Windows.Forms.TextBox();
            this.TxtCurrentNicks = new System.Windows.Forms.TextBox();
            this.TxtMaxNicks = new System.Windows.Forms.TextBox();
            this.TimerMic = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TimerMonitor = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // ListBoxHistory
            // 
            this.ListBoxHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListBoxHistory.FormattingEnabled = true;
            this.ListBoxHistory.Location = new System.Drawing.Point(2, 43);
            this.ListBoxHistory.Name = "ListBoxHistory";
            this.ListBoxHistory.Size = new System.Drawing.Size(191, 251);
            this.ListBoxHistory.TabIndex = 0;
            this.ListBoxHistory.TabStop = false;
            // 
            // ComboBoxInterval
            // 
            this.ComboBoxInterval.FormattingEnabled = true;
            this.ComboBoxInterval.Items.AddRange(new object[] {
            "00:30 ",
            "01:00",
            "01:30",
            "02:00",
            "02:30",
            "03:00"});
            this.ComboBoxInterval.Location = new System.Drawing.Point(10, 319);
            this.ComboBoxInterval.Name = "ComboBoxInterval";
            this.ComboBoxInterval.Size = new System.Drawing.Size(52, 21);
            this.ComboBoxInterval.TabIndex = 4;
            this.ComboBoxInterval.SelectedIndexChanged += new System.EventHandler(this.ComboBoxInterval_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 303);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Interval";
            // 
            // CheckBoxSendTxt
            // 
            this.CheckBoxSendTxt.AutoSize = true;
            this.CheckBoxSendTxt.Checked = true;
            this.CheckBoxSendTxt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxSendTxt.Location = new System.Drawing.Point(72, 323);
            this.CheckBoxSendTxt.Name = "CheckBoxSendTxt";
            this.CheckBoxSendTxt.Size = new System.Drawing.Size(54, 17);
            this.CheckBoxSendTxt.TabIndex = 5;
            this.CheckBoxSendTxt.Text = "Send ";
            this.CheckBoxSendTxt.UseVisualStyleBackColor = true;
            // 
            // ButtonHistoryClear
            // 
            this.ButtonHistoryClear.Location = new System.Drawing.Point(136, 317);
            this.ButtonHistoryClear.Name = "ButtonHistoryClear";
            this.ButtonHistoryClear.Size = new System.Drawing.Size(52, 23);
            this.ButtonHistoryClear.TabIndex = 6;
            this.ButtonHistoryClear.Text = "Clear";
            this.ButtonHistoryClear.UseVisualStyleBackColor = true;
            this.ButtonHistoryClear.Click += new System.EventHandler(this.ButtonHistoryClear_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 303);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Text To Pt";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(142, 303);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "History";
            // 
            // BtnGetPt
            // 
            this.BtnGetPt.Location = new System.Drawing.Point(10, 346);
            this.BtnGetPt.Name = "BtnGetPt";
            this.BtnGetPt.Size = new System.Drawing.Size(52, 23);
            this.BtnGetPt.TabIndex = 1;
            this.BtnGetPt.Text = "Get Pt";
            this.BtnGetPt.UseVisualStyleBackColor = true;
            this.BtnGetPt.Click += new System.EventHandler(this.BtnGetPt_Click);
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(73, 346);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(52, 23);
            this.BtnStart.TabIndex = 2;
            this.BtnStart.Text = "Start";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(136, 346);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(52, 23);
            this.BtnExit.TabIndex = 3;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // TxtClock
            // 
            this.TxtClock.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtClock.Location = new System.Drawing.Point(60, 6);
            this.TxtClock.Name = "TxtClock";
            this.TxtClock.ReadOnly = true;
            this.TxtClock.Size = new System.Drawing.Size(78, 31);
            this.TxtClock.TabIndex = 7;
            this.TxtClock.Text = "00:00";
            this.TxtClock.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TxtCurrentNicks
            // 
            this.TxtCurrentNicks.Location = new System.Drawing.Point(10, 17);
            this.TxtCurrentNicks.Name = "TxtCurrentNicks";
            this.TxtCurrentNicks.Size = new System.Drawing.Size(25, 20);
            this.TxtCurrentNicks.TabIndex = 8;
            this.TxtCurrentNicks.Text = "000";
            this.TxtCurrentNicks.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TxtMaxNicks
            // 
            this.TxtMaxNicks.Location = new System.Drawing.Point(163, 17);
            this.TxtMaxNicks.Name = "TxtMaxNicks";
            this.TxtMaxNicks.Size = new System.Drawing.Size(25, 20);
            this.TxtMaxNicks.TabIndex = 9;
            this.TxtMaxNicks.Text = "000";
            this.TxtMaxNicks.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TimerMic
            // 
            this.TimerMic.Interval = 1000;
            this.TimerMic.Tick += new System.EventHandler(this.TimerMic_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Nicks";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(159, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Max#";
            // 
            // TimerMonitor
            // 
            this.TimerMonitor.Interval = 1000;
            this.TimerMonitor.Tick += new System.EventHandler(this.TimerMonitor_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 371);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TxtMaxNicks);
            this.Controls.Add(this.TxtCurrentNicks);
            this.Controls.Add(this.TxtClock);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.BtnGetPt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ButtonHistoryClear);
            this.Controls.Add(this.CheckBoxSendTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ComboBoxInterval);
            this.Controls.Add(this.ListBoxHistory);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(210, 410);
            this.MinimumSize = new System.Drawing.Size(210, 410);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Pt Timer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ListBoxHistory;
        private System.Windows.Forms.ComboBox ComboBoxInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox CheckBoxSendTxt;
        private System.Windows.Forms.Button ButtonHistoryClear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnGetPt;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.TextBox TxtClock;
        private System.Windows.Forms.TextBox TxtCurrentNicks;
        private System.Windows.Forms.TextBox TxtMaxNicks;
        private System.Windows.Forms.Timer TimerMic;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer TimerMonitor;
    }
}

