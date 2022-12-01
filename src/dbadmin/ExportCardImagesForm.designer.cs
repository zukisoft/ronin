namespace zuki.ronin
{
	partial class ExportCardImagesForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportCardImagesForm));
			this.m_folder = new System.Windows.Forms.TextBox();
			this.m_browse = new System.Windows.Forms.Button();
			this.m_folderlabel = new System.Windows.Forms.Label();
			this.m_close = new System.Windows.Forms.Button();
			this.m_render = new System.Windows.Forms.Button();
			this.m_folderbrowser = new System.Windows.Forms.FolderBrowserDialog();
			this.SuspendLayout();
			// 
			// m_folder
			// 
			this.m_folder.Location = new System.Drawing.Point(66, 11);
			this.m_folder.Name = "m_folder";
			this.m_folder.Size = new System.Drawing.Size(377, 23);
			this.m_folder.TabIndex = 6;
			this.m_folder.TextChanged += new System.EventHandler(this.OnFolderChanged);
			// 
			// m_browse
			// 
			this.m_browse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_browse.Location = new System.Drawing.Point(449, 10);
			this.m_browse.Name = "m_browse";
			this.m_browse.Size = new System.Drawing.Size(34, 23);
			this.m_browse.TabIndex = 7;
			this.m_browse.Text = "...";
			this.m_browse.UseVisualStyleBackColor = true;
			this.m_browse.Click += new System.EventHandler(this.OnBrowse);
			// 
			// m_folderlabel
			// 
			this.m_folderlabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_folderlabel.Location = new System.Drawing.Point(12, 14);
			this.m_folderlabel.Name = "m_folderlabel";
			this.m_folderlabel.Size = new System.Drawing.Size(48, 15);
			this.m_folderlabel.TabIndex = 8;
			this.m_folderlabel.Text = "Folder:";
			// 
			// m_close
			// 
			this.m_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_close.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_close.Location = new System.Drawing.Point(409, 45);
			this.m_close.Name = "m_close";
			this.m_close.Size = new System.Drawing.Size(75, 23);
			this.m_close.TabIndex = 9;
			this.m_close.Text = "Close";
			this.m_close.UseVisualStyleBackColor = true;
			this.m_close.Click += new System.EventHandler(this.OnClose);
			// 
			// m_render
			// 
			this.m_render.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_render.Enabled = false;
			this.m_render.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_render.Location = new System.Drawing.Point(328, 45);
			this.m_render.Name = "m_render";
			this.m_render.Size = new System.Drawing.Size(75, 23);
			this.m_render.TabIndex = 10;
			this.m_render.Text = "Render";
			this.m_render.UseVisualStyleBackColor = true;
			this.m_render.Click += new System.EventHandler(this.OnRender);
			// 
			// m_folderbrowser
			// 
			this.m_folderbrowser.Description = "Select Export Folder";
			this.m_folderbrowser.RootFolder = System.Environment.SpecialFolder.MyComputer;
			// 
			// ExportCardImagesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(496, 80);
			this.Controls.Add(this.m_render);
			this.Controls.Add(this.m_close);
			this.Controls.Add(this.m_folderlabel);
			this.Controls.Add(this.m_browse);
			this.Controls.Add(this.m_folder);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExportCardImagesForm";
			this.Text = "Export Card Images";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox m_folder;
		private System.Windows.Forms.Button m_browse;
		private System.Windows.Forms.Label m_folderlabel;
		private System.Windows.Forms.Button m_close;
		private System.Windows.Forms.Button m_render;
		private System.Windows.Forms.FolderBrowserDialog m_folderbrowser;
	}
}