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
using zuki.ronin.data;

namespace zuki.ronin.renderer
{
	/// <summary>
	/// Implements the trap card rendering engine
	/// </summary>
	internal class TrapCardRenderer
	{
		/// <summary>
		/// Instance Constructor
		/// </summary>
		public TrapCardRenderer() : this(RenderFlags.None)
		{
		}

		/// <summary>
		/// Instance Constructor
		/// </summary>
		/// <param name="flags">Rendering flags</param>
		public TrapCardRenderer(RenderFlags flags)
		{
			m_flags = flags;
		}

		/// <summary>
		/// Renders a trap card
		/// </summary>
		/// <param name="card">TrapCard to be rendered</param>
		public Bitmap RenderCard(TrapCard card)
		{
			return null;
		}

		/// <summary>
		/// Renders a specific print of a trap card
		/// </summary>
		/// <param name="card">TrapCard to be rendered</param>
		/// <param name="print">Print information about the TrapCard</param>
		public Bitmap RenderPrint(TrapCard card, Print print)
		{
			return null;
		}

		//-------------------------------------------------------------------------
		// Member Variables

		private readonly RenderFlags m_flags;
	}
}
