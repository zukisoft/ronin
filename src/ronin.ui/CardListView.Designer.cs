namespace zuki.ronin.ui
{
	partial class CardListView
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
			this.m_listviewcolumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.m_dummyimagelist = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// m_listview
			// 
			this.m_listview.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.m_listview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_listviewcolumn});
			this.m_listview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_listview.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_listview.FullRowSelect = true;
			this.m_listview.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.m_listview.HideSelection = false;
			this.m_listview.LabelWrap = false;
			this.m_listview.Location = new System.Drawing.Point(0, 0);
			this.m_listview.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.m_listview.MultiSelect = false;
			this.m_listview.Name = "m_listview";
			this.m_listview.OwnerDraw = true;
			this.m_listview.Size = new System.Drawing.Size(330, 457);
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
			// m_dummyimagelist
			// 
			this.m_dummyimagelist.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.m_dummyimagelist.ImageSize = new System.Drawing.Size(1, 32);
			this.m_dummyimagelist.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// CardListView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.Controls.Add(this.m_listview);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "CardListView";
			this.Size = new System.Drawing.Size(330, 457);
			this.ResumeLayout(false);

		}

        #endregion

        private zuki.ronin.ui.VirtualListView m_listview;
        private System.Windows.Forms.ColumnHeader m_listviewcolumn;
        private System.Windows.Forms.ImageList m_dummyimagelist;
    }
}
