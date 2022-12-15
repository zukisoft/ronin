namespace zuki.ronin.ui
{
	partial class CardSelector
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.m_toppanel = new System.Windows.Forms.Panel();
			this.m_filter = new System.Windows.Forms.TextBox();
			this.m_cardlistview = new zuki.ronin.ui.CardListView();
			this.m_toppanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_toppanel
			// 
			this.m_toppanel.Controls.Add(this.m_filter);
			this.m_toppanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_toppanel.Location = new System.Drawing.Point(0, 0);
			this.m_toppanel.Name = "m_toppanel";
			this.m_toppanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
			this.m_toppanel.Size = new System.Drawing.Size(294, 28);
			this.m_toppanel.TabIndex = 0;
			// 
			// m_filter
			// 
			this.m_filter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_filter.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_filter.Location = new System.Drawing.Point(0, 0);
			this.m_filter.Name = "m_filter";
			this.m_filter.Size = new System.Drawing.Size(294, 23);
			this.m_filter.TabIndex = 0;
			this.m_filter.TextChanged += new System.EventHandler(this.OnFilterTextChanged);
			// 
			// m_cardlistview
			// 
			this.m_cardlistview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_cardlistview.Location = new System.Drawing.Point(0, 28);
			this.m_cardlistview.Name = "m_cardlistview";
			this.m_cardlistview.Size = new System.Drawing.Size(294, 404);
			this.m_cardlistview.TabIndex = 1;
			this.m_cardlistview.SelectionChanged += new System.EventHandler<zuki.ronin.data.Card>(this.OnSelectionChanged);
			// 
			// CardSelector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.m_cardlistview);
			this.Controls.Add(this.m_toppanel);
			this.DoubleBuffered = true;
			this.Name = "CardSelector";
			this.Size = new System.Drawing.Size(294, 432);
			this.m_toppanel.ResumeLayout(false);
			this.m_toppanel.PerformLayout();
			this.ResumeLayout(false);

		}

        #endregion

        private System.Windows.Forms.Panel m_toppanel;
        private System.Windows.Forms.TextBox m_filter;
		private CardListView m_cardlistview;
	}
}
