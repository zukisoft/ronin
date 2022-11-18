namespace zuki.ronin.ui
{
	partial class CardImage
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.m_image = new zuki.ronin.ui.InterpolatingPictureBox();
			((System.ComponentModel.ISupportInitialize)(this.m_image)).BeginInit();
			this.SuspendLayout();
			// 
			// m_image
			// 
			this.m_image.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_image.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			this.m_image.Location = new System.Drawing.Point(0, 0);
			this.m_image.Name = "m_image";
			this.m_image.Size = new System.Drawing.Size(150, 150);
			this.m_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.m_image.TabIndex = 0;
			this.m_image.TabStop = false;
			// 
			// CardImage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.m_image);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "CardImage";
			((System.ComponentModel.ISupportInitialize)(this.m_image)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private InterpolatingPictureBox m_image;
	}
}
