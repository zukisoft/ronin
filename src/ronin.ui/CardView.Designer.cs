namespace zuki.ronin.ui
{
	partial class CardView
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.m_cardimage = new zuki.ronin.ui.CardImage();
			this.cardText1 = new zuki.ronin.ui.CardText();
			this.cardInfo1 = new zuki.ronin.ui.CardInfo();
			this.printInfo1 = new zuki.ronin.ui.PrintInfo();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.m_cardimage, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.cardText1, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.cardInfo1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.printInfo1, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.08624F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.41459F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(788, 603);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// m_cardimage
			// 
			this.m_cardimage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_cardimage.Location = new System.Drawing.Point(1, 1);
			this.m_cardimage.Margin = new System.Windows.Forms.Padding(1);
			this.m_cardimage.Name = "m_cardimage";
			this.tableLayoutPanel1.SetRowSpan(this.m_cardimage, 2);
			this.m_cardimage.Size = new System.Drawing.Size(392, 399);
			this.m_cardimage.TabIndex = 0;
			// 
			// cardText1
			// 
			this.cardText1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cardText1.Location = new System.Drawing.Point(395, 98);
			this.cardText1.Margin = new System.Windows.Forms.Padding(1);
			this.cardText1.Name = "cardText1";
			this.cardText1.Size = new System.Drawing.Size(392, 302);
			this.cardText1.TabIndex = 1;
			// 
			// cardInfo1
			// 
			this.cardInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cardInfo1.Location = new System.Drawing.Point(395, 1);
			this.cardInfo1.Margin = new System.Windows.Forms.Padding(1);
			this.cardInfo1.Name = "cardInfo1";
			this.cardInfo1.Size = new System.Drawing.Size(392, 95);
			this.cardInfo1.TabIndex = 2;
			// 
			// printInfo1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.printInfo1, 2);
			this.printInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.printInfo1.Location = new System.Drawing.Point(1, 402);
			this.printInfo1.Margin = new System.Windows.Forms.Padding(1);
			this.printInfo1.Name = "printInfo1";
			this.printInfo1.Size = new System.Drawing.Size(786, 200);
			this.printInfo1.TabIndex = 3;
			// 
			// CardView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.tableLayoutPanel1);
			this.DoubleBuffered = true;
			this.Name = "CardView";
			this.Size = new System.Drawing.Size(788, 603);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private CardImage m_cardimage;
		private CardText cardText1;
		private CardInfo cardInfo1;
		private PrintInfo printInfo1;
	}
}
