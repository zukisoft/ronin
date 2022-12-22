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
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using zuki.ronin.data;
using zuki.ronin.util;

namespace zuki.ronin.ui
{
	/// <summary>
	/// Implements the markdown-based rulings web view
	/// </summary>
	public partial class RulingsView : UserControlBase
	{
		/// <summary>
		/// Instance Constructor
		/// </summary>
		public RulingsView()
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
			StringBuilder sb = new StringBuilder();
			string name = card == null ? string.Empty : card.Name;

			// Combine all of the individual rulings into a single string
			if((rulings != null) && (rulings.Count > 0))
			{
				foreach(Ruling ruling in rulings)
				{
					sb.Append(ruling.Text);
					sb.Append("\r\n\r\n");
				}
			}

			string markdown = sb.ToString().TrimEnd(new char[] { '\r', '\n' });

			// Create a List<> of all the unique card names present in the rulings
			List<string> cardnames = new List<string>();
			foreach(Match match in Regex.Matches(markdown, "\\[\\[(?<cardname>.*?)\\]\\]"))
			{
				string cardname = match.Result("${cardname}");
				if(!cardnames.Contains(cardname)) cardnames.Add(cardname);
			}

			// Convert all the card names into markdown hyperlinks
			foreach(string cardname in cardnames)
			{
				// The identity card gets BOLDed, referenced cards become hyperlinks
				// with the card name URL encoded ...
				string replacewith = (cardname == name ? "**" + cardname + "**" :
					"[**" + cardname + "**](" + WebUtility.UrlEncode(cardname) + ")");
				markdown = markdown.Replace("[[" + cardname + "]]", replacewith);
			}

			// Convert the Markdown into an HTML document body and render it
			m_currentbody = "<body>" + Markdown.ToHTML(markdown) + "</body>";
			RenderDocument();

			// Delay visibility of the browser until this function has been called
			if(!m_webbrowser.Visible) m_webbrowser.Visible = true;
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
			RenderDocument();
		}

		/// <summary>
		/// Invoked when the web browser has finished rendering a document
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Event arguments</param>
		private void OnDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs args)
		{
			if(args.Url.AbsoluteUri == "about:blank")
			{
				m_webbrowser.Document.BackColor = ApplicationTheme.PanelBackColor;
				m_webbrowser.Document.ForeColor = ApplicationTheme.PanelForeColor;
			}
		}

		//---------------------------------------------------------------------
		// Private Member Functions
		//---------------------------------------------------------------------

		/// <summary>
		/// Renders the current document as HTML
		/// </summary>
		private void RenderDocument()
		{
			// Assign the complete HTML document to the WebBrowser
			m_webbrowser.DocumentText = "<html>" + s_header + (ApplicationTheme.DarkMode ? s_styledark : s_stylelight) +
				m_currentbody + "</html>";
		}

		//---------------------------------------------------------------------
		// Member Variables
		//---------------------------------------------------------------------

		/// <summary>
		/// The current document body text
		/// </summary>
		private string m_currentbody = "<body/>";

		/// <summary>
		/// HTML header
		/// </summary>
		static string s_header = @"
<head>
    <meta http-equiv=""X-UA-Compatible"" content=""IE=11""/>
</head>
";
		/// <summary>
		/// Cascading Style Sheet for light theme
		/// </summary>
		static string s_stylelight = @"
<style>
    body {
        margin: 0 auto;
        background: #ffffff;
        font-family: Segoe UI;
        color: #000000;
        padding: 1em;
        font-size: .8em;
    }
    a {
        color: #4682b4;
        margin: 0;
        padding: 0;
        vertical-align: baseline;
        text-decoration: none;
    }
    ul, li {
        padding-top: 0;
		padding-bottom: 0;
		padding-left: 1.25em;
		padding-right: 0;
    }
    li {
        list-style-type: none;
        position: relative;
    }
    li::before {
        content: '\25cf';
        position: absolute;
        left: -0.8em;
        font-size: 1.1em;
    }
    blockquote {
        background: #e3e3e3;
        border-radius: .5em;
        margin-left: 2.4em;
        padding: .01em 1.25em;
        border-left: .5em solid #b0b0b0
    }
</style>
";
		/// <summary>
		/// Cascading Style Sheet for dark theme
		/// </summary>
		static string s_styledark = @"
<style>
    body {
        margin: 0 auto;
        background: #2b2b2b;
        font-family: Segoe UI;
        color: #ffffff;
        padding: 1em;
        font-size: .8em;
    }
    a {
        color: #87cefa;
        margin: 0;
        padding: 0;
        vertical-align: baseline;
        text-decoration: none;
    }
    ul, li {
        padding-top: 0;
		padding-bottom: 0;
		padding-left: 1.25em;
		padding-right: 0;
    }
    li {
        list-style-type: none;
        position: relative;
    }
    li::before {
        content: '\25cf';
        position: absolute;
        left: -0.8em;
        font-size: 1.1em;
    }
    blockquote {
        background: #4b4b4b;
        border-radius: .5em;
        margin-left: 2.4em;
        padding: .01em 1.25em;
        border-left: .5em solid #585858
    }
</style>
";
	}
}
