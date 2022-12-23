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
			this.rulingsView1 = new zuki.ronin.ui.RulingsView();
			this.cardText1 = new zuki.ronin.ui.CardText();
			this.m_cardimage = new zuki.ronin.ui.CardImage();
			this.printListView1 = new zuki.ronin.ui.PrintListView();
			this.cardName1 = new zuki.ronin.ui.CardName();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 203F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.01802F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.98198F));
			this.tableLayoutPanel1.Controls.Add(this.rulingsView1, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.cardText1, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.m_cardimage, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.printListView1, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.cardName1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(788, 603);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// rulingsView1
			// 
			this.rulingsView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rulingsView1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rulingsView1.ForeColor = System.Drawing.Color.Black;
			this.rulingsView1.Location = new System.Drawing.Point(428, 3);
			this.rulingsView1.Name = "rulingsView1";
			this.tableLayoutPanel1.SetRowSpan(this.rulingsView1, 3);
			this.rulingsView1.Size = new System.Drawing.Size(357, 597);
			this.rulingsView1.TabIndex = 8;
			// 
			// cardText1
			// 
			this.cardText1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cardText1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cardText1.ForeColor = System.Drawing.Color.Black;
			this.cardText1.Location = new System.Drawing.Point(206, 93);
			this.cardText1.Name = "cardText1";
			this.cardText1.Size = new System.Drawing.Size(216, 295);
			this.cardText1.TabIndex = 7;
			// 
			// m_cardimage
			// 
			this.m_cardimage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_cardimage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_cardimage.ForeColor = System.Drawing.Color.Black;
			this.m_cardimage.Location = new System.Drawing.Point(1, 91);
			this.m_cardimage.Margin = new System.Windows.Forms.Padding(1);
			this.m_cardimage.Name = "m_cardimage";
			this.m_cardimage.Size = new System.Drawing.Size(201, 299);
			this.m_cardimage.TabIndex = 0;
			// 
			// printListView1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.printListView1, 2);
			this.printListView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.printListView1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.printListView1.ForeColor = System.Drawing.Color.Black;
			this.printListView1.Location = new System.Drawing.Point(3, 394);
			this.printListView1.Name = "printListView1";
			this.printListView1.Size = new System.Drawing.Size(419, 206);
			this.printListView1.TabIndex = 5;
			// 
			// cardName1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.cardName1, 2);
			this.cardName1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cardName1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cardName1.ForeColor = System.Drawing.Color.Black;
			this.cardName1.Location = new System.Drawing.Point(3, 3);
			this.cardName1.Name = "cardName1";
			this.cardName1.Size = new System.Drawing.Size(419, 84);
			this.cardName1.TabIndex = 6;
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
		private PrintListView printListView1;
		private CardName cardName1;
		private CardText cardText1;
		private RulingsView rulingsView1;
	}
}
