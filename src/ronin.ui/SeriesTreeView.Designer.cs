﻿namespace zuki.ronin.ui
{
	partial class SeriesTreeView
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
			this.m_treeview = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// m_treeview
			// 
			this.m_treeview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_treeview.Location = new System.Drawing.Point(0, 0);
			this.m_treeview.Name = "m_treeview";
			this.m_treeview.Size = new System.Drawing.Size(335, 389);
			this.m_treeview.TabIndex = 0;
			// 
			// SeriesTreeView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.m_treeview);
			this.Name = "SeriesTreeView";
			this.Size = new System.Drawing.Size(335, 389);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView m_treeview;
	}
}
