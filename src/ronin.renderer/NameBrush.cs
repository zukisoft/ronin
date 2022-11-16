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

using System.Drawing;

namespace zuki.ronin.renderer
{
	/// <summary>
	/// Describes the textures that can be used to draw text
	/// </summary>
	internal static class NameBrush
	{
		/// <summary>
		/// Solid black brush
		/// </summary>
		public static Brush SolidBlack => s_solidblack;

		/// <summary>
		/// Solid white brush
		/// </summary>
		public static Brush SolidWhite => s_solidwhite;

		//-------------------------------------------------------------------
		// Member Variables

		/// <summary>
		/// Solid black brush
		/// </summary>
		private static readonly SolidBrush s_solidblack = new SolidBrush(Color.Black);

		/// <summary>
		/// Solid white brush
		/// </summary>
		private static readonly SolidBrush s_solidwhite = new SolidBrush(Color.White);
	}
}
