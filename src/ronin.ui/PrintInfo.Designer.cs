namespace zuki.ronin.ui
{
	partial class PrintInfo
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
			this.m_image = new zuki.ronin.ui.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.m_image)).BeginInit();
			this.SuspendLayout();
			// 
			// m_image
			// 
			this.m_image.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_image.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			this.m_image.Location = new System.Drawing.Point(12, 12);
			this.m_image.Name = "m_image";
			this.m_image.Size = new System.Drawing.Size(259, 278);
			this.m_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.m_image.TabIndex = 0;
			this.m_image.TabStop = false;
			// 
			// PrintInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.m_image);
			this.DoubleBuffered = true;
			this.Name = "PrintInfo";
			this.Size = new System.Drawing.Size(277, 296);
			this.Load += new System.EventHandler(this.OnLoad);
			((System.ComponentModel.ISupportInitialize)(this.m_image)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private PictureBox m_image;
	}
}
