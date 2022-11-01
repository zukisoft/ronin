﻿//---------------------------------------------------------------------------
// Copyright (c) 2004-2022 Michael G. Brehm
// Copyright (c) 2007-2009 Sean M. Patterson
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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Microsoft.Win32;
using zuki.ronin.Properties;

namespace zuki.ronin
{
	/// <summary>
	/// Provides the theme color elements for the application
	/// </summary>
	internal static class ApplicationTheme
	{
		/// <summary>
		/// Static Constructor
		/// </summary>
		static ApplicationTheme()
		{
			if(Settings.Default.Theme == Theme.System) s_darkmode = GetSystemTheme() == Theme.Dark;
			else if(Settings.Default.Theme == Theme.Light) s_darkmode = false;
			else if(Settings.Default.Theme == Theme.Dark) s_darkmode = true;

			// Wire up a handler to watch for property changes
			Settings.Default.PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);
		}

		//-------------------------------------------------------------------------
		// Events
		//-------------------------------------------------------------------------

		/// <summary>
		/// Invoked when the application theme has changed
		/// </summary>
		public static event EventHandler Changed;

		//-------------------------------------------------------------------
		// Member Functions
		//-------------------------------------------------------------------

		/// <summary>
		/// Invoked when the system theme(s) have changed
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		public static void SystemThemesChanged(object sender, EventArgs args)
		{
			// This is only applicable if the setting is set to System
			if(Settings.Default.Theme == Theme.System)
			{
				// Get a new dark mode flag for the system, and if it has changed
				// inform any listeners on the Changed event to switch the theme
				bool dark = GetSystemTheme() == Theme.Dark;
				if(dark != s_darkmode)
				{
					s_darkmode = dark;
					Changed?.Invoke(typeof(ApplicationTheme), EventArgs.Empty);
				}
			}
		}

		//-------------------------------------------------------------------
		// Properties
		//-------------------------------------------------------------------

		/// <summary>
		/// The foreground color of a form
		/// </summary>
		public static Color FormForeColor => s_darkmode ? Color.White : Color.Black;

		/// <summary>
		/// The background color of a form
		/// </summary>
		public static Color FormBackColor => s_darkmode ? Color.FromArgb(0x20, 0x20, 0x20) : Color.FromArgb(0xF3, 0xF3, 0xF3);

		/// <summary>
		/// The text color of a hyperlink
		/// </summary>
		public static Color LinkColor => s_darkmode ? Color.LightSkyBlue : Color.SteelBlue;

		/// <summary>
		/// The background color of a menu item
		/// </summary>
		public static Color MenuBackColor => s_darkmode ? Color.FromArgb(0x2B, 0x2B, 0x2B) : Color.White;

		/// <summary>
		/// The border color of a menu item
		/// </summary>
		public static Color MenuBorderColor => s_darkmode ? Color.Black : Color.FromArgb(0x80, 0x80, 0x80);

		/// <summary>
		/// The foreground color of a menu item
		/// </summary>
		public static Color MenuForeColor => s_darkmode ? Color.White : Color.Black;

		/// <summary>
		/// The color of a highlighted menu item
		/// </summary>
		public static Color MenuHighlightColor => s_darkmode ? Color.FromArgb(0x4B, 0x4B, 0x4B) : Color.FromArgb(0xE3, 0xE3, 0xE3);

		/// <summary>
		/// The color of the image portion of a menu item
		/// </summary>
		public static Color MenuImageColor => s_darkmode ? Color.FromArgb(0x4B, 0x4B, 0x4B) : Color.FromArgb(0xE3, 0xE3, 0xE3);

		/// <summary>
		/// Accesses the ProfessionalColorTable instance
		/// </summary>
		public static ProfessionalColorTable ProfessionalColorTable => s_colortable;

		//-------------------------------------------------------------------
		// Event Handlers
		//-------------------------------------------------------------------

		/// <summary>
		/// Invoked when a settings property has been changed
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Property changed event arguments</param>
		private static void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			// Theme
			if(args.PropertyName == nameof(Settings.Default.Theme))
			{
				bool dark = s_darkmode;             // Flag for dark mode

				if(Settings.Default.Theme == Theme.System) dark = GetSystemTheme() == Theme.Dark;
				else if(Settings.Default.Theme == Theme.Light) dark = false;
				else if(Settings.Default.Theme == Theme.Dark) dark = true;

				// If the mode has changed, invoke the event to change it
				if(dark != s_darkmode)
				{
					s_darkmode = dark;
					Changed?.Invoke(typeof(ApplicationTheme), EventArgs.Empty);
				}
			}
		}

		//---------------------------------------------------------------------
		// Private Data Types
		//---------------------------------------------------------------------

		/// <summary>
		/// Implements a color palette for Professional rendered controls
		/// </summary>
		class ApplicationProfessionalColorTable : ProfessionalColorTable
		{
			/// <summary>
			/// Gets the starting color of the gradient used in the image margin of a ToolStripDropDownMenu
			/// </summary>
			public override Color ImageMarginGradientBegin => MenuImageColor;

			/// <summary>
			/// Gets the end color of the gradient used in the image margin of a ToolStripDropDownMenu
			/// </summary>
			public override Color ImageMarginGradientEnd => MenuImageColor;

			/// <summary>
			/// Gets the middle color of the gradient used in the image margin of a ToolStripDropDownMenu
			/// </summary>
			public override Color ImageMarginGradientMiddle => MenuImageColor;

			/// <summary>
			/// Gets the color that is the border color to use on a MenuStrip
			/// </summary>
			public override Color MenuBorder => MenuBorderColor;

			/// <summary>
			/// Gets the border color to use with a ToolStripMenuItem
			/// </summary>
			public override Color MenuItemBorder => MenuBorderColor;

			/// <summary>
			/// Gets the starting color of the gradient used when a top-level ToolStripMenuItem is pressed
			/// </summary>
			public override Color MenuItemPressedGradientBegin => MenuBackColor;

			/// <summary>
			/// Gets the end color of the gradient used when a top-level ToolStripMenuItem is pressed
			/// </summary>
			public override Color MenuItemPressedGradientEnd => MenuBackColor;

			/// <summary>
			/// Gets the middle color of the gradient used when a top-level ToolStripMenuItem is pressed
			/// </summary>
			public override Color MenuItemPressedGradientMiddle => MenuBackColor;

			/// <summary>
			/// Gets the solid color to use when a ToolStripMenuItem other than the top-level ToolStripMenuItem is selected
			/// </summary>
			public override Color MenuItemSelected => MenuHighlightColor;

			/// <summary>
			/// Gets the starting color of the gradient used when the ToolStripMenuItem is selected
			/// </summary>
			public override Color MenuItemSelectedGradientBegin => MenuHighlightColor;

			/// <summary>
			/// Gets the end color of the gradient used when the ToolStripMenuItem is selected
			/// </summary>
			public override Color MenuItemSelectedGradientEnd => MenuHighlightColor;

			/// <summary>
			/// Gets the starting color of the gradient used in the MenuStrip
			/// </summary>
			public override Color MenuStripGradientBegin => MenuBackColor;

			/// <summary>
			/// Gets the end color of the gradient used in the MenuStrip
			/// </summary>
			public override Color MenuStripGradientEnd => MenuBackColor;
		}

		//-------------------------------------------------------------------
		// Private Member Functions
		//-------------------------------------------------------------------

		/// <summary>
		/// Gets the system-wide application theme
		/// </summary>
		/// <returns>Current system-wide application theme</returns>
		private static Theme GetSystemTheme()
		{
			if(VersionHelper.IsWindows10OrGreater())
			{
				// NOTE: Assume one (light) here if the value is missing
				object value = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", 1);
				return ((value is int @int) && (@int == 0)) ? Theme.Dark : Theme.Light;
			}

			return Theme.Light;			// Always default to light theme
		}

		//-------------------------------------------------------------------
		// Member Variables
		//-------------------------------------------------------------------

		/// <summary>
		/// Flag indicating that dark mode has been set
		/// </summary>
		private static bool s_darkmode = false;

		/// <summary>
		/// The ProfessionalColorTable instance for the application theme
		/// </summary>
		private static readonly ProfessionalColorTable s_colortable = new ApplicationProfessionalColorTable();
	}
}
