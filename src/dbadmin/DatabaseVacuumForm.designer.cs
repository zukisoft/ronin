namespace zuki.ronin
{
	partial class DatabaseVacuumForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseVacuumForm));
			this.m_originallabel = new System.Windows.Forms.Label();
			this.m_currentlabel = new System.Windows.Forms.Label();
			this.m_original = new System.Windows.Forms.Label();
			this.m_current = new System.Windows.Forms.Label();
			this.m_vacuum = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// m_originallabel
			// 
			this.m_originallabel.AutoSize = true;
			this.m_originallabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_originallabel.Location = new System.Drawing.Point(13, 13);
			this.m_originallabel.Name = "m_originallabel";
			this.m_originallabel.Size = new System.Drawing.Size(129, 15);
			this.m_originallabel.TabIndex = 0;
			this.m_originallabel.Text = "Original Database Size:";
			// 
			// m_currentlabel
			// 
			this.m_currentlabel.AutoSize = true;
			this.m_currentlabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_currentlabel.Location = new System.Drawing.Point(12, 34);
			this.m_currentlabel.Name = "m_currentlabel";
			this.m_currentlabel.Size = new System.Drawing.Size(126, 15);
			this.m_currentlabel.TabIndex = 1;
			this.m_currentlabel.Text = "Current Database Size:";
			// 
			// m_original
			// 
			this.m_original.AutoSize = true;
			this.m_original.Location = new System.Drawing.Point(149, 14);
			this.m_original.Name = "m_original";
			this.m_original.Size = new System.Drawing.Size(77, 15);
			this.m_original.TabIndex = 2;
			this.m_original.Text = "{ m_original }";
			// 
			// m_current
			// 
			this.m_current.AutoSize = true;
			this.m_current.Location = new System.Drawing.Point(149, 34);
			this.m_current.Name = "m_current";
			this.m_current.Size = new System.Drawing.Size(75, 15);
			this.m_current.TabIndex = 3;
			this.m_current.Text = "{ m_current }";
			// 
			// m_vacuum
			// 
			this.m_vacuum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_vacuum.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_vacuum.Location = new System.Drawing.Point(300, 62);
			this.m_vacuum.Name = "m_vacuum";
			this.m_vacuum.Size = new System.Drawing.Size(75, 23);
			this.m_vacuum.TabIndex = 4;
			this.m_vacuum.Text = "Vacuum";
			this.m_vacuum.UseVisualStyleBackColor = true;
			this.m_vacuum.Click += new System.EventHandler(this.OnVacuum);
			// 
			// DatabaseVacuumForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(387, 97);
			this.Controls.Add(this.m_vacuum);
			this.Controls.Add(this.m_current);
			this.Controls.Add(this.m_original);
			this.Controls.Add(this.m_currentlabel);
			this.Controls.Add(this.m_originallabel);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DatabaseVacuumForm";
			this.Text = "Vacuum Database";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label m_originallabel;
		private System.Windows.Forms.Label m_currentlabel;
		private System.Windows.Forms.Label m_original;
		private System.Windows.Forms.Label m_current;
		private System.Windows.Forms.Button m_vacuum;
	}
}