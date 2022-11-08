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
	/// Implements the spell card rendering engine
	/// </summary>
	internal class SpellCardRenderer
	{
		/// <summary>
		/// Instance Constructor
		/// </summary>
		public SpellCardRenderer() : this(RenderFlags.None)
		{
		}

		/// <summary>
		/// Instance Constructor
		/// </summary>
		/// <param name="flags">Rendering flags</param>
		public SpellCardRenderer(RenderFlags flags)
		{
			m_flags = flags;
		}

		/// <summary>
		/// Renders a spell card
		/// </summary>
		/// <param name="card">SpellCard to be rendered</param>
		public Bitmap RenderCard(SpellCard card)
		{
			Bitmap bitmap = RenderCommon(card);

			return bitmap;
		}

		/// <summary>
		/// Renders a specific print of a spell card
		/// </summary>
		/// <param name="spellcard">SpellCard to be rendered</param>
		/// <param name="print">Print information about the SpellCard</param>
		public Bitmap RenderPrint(SpellCard spellcard, Print print)
		{
			Bitmap bitmap = RenderCommon(spellcard);

			return bitmap;
		}

		//-------------------------------------------------------------------------
		// Private Member Functions

		/// <summary>
		/// Renders the common elements between a Card render and a Print render
		/// </summary>
		/// <param name="spellcard">SpellCard instance</param>
		private Bitmap RenderCommon(SpellCard spellcard)
		{
			// Generate the background image for the spell card
			Bitmap background = Engine.RenderBackground(s_layout, m_flags, Background.Spell);
			
			return background;
		}

		//-------------------------------------------------------------------------
		// Member Variables

		/// <summary>
		/// Only the 'medium' renderer is currently supported
		/// </summary>
		private static Layout s_layout = new LayoutMedium();

		/// <summary>
		/// Specified special rendering flags
		/// </summary>
		private readonly RenderFlags m_flags;
	}
}
