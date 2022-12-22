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
			this.m_menu = new System.Windows.Forms.MenuStrip();
			this.m_file = new System.Windows.Forms.ToolStripMenuItem();
			this.m_fileexit = new System.Windows.Forms.ToolStripMenuItem();
			this.m_help = new System.Windows.Forms.ToolStripMenuItem();
			this.m_helpabout = new System.Windows.Forms.ToolStripMenuItem();
			this.m_statusstrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
			this.cardSelector1 = new zuki.ronin.ui.CardSelector();
			this.cardImage1 = new zuki.ronin.ui.CardImage();
			this.rulingsView1 = new zuki.ronin.ui.RulingsView();
			this.m_menu.SuspendLayout();
			this.m_statusstrip.SuspendLayout();
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
			this.m_fileexit.Size = new System.Drawing.Size(93, 22);
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
			// m_statusstrip
			// 
			this.m_statusstrip.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_statusstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel4});
			this.m_statusstrip.Location = new System.Drawing.Point(0, 487);
			this.m_statusstrip.Name = "m_statusstrip";
			this.m_statusstrip.Size = new System.Drawing.Size(933, 32);
			this.m_statusstrip.TabIndex = 2;
			this.m_statusstrip.Text = "m_statusstrip";
			// 
			// toolStripStatusLabel3
			// 
			this.toolStripStatusLabel3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.toolStripStatusLabel3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripStatusLabel3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
			this.toolStripStatusLabel3.Padding = new System.Windows.Forms.Padding(4);
			this.toolStripStatusLabel3.Size = new System.Drawing.Size(79, 27);
			this.toolStripStatusLabel3.Text = "2840 Cards";
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripStatusLabel2.Font = new System.Drawing.Font("Segoe MDL2 Assets", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Padding = new System.Windows.Forms.Padding(4, 4, 0, 4);
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(27, 27);
			this.toolStripStatusLabel2.Text = "";
			this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Padding = new System.Windows.Forms.Padding(0, 4, 4, 4);
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(115, 27);
			this.toolStripStatusLabel1.Text = "September 1, 2008";
			this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusLabel4
			// 
			this.toolStripStatusLabel4.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.toolStripStatusLabel4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripStatusLabel4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
			this.toolStripStatusLabel4.Padding = new System.Windows.Forms.Padding(4);
			this.toolStripStatusLabel4.Size = new System.Drawing.Size(172, 27);
			this.toolStripStatusLabel4.Text = "Rulebook Version 6.0 Format";
			// 
			// cardSelector1
			// 
			this.cardSelector1.Dock = System.Windows.Forms.DockStyle.Left;
			this.cardSelector1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cardSelector1.Location = new System.Drawing.Point(0, 24);
			this.cardSelector1.Name = "cardSelector1";
			this.cardSelector1.Padding = new System.Windows.Forms.Padding(4);
			this.cardSelector1.Size = new System.Drawing.Size(316, 463);
			this.cardSelector1.TabIndex = 3;
			this.cardSelector1.SelectionChanged += new System.EventHandler<zuki.ronin.data.Card>(this.OnCardSelectionChanged);
			// 
			// cardImage1
			// 
			this.cardImage1.Dock = System.Windows.Forms.DockStyle.Left;
			this.cardImage1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cardImage1.Location = new System.Drawing.Point(316, 24);
			this.cardImage1.Name = "cardImage1";
			this.cardImage1.Padding = new System.Windows.Forms.Padding(4);
			this.cardImage1.Size = new System.Drawing.Size(325, 463);
			this.cardImage1.TabIndex = 4;
			// 
			// rulingsView1
			// 
			this.rulingsView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rulingsView1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rulingsView1.Location = new System.Drawing.Point(641, 24);
			this.rulingsView1.Name = "rulingsView1";
			this.rulingsView1.Size = new System.Drawing.Size(292, 463);
			this.rulingsView1.TabIndex = 5;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(933, 519);
			this.Controls.Add(this.rulingsView1);
			this.Controls.Add(this.cardImage1);
			this.Controls.Add(this.cardSelector1);
			this.Controls.Add(this.m_statusstrip);
			this.Controls.Add(this.m_menu);
			this.DoubleBuffered = true;
			this.Location = new System.Drawing.Point(0, 0);
			this.Name = "MainForm";
			this.Text = "RONIN";
			this.Load += new System.EventHandler(this.OnLoad);
			this.m_menu.ResumeLayout(false);
			this.m_menu.PerformLayout();
			this.m_statusstrip.ResumeLayout(false);
			this.m_statusstrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

        #endregion
        private System.Windows.Forms.MenuStrip m_menu;
        private System.Windows.Forms.ToolStripMenuItem m_file;
        private System.Windows.Forms.ToolStripMenuItem m_fileexit;
        private System.Windows.Forms.ToolStripMenuItem m_help;
        private System.Windows.Forms.ToolStripMenuItem m_helpabout;
		private System.Windows.Forms.StatusStrip m_statusstrip;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
		private ui.CardSelector cardSelector1;
		private ui.CardImage cardImage1;
		private ui.RulingsView rulingsView1;
	}
}