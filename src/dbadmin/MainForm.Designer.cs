namespace zuki.ronin
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.m_opendatabase = new System.Windows.Forms.OpenFileDialog();
			this.m_statusstrip = new System.Windows.Forms.StatusStrip();
			this.m_statuslabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.m_statusstrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_opendatabase
			// 
			this.m_opendatabase.FileName = "*.db";
			this.m_opendatabase.Filter = "Database files|*.db|All files|*.*";
			this.m_opendatabase.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
			this.m_opendatabase.RestoreDirectory = true;
			this.m_opendatabase.ShowReadOnly = true;
			this.m_opendatabase.Title = "Open RONIN Database";
			// 
			// m_statusstrip
			// 
			this.m_statusstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_statuslabel});
			this.m_statusstrip.Location = new System.Drawing.Point(0, 497);
			this.m_statusstrip.Name = "m_statusstrip";
			this.m_statusstrip.Size = new System.Drawing.Size(933, 22);
			this.m_statusstrip.TabIndex = 1;
			this.m_statusstrip.Text = "statusStrip1";
			// 
			// m_statuslabel
			// 
			this.m_statuslabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_statuslabel.Name = "m_statuslabel";
			this.m_statuslabel.Size = new System.Drawing.Size(0, 17);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(933, 519);
			this.Controls.Add(this.m_statusstrip);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.Name = "MainForm";
			this.Text = "RONIN Database Administration";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.OnLoad);
			this.m_statusstrip.ResumeLayout(false);
			this.m_statusstrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

        #endregion

        private System.Windows.Forms.OpenFileDialog m_opendatabase;
        private System.Windows.Forms.StatusStrip m_statusstrip;
        private System.Windows.Forms.ToolStripStatusLabel m_statuslabel;
    }
}