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

using System.Drawing;

using zuki.ronin.data;

namespace zuki.ronin.renderer
{
	/// <summary>
	/// Describes the textures that can be used to draw text
	/// </summary>
	internal static class NameBrush
	{
		//---------------------------------------------------------------------
		// Fields
		//---------------------------------------------------------------------

		/// <summary>
		/// Solid black brush
		/// </summary>
		public static Brush SolidBlack => s_solidblack;

		/// <summary>
		/// Solid gold brush
		/// </summary>
		public static Brush GoldFoil => s_solidgold;

		/// <summary>
		/// Solid silver brush
		/// </summary>
		public static Brush SilverFoil => s_solidsilver;

		/// <summary>
		/// Solid white brush
		/// </summary>
		public static Brush SolidWhite => s_solidwhite;

		//---------------------------------------------------------------------
		// Member Functions
		//---------------------------------------------------------------------

		/// <summary>
		/// Selects the brush based on the card type and print rarity
		/// </summary>
		/// <param name="cardtype">Type of card being rendered</param>
		/// <param name="rarity">Print rarity of the card</param>
		public static Brush FromRarity(CardType cardtype, PrintRarity rarity)
		{
			switch(rarity)
			{
				// GoldRare
				// UltraParallelRare
				// UltraRare
				case PrintRarity.GoldRare:
				case PrintRarity.UltraParallelRare:
				case PrintRarity.UltraRare:
					return s_solidgold;

				// TODO: Prismatic Secret Rare

				// Rare
				case PrintRarity.Rare:
					return s_solidsilver;

				// TODO: Secret Rare
			}

			// Common
			// ParallelRare
			// SuperRare
			if(cardtype == CardType.Monster) return s_solidblack;
			else return s_solidwhite;
		}

		//-------------------------------------------------------------------
		// Member Variables
		//-------------------------------------------------------------------

		/// <summary>
		/// Solid black brush
		/// </summary>
		private static readonly Brush s_solidblack = new SolidBrush(Color.Black);

		/// <summary>
		/// Gold foil texture brush
		/// </summary>
		private static readonly Brush s_solidgold = new SolidBrush(Color.Gold);

		/// <summary>
		/// Silver foil texture brush
		/// </summary>
		private static readonly Brush s_solidsilver = new SolidBrush(Color.FromArgb(0xBC, 0xC6, 0xCC));

		/// <summary>
		/// Solid white brush
		/// </summary>
		private static readonly Brush s_solidwhite = new SolidBrush(Color.White);
	}
}
