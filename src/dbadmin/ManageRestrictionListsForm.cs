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

using zuki.ronin.data;
using zuki.ronin.ui;

namespace zuki.ronin
{
	/// <summary>
	/// Implements the restriction list management tools; this was
	/// pared down into just a viewer to not need to add a bunch of
	/// functions to the database layer when any updates can be done
	/// manually at this point if they become necessary
	/// </summary>
	public partial class ManageRestrictionListsForm : FormBase
	{
		/// <summary>
		/// Default Constructor
		/// </summary>
		private ManageRestrictionListsForm()
		{
			InitializeComponent();

			// Manual DPI scaling
			m_forbiddencards.Padding = m_forbiddencards.Padding.ScaleDPI(ApplicationTheme.ScalingFactor);
			m_limitedcards.Padding = m_limitedcards.Padding.ScaleDPI(ApplicationTheme.ScalingFactor);
			m_semilimitedcards.Padding = m_semilimitedcards.Padding.ScaleDPI(ApplicationTheme.ScalingFactor);

			// Reset the theme based on the current system settings
			OnApplicationThemeChanged(this, EventArgs.Empty);
		}

		/// <summary>
		/// Instance constructor
		/// </summary>
		/// <param name="database">Database instance to use</param>
		public ManageRestrictionListsForm(Database database) : this()
		{
			m_database = database ?? throw new ArgumentNullException(nameof(database));
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

			m_accentpanel.BackColor = ApplicationTheme.PanelBackColor;
			m_accentpanel.ForeColor = ApplicationTheme.PanelForeColor;
			m_lowerpanel.BackColor = ApplicationTheme.FormBackColor;
			m_lowerpanel.ForeColor = ApplicationTheme.FormForeColor;
		}

		/// <summary>
		/// Invoked when the form has been loaded
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnLoad(object sender, EventArgs args)
		{
			Reload();
		}

		/// <summary>
		/// Invoked when the selected restriction list index changes
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnSelectedListChanged(object sender, EventArgs args)
		{
			// No selection -- use a dummy List<>
			if(m_reslistcombo.SelectedItem == null)
			{
				var dummy = new List<Card>();
				m_forbiddencards.Cards = dummy;
				m_limitedcards.Cards = dummy;
				m_semilimitedcards.Cards = dummy;
				return;
			}

			RestrictionList list = (RestrictionList)m_reslistcombo.SelectedItem;

			// Reload all the individual restricted card lists
			m_forbiddencards.Cards = list.GetCards(Restriction.Forbidden);
			m_limitedcards.Cards = list.GetCards(Restriction.Limited);
			m_semilimitedcards.Cards = list.GetCards(Restriction.SemiLimited);
		}

		//---------------------------------------------------------------------
		// Private Member Functions
		//---------------------------------------------------------------------

		/// <summary>
		/// Reloads the restriction list information from the database
		/// </summary>
		private void Reload()
		{
			m_lists.Clear();
			m_database.EnumerateRestrictionLists(reslist => { m_lists.Add(reslist); });

			// Rebind the restriction list combo box to the List<>
			m_reslistcombo.DataSource = null;
			m_reslistcombo.DataSource = m_lists;
			m_reslistcombo.DisplayMember = "Name";
			m_reslistcombo.ValueMember = "EffectiveDate";
		}

		//---------------------------------------------------------------------
		// Member Variables
		//---------------------------------------------------------------------

		/// <summary>
		/// Underlying Database instance
		/// </summary>
		readonly Database m_database;

		/// <summary>
		/// Restriction list instances
		/// </summary>
		List<RestrictionList> m_lists = new List<RestrictionList>();
	}
}
