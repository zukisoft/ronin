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
using zuki.ronin.ui.Properties;

namespace zuki.ronin.ui
{
	/// <summary>
	/// Implements a Card ListView control
	/// </summary>
	public partial class CardListView : UserControlBase
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
		public CardListView()
		{
			InitializeComponent();

			// Manual DPI scaling
			m_dummyimagelist.ImageSize = m_dummyimagelist.ImageSize.ScaleDPI(ApplicationTheme.ScalingFactor);

			// Fix the flicker seen on the owner drawn listview items when the mouse moves over them
			IntPtr lvmstyles = NativeMethods.SendMessageW(m_listview.Handle, NativeMethods.LVM_GETEXTENDEDLISTVIEWSTYLE);
			lvmstyles = new IntPtr(lvmstyles.ToInt32() | NativeMethods.LVS_EX_DOUBLEBUFFER);
			NativeMethods.SendMessageW(m_listview.Handle, NativeMethods.LVM_SETEXTENDEDLISTVIEWSTYLE, IntPtr.Zero, lvmstyles);

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
		// Properties
		//-------------------------------------------------------------------

		/// <summary>
		/// Gets/sets the enumerable list of Card objects to display
		/// </summary>
		[Browsable(false)]
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IEnumerable<Card> Cards
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
					// Scroll the list back to the top on a list change, this prevents odd
					// behaviors when the previous top item had an index higher than the new total
					if((m_listview.VirtualListSize > 0) && (m_listview.TopItem != null))
						m_listview.TopItem = m_listview.Items[0];

					// Clear out and reload the existing list of cards
					m_cards.Clear();
					m_cards.AddRange(value);

					// Update the number of virtual items available for the ListView control
					m_listview.VirtualListSize = m_cards.Count;
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
			// Take off a few pixels at the top and bottom of the cell to provide the "lines"
			// that serve as the visual separator between the items
			var adjustedBounds = args.Bounds.InflateDPI(0, -2, ApplicationTheme.ScalingFactor);

			// Render the backdrop for the item
			bool isdark = ApplicationTheme.DarkMode ? !args.Item.Selected : args.Item.Selected;
			using(GraphicsPath gp = new GraphicsPath())
			{
				float CornerRadius = 4.ScaleDPI(ApplicationTheme.ScalingFactor) * 2.0F;
				gp.AddArc(adjustedBounds.Left -1, adjustedBounds.Top -1, CornerRadius, CornerRadius, 180, 90);
				gp.AddArc(adjustedBounds.Left + adjustedBounds.Width - CornerRadius, adjustedBounds.Top -1, CornerRadius, CornerRadius, 270, 90);
				gp.AddArc(adjustedBounds.Left + adjustedBounds.Width - CornerRadius, adjustedBounds.Top + adjustedBounds.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
				gp.AddArc(adjustedBounds.Left -1, adjustedBounds.Top + adjustedBounds.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
				args.Graphics.SetClip(gp);
				args.Graphics.DrawImage(SelectBackdrop((Card)args.Item.Tag, isdark), adjustedBounds);
				args.Graphics.ResetClip();
			}
		}

		/// <summary>
		/// Invoked when a subitem of the ListView control needs to be drawn
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Draw item event arguments</param>
		private void OnDrawSubItem(object sender, DrawListViewSubItemEventArgs args)
		{
			var adjustedBounds = args.SubItem.Bounds.InflateDPI(-4, -2, ApplicationTheme.ScalingFactor);

			// Use high-quality text rendering for the subitem
			args.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			args.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			args.Graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

			// TextRenderer does a better job than Graphics.DrawString, quality-wise
			bool isdark = ApplicationTheme.DarkMode ? !args.Item.Selected : args.Item.Selected;
			TextRenderer.DrawText(args.Graphics, args.SubItem.Text, args.SubItem.Font, adjustedBounds,
				isdark ? Color.White : Color.Black, TextFormatFlags.VerticalCenter);
		}

		/// <summary>
		/// Invoked when the ListView control is resized
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnResize(object sender, EventArgs args)
		{
			// Resize the column(s) of the list view
			m_listviewcolumn.Width = m_listview.ClientSize.Width - 4.ScaleDPI(ApplicationTheme.ScalingFactor);
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
		// Private Member Functions
		//---------------------------------------------------------------------

		/// <summary>
		/// Selects the backdrop bitmap image for the ListView item
		/// </summary>
		/// <param name="card">Selected Card instance</param>
		/// <param name="dark">Flag indicating if the dark backdrop should be used</param>
		Bitmap SelectBackdrop(Card card, bool dark)
		{
			// SPELL CARD
			if(card.Type == CardType.Spell) return dark ? s_backdrop_spell_dark : s_backdrop_spell_light;

			// TRAP CARD
			else if(card.Type == CardType.Trap) return dark ? s_backdrop_trap_dark : s_backdrop_trap_light;

			// MONSTER CARD
			else if(card is MonsterCard monster)
			{
				if(monster.Normal) return dark ? s_backdrop_normal_dark : s_backdrop_normal_light;
				else if(monster.Fusion) return dark ? s_backdrop_fusion_dark : s_backdrop_fusion_light;
				else if(monster.Ritual) return dark ? s_backdrop_ritual_dark : s_backdrop_ritual_light;
			}

			// DEFAULT: EFFECT MONSTER CARD
			return dark ? s_backdrop_effect_dark : s_backdrop_effect_light;
		}

		//---------------------------------------------------------------------
		// Member Variables
		//---------------------------------------------------------------------

		/// <summary>
		/// Backing List<> for the virtual list view
		/// </summary>
		private readonly List<Card> m_cards = new List<Card>();

		/// <summary>
		/// Dark effect monster backdrop
		/// </summary>
		private static readonly Bitmap s_backdrop_effect_dark = Resources.listbackdrop_effect_dark;

		/// <summary>
		/// Dark fusion monster backdrop
		/// </summary>
		private static readonly Bitmap s_backdrop_fusion_dark = Resources.listbackdrop_fusion_dark;

		/// <summary>
		/// Dark normal monster backdrop
		/// </summary>
		private static readonly Bitmap s_backdrop_normal_dark = Resources.listbackdrop_normal_dark;

		/// <summary>
		/// Dark ritual monster backdrop
		/// </summary>
		private static readonly Bitmap s_backdrop_ritual_dark = Resources.listbackdrop_ritual_dark;

		/// <summary>
		/// Dark spell backdrop
		/// </summary>
		private static readonly Bitmap s_backdrop_spell_dark = Resources.listbackdrop_spell_dark;

		/// <summary>
		/// Dark trap backdrop
		/// </summary>
		private static readonly Bitmap s_backdrop_trap_dark = Resources.listbackdrop_trap_dark;

		/// <summary>
		/// Light effect monster backdrop
		/// </summary>
		private static readonly Bitmap s_backdrop_effect_light = Resources.listbackdrop_effect_light;

		/// <summary>
		/// Light fusion monster backdrop
		/// </summary>
		private static readonly Bitmap s_backdrop_fusion_light = Resources.listbackdrop_fusion_light;

		/// <summary>
		/// Light normal monster backdrop
		/// </summary>
		private static readonly Bitmap s_backdrop_normal_light = Resources.listbackdrop_normal_light;

		/// <summary>
		/// Light ritual monster backdrop
		/// </summary>
		private static readonly Bitmap s_backdrop_ritual_light = Resources.listbackdrop_ritual_light;

		/// <summary>
		/// Light spell backdrop
		/// </summary>
		private static readonly Bitmap s_backdrop_spell_light = Resources.listbackdrop_spell_light;

		/// <summary>
		/// Light trap backdrop
		/// </summary>
		private static readonly Bitmap s_backdrop_trap_light = Resources.listbackdrop_trap_light;
	}
}
