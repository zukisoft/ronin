namespace zuki.ronin
{
	partial class AboutDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
			this.m_icon = new System.Windows.Forms.PictureBox();
			this.m_version = new System.Windows.Forms.Label();
			this.m_static2 = new System.Windows.Forms.Label();
			this.m_static3 = new System.Windows.Forms.Label();
			this.m_static4 = new System.Windows.Forms.Label();
			this.m_ok = new System.Windows.Forms.Button();
			this.m_static5 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.m_icon)).BeginInit();
			this.SuspendLayout();
			// 
			// m_icon
			// 
			this.m_icon.Image = ((System.Drawing.Image)(resources.GetObject("m_icon.Image")));
			this.m_icon.Location = new System.Drawing.Point(13, 13);
			this.m_icon.Name = "m_icon";
			this.m_icon.Size = new System.Drawing.Size(100, 100);
			this.m_icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.m_icon.TabIndex = 0;
			this.m_icon.TabStop = false;
			// 
			// m_version
			// 
			this.m_version.AutoSize = true;
			this.m_version.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_version.Location = new System.Drawing.Point(120, 17);
			this.m_version.Name = "m_version";
			this.m_version.Size = new System.Drawing.Size(69, 15);
			this.m_version.TabIndex = 1;
			this.m_version.Text = "{m_version}";
			// 
			// m_static2
			// 
			this.m_static2.AutoSize = true;
			this.m_static2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_static2.Location = new System.Drawing.Point(120, 35);
			this.m_static2.Name = "m_static2";
			this.m_static2.Size = new System.Drawing.Size(244, 15);
			this.m_static2.TabIndex = 2;
			this.m_static2.Text = "Rulings Online Networked Information Node";
			// 
			// m_static3
			// 
			this.m_static3.AutoSize = true;
			this.m_static3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_static3.Location = new System.Drawing.Point(119, 80);
			this.m_static3.Name = "m_static3";
			this.m_static3.Size = new System.Drawing.Size(235, 15);
			this.m_static3.TabIndex = 3;
			this.m_static3.Text = "Copyright © 2007-2009 Sean M. Patterson";
			// 
			// m_static4
			// 
			this.m_static4.AutoSize = true;
			this.m_static4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_static4.Location = new System.Drawing.Point(119, 98);
			this.m_static4.Name = "m_static4";
			this.m_static4.Size = new System.Drawing.Size(107, 15);
			this.m_static4.TabIndex = 4;
			this.m_static4.Text = "All Rights Reserved";
			// 
			// m_ok
			// 
			this.m_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.m_ok.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_ok.Location = new System.Drawing.Point(297, 131);
			this.m_ok.Name = "m_ok";
			this.m_ok.Size = new System.Drawing.Size(75, 23);
			this.m_ok.TabIndex = 6;
			this.m_ok.Text = "OK";
			this.m_ok.UseVisualStyleBackColor = true;
			// 
			// m_static5
			// 
			this.m_static5.AutoSize = true;
			this.m_static5.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_static5.Location = new System.Drawing.Point(119, 62);
			this.m_static5.Name = "m_static5";
			this.m_static5.Size = new System.Drawing.Size(234, 15);
			this.m_static5.TabIndex = 7;
			this.m_static5.Text = "Copyright © 2004-2022 Michael G. Brehm";
			// 
			// AboutDialog
			// 
			this.AcceptButton = this.m_ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.CancelButton = this.m_ok;
			this.ClientSize = new System.Drawing.Size(384, 166);
			this.Controls.Add(this.m_static5);
			this.Controls.Add(this.m_ok);
			this.Controls.Add(this.m_static4);
			this.Controls.Add(this.m_static3);
			this.Controls.Add(this.m_static2);
			this.Controls.Add(this.m_version);
			this.Controls.Add(this.m_icon);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About RONIN";
			((System.ComponentModel.ISupportInitialize)(this.m_icon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

        #endregion

        private System.Windows.Forms.PictureBox m_icon;
        private System.Windows.Forms.Label m_version;
        private System.Windows.Forms.Label m_static2;
        private System.Windows.Forms.Label m_static3;
        private System.Windows.Forms.Label m_static4;
        private System.Windows.Forms.Button m_ok;
        private System.Windows.Forms.Label m_static5;
    }
}