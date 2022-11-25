namespace zuki.ronin
{
	partial class ArtworkTile
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
			this.m_lowerpanel = new System.Windows.Forms.Panel();
			this.m_update = new System.Windows.Forms.Button();
			this.m_setdefault = new System.Windows.Forms.Button();
			this.m_image = new zuki.ronin.ui.PictureBox();
			this.m_openfile = new System.Windows.Forms.OpenFileDialog();
			this.m_lowerpanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_image)).BeginInit();
			this.SuspendLayout();
			// 
			// m_lowerpanel
			// 
			this.m_lowerpanel.Controls.Add(this.m_setdefault);
			this.m_lowerpanel.Controls.Add(this.m_update);
			this.m_lowerpanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.m_lowerpanel.Location = new System.Drawing.Point(4, 314);
			this.m_lowerpanel.Name = "m_lowerpanel";
			this.m_lowerpanel.Size = new System.Drawing.Size(301, 33);
			this.m_lowerpanel.TabIndex = 0;
			// 
			// m_update
			// 
			this.m_update.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_update.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_update.Location = new System.Drawing.Point(226, 10);
			this.m_update.Name = "m_update";
			this.m_update.Size = new System.Drawing.Size(75, 23);
			this.m_update.TabIndex = 2;
			this.m_update.Text = "Update...";
			this.m_update.UseVisualStyleBackColor = true;
			this.m_update.Click += new System.EventHandler(this.OnUpdate);
			// 
			// m_setdefault
			// 
			this.m_setdefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.m_setdefault.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_setdefault.Location = new System.Drawing.Point(0, 10);
			this.m_setdefault.Name = "m_setdefault";
			this.m_setdefault.Size = new System.Drawing.Size(75, 23);
			this.m_setdefault.TabIndex = 3;
			this.m_setdefault.Text = "Set Default";
			this.m_setdefault.UseVisualStyleBackColor = true;
			this.m_setdefault.Click += new System.EventHandler(this.OnSetDefault);
			// 
			// m_image
			// 
			this.m_image.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_image.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_image.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			this.m_image.Location = new System.Drawing.Point(4, 4);
			this.m_image.Name = "m_image";
			this.m_image.Size = new System.Drawing.Size(301, 310);
			this.m_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.m_image.TabIndex = 1;
			this.m_image.TabStop = false;
			// 
			// m_openfile
			// 
			this.m_openfile.AddExtension = false;
			this.m_openfile.Filter = "JPEG files|*.jpg|PNG files|*.png|BMP files|*.bmp|All files|*.*";
			// 
			// ArtworkTile
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.m_image);
			this.Controls.Add(this.m_lowerpanel);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "ArtworkTile";
			this.Padding = new System.Windows.Forms.Padding(4);
			this.Size = new System.Drawing.Size(309, 351);
			this.m_lowerpanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.m_image)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel m_lowerpanel;
		private ui.PictureBox m_image;
		private System.Windows.Forms.Button m_setdefault;
		private System.Windows.Forms.Button m_update;
		private System.Windows.Forms.OpenFileDialog m_openfile;
	}
}
