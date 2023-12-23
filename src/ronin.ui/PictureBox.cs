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

using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace zuki.ronin.ui
{
	/// <summary>
	/// Extends the Windows Forms PictureBox class
	/// </summary>
	public class PictureBox : System.Windows.Forms.PictureBox
	{
		/// <summary>
		/// Instance Constructor
		/// </summary>
		public PictureBox() : base()
		{
			InterpolationMode = InterpolationMode.Default;
		}

		//---------------------------------------------------------------------
		// Properties
		//---------------------------------------------------------------------

		/// <summary>
		/// Gets/sets the interpolation mode to use when drawing the image
		/// </summary>
		[Category("Behavior")]
		[DefaultValue(InterpolationMode.Default)]
		public InterpolationMode InterpolationMode { get; set; }

		//---------------------------------------------------------------------
		// PictureBox overrides
		//---------------------------------------------------------------------

		/// <summary>
		/// Invoked when the control is going to be painted
		/// </summary>
		/// <param name="args">Paint event arguments</param>
		protected override void OnPaint(PaintEventArgs args)
		{
			args.Graphics.InterpolationMode = InterpolationMode;
			base.OnPaint(args);
		}
	}
}
