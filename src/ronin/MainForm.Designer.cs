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
			this.m_menu = new System.Windows.Forms.MenuStrip();
			this.m_file = new System.Windows.Forms.ToolStripMenuItem();
			this.m_fileexit = new System.Windows.Forms.ToolStripMenuItem();
			this.m_help = new System.Windows.Forms.ToolStripMenuItem();
			this.m_helpabout = new System.Windows.Forms.ToolStripMenuItem();
			this.m_menu.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_menu
			// 
			this.m_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_file,
            this.m_help});
			this.m_menu.Location = new System.Drawing.Point(0, 0);
			this.m_menu.Name = "m_menu";
			this.m_menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.m_menu.Size = new System.Drawing.Size(933, 24);
			this.m_menu.TabIndex = 1;
			this.m_menu.Text = "m_menustrip";
			// 
			// m_file
			// 
			this.m_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_fileexit});
			this.m_file.Name = "m_file";
			this.m_file.Size = new System.Drawing.Size(37, 20);
			this.m_file.Text = "&File";
			// 
			// m_fileexit
			// 
			this.m_fileexit.Name = "m_fileexit";
			this.m_fileexit.Size = new System.Drawing.Size(180, 22);
			this.m_fileexit.Text = "E&xit";
			this.m_fileexit.Click += new System.EventHandler(this.OnFileExit);
			// 
			// m_help
			// 
			this.m_help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_helpabout});
			this.m_help.Name = "m_help";
			this.m_help.Size = new System.Drawing.Size(44, 20);
			this.m_help.Text = "&Help";
			// 
			// m_helpabout
			// 
			this.m_helpabout.Name = "m_helpabout";
			this.m_helpabout.Size = new System.Drawing.Size(156, 22);
			this.m_helpabout.Text = "&About RONIN...";
			this.m_helpabout.Click += new System.EventHandler(this.OnHelpAbout);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(933, 519);
			this.Controls.Add(this.m_menu);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "MainForm";
			this.Text = "RONIN";
			this.Load += new System.EventHandler(this.OnLoad);
			this.m_menu.ResumeLayout(false);
			this.m_menu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

        #endregion
        private System.Windows.Forms.MenuStrip m_menu;
        private System.Windows.Forms.ToolStripMenuItem m_file;
        private System.Windows.Forms.ToolStripMenuItem m_fileexit;
        private System.Windows.Forms.ToolStripMenuItem m_help;
        private System.Windows.Forms.ToolStripMenuItem m_helpabout;
    }
}