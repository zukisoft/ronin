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
			this.m_cardselector = new zuki.ronin.ui.CardSelector();
			this.m_leftpanel = new System.Windows.Forms.Panel();
			this.m_rightpanel = new System.Windows.Forms.Panel();
			this.m_lowerrightpanel = new System.Windows.Forms.Panel();
			this.m_separator = new System.Windows.Forms.Panel();
			this.m_edittext = new System.Windows.Forms.Button();
			this.m_image = new zuki.ronin.ui.CardImage();
			this.m_leftpanel.SuspendLayout();
			this.m_rightpanel.SuspendLayout();
			this.m_lowerrightpanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_cardselector
			// 
			this.m_cardselector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
			this.m_cardselector.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_cardselector.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_cardselector.ForeColor = System.Drawing.Color.Black;
			this.m_cardselector.Location = new System.Drawing.Point(4, 4);
			this.m_cardselector.Name = "m_cardselector";
			this.m_cardselector.Size = new System.Drawing.Size(296, 566);
			this.m_cardselector.TabIndex = 0;
			this.m_cardselector.SelectionChanged += new System.EventHandler<zuki.ronin.data.Card>(this.OnSelectionChanged);
			// 
			// m_leftpanel
			// 
			this.m_leftpanel.Controls.Add(this.m_cardselector);
			this.m_leftpanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.m_leftpanel.Location = new System.Drawing.Point(0, 0);
			this.m_leftpanel.Name = "m_leftpanel";
			this.m_leftpanel.Padding = new System.Windows.Forms.Padding(4);
			this.m_leftpanel.Size = new System.Drawing.Size(304, 574);
			this.m_leftpanel.TabIndex = 1;
			// 
			// m_rightpanel
			// 
			this.m_rightpanel.Controls.Add(this.m_image);
			this.m_rightpanel.Controls.Add(this.m_lowerrightpanel);
			this.m_rightpanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_rightpanel.Location = new System.Drawing.Point(304, 0);
			this.m_rightpanel.Name = "m_rightpanel";
			this.m_rightpanel.Padding = new System.Windows.Forms.Padding(8);
			this.m_rightpanel.Size = new System.Drawing.Size(373, 574);
			this.m_rightpanel.TabIndex = 2;
			// 
			// m_lowerrightpanel
			// 
			this.m_lowerrightpanel.Controls.Add(this.m_separator);
			this.m_lowerrightpanel.Controls.Add(this.m_edittext);
			this.m_lowerrightpanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.m_lowerrightpanel.Location = new System.Drawing.Point(8, 534);
			this.m_lowerrightpanel.Name = "m_lowerrightpanel";
			this.m_lowerrightpanel.Size = new System.Drawing.Size(357, 32);
			this.m_lowerrightpanel.TabIndex = 1;
			// 
			// m_separator
			// 
			this.m_separator.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.m_separator.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_separator.Location = new System.Drawing.Point(0, 0);
			this.m_separator.Name = "m_separator";
			this.m_separator.Size = new System.Drawing.Size(357, 2);
			this.m_separator.TabIndex = 2;
			// 
			// m_edittext
			// 
			this.m_edittext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_edittext.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_edittext.Location = new System.Drawing.Point(245, 9);
			this.m_edittext.Name = "m_edittext";
			this.m_edittext.Size = new System.Drawing.Size(112, 23);
			this.m_edittext.TabIndex = 0;
			this.m_edittext.Text = "Edit Card Text...";
			this.m_edittext.UseVisualStyleBackColor = true;
			this.m_edittext.Click += new System.EventHandler(this.OnEditText);
			// 
			// m_image
			// 
			this.m_image.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_image.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_image.Location = new System.Drawing.Point(8, 8);
			this.m_image.Name = "m_image";
			this.m_image.Size = new System.Drawing.Size(353, 520);
			this.m_image.TabIndex = 2;
			// 
			// CardViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(677, 574);
			this.Controls.Add(this.m_rightpanel);
			this.Controls.Add(this.m_leftpanel);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CardViewer";
			this.Text = "View Card";
			this.Load += new System.EventHandler(this.OnLoad);
			this.m_leftpanel.ResumeLayout(false);
			this.m_rightpanel.ResumeLayout(false);
			this.m_lowerrightpanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

        #endregion

        private ui.CardSelector m_cardselector;
        private System.Windows.Forms.Panel m_leftpanel;
        private System.Windows.Forms.Panel m_rightpanel;
        private System.Windows.Forms.Panel m_lowerrightpanel;
        private System.Windows.Forms.Button m_edittext;
        private System.Windows.Forms.Panel m_separator;
		private ui.CardImage m_image;
	}
}