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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using zuki.ronin.util;

namespace zuki.ronin.ui
{
	/// <summary>
	/// Implements extension methods
	/// </summary>
	public static class Extensions
	{
		#region Win32 API Declarations
		private static class NativeMethods
		{
			public const int WS_EX_COMPOSITED = 0x02000000;

			public enum DWMWINDOWATTRIBUTE
			{
				DWMA_USE_IMMERSIVE_DARK_MODE_WIN10 = 19,
				DWMA_USE_IMMERSIVE_DARK_MODE = 20,
				DWMWA_WINDOW_CORNER_PREFERENCE = 33
			}

			public enum DWM_WINDOW_CORNER_PREFERENCE
			{
				DWMWCP_DEFAULT = 0,
				DWMWCP_DONOTROUND = 1,
				DWMWCP_ROUND = 2,
				DWMWCP_ROUNDSMALL = 3
			}

			[DllImport("dwmapi.dll")]
			public static extern long DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attribute, ref uint pvAttribute, uint cbAttribute);
		}
		#endregion

		/// <summary>
		/// Enables/disables DWM immersive dark mode
		/// </summary>
		/// <param name="form"></param>
		/// <param name="enable"></param>
		public static void EnableImmersiveDarkMode(this Form form, bool enable)
		{
			// Only available on Windows 10 1809 (17763) and above
			if(VersionHelper.IsWindows10OrGreater(17763))
			{
				// Beginning in Windows 10 20H1 (19041) the attribute became documented and changed values
				NativeMethods.DWMWINDOWATTRIBUTE attribute = VersionHelper.IsWindows10OrGreater(19041) ?
					NativeMethods.DWMWINDOWATTRIBUTE.DWMA_USE_IMMERSIVE_DARK_MODE :
					NativeMethods.DWMWINDOWATTRIBUTE.DWMA_USE_IMMERSIVE_DARK_MODE_WIN10;

				uint preference = (enable) ? 0xFFFFFFFF : 0;
				NativeMethods.DwmSetWindowAttribute(form.Handle, NativeMethods.DWMWINDOWATTRIBUTE.DWMA_USE_IMMERSIVE_DARK_MODE,
					ref preference, sizeof(uint));
			}
		}

		/// <summary>
		/// Enables/disables DWM rounded window corners
		/// </summary>
		/// <param name="form">Form instance</param>
		/// <param name="enable">Flag to enable/disable the rounded corners</param>
		public static void EnableRoundedCorners(this Form form, bool enable)
		{
			// Only available on Windows 11 and above
			if(VersionHelper.IsWindows11OrGreater())
			{
				// Apply rounded corners to the form
				uint preference = enable ? (uint)NativeMethods.DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND :
					(uint)NativeMethods.DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_DEFAULT;
				NativeMethods.DwmSetWindowAttribute(form.Handle, NativeMethods.DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE,
					ref preference, sizeof(uint));
			}
		}

		/// <summary>
		/// Enables double-buffering for a control via reflection
		/// </summary>
		/// <param name="control">Control to enable double-buffering for</param>
		public static void EnableDoubleBuffering(this Control control)
		{
			PropertyInfo property = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
			property.SetValue(control, true, null);
		}

		/// <summary>
		/// Fills a rounded rectangle specified by a bounding Rectangle with a common corner radius value for each corners
		/// </summary>
		/// <param name="graphics">Graphics instance</param>
		/// <param name="brush">Brush color to use</param>
		/// <param name="bounds">Rectangle bounds</param>
		/// <param name="cornerRadius">Radius to use for the rounding</param>
		public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle bounds, int cornerRadius)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(brush == null) throw new ArgumentNullException(nameof(brush));

			using(GraphicsPath path = CreateRoundedRectangle(bounds, cornerRadius))
			{
				graphics.FillPath(brush, path);
			}
		}

		/// <summary>
		/// Inflates a Rectangle based on a previously calculated scaling factor
		/// </summary>
		/// <param name="rectangle">Rectangle to be inflated</param>
		/// <param name="width">Unscaled width to inflate</param>
		/// <param name="height">Unscaled height to inflate</param>
		/// <param name="factor">Precalcualted scaling factor</param>
		/// <returns>Scaled Rectangle value</returns>
		public static Rectangle InflateDPI(this Rectangle rectangle, int width, int height, SizeF factor)
		{
			// Inflate the provided Rectangle based on the scaling factor
			int scaledwidth = (int)(width * factor.Width);
			int scaledheight = (int)(height * factor.Height);
			rectangle.Inflate(scaledwidth, scaledheight);

			// Return the scaled/inflated rectangle
			return rectangle;
		}

		/// <summary>
		/// Scales an int value based on a previously calculated scaling factor
		/// </summary>
		/// <param name="value">Value to be scaled</param>
		/// <param name="factor">Precalcuated scaling factor</param>
		/// <returns>Scaled int value</returns>
		public static int ScaleDPI(this int value, SizeF factor)
		{
			// Use the mean of the width and height scaling to apply to an integer
			return (int)(value * ((factor.Width + factor.Height) / 2.0F));
		}

		/// <summary>
		/// Scales a Padding value based on a previously calculated scaling factor
		/// </summary>
		/// <param name="padding">Padding to be scaled</param>
		/// <param name="factor">Precalcuated scaling factor</param>
		/// <returns>Scaled Padding value</returns>
		public static Padding ScaleDPI(this Padding padding, SizeF factor)
		{
			// Create a new Padding instance with the scaling factors applied
			return new Padding((int)(padding.Left * factor.Width), (int)(padding.Top * factor.Height),
				(int)(padding.Right * factor.Width), (int)(padding.Bottom * factor.Height));
		}

		/// <summary>
		/// Scales a Size value based on a previously calculated scaling factor
		/// </summary>
		/// <param name="size">Size to be scaled</param>
		/// <param name="factor">Precalcuated scaling factor</param>
		/// <returns>Scaled Size value</returns>
		public static Size ScaleDPI(this Size size, SizeF factor)
		{
			// Create a new Size instance with the scaling factors applied
			return new Size((int)(size.Width * factor.Width), (int)(size.Height * factor.Height));
		}

		//-------------------------------------------------------------------
		// Private Member Functions
		//-------------------------------------------------------------------

		/// <summary>
		/// Returns the path for a rounded rectangle specified by a bounding Rectangle and a common radius
		/// </summary>
		/// <param name="bounds">Rectangle boundaries</param>
		/// <param name="radius">Radius</param>
		private static GraphicsPath CreateRoundedRectangle(Rectangle bounds, int radius)
		{
			GraphicsPath path = new GraphicsPath();
			if(radius == 0)
			{
				path.AddRectangle(bounds);
				return path;
			}

			int diameter = radius * 2;
			Size size = new Size(diameter, diameter);
			Rectangle arc = new Rectangle(bounds.Location, size);

			// top left arc
			path.AddArc(arc, 180, 90);

			// top right arc
			arc.X = bounds.Right - diameter;
			path.AddArc(arc, 270, 90);

			// bottom right arc
			arc.Y = bounds.Bottom - diameter;
			path.AddArc(arc, 0, 90);

			// bottom left arc
			arc.X = bounds.Left;
			path.AddArc(arc, 90, 90);

			path.CloseFigure();
			return path;
		}

	}
}
