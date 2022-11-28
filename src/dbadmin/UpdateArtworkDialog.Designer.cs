namespace zuki.ronin
{
	partial class UpdateArtworkDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateArtworkDialog));
			this.m_cancel = new System.Windows.Forms.Button();
			this.m_ok = new System.Windows.Forms.Button();
			this.m_original = new System.Windows.Forms.PictureBox();
			this.m_updated = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.m_original)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_updated)).BeginInit();
			this.SuspendLayout();
			// 
			// m_cancel
			// 
			this.m_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_cancel.Location = new System.Drawing.Point(755, 423);
			this.m_cancel.Name = "m_cancel";
			this.m_cancel.Size = new System.Drawing.Size(75, 23);
			this.m_cancel.TabIndex = 0;
			this.m_cancel.Text = "Cancel";
			this.m_cancel.UseVisualStyleBackColor = true;
			// 
			// m_ok
			// 
			this.m_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.m_ok.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_ok.Location = new System.Drawing.Point(674, 423);
			this.m_ok.Name = "m_ok";
			this.m_ok.Size = new System.Drawing.Size(75, 23);
			this.m_ok.TabIndex = 1;
			this.m_ok.Text = "OK";
			this.m_ok.UseVisualStyleBackColor = true;
			// 
			// m_original
			// 
			this.m_original.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.m_original.Location = new System.Drawing.Point(13, 13);
			this.m_original.Name = "m_original";
			this.m_original.Size = new System.Drawing.Size(400, 400);
			this.m_original.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.m_original.TabIndex = 2;
			this.m_original.TabStop = false;
			// 
			// m_updated
			// 
			this.m_updated.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_updated.Location = new System.Drawing.Point(428, 13);
			this.m_updated.Name = "m_updated";
			this.m_updated.Size = new System.Drawing.Size(400, 400);
			this.m_updated.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.m_updated.TabIndex = 3;
			this.m_updated.TabStop = false;
			// 
			// UpdateArtworkDialog
			// 
			this.AcceptButton = this.m_cancel;
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.CancelButton = this.m_ok;
			this.ClientSize = new System.Drawing.Size(842, 458);
			this.Controls.Add(this.m_updated);
			this.Controls.Add(this.m_original);
			this.Controls.Add(this.m_ok);
			this.Controls.Add(this.m_cancel);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UpdateArtworkDialog";
			this.Text = "Update Artwork";
			((System.ComponentModel.ISupportInitialize)(this.m_original)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_updated)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button m_cancel;
		private System.Windows.Forms.Button m_ok;
		private System.Windows.Forms.PictureBox m_original;
		private System.Windows.Forms.PictureBox m_updated;
	}
}