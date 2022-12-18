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
using System.Text;
using System.Text.RegularExpressions;

using zuki.ronin.data;
using zuki.ronin.util;

namespace zuki.ronin.ui
{
	/// <summary>
	/// Implements the markdown-based rulings web view
	/// </summary>
	public partial class RulingsWebView : UserControlBase
	{
		/// <summary>
		/// Instance Constructor
		/// </summary>
		public RulingsWebView()
		{
			InitializeComponent();

			// Reset the theme based on the current settings
			OnApplicationThemeChanged(this, EventArgs.Empty);
		}

		//---------------------------------------------------------------------
		// Member Functions
		//---------------------------------------------------------------------

		/// <summary>
		/// Sets the rulings to display
		/// </summary>
		/// <param name="rulings">Ruling collection</param>
		public void SetRulings(Card card, List<Ruling> rulings)
		{
			string name = card == null ? string.Empty : card.Name;

			StringBuilder sb = new StringBuilder();

			if((rulings != null) && (rulings.Count > 0))
			{
				foreach(Ruling ruling in rulings)
				{
					sb.Append(ruling.Text);
					sb.Append("\r\n\r\n");
				}
			}

			string markdown = sb.ToString().TrimEnd(new char[] { '\r', '\n' });
			foreach(Match match in Regex.Matches(markdown, "\\[\\[(?<cardname>.*?)\\]\\]"))
			{
				string cardname = match.Result("${cardname}");
				string replace = "[[" + cardname + "]]";
				string replacewith = (cardname == name ? "**" + cardname + "**" : "[**" + cardname + "**](" + cardname.Replace(" ", "") + ")");
				markdown = markdown.Replace(replace, replacewith);
			}

			m_webbrowser.DocumentText = Markdown.ToHTML(markdown);
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
		}

		/// <summary>
		/// Invoked when the user control has been loaded
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnLoad(object sender, EventArgs args)
		{
		}

		//---------------------------------------------------------------------
		// Private Member Functions
		//---------------------------------------------------------------------

		//---------------------------------------------------------------------
		// Member Variables
		//---------------------------------------------------------------------
	}
}
