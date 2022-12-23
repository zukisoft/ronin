namespace zuki.ronin.ui
{
	partial class PrintListView
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.m_listview = new zuki.ronin.ui.VirtualListView();
			this.m_dummycolumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.m_setnamecolumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.m_setcodecolumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.m_raritycolumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.m_dummyimagelist = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// m_listview
			// 
			this.m_listview.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.m_listview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_dummycolumn,
            this.m_setnamecolumn,
            this.m_setcodecolumn,
            this.m_raritycolumn});
			this.m_listview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_listview.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_listview.FullRowSelect = true;
			this.m_listview.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.m_listview.HideSelection = false;
			this.m_listview.LabelWrap = false;
			this.m_listview.Location = new System.Drawing.Point(0, 0);
			this.m_listview.MultiSelect = false;
			this.m_listview.Name = "m_listview";
			this.m_listview.OwnerDraw = true;
			this.m_listview.Size = new System.Drawing.Size(417, 304);
			this.m_listview.SmallImageList = this.m_dummyimagelist;
			this.m_listview.TabIndex = 0;
			this.m_listview.UseCompatibleStateImageBehavior = false;
			this.m_listview.View = System.Windows.Forms.View.Details;
			this.m_listview.VirtualMode = true;
			this.m_listview.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.OnDrawColumnHeader);
			this.m_listview.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.OnDrawItem);
			this.m_listview.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.OnDrawSubItem);
			this.m_listview.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.OnSelectionChanged);
			this.m_listview.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.OnRetrieveVirtualItem);
			this.m_listview.Resize += new System.EventHandler(this.OnResize);
			// 
			// m_dummycolumn
			// 
			this.m_dummycolumn.DisplayIndex = 1;
			this.m_dummycolumn.Width = 0;
			// 
			// m_setnamecolumn
			// 
			this.m_setnamecolumn.DisplayIndex = 0;
			// 
			// m_setcodecolumn
			// 
			this.m_setcodecolumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// m_raritycolumn
			// 
			this.m_raritycolumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// m_dummyimagelist
			// 
			this.m_dummyimagelist.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.m_dummyimagelist.ImageSize = new System.Drawing.Size(1, 30);
			this.m_dummyimagelist.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// PrintListView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.m_listview);
			this.DoubleBuffered = true;
			this.Name = "PrintListView";
			this.Size = new System.Drawing.Size(417, 304);
			this.ResumeLayout(false);

		}

        #endregion

        private zuki.ronin.ui.VirtualListView m_listview;
        private System.Windows.Forms.ColumnHeader m_setnamecolumn;
        private System.Windows.Forms.ImageList m_dummyimagelist;
		private System.Windows.Forms.ColumnHeader m_dummycolumn;
		private System.Windows.Forms.ColumnHeader m_setcodecolumn;
		private System.Windows.Forms.ColumnHeader m_raritycolumn;
	}
}
