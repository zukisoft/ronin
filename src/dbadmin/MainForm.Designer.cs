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
			this.m_opendatabase = new System.Windows.Forms.OpenFileDialog();
			this.m_statusstrip = new System.Windows.Forms.StatusStrip();
			this.m_statuslabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.m_menu = new System.Windows.Forms.MenuStrip();
			this.m_filemenu = new System.Windows.Forms.ToolStripMenuItem();
			this.m_fileexit = new System.Windows.Forms.ToolStripMenuItem();
			this.m_databasemenu = new System.Windows.Forms.ToolStripMenuItem();
			this.m_databasevacuum = new System.Windows.Forms.ToolStripMenuItem();
			this.m_managemenu = new System.Windows.Forms.ToolStripMenuItem();
			this.m_manageartwork = new System.Windows.Forms.ToolStripMenuItem();
			this.m_managecards = new System.Windows.Forms.ToolStripMenuItem();
			this.m_exportmenu = new System.Windows.Forms.ToolStripMenuItem();
			this.m_exportartwork = new System.Windows.Forms.ToolStripMenuItem();
			this.m_exportcardimages = new System.Windows.Forms.ToolStripMenuItem();
			this.m_windowmenu = new System.Windows.Forms.ToolStripMenuItem();
			this.m_windowcloseall = new System.Windows.Forms.ToolStripMenuItem();
			this.m_managerestrictionlists = new System.Windows.Forms.ToolStripMenuItem();
			this.m_statusstrip.SuspendLayout();
			this.m_menu.SuspendLayout();
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
			// m_menu
			// 
			this.m_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_filemenu,
            this.m_databasemenu,
            this.m_managemenu,
            this.m_exportmenu,
            this.m_windowmenu});
			this.m_menu.Location = new System.Drawing.Point(0, 0);
			this.m_menu.MdiWindowListItem = this.m_windowmenu;
			this.m_menu.Name = "m_menu";
			this.m_menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.m_menu.Size = new System.Drawing.Size(933, 24);
			this.m_menu.TabIndex = 3;
			this.m_menu.Text = "m_menu";
			// 
			// m_filemenu
			// 
			this.m_filemenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_fileexit});
			this.m_filemenu.Name = "m_filemenu";
			this.m_filemenu.Size = new System.Drawing.Size(37, 20);
			this.m_filemenu.Text = "&File";
			// 
			// m_fileexit
			// 
			this.m_fileexit.Name = "m_fileexit";
			this.m_fileexit.Size = new System.Drawing.Size(93, 22);
			this.m_fileexit.Text = "E&xit";
			this.m_fileexit.Click += new System.EventHandler(this.OnFileExit);
			// 
			// m_databasemenu
			// 
			this.m_databasemenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_databasevacuum});
			this.m_databasemenu.Name = "m_databasemenu";
			this.m_databasemenu.Size = new System.Drawing.Size(67, 20);
			this.m_databasemenu.Text = "&Database";
			// 
			// m_databasevacuum
			// 
			this.m_databasevacuum.Name = "m_databasevacuum";
			this.m_databasevacuum.Size = new System.Drawing.Size(117, 22);
			this.m_databasevacuum.Text = "&Vacuum";
			this.m_databasevacuum.Click += new System.EventHandler(this.OnDatabaseVacuum);
			// 
			// m_managemenu
			// 
			this.m_managemenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_manageartwork,
            this.m_managecards,
            this.m_managerestrictionlists});
			this.m_managemenu.Name = "m_managemenu";
			this.m_managemenu.Size = new System.Drawing.Size(62, 20);
			this.m_managemenu.Text = "&Manage";
			// 
			// m_manageartwork
			// 
			this.m_manageartwork.Name = "m_manageartwork";
			this.m_manageartwork.Size = new System.Drawing.Size(180, 22);
			this.m_manageartwork.Text = "&Artwork...";
			this.m_manageartwork.Click += new System.EventHandler(this.OnManageArtwork);
			// 
			// m_managecards
			// 
			this.m_managecards.Name = "m_managecards";
			this.m_managecards.Size = new System.Drawing.Size(180, 22);
			this.m_managecards.Text = "&Cards...";
			this.m_managecards.Click += new System.EventHandler(this.OnManageCards);
			// 
			// m_exportmenu
			// 
			this.m_exportmenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_exportartwork,
            this.m_exportcardimages});
			this.m_exportmenu.Name = "m_exportmenu";
			this.m_exportmenu.Size = new System.Drawing.Size(53, 20);
			this.m_exportmenu.Text = "E&xport";
			// 
			// m_exportartwork
			// 
			this.m_exportartwork.Name = "m_exportartwork";
			this.m_exportartwork.Size = new System.Drawing.Size(149, 22);
			this.m_exportartwork.Text = "&Artwork...";
			this.m_exportartwork.Click += new System.EventHandler(this.OnExportArtwork);
			// 
			// m_exportcardimages
			// 
			this.m_exportcardimages.Name = "m_exportcardimages";
			this.m_exportcardimages.Size = new System.Drawing.Size(149, 22);
			this.m_exportcardimages.Text = "&Card Images...";
			this.m_exportcardimages.Click += new System.EventHandler(this.OnExportCardImages);
			// 
			// m_windowmenu
			// 
			this.m_windowmenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_windowcloseall});
			this.m_windowmenu.Name = "m_windowmenu";
			this.m_windowmenu.Size = new System.Drawing.Size(63, 20);
			this.m_windowmenu.Text = "&Window";
			// 
			// m_windowcloseall
			// 
			this.m_windowcloseall.Name = "m_windowcloseall";
			this.m_windowcloseall.Size = new System.Drawing.Size(120, 22);
			this.m_windowcloseall.Text = "&Close All";
			this.m_windowcloseall.Click += new System.EventHandler(this.OnWindowCloseAll);
			// 
			// m_managerestrictionlists
			// 
			this.m_managerestrictionlists.Name = "m_managerestrictionlists";
			this.m_managerestrictionlists.Size = new System.Drawing.Size(180, 22);
			this.m_managerestrictionlists.Text = "Restriction &Lists...";
			this.m_managerestrictionlists.Click += new System.EventHandler(this.OnManageRestrictionLists);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(933, 519);
			this.Controls.Add(this.m_statusstrip);
			this.Controls.Add(this.m_menu);
			this.IsMdiContainer = true;
			this.Location = new System.Drawing.Point(0, 0);
			this.MainMenuStrip = this.m_menu;
			this.Name = "MainForm";
			this.Text = "RONIN Database Administration";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.OnLoad);
			this.m_statusstrip.ResumeLayout(false);
			this.m_statusstrip.PerformLayout();
			this.m_menu.ResumeLayout(false);
			this.m_menu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

        #endregion

        private System.Windows.Forms.OpenFileDialog m_opendatabase;
        private System.Windows.Forms.StatusStrip m_statusstrip;
        private System.Windows.Forms.ToolStripStatusLabel m_statuslabel;
		private System.Windows.Forms.MenuStrip m_menu;
		private System.Windows.Forms.ToolStripMenuItem m_filemenu;
		private System.Windows.Forms.ToolStripMenuItem m_fileexit;
		private System.Windows.Forms.ToolStripMenuItem m_managemenu;
		private System.Windows.Forms.ToolStripMenuItem m_manageartwork;
		private System.Windows.Forms.ToolStripMenuItem m_exportmenu;
		private System.Windows.Forms.ToolStripMenuItem m_exportartwork;
		private System.Windows.Forms.ToolStripMenuItem m_windowmenu;
		private System.Windows.Forms.ToolStripMenuItem m_windowcloseall;
		private System.Windows.Forms.ToolStripMenuItem m_managecards;
		private System.Windows.Forms.ToolStripMenuItem m_databasemenu;
		private System.Windows.Forms.ToolStripMenuItem m_databasevacuum;
		private System.Windows.Forms.ToolStripMenuItem m_exportcardimages;
		private System.Windows.Forms.ToolStripMenuItem m_managerestrictionlists;
	}
}