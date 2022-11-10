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
using System.IO;
using System.Reflection;
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
			public const string MatrixBook = "MatrixBook";
			public const string MatrixSmallCaps = "MatrixSmallCaps";
			public const string StoneSerifLT = "StoneSerifLT";
		}
		#endregion

		/// <summary>
		/// Static Constructor
		/// </summary>
		static FontManager()
		{
			// Load the embedded font objects
			s_matrixbook = AddFontToCollection(Resources.matrixbook);
			s_matrixsmallcaps = AddFontToCollection(Resources.matrixsmallcaps);
			s_stoneseriflt = AddFontToCollection(Resources.stoneseriflt);
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
			FontFamily fontfamily = null;               // Font family object

			if(string.Compare(family, EmbeddedFonts.MatrixBook, true) == 0) fontfamily = s_matrixbook;
			else if(string.Compare(family, EmbeddedFonts.MatrixSmallCaps, true) == 0) fontfamily = s_matrixsmallcaps;
			else if(string.Compare(family, EmbeddedFonts.StoneSerifLT, true) == 0) fontfamily = s_stoneseriflt;

			// Supply a generic sans-serif font if the family was not found
			if(fontfamily == null) fontfamily = new FontFamily(GenericFontFamilies.SansSerif);

			// Construct and return the font object
			return new Font(fontfamily, size, style, graphicsunit);
		}

		//---------------------------------------------------------------------
		// Private Member Functions
		//---------------------------------------------------------------------

		/// <summary>
		/// Adds a font into the private font collection and returns the family object
		/// </summary>
		/// <param name="font">Font in byte array format</param>
		private static FontFamily AddFontToCollection(byte[] font)
		{
			if(font == null) throw new ArgumentException("font");

			// Pin the byte array
			GCHandle pin = GCHandle.Alloc(font, GCHandleType.Pinned);

			// Attempt to add the private font, and release the pinned pointer
			try { s_fonts.AddMemoryFont(pin.AddrOfPinnedObject(), font.Length); }
			finally { pin.Free(); }

			// Return the newly added font family object
			return s_fonts.Families[s_fonts.Families.Length - 1];
		}

		//---------------------------------------------------------------------
		// Member Variables
		//---------------------------------------------------------------------

		/// <summary>
		/// Embedded fonts collection
		/// </summary>
		private static PrivateFontCollection s_fonts = new PrivateFontCollection();

		/// <summary>
		/// Matrix Book
		/// </summary>
		private static FontFamily s_matrixbook;

		/// <summary>
		/// Matrix Small Caps
		/// </summary>
		private static FontFamily s_matrixsmallcaps;

		/// <summary>
		/// Stone Serif LT
		/// </summary>
		private static FontFamily s_stoneseriflt;
	}
}
