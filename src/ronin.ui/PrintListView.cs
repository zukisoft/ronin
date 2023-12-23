//---------------------------------------------------------------------------
// Copyright (c) 2004-2024 Michael G. Brehm
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using zuki.ronin.data;

namespace zuki.ronin.ui
{
	/// <summary>
	/// Implements a Print ListView control
	/// </summary>
	public partial class PrintListView : UserControlBase
	{
		#region Win32 API Declarations
		private static class NativeMethods
		{
			public const int SB_HORZ = 0;
			public const int SB_VERT = 1;
			public const int SB_BOTH = 3;

			public const uint LVM_SETEXTENDEDLISTVIEWSTYLE = 0x1000 + 54;
			public const uint LVM_GETEXTENDEDLISTVIEWSTYLE = 0x1000 + 55;
			public const uint LVS_EX_DOUBLEBUFFER = 0x10000;

			[DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			public static extern IntPtr SendMessageW(IntPtr hWnd, uint Msg, IntPtr wParam = default, IntPtr lParam = default);

			[DllImport("user32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool ShowScrollBar(IntPtr hwnd, int wBar, [MarshalAs(UnmanagedType.Bool)] bool bShow);
		}
		#endregion

		/// <summary>
		/// Instance Constructor
		/// </summary>
		public PrintListView()
		{
			InitializeComponent();

			// Manual DPI scaling
			m_dummyimagelist.ImageSize = m_dummyimagelist.ImageSize.ScaleDPI(ApplicationTheme.ScalingFactor);

			// Reset the theme based on the current settings
			OnApplicationThemeChanged(this, EventArgs.Empty);
		}

		//-------------------------------------------------------------------
		// Events
		//-------------------------------------------------------------------

		/// <summary>
		/// Fired when the selected item has changed
		/// </summary>
		[Browsable(true), Category("Behavior")]
		public event EventHandler<Card> SelectionChanged;

		//-------------------------------------------------------------------
		// Member Functions
		//-------------------------------------------------------------------

		/// <summary>
		/// Sets the enumerable list of Card objects to display
		/// </summary>
		public void SetPrints(IEnumerable<Print> prints)
		{
			// De-select any item(s) that happen to be selected and fire off a
			// SelectionChanged() event to make sure everyone else gets that
			m_listview.SelectedIndices.Clear();
			SelectionChanged?.Invoke(this, null);

			m_listview.BeginUpdate();

			try
			{
				// Scroll the list back to the top on a list change, this prevents odd
				// behaviors when the previous top item had an index higher than the new total
				if((m_listview.VirtualListSize > 0) && (m_listview.TopItem != null))
					m_listview.TopItem = m_listview.Items[0];

				// Replace the existing List<> of prints
				m_prints = new List<Print>(prints);

				// Update the number of virtual items available for the ListView control
				m_listview.VirtualListSize = m_prints.Count;

				// TODO: TESTING
				m_maxrarity = 0;
				m_maxsetcode = 0;
				foreach(Print p in m_prints)
				{
					Size size = TextRenderer.MeasureText(p.Rarity.EnumDescription(), m_listview.Font);
					if(size.Width > m_maxrarity) m_maxrarity = size.Width;

					size = TextRenderer.MeasureText(p.ToString(), m_listview.Font);
					if(size.Width > m_maxsetcode) m_maxsetcode = size.Width;
				}
				OnResize(this, EventArgs.Empty);

				// END TODO: TESTING
			}

			catch { m_listview.VirtualListSize = 0; throw; }
			finally { m_listview.EndUpdate(); }

			// Disable the horizontal scroll bar on the ListView control
			NativeMethods.ShowScrollBar(m_listview.Handle, NativeMethods.SB_HORZ, false);
		}
		
		//---------------------------------------------------------------------
		// Event Handlers
		//---------------------------------------------------------------------

		/// <summary>
		/// Invoked when the application theme has changed
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		protected override void OnApplicationThemeChanged(object sender, EventArgs args)
		{
			base.OnApplicationThemeChanged(sender, args);

			m_listview.BackColor = ApplicationTheme.FormBackColor;
			m_listview.ForeColor = ApplicationTheme.FormForeColor;
		}

		/// <summary>
		/// Invoked when a column header of the ListView control needs to be drawn
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Draw item event arguments</param>
		private void OnDrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs args)
		{
			args.DrawDefault = true;
		}

		/// <summary>
		/// Invoked when the primary item of the ListView control needs to be drawn
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Draw item event arguments</param>
		private void OnDrawItem(object sender, DrawListViewItemEventArgs args)
		{
			Print print = (Print)args.Item.Tag;
			Debug.Assert(print != null);

			// Use high-quality text rendering for the item
			args.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			args.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			args.Graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

			// Take off a few pixels at the top and bottom of the item to provide whitespace
			var adjustedBounds = args.Bounds.InflateDPI(0, -3, ApplicationTheme.ScalingFactor);

			// Render the item inside a GraphicsPath to round the corners
			using(GraphicsPath gp = new GraphicsPath())
			{
				float CornerRadius = 6.ScaleDPI(ApplicationTheme.ScalingFactor) * 2.0F;
				gp.AddArc(adjustedBounds.Left - 1, adjustedBounds.Top - 1, CornerRadius, CornerRadius, 180, 90);
				gp.AddArc(adjustedBounds.Left + adjustedBounds.Width - CornerRadius, adjustedBounds.Top - 1, CornerRadius, CornerRadius, 270, 90);
				gp.AddArc(adjustedBounds.Left + adjustedBounds.Width - CornerRadius, adjustedBounds.Top + adjustedBounds.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
				gp.AddArc(adjustedBounds.Left - 1, adjustedBounds.Top + adjustedBounds.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
				args.Graphics.SetClip(gp);

				// Draw the background color
				args.Graphics.FillPath(new SolidBrush(args.Item.Selected ? ApplicationTheme.SelectedListItemBackColor :
					ApplicationTheme.ListItemBackColor), gp);

				args.Graphics.ResetClip();
			}
		}

		private void OnDrawSubItem(object sender, DrawListViewSubItemEventArgs args)
		{
			HorizontalAlignment alignment = m_listview.Columns[args.ColumnIndex].TextAlign;
			TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.WordEllipsis;
			if(alignment == HorizontalAlignment.Center) flags |= TextFormatFlags.HorizontalCenter;
			else if(alignment == HorizontalAlignment.Right) flags |= TextFormatFlags.Right;

			TextRenderer.DrawText(args.Graphics, args.SubItem.Text, m_listview.Font, args.Bounds,
				args.Item.Selected ? ApplicationTheme.ListItemForeColor : ApplicationTheme.SelectedListItemForeColor,
				flags);
		}

		/// <summary>
		/// Invoked when the ListView control is resized
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnResize(object sender, EventArgs args)
		{
			int width = m_listview.ClientSize.Width - 4.ScaleDPI(ApplicationTheme.ScalingFactor);
			m_setcodecolumn.Width = m_maxsetcode + 4.ScaleDPI(ApplicationTheme.ScalingFactor);
			m_raritycolumn.Width = m_maxrarity + 4.ScaleDPI(ApplicationTheme.ScalingFactor);
			m_setnamecolumn.Width = width - m_setcodecolumn.Width - m_raritycolumn.Width;

			// Resize the column(s) of the list view
			//m_listview.Width = ClientSize.Width - 4.ScaleDPI(ApplicationTheme.ScalingFactor);
			//m_setnamecolumn.Width = m_listview.ClientSize.Width - 4.ScaleDPI(ApplicationTheme.ScalingFactor);	
		}

		/// <summary>
		/// Invoked when the virtual listview needs to access an item
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Virtual listview event arguments</param>
		private void OnRetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs args)
		{
			Debug.Assert(args.ItemIndex < m_prints.Count);

			Print print = m_prints[args.ItemIndex];

			// Create and initialize a new ListViewItem to return
			// TODO: The call to .GetSeries every time will be a performance concern
			args.Item = new ListViewItem(new string[] { String.Empty, print.GetSeries().Name, print.ToString(), print.Rarity.EnumDescription() })
			{
				Font = m_listview.Font,
				Tag = m_prints[args.ItemIndex]
			};
		}

		/// <summary>
		/// Invoked when the selection(s) of the ListView have changed
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">ListView selection event arguments</param>
		private void OnSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs args)
		{
			// Raise the SelectionChanged event, send null if no items are actually selected
			if(args.IsSelected) SelectionChanged?.Invoke(this, (Card)args.Item.Tag);
			else SelectionChanged?.Invoke(this, null);
		}

		//---------------------------------------------------------------------
		// Member Variables
		//---------------------------------------------------------------------

		/// <summary>
		/// Backing List<> for the virtual list view
		/// </summary>
		private List<Print> m_prints = new List<Print>();

		private int m_maxrarity = 0;

		private int m_maxsetcode = 0;
	}
}
