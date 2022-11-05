//---------------------------------------------------------------------------
// Copyright (c) 2004-2022 Michael G. Brehm
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
	/// Implements a Card ListView control
	/// </summary>
	[DefaultBindingProperty("Cards")]
	public partial class CardListView : UserControl
	{
		#region Win32 API Declarations
		private static class NativeMethods
		{
			public const int SB_HORZ = 0;
			public const int SB_VERT = 1;
			public const int SB_BOTH = 3;

			[DllImport("user32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool ShowScrollBar(IntPtr hwnd, int wBar, [MarshalAs(UnmanagedType.Bool)] bool bShow);
		}
		#endregion

		/// <summary>
		/// Instance Constructor
		/// </summary>
		public CardListView()
		{
			InitializeComponent();
			m_listview.EnableDoubleBuffering();

			// Wire up the application theme change handler
			m_appthemechanged = new EventHandler(OnApplicationThemeChanged);
			ApplicationTheme.Changed += m_appthemechanged;

			// Reset the theme based on the current settings
			OnApplicationThemeChanged(this, EventArgs.Empty);

			// Manual DPI scaling
			Margin.ScaleDPI(ApplicationTheme.ScalingFactor);
			Padding.ScaleDPI(ApplicationTheme.ScalingFactor);
		}

		/// <summary>
		/// Clean up any resources being used
		/// </summary>
		/// <param name="disposing">flag if managed resources should be disposed</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing)
			{
				if(m_appthemechanged != null) ApplicationTheme.Changed -= m_appthemechanged;
				if(components != null) components.Dispose();
			}

			base.Dispose(disposing);
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
		// Properties
		//-------------------------------------------------------------------

		public List<Card> Cards
		{
			get { return m_cards; }
			set
			{
				// De-select any item(s) that happen to be selected and fire off a
				// SelectionChanged() event to make sure everyone else gets that
				m_listview.SelectedIndices.Clear();
				SelectionChanged?.Invoke(this, null);

				m_listview.BeginUpdate();

				try
				{
					m_cards = value;

					// Clear out and reload the existing list of cards
					//m_list.Clear();
					//m_list.AddRange(cards);

					// Update the number of virtual items available for the ListView control
					m_listview.VirtualListSize = m_cards.Count;

					// Scroll the list back to the top on a list change, this prevents odd
					// behaviors when the previous top item had an index higher than the new total
					//if((m_listView.VirtualListSize > 0) && (m_listView.TopItem != null))
					//	m_listView.TopItem = m_listView.Items[0];
				}

				catch { m_listview.VirtualListSize = 0; throw; }
				finally { m_listview.EndUpdate(); }

				// Disable the horizontal scroll bar on the ListView control
				NativeMethods.ShowScrollBar(m_listview.Handle, NativeMethods.SB_HORZ, false);
			}
		}

		//---------------------------------------------------------------------
		// Event Handlers
		//---------------------------------------------------------------------

		/// <summary>
		/// Invoked when the application theme has changed
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnApplicationThemeChanged(object sender, EventArgs args)
		{
			BackColor = ApplicationTheme.FormBackColor;
			ForeColor = ApplicationTheme.FormForeColor;

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
			// Take off a few pixels at the top and bottom of the cell to provide the "lines"
			// that serve as the visual separator between the items
			var adjustedBounds = args.Bounds.InflateDPI(-4, -4, ApplicationTheme.ScalingFactor);

			// Can't use args.State here - known (and very old) defect in .NET
			Brush background = new SolidBrush(args.Item.Selected ? ApplicationTheme.InvertedPanelBackColor : 
				ApplicationTheme.PanelBackColor);

			// Fill the background with a different color based on if the item is selected or not
			args.Graphics.FillRectangle(background, adjustedBounds);
		}

		/// <summary>
		/// Invoked when a subitem of the ListView control needs to be drawn
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Draw item event arguments</param>
		private void OnDrawSubItem(object sender, DrawListViewSubItemEventArgs args)
		{
			var adjustedBounds = args.SubItem.Bounds.InflateDPI(-8, -4, ApplicationTheme.ScalingFactor);

			// Use high-quality text rendering for the subitem
			args.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			args.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			args.Graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

			// TextRenderer does a better job than Graphics.DrawString, quality-wise
			TextRenderer.DrawText(args.Graphics, args.SubItem.Text, args.SubItem.Font, adjustedBounds,
				args.Item.Selected ? ApplicationTheme.InvertedPanelForeColor : ApplicationTheme.PanelForeColor,
				TextFormatFlags.VerticalCenter);
		}

		/// <summary>
		/// Invoked when the ListView control is resized
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnResize(object sender, EventArgs args)
		{
			// Resize the column(s) of the list view
			m_listviewcolumn.Width = m_listview.ClientSize.Width - 1;
		}

		/// <summary>
		/// Invoked when the virtual listview needs to access an item
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Virtual listview event arguments</param>
		private void OnRetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs args)
		{
			Debug.Assert(args.ItemIndex < m_cards.Count);

			// Create and initialize a new ListViewItem to return
			args.Item = new ListViewItem(m_cards[args.ItemIndex].Name.Replace("&", "&&"))
			{
				Font = m_listview.Font,
				Tag = m_cards[args.ItemIndex]
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
		/// Event handler for application theme changes
		/// </summary>
		private readonly EventHandler m_appthemechanged;

		/// <summary>
		/// Backing List<> for the virtual list view
		/// </summary>
		private List<Card> m_cards = new List<Card>();
	}
}
