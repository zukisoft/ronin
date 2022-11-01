//---------------------------------------------------------------------------
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

using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace zuki.ronin
{
	/// <summary>
	/// Implements extension methods
	/// </summary>
	internal static class Extensions
	{
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
	}
}
