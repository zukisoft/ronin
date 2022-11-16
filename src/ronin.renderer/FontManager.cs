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
using System.Drawing.Text;
using System.Runtime.InteropServices;

using zuki.ronin.renderer.Properties;

namespace zuki.ronin.renderer
{
	/// <summary>
	/// Manages the creation of font instances
	/// </summary>
	internal static class FontManager
	{
		#region EmbeddedFonts Class
		public struct EmbeddedFonts
		{
			public const string MatrixBoldSmallCaps = "Matrix Bold Small Caps";
			public const string MatrixBook = "Matrix Book";
			public const string MatrixSmallCaps = "Matrix Small Caps";
			public const string StoneSerifBoldSmallCaps = "ITC Stone Serif Bold Small Caps";
			public const string StoneSerifLT = "ITC Stone Serif LT";
			public const string StoneSerifLTItalic = "ITC Stone Serif LT Italic";
			public const string StoneSerifSemibold = "ITC Stone Serif Semibold";
		}
		#endregion

		/// <summary>
		/// Static Constructor
		/// </summary>
		static FontManager()
		{
			// Load the embedded font objects
			AddFontToCollection(Resources.matrixboldsmallcaps);
			AddFontToCollection(Resources.matrixbook);
			AddFontToCollection(Resources.matrixsmallcaps);
			AddFontToCollection(Resources.stoneserifboldsmallcaps);
			AddFontToCollection(Resources.stoneseriflt);
			AddFontToCollection(Resources.stoneserifltitalic);
			AddFontToCollection(Resources.stoneserifsemibold);
		}

		//---------------------------------------------------------------------
		// Member Functions
		//---------------------------------------------------------------------

		/// <summary>
		/// Creates a font object; using the generic or embedded fonts as necessary
		/// </summary>
		/// <param name="family">Font Family name</param>
		/// <param name="size">Font Size</param>
		/// <param name="style">Font Style</param>
		/// <param name="graphicsunit">GraphicsUnit to use for size</param>
		public static Font Create(string family, float size, FontStyle style, GraphicsUnit graphicsunit)
		{
			foreach(var fontfamily in s_fonts.Families)
			{
				// The array of private fonts has be iterated to find the font family
				if(string.Compare(family, fontfamily.Name, true) == 0)
				{
					return new Font(fontfamily, size, style, graphicsunit);
				}
			}

			// Supply a generic sans-serif font if the family was not found
			return new Font(new FontFamily(GenericFontFamilies.SansSerif), size, style, graphicsunit);
		}

		//---------------------------------------------------------------------
		// Private Member Functions
		//---------------------------------------------------------------------

		/// <summary>
		/// Adds a font into the private font collection and returns the family object
		/// </summary>
		/// <param name="font">Font in byte array format</param>
		private static void AddFontToCollection(byte[] font)
		{
			if(font == null) throw new ArgumentException("font");

			// Pin the byte array
			GCHandle pin = GCHandle.Alloc(font, GCHandleType.Pinned);

			// Attempt to add the private font, and release the pinned pointer
			try { s_fonts.AddMemoryFont(pin.AddrOfPinnedObject(), font.Length); }
			finally { pin.Free(); }
		}

		//---------------------------------------------------------------------
		// Member Variables
		//---------------------------------------------------------------------

		/// <summary>
		/// Embedded fonts collection
		/// </summary>
		private static readonly PrivateFontCollection s_fonts = new PrivateFontCollection();
	}
}
