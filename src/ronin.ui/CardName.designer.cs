namespace zuki.ronin.ui
{
	partial class CardName
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
			this.m_name = new System.Windows.Forms.Label();
			this.m_stats = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// m_name
			// 
			this.m_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_name.AutoEllipsis = true;
			this.m_name.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_name.Location = new System.Drawing.Point(12, 12);
			this.m_name.Name = "m_name";
			this.m_name.Size = new System.Drawing.Size(276, 20);
			this.m_name.TabIndex = 0;
			this.m_name.Text = "{ m_name }";
			this.m_name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// m_stats
			// 
			this.m_stats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_stats.AutoEllipsis = true;
			this.m_stats.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_stats.Location = new System.Drawing.Point(13, 34);
			this.m_stats.Name = "m_stats";
			this.m_stats.Size = new System.Drawing.Size(276, 15);
			this.m_stats.TabIndex = 1;
			this.m_stats.Text = "{ m_stats }";
			this.m_stats.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CardName
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.m_name);
			this.Controls.Add(this.m_stats);
			this.DoubleBuffered = true;
			this.Name = "CardName";
			this.Size = new System.Drawing.Size(300, 65);
			this.Load += new System.EventHandler(this.OnLoad);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label m_stats;
		private System.Windows.Forms.Label m_name;
	}
}
