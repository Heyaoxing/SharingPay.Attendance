namespace SharingPay.Attendance
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axFP_CLOCK = new AxFP_CLOCKLib.AxFP_CLOCK();
            this.Start_Btn = new System.Windows.Forms.Button();
            this.Close_Btn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.notifyIconSystem = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Open = new System.Windows.Forms.ToolStripMenuItem();
            this.Hide = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.axFP_CLOCK)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // axFP_CLOCK
            // 
            this.axFP_CLOCK.Enabled = true;
            this.axFP_CLOCK.Location = new System.Drawing.Point(360, 286);
            this.axFP_CLOCK.Name = "axFP_CLOCK";
            this.axFP_CLOCK.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axFP_CLOCK.OcxState")));
            this.axFP_CLOCK.Size = new System.Drawing.Size(35, 17);
            this.axFP_CLOCK.TabIndex = 0;
            // 
            // Start_Btn
            // 
            this.Start_Btn.Location = new System.Drawing.Point(28, 219);
            this.Start_Btn.Name = "Start_Btn";
            this.Start_Btn.Size = new System.Drawing.Size(75, 23);
            this.Start_Btn.TabIndex = 1;
            this.Start_Btn.Text = "开始";
            this.Start_Btn.UseVisualStyleBackColor = true;
            this.Start_Btn.Click += new System.EventHandler(this.Start_Btn_Click);
            // 
            // Close_Btn
            // 
            this.Close_Btn.Location = new System.Drawing.Point(154, 220);
            this.Close_Btn.Name = "Close_Btn";
            this.Close_Btn.Size = new System.Drawing.Size(75, 23);
            this.Close_Btn.TabIndex = 2;
            this.Close_Btn.Text = "关闭";
            this.Close_Btn.UseVisualStyleBackColor = true;
            this.Close_Btn.Click += new System.EventHandler(this.Close_Btn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(297, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "更新全部";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // notifyIconSystem
            // 
            this.notifyIconSystem.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIconSystem.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconSystem.Icon")));
            this.notifyIconSystem.Text = "考勤提醒系统";
            this.notifyIconSystem.Visible = true;
            this.notifyIconSystem.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconSystem_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Open,
            this.Hide});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 48);
            // 
            // Open
            // 
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(152, 22);
            this.Open.Text = "打开";
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // Hide
            // 
            this.Hide.Name = "Hide";
            this.Hide.Size = new System.Drawing.Size(152, 22);
            this.Hide.Text = "隐藏";
            this.Hide.Click += new System.EventHandler(this.Hide_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 305);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Close_Btn);
            this.Controls.Add(this.Start_Btn);
            this.Controls.Add(this.axFP_CLOCK);
            this.IsMdiContainer = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.axFP_CLOCK)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AxFP_CLOCKLib.AxFP_CLOCK axFP_CLOCK;
        private System.Windows.Forms.Button Start_Btn;
        private System.Windows.Forms.Button Close_Btn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NotifyIcon notifyIconSystem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Open;
        private System.Windows.Forms.ToolStripMenuItem Hide;
    }
}

