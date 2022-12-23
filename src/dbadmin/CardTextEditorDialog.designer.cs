namespace zuki.ronin
{
	partial class CardTextEditorDialog
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.m_cancel = new System.Windows.Forms.Button();
			this.m_ok = new System.Windows.Forms.Button();
			this.m_panel = new System.Windows.Forms.Panel();
			this.m_innerpanel = new System.Windows.Forms.Panel();
			this.m_text = new System.Windows.Forms.TextBox();
			this.m_insertdot = new System.Windows.Forms.LinkLabel();
			this.m_image = new zuki.ronin.ui.CardImage();
			this.m_panel.SuspendLayout();
			this.m_innerpanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_cancel
			// 
			this.m_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_cancel.Location = new System.Drawing.Point(670, 470);
			this.m_cancel.Name = "m_cancel";
			this.m_cancel.Size = new System.Drawing.Size(75, 23);
			this.m_cancel.TabIndex = 2;
			this.m_cancel.Text = "Cancel";
			this.m_cancel.UseVisualStyleBackColor = true;
			// 
			// m_ok
			// 
			this.m_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.m_ok.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_ok.Location = new System.Drawing.Point(589, 470);
			this.m_ok.Name = "m_ok";
			this.m_ok.Size = new System.Drawing.Size(75, 23);
			this.m_ok.TabIndex = 1;
			this.m_ok.Text = "OK";
			this.m_ok.UseVisualStyleBackColor = true;
			// 
			// m_panel
			// 
			this.m_panel.Controls.Add(this.m_innerpanel);
			this.m_panel.Controls.Add(this.m_image);
			this.m_panel.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_panel.Location = new System.Drawing.Point(0, 0);
			this.m_panel.Name = "m_panel";
			this.m_panel.Padding = new System.Windows.Forms.Padding(4);
			this.m_panel.Size = new System.Drawing.Size(757, 465);
			this.m_panel.TabIndex = 2;
			// 
			// m_innerpanel
			// 
			this.m_innerpanel.Controls.Add(this.m_text);
			this.m_innerpanel.Controls.Add(this.m_insertdot);
			this.m_innerpanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_innerpanel.Location = new System.Drawing.Point(342, 4);
			this.m_innerpanel.Name = "m_innerpanel";
			this.m_innerpanel.Padding = new System.Windows.Forms.Padding(8);
			this.m_innerpanel.Size = new System.Drawing.Size(411, 457);
			this.m_innerpanel.TabIndex = 1;
			// 
			// m_text
			// 
			this.m_text.AcceptsReturn = true;
			this.m_text.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_text.Location = new System.Drawing.Point(8, 8);
			this.m_text.Multiline = true;
			this.m_text.Name = "m_text";
			this.m_text.Size = new System.Drawing.Size(395, 417);
			this.m_text.TabIndex = 0;
			this.m_text.Validated += new System.EventHandler(this.OnTextValidated);
			// 
			// m_insertdot
			// 
			this.m_insertdot.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
			this.m_insertdot.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.m_insertdot.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.m_insertdot.Location = new System.Drawing.Point(8, 425);
			this.m_insertdot.Name = "m_insertdot";
			this.m_insertdot.Size = new System.Drawing.Size(395, 24);
			this.m_insertdot.TabIndex = 1;
			this.m_insertdot.TabStop = true;
			this.m_insertdot.Text = "Insert ●";
			this.m_insertdot.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.m_insertdot.VisitedLinkColor = System.Drawing.SystemColors.HotTrack;
			this.m_insertdot.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnInsertDot);
			// 
			// m_image
			// 
			this.m_image.Dock = System.Windows.Forms.DockStyle.Left;
			this.m_image.Location = new System.Drawing.Point(4, 4);
			this.m_image.Name = "m_image";
			this.m_image.Padding = new System.Windows.Forms.Padding(4);
			this.m_image.Size = new System.Drawing.Size(338, 457);
			this.m_image.TabIndex = 3;
			this.m_image.TabStop = false;
			// 
			// CardTextEditorDialog
			// 
			this.AcceptButton = this.m_ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.CancelButton = this.m_cancel;
			this.ClientSize = new System.Drawing.Size(757, 505);
			this.Controls.Add(this.m_panel);
			this.Controls.Add(this.m_ok);
			this.Controls.Add(this.m_cancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Location = new System.Drawing.Point(0, 0);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CardTextEditorDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit Card Text";
			this.Load += new System.EventHandler(this.OnLoad);
			this.m_panel.ResumeLayout(false);
			this.m_innerpanel.ResumeLayout(false);
			this.m_innerpanel.PerformLayout();
			this.ResumeLayout(false);

		}

        #endregion

        private System.Windows.Forms.Button m_cancel;
        private System.Windows.Forms.Button m_ok;
        private System.Windows.Forms.Panel m_panel;
        private System.Windows.Forms.Panel m_innerpanel;
        private System.Windows.Forms.TextBox m_text;
        private ui.CardImage m_image;
        private System.Windows.Forms.LinkLabel m_insertdot;
    }
}