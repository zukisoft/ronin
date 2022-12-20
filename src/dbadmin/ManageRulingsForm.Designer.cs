namespace zuki.ronin
{
	partial class ManageRulingsForm
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
			this.m_rightpanel = new System.Windows.Forms.Panel();
			this.m_rulingsview = new zuki.ronin.ui.RulingsView();
			this.m_rulings = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.m_update = new System.Windows.Forms.Button();
			this.m_cardselector = new zuki.ronin.ui.CardSelector();
			this.m_rightpanel.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_rightpanel
			// 
			this.m_rightpanel.Controls.Add(this.m_rulingsview);
			this.m_rightpanel.Controls.Add(this.m_rulings);
			this.m_rightpanel.Controls.Add(this.panel1);
			this.m_rightpanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_rightpanel.Location = new System.Drawing.Point(288, 4);
			this.m_rightpanel.Name = "m_rightpanel";
			this.m_rightpanel.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
			this.m_rightpanel.Size = new System.Drawing.Size(667, 418);
			this.m_rightpanel.TabIndex = 1;
			// 
			// m_rulingsview
			// 
			this.m_rulingsview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_rulingsview.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_rulingsview.ForeColor = System.Drawing.Color.White;
			this.m_rulingsview.Location = new System.Drawing.Point(442, 0);
			this.m_rulingsview.Name = "m_rulingsview";
			this.m_rulingsview.Size = new System.Drawing.Size(225, 386);
			this.m_rulingsview.TabIndex = 2;
			// 
			// m_rulings
			// 
			this.m_rulings.AcceptsReturn = true;
			this.m_rulings.Dock = System.Windows.Forms.DockStyle.Left;
			this.m_rulings.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_rulings.Location = new System.Drawing.Point(4, 0);
			this.m_rulings.Multiline = true;
			this.m_rulings.Name = "m_rulings";
			this.m_rulings.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.m_rulings.Size = new System.Drawing.Size(438, 386);
			this.m_rulings.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.m_update);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(4, 386);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(663, 32);
			this.panel1.TabIndex = 1;
			// 
			// m_update
			// 
			this.m_update.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_update.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.m_update.Location = new System.Drawing.Point(585, 6);
			this.m_update.Name = "m_update";
			this.m_update.Size = new System.Drawing.Size(75, 23);
			this.m_update.TabIndex = 0;
			this.m_update.Text = "Update";
			this.m_update.UseVisualStyleBackColor = true;
			this.m_update.Click += new System.EventHandler(this.OnUpdate);
			// 
			// m_cardselector
			// 
			this.m_cardselector.Dock = System.Windows.Forms.DockStyle.Left;
			this.m_cardselector.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_cardselector.Location = new System.Drawing.Point(4, 4);
			this.m_cardselector.Name = "m_cardselector";
			this.m_cardselector.Size = new System.Drawing.Size(284, 418);
			this.m_cardselector.TabIndex = 0;
			this.m_cardselector.SelectionChanged += new System.EventHandler<zuki.ronin.data.Card>(this.OnSelectionChanged);
			// 
			// ManageRulingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(959, 426);
			this.Controls.Add(this.m_rightpanel);
			this.Controls.Add(this.m_cardselector);
			this.Location = new System.Drawing.Point(0, 0);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "ManageRulingsForm";
			this.Padding = new System.Windows.Forms.Padding(4);
			this.Text = "Manage Rulings";
			this.Load += new System.EventHandler(this.OnLoad);
			this.m_rightpanel.ResumeLayout(false);
			this.m_rightpanel.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

        #endregion

        private zuki.ronin.ui.CardSelector m_cardselector;
        private System.Windows.Forms.Panel m_rightpanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_update;
        private System.Windows.Forms.TextBox m_rulings;
		private ui.RulingsView m_rulingsview;
	}
}