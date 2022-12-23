namespace zuki.ronin
{
	partial class ManageRestrictionListsForm
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
			this.m_toppanel = new System.Windows.Forms.Panel();
			this.m_reslistlabel = new System.Windows.Forms.Label();
			this.m_reslistcombo = new System.Windows.Forms.ComboBox();
			this.m_accentpanel = new System.Windows.Forms.Panel();
			this.m_lowerpanel = new System.Windows.Forms.Panel();
			this.m_semilimitedcards = new zuki.ronin.ui.CardListView();
			this.m_limitedcards = new zuki.ronin.ui.CardListView();
			this.m_forbiddencards = new zuki.ronin.ui.CardListView();
			this.m_semilimitedlabel = new System.Windows.Forms.Label();
			this.m_limitedlabel = new System.Windows.Forms.Label();
			this.m_forbiddenlabel = new System.Windows.Forms.Label();
			this.m_toppanel.SuspendLayout();
			this.m_accentpanel.SuspendLayout();
			this.m_lowerpanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_toppanel
			// 
			this.m_toppanel.Controls.Add(this.m_reslistlabel);
			this.m_toppanel.Controls.Add(this.m_reslistcombo);
			this.m_toppanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_toppanel.Location = new System.Drawing.Point(0, 0);
			this.m_toppanel.Name = "m_toppanel";
			this.m_toppanel.Size = new System.Drawing.Size(998, 42);
			this.m_toppanel.TabIndex = 0;
			// 
			// m_reslistlabel
			// 
			this.m_reslistlabel.AutoSize = true;
			this.m_reslistlabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_reslistlabel.Location = new System.Drawing.Point(13, 13);
			this.m_reslistlabel.Name = "m_reslistlabel";
			this.m_reslistlabel.Size = new System.Drawing.Size(87, 15);
			this.m_reslistlabel.TabIndex = 1;
			this.m_reslistlabel.Text = "Restriction List:";
			// 
			// m_reslistcombo
			// 
			this.m_reslistcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.m_reslistcombo.FormattingEnabled = true;
			this.m_reslistcombo.Location = new System.Drawing.Point(106, 10);
			this.m_reslistcombo.Name = "m_reslistcombo";
			this.m_reslistcombo.Size = new System.Drawing.Size(129, 23);
			this.m_reslistcombo.TabIndex = 0;
			this.m_reslistcombo.SelectedIndexChanged += new System.EventHandler(this.OnSelectedListChanged);
			// 
			// m_accentpanel
			// 
			this.m_accentpanel.BackColor = System.Drawing.SystemColors.ControlDark;
			this.m_accentpanel.Controls.Add(this.m_lowerpanel);
			this.m_accentpanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_accentpanel.Location = new System.Drawing.Point(0, 42);
			this.m_accentpanel.Name = "m_accentpanel";
			this.m_accentpanel.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
			this.m_accentpanel.Size = new System.Drawing.Size(998, 529);
			this.m_accentpanel.TabIndex = 1;
			// 
			// m_lowerpanel
			// 
			this.m_lowerpanel.BackColor = System.Drawing.SystemColors.Control;
			this.m_lowerpanel.Controls.Add(this.m_semilimitedcards);
			this.m_lowerpanel.Controls.Add(this.m_limitedcards);
			this.m_lowerpanel.Controls.Add(this.m_forbiddencards);
			this.m_lowerpanel.Controls.Add(this.m_semilimitedlabel);
			this.m_lowerpanel.Controls.Add(this.m_limitedlabel);
			this.m_lowerpanel.Controls.Add(this.m_forbiddenlabel);
			this.m_lowerpanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_lowerpanel.Location = new System.Drawing.Point(0, 2);
			this.m_lowerpanel.Name = "m_lowerpanel";
			this.m_lowerpanel.Size = new System.Drawing.Size(998, 527);
			this.m_lowerpanel.TabIndex = 3;
			// 
			// m_semilimitedcards
			// 
			this.m_semilimitedcards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
			this.m_semilimitedcards.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_semilimitedcards.ForeColor = System.Drawing.Color.Black;
			this.m_semilimitedcards.Location = new System.Drawing.Point(664, 33);
			this.m_semilimitedcards.Name = "m_semilimitedcards";
			this.m_semilimitedcards.Padding = new System.Windows.Forms.Padding(4);
			this.m_semilimitedcards.Size = new System.Drawing.Size(318, 482);
			this.m_semilimitedcards.TabIndex = 18;
			// 
			// m_limitedcards
			// 
			this.m_limitedcards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
			this.m_limitedcards.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_limitedcards.ForeColor = System.Drawing.Color.Black;
			this.m_limitedcards.Location = new System.Drawing.Point(340, 33);
			this.m_limitedcards.Name = "m_limitedcards";
			this.m_limitedcards.Padding = new System.Windows.Forms.Padding(4);
			this.m_limitedcards.Size = new System.Drawing.Size(318, 482);
			this.m_limitedcards.TabIndex = 17;
			// 
			// m_forbiddencards
			// 
			this.m_forbiddencards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
			this.m_forbiddencards.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_forbiddencards.ForeColor = System.Drawing.Color.Black;
			this.m_forbiddencards.Location = new System.Drawing.Point(16, 33);
			this.m_forbiddencards.Name = "m_forbiddencards";
			this.m_forbiddencards.Padding = new System.Windows.Forms.Padding(4);
			this.m_forbiddencards.Size = new System.Drawing.Size(318, 482);
			this.m_forbiddencards.TabIndex = 16;
			// 
			// m_semilimitedlabel
			// 
			this.m_semilimitedlabel.AutoSize = true;
			this.m_semilimitedlabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_semilimitedlabel.Location = new System.Drawing.Point(661, 15);
			this.m_semilimitedlabel.Name = "m_semilimitedlabel";
			this.m_semilimitedlabel.Size = new System.Drawing.Size(114, 15);
			this.m_semilimitedlabel.TabIndex = 12;
			this.m_semilimitedlabel.Text = "Semi-Limited Cards:";
			// 
			// m_limitedlabel
			// 
			this.m_limitedlabel.AutoSize = true;
			this.m_limitedlabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_limitedlabel.Location = new System.Drawing.Point(337, 15);
			this.m_limitedlabel.Name = "m_limitedlabel";
			this.m_limitedlabel.Size = new System.Drawing.Size(82, 15);
			this.m_limitedlabel.TabIndex = 10;
			this.m_limitedlabel.Text = "Limited Cards:";
			// 
			// m_forbiddenlabel
			// 
			this.m_forbiddenlabel.AutoSize = true;
			this.m_forbiddenlabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_forbiddenlabel.Location = new System.Drawing.Point(13, 15);
			this.m_forbiddenlabel.Name = "m_forbiddenlabel";
			this.m_forbiddenlabel.Size = new System.Drawing.Size(96, 15);
			this.m_forbiddenlabel.TabIndex = 8;
			this.m_forbiddenlabel.Text = "Forbidden Cards:";
			// 
			// ManageRestrictionListsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(998, 571);
			this.Controls.Add(this.m_accentpanel);
			this.Controls.Add(this.m_toppanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Location = new System.Drawing.Point(0, 0);
			this.MaximizeBox = false;
			this.Name = "ManageRestrictionListsForm";
			this.Text = "Manage Restriction Lists";
			this.Load += new System.EventHandler(this.OnLoad);
			this.m_toppanel.ResumeLayout(false);
			this.m_toppanel.PerformLayout();
			this.m_accentpanel.ResumeLayout(false);
			this.m_lowerpanel.ResumeLayout(false);
			this.m_lowerpanel.PerformLayout();
			this.ResumeLayout(false);

		}

        #endregion

        private System.Windows.Forms.Panel m_toppanel;
        private System.Windows.Forms.Label m_reslistlabel;
        private System.Windows.Forms.ComboBox m_reslistcombo;
        private System.Windows.Forms.Panel m_accentpanel;
        private System.Windows.Forms.Panel m_lowerpanel;
        private System.Windows.Forms.Label m_semilimitedlabel;
        private System.Windows.Forms.Label m_limitedlabel;
        private System.Windows.Forms.Label m_forbiddenlabel;
        private zuki.ronin.ui.CardListView m_forbiddencards;
        private zuki.ronin.ui.CardListView m_semilimitedcards;
        private zuki.ronin.ui.CardListView m_limitedcards;
    }
}