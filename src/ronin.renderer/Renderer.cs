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

using zuki.ronin.data;

namespace zuki.ronin.renderer
{
	/// <summary>
	/// Implements the card rendering engine
	/// </summary>
	public static class Renderer
    {
		/// <summary>
		/// Renders a generic instance of the specified card
		/// </summary>
		/// <param name="card">Card being rendered</param>
		public static Bitmap RenderCard(Card card)
		{
			if(card == null) throw new ArgumentException(nameof(card));

			if(card is MonsterCard monstercard)
			{
				MonsterCardRenderer renderer = new MonsterCardRenderer();
				return renderer.RenderCard(monstercard);
			}

			else if(card is SpellCard spellcard)
			{
				SpellCardRenderer renderer = new SpellCardRenderer();
				return renderer.RenderCard(spellcard);
			}

			else if(card is TrapCard trapcard)
			{
				TrapCardRenderer renderer = new TrapCardRenderer();
				return renderer.RenderCard(trapcard);
			}

			return null;
		}

		/// <summary>
		/// Renders a specific print of the specified card
		/// </summary>
		/// <param name="print">Print being rendered</param>
		public static Bitmap RenderPrint(Print print)
		{
			if(print == null) throw new ArgumentException(nameof(print));

			// Retrieve the Card information for the print from the database
			Card card = print.GetCard();
			if(card == null) throw new Exception("Failed to retrieve Card instance for the specified Print");

			if(card is MonsterCard monstercard)
			{
				MonsterCardRenderer renderer = new MonsterCardRenderer();
				return renderer.RenderCard(monstercard);
			}

			else if(card is SpellCard spellcard)
			{
				SpellCardRenderer renderer = new SpellCardRenderer();
				return renderer.RenderPrint(spellcard, print);
			}

			else if(card is TrapCard trapcard)
			{
				TrapCardRenderer renderer = new TrapCardRenderer();
				return renderer.RenderPrint(trapcard, print);
			}

			return null;
		}

		/// <summary>
		/// Renders a token card
		/// </summary>
		/// <param name="artwork">Artwork to apply to the token card</param>
		public static Bitmap RenderToken(Image artwork)
		{
			return null;
		}
	}
}
