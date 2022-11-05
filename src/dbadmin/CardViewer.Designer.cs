namespace zuki.ronin
{
	partial class CardViewer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardViewer));
			this.m_cardlistview = new zuki.ronin.ui.CardListView();
			this.SuspendLayout();
			// 
			// m_cardlistview
			// 
			this.m_cardlistview.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_cardlistview.Location = new System.Drawing.Point(13, 13);
			this.m_cardlistview.Name = "m_cardlistview";
			this.m_cardlistview.Size = new System.Drawing.Size(330, 457);
			this.m_cardlistview.TabIndex = 0;
			// 
			// CardViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(716, 547);
			this.Controls.Add(this.m_cardlistview);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "CardViewer";
			this.Text = "View Card";
			this.Load += new System.EventHandler(this.OnLoad);
			this.ResumeLayout(false);

		}

        #endregion

        private ui.CardListView m_cardlistview;
    }
}