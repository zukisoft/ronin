namespace zuki.ronin
{
	partial class BackgroundTaskDialog
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
			if(disposing && (components != null))
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
			this.m_oneshot = new System.Windows.Forms.Timer(this.components);
			this.m_panel = new System.Windows.Forms.Panel();
			this.m_marquee = new System.Windows.Forms.ProgressBar();
			this.m_banner = new System.Windows.Forms.Label();
			this.m_worker = new System.ComponentModel.BackgroundWorker();
			this.m_panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_oneshot
			// 
			this.m_oneshot.Tick += new System.EventHandler(this.OnOneShot);
			// 
			// m_panel
			// 
			this.m_panel.Controls.Add(this.m_marquee);
			this.m_panel.Controls.Add(this.m_banner);
			this.m_panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_panel.Location = new System.Drawing.Point(4, 4);
			this.m_panel.Name = "m_panel";
			this.m_panel.Size = new System.Drawing.Size(432, 55);
			this.m_panel.TabIndex = 0;
			this.m_panel.UseWaitCursor = true;
			// 
			// m_marquee
			// 
			this.m_marquee.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_marquee.Location = new System.Drawing.Point(8, 33);
			this.m_marquee.Margin = new System.Windows.Forms.Padding(2);
			this.m_marquee.MarqueeAnimationSpeed = 20;
			this.m_marquee.Name = "m_marquee";
			this.m_marquee.Size = new System.Drawing.Size(417, 15);
			this.m_marquee.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.m_marquee.TabIndex = 1;
			this.m_marquee.UseWaitCursor = true;
			// 
			// m_banner
			// 
			this.m_banner.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_banner.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_banner.Location = new System.Drawing.Point(0, 0);
			this.m_banner.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.m_banner.Name = "m_banner";
			this.m_banner.Size = new System.Drawing.Size(432, 31);
			this.m_banner.TabIndex = 0;
			this.m_banner.Text = "{m_banner}";
			this.m_banner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.m_banner.UseWaitCursor = true;
			// 
			// BackgroundTaskDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(440, 63);
			this.ControlBox = false;
			this.Controls.Add(this.m_panel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BackgroundTaskDialog";
			this.Padding = new System.Windows.Forms.Padding(4);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.UseWaitCursor = true;
			this.Load += new System.EventHandler(this.OnLoad);
			this.m_panel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer m_oneshot;
		private System.Windows.Forms.Panel m_panel;
		private System.Windows.Forms.Label m_banner;
		private System.Windows.Forms.ProgressBar m_marquee;
		private System.ComponentModel.BackgroundWorker m_worker;
	}
}