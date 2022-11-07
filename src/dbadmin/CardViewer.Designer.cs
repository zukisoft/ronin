﻿namespace zuki.ronin
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
			this.m_splitcontainer = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.m_splitcontainer)).BeginInit();
			this.m_splitcontainer.Panel1.SuspendLayout();
			this.m_splitcontainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_cardselector
			// 
			this.m_cardselector.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
			this.m_cardselector.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_cardselector.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_cardselector.ForeColor = System.Drawing.Color.Black;
			this.m_cardselector.Location = new System.Drawing.Point(0, 0);
			this.m_cardselector.Name = "m_cardselector";
			this.m_cardselector.Size = new System.Drawing.Size(339, 539);
			this.m_cardselector.TabIndex = 1;
			// 
			// m_splitcontainer
			// 
			this.m_splitcontainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_splitcontainer.Location = new System.Drawing.Point(4, 4);
			this.m_splitcontainer.Name = "m_splitcontainer";
			// 
			// m_splitcontainer.Panel1
			// 
			this.m_splitcontainer.Panel1.Controls.Add(this.m_cardselector);
			this.m_splitcontainer.Size = new System.Drawing.Size(1019, 539);
			this.m_splitcontainer.SplitterDistance = 339;
			this.m_splitcontainer.TabIndex = 2;
			// 
			// CardViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(1027, 547);
			this.Controls.Add(this.m_splitcontainer);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "CardViewer";
			this.Padding = new System.Windows.Forms.Padding(4);
			this.Text = "View Card";
			this.Load += new System.EventHandler(this.OnLoad);
			this.m_splitcontainer.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.m_splitcontainer)).EndInit();
			this.m_splitcontainer.ResumeLayout(false);
			this.ResumeLayout(false);

		}

        #endregion
        private ui.CardSelector m_cardselector;
        private System.Windows.Forms.SplitContainer m_splitcontainer;
    }
}