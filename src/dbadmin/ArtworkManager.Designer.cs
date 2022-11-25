namespace zuki.ronin
{
	partial class ArtworkManager
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArtworkManager));
			this.m_cardselector = new zuki.ronin.ui.CardSelector();
			this.m_leftpanel = new System.Windows.Forms.Panel();
			this.m_rightpanel = new System.Windows.Forms.Panel();
			this.m_layoutpanel = new System.Windows.Forms.TableLayoutPanel();
			this.m_lowerrightpanel = new System.Windows.Forms.Panel();
			this.m_separator = new System.Windows.Forms.Panel();
			this.m_import = new System.Windows.Forms.Button();
			this.m_openfile = new System.Windows.Forms.OpenFileDialog();
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
			this.m_cardselector.Size = new System.Drawing.Size(296, 630);
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
			this.m_leftpanel.Size = new System.Drawing.Size(304, 638);
			this.m_leftpanel.TabIndex = 1;
			// 
			// m_rightpanel
			// 
			this.m_rightpanel.Controls.Add(this.m_layoutpanel);
			this.m_rightpanel.Controls.Add(this.m_lowerrightpanel);
			this.m_rightpanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_rightpanel.Location = new System.Drawing.Point(304, 0);
			this.m_rightpanel.Name = "m_rightpanel";
			this.m_rightpanel.Padding = new System.Windows.Forms.Padding(8);
			this.m_rightpanel.Size = new System.Drawing.Size(544, 638);
			this.m_rightpanel.TabIndex = 2;
			// 
			// m_layoutpanel
			// 
			this.m_layoutpanel.ColumnCount = 2;
			this.m_layoutpanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.m_layoutpanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.m_layoutpanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_layoutpanel.Location = new System.Drawing.Point(8, 8);
			this.m_layoutpanel.Name = "m_layoutpanel";
			this.m_layoutpanel.RowCount = 2;
			this.m_layoutpanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.m_layoutpanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.m_layoutpanel.Size = new System.Drawing.Size(528, 590);
			this.m_layoutpanel.TabIndex = 2;
			// 
			// m_lowerrightpanel
			// 
			this.m_lowerrightpanel.Controls.Add(this.m_separator);
			this.m_lowerrightpanel.Controls.Add(this.m_import);
			this.m_lowerrightpanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.m_lowerrightpanel.Location = new System.Drawing.Point(8, 598);
			this.m_lowerrightpanel.Name = "m_lowerrightpanel";
			this.m_lowerrightpanel.Size = new System.Drawing.Size(528, 32);
			this.m_lowerrightpanel.TabIndex = 1;
			// 
			// m_separator
			// 
			this.m_separator.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.m_separator.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_separator.Location = new System.Drawing.Point(0, 0);
			this.m_separator.Name = "m_separator";
			this.m_separator.Size = new System.Drawing.Size(528, 2);
			this.m_separator.TabIndex = 2;
			// 
			// m_import
			// 
			this.m_import.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_import.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_import.Location = new System.Drawing.Point(451, 9);
			this.m_import.Name = "m_import";
			this.m_import.Size = new System.Drawing.Size(77, 23);
			this.m_import.TabIndex = 0;
			this.m_import.Text = "Import...";
			this.m_import.UseVisualStyleBackColor = true;
			this.m_import.Click += new System.EventHandler(this.OnImport);
			// 
			// m_openfile
			// 
			this.m_openfile.AddExtension = false;
			this.m_openfile.Filter = "JPEG files|*.jpg|PNG files|*.png|BMP files|*.bmp|All files|*.*";
			// 
			// ArtworkManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(848, 638);
			this.Controls.Add(this.m_rightpanel);
			this.Controls.Add(this.m_leftpanel);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ArtworkManager";
			this.Text = "Manage Artwork";
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
        private System.Windows.Forms.Button m_import;
        private System.Windows.Forms.Panel m_separator;
		private System.Windows.Forms.TableLayoutPanel m_layoutpanel;
		private System.Windows.Forms.OpenFileDialog m_openfile;
	}
}