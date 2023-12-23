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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

using zuki.ronin.data;

namespace zuki.ronin.ui
{
	/// <summary>
	/// Implements the card text view
	/// </summary>
	public partial class CardText : UserControlBase
	{
		/// <summary>
		/// Instance Constructor
		/// </summary>
		public CardText()
		{
			InitializeComponent();

			// The background has to be completely redrawn when resized
			ResizeRedraw = true;

			// Reset the theme based on the current settings
			OnApplicationThemeChanged(this, EventArgs.Empty);
		}

		//---------------------------------------------------------------------
		// Member Functions
		//---------------------------------------------------------------------

		/// <summary>
		/// Sets the image from a Card instance
		/// </summary>
		/// <param name="card">Card for which to render and display the image</param>
		public void SetCard(Card card)
		{
			m_topline = m_text = string.Empty; 
			
			if(card != null)
			{
				m_text = card.Text;

				if(card is MonsterCard)
				{
					MonsterCard monster = (MonsterCard)card;
					if(monster.Fusion && monster.Effect)
					{
						// Effect fusion monsters don't always have materials listed, use the presence
						// a CRLF pair in the string to detect
						int linebreak = monster.Text.IndexOf("\r\n");
						if(linebreak > 0)
						{
							m_topline = monster.Text.Substring(0, linebreak + 2).TrimEnd(new char[] { '\r', '\n' });
							m_text = monster.Text.Substring(linebreak + 2);
						}
					}
				}
			}

			Invalidate();
		}

		//---------------------------------------------------------------------
		// Event Handlers
		//---------------------------------------------------------------------

		/// <summary>
		/// Invoked when the application theme has changed
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		protected override void OnApplicationThemeChanged(object sender, EventArgs args)
		{
			base.OnApplicationThemeChanged(sender, args);
		}

		/// <summary>
		/// Invoked when the control has been loaded
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnLoad(object sender, EventArgs args)
		{
			//// Reposition/resize the PictureBox after the control has been loaded
			//m_image.Location = new Point(12.ScaleDPI(ApplicationTheme.ScalingFactor), 12.ScaleDPI(ApplicationTheme.ScalingFactor));
			//m_image.Size = new Size(Width - 2 * 12.ScaleDPI(ApplicationTheme.ScalingFactor), Height - 2 * 12.ScaleDPI(ApplicationTheme.ScalingFactor));
		}

		/// <summary>
		/// Invoked when the UserControl requires painting
		/// </summary>
		/// <param name="args">Paint event arguments</param>
		protected override void OnPaint(PaintEventArgs args)
		{
			base.OnPaint(args);

			// Fill with the background color first
			args.Graphics.FillRectangle(new SolidBrush(BackColor), ClientRectangle);

			// Render the item inside a GraphicsPath to round the corners
			using(GraphicsPath gp = new GraphicsPath())
			{
				Rectangle bounds = args.ClipRectangle.InflateDPI(-4, -4, ApplicationTheme.ScalingFactor);
				float CornerRadius = 8.ScaleDPI(ApplicationTheme.ScalingFactor) * 2.0F;
				gp.AddArc(bounds.Left - 1, bounds.Top - 1, CornerRadius, CornerRadius, 180, 90);
				gp.AddArc(bounds.Left + bounds.Width - CornerRadius, bounds.Top - 1, CornerRadius, CornerRadius, 270, 90);
				gp.AddArc(bounds.Left + bounds.Width - CornerRadius, bounds.Top + bounds.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
				gp.AddArc(bounds.Left - 1, bounds.Top + bounds.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
				args.Graphics.SetClip(gp);

				// Draw the background color
				args.Graphics.FillRectangle(new SolidBrush(ApplicationTheme.PanelBackColor), ClientRectangle);

				// Render the text fully justified
				if(!string.IsNullOrEmpty(m_text))
				{
					RectangleF textbounds = new RectangleF(args.ClipRectangle.Left + 12.ScaleDPI(ApplicationTheme.ScalingFactor),
						args.ClipRectangle.Top + 12.ScaleDPI(ApplicationTheme.ScalingFactor), args.ClipRectangle.Width - 2 * 12.ScaleDPI(ApplicationTheme.ScalingFactor),
						args.ClipRectangle.Height - 2 * 12.ScaleDPI(ApplicationTheme.ScalingFactor));
					JustifyText2(args.Graphics, textbounds, Font, new SolidBrush(ApplicationTheme.PanelForeColor), m_text, m_topline);
				}

				args.Graphics.ResetClip();
			}
		}

		//---------------------------------------------------------------------
		// Private Member Functions
		//---------------------------------------------------------------------

		/// <summary>
		/// Counts the number of spaces in a string
		/// </summary>
		/// <param name="str">string instance</param>
		private static int CountSpaces(string str)
		{
			int count = 0;
			char[] testchars = str.ToCharArray();
			int length = testchars.Length;
			for(int n = 0; n < length; n++)
			{
				if(testchars[n] == ' ')
					count++;
			}
			return count;
		}

		/// <summary>
		/// Fully justifies text in a rectangle
		/// </summary>
		/// <param name="graphics">Graphics instance</param>
		/// <param name="bounds">Bounding rectangle</param>
		/// <param name="font">Base font</param>
		/// <param name="brush">Text color brush</param>
		/// <param name="text">Text to be justified</param>
		/// <param name="topline">Topline text to be justified (fusion monsters)</param>
		private static void JustifyText2(Graphics graphics, RectangleF bounds, Font font, Brush brush, string text,
			string topline)
		{
			if(graphics == null) throw new ArgumentNullException(nameof(graphics));
			if(font == null) throw new ArgumentNullException(nameof(font));
			if(brush == null) throw new ArgumentNullException(nameof(brush));
			if(text == null) throw new ArgumentNullException(nameof(text));

			graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

			// The rendering format requires GDI+ to measure trailing spaces in a string
			StringFormat renderformat = new StringFormat(StringFormat.GenericTypographic);
			renderformat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

			// Remove any CR/LF/SPACE characters from the end of both the topline and text strings
			if(topline != null) topline = topline.TrimEnd(new char[] { '\r', '\n', ' ' });
			text = text.TrimEnd(new char[] { '\r', '\n', ' ' });

			// Fusion monsters require the top line of text to be justified differently, accomodate
			// the necessary vertical space by adding a CRLF to the beginning of the effect text
			string measuretext = string.IsNullOrEmpty(topline) ? text : "\r\n" + text;

			// Calculate the available boundaries for the justified text
			RectangleF textbounds = new RectangleF(0.0F, 0.0F, bounds.Width, bounds.Height);

			// Determine the maximum number of lines of text that will fit
			float lineheight = graphics.MeasureString("\r\n", font, int.MaxValue, renderformat).Height;
			int maxlines = (int)Math.Floor(bounds.Height / lineheight);

			// Break up the text into individual lines based on the available width
			bool hack = false;	// TODO
			List<string> lines = new List<string>();
			foreach(string hardline in measuretext.Split(new string[] { "\r\n" }, StringSplitOptions.None))
			{
				if(hack) break;
				string line = string.Empty;

				// Break up the hard line of text into individual words
				string[] words = hardline.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

				// Accumulate words until we run out of words or run out of width to draw them
				int wordindex = 0;
				while(wordindex < words.Length)
				{
					// Append the next word to the current line and calculate the new width
					string linetemp = line;
					linetemp += ((line.Length == 0) ? "" : " ") + words[wordindex];
					SizeF linesize = graphics.MeasureString(linetemp, font, int.MaxValue, renderformat);

					// If the new word would exceed the available width, terminate the line
					if(linesize.Width > textbounds.Width)
					{
						// Don't end a line on a bullet character
						if((wordindex > 0) && (words[wordindex - 1] == "●"))
						{
							line = line.TrimEnd(new char[] { ' ', '●' });
							wordindex--;
						}

						// If the final word that will fit on the line contains a hypen, see if
						// breaking that word after the hyphen will allow it to fit and adjust
						else if(words[wordindex].Contains("-"))
						{
							string[] hyphenated = words[wordindex].Split(new char[] { '-' }, 2);
							if(hyphenated.Length == 2)
							{
								string hyphentemp = line + ((line.Length == 0) ? "" : " ") + hyphenated[0] + "-";
								SizeF hyphensize = graphics.MeasureString(hyphentemp, font, int.MaxValue, renderformat);
								if(hyphensize.Width <= textbounds.Width)
								{
									line = hyphentemp;
									words[wordindex] = hyphenated[1];
								}
							}
						}

						// If we're going to run out of lines of text, make the last line contain
						// all of the remaining text
						if(((lines.Count + 1) >= maxlines) && (wordindex < words.Length))
						{
							while(wordindex < words.Length) line += " " + words[wordindex++];
							hack = true;
							//string ellipsistemp = line.TrimEnd(new char[] { '.', ',', ';' }) + " …";
							//SizeF ellipsissize = graphics.MeasureString(ellipsistemp, font, int.MaxValue, renderformat);
							//if(ellipsissize.Width > textbounds.Width)
							//{
							//	// TODO
							//	int x = 123;
							//}

							//line = ellipsistemp;
							//wordindex = words.Length;
						}

						lines.Add(line);
						line = string.Empty;
					}

					else
					{
						// Still under the width limit; move onto the next word in this line
						line = linetemp;
						wordindex++;
					}
				}

				// When the line didn't take up all the available width, terminate with a CRLF
				if(!hack) lines.Add(line + "\r\n");
			}

			// Draw the topline text as a single line using ellipsis if necessary
			if(!string.IsNullOrEmpty(topline))
			{
				StringFormat toplineformat = new StringFormat(renderformat);
				toplineformat.FormatFlags |= StringFormatFlags.NoWrap;
				toplineformat.Trimming = StringTrimming.EllipsisWord;
				graphics.DrawString(topline, font, brush, bounds, toplineformat);
			}

			// Current rendering vertical offset
			float top = bounds.Top;

			// Iterate over the generated lines of text to render
			for(int index = 0; index < lines.Count; index++)
			{
				// Current rendering horizontal offset
				float left = bounds.Left;

				// Render with full justification unless this is the last line or the
				// line ends with a CRLF break
				if((index + 1 < lines.Count) && !lines[index].EndsWith("\r\n"))
				{
					string line = lines[index];

					// Replace any spaces after a bullet with a non-breaking space
					if(line.Contains("● ")) line = line.Replace("● ", "●\u00a0");

					// Calculate the metrics to fully justify the line of text
					float lineextra = bounds.Width - graphics.MeasureString(line, font, int.MaxValue, renderformat).Width;
					int numspaces = CountSpaces(line);
					float spaceextra = (numspaces == 0) ? 0.0F : (float)Math.Floor(lineextra / numspaces);
					float charextra = (lineextra - (spaceextra * numspaces)) / line.Length;

					// Render each character in the string individually
					foreach(char character in line)
					{
						string str = new string(character, 1);
						float strwidth = graphics.MeasureString(str, font, int.MaxValue, renderformat).Width;
						graphics.DrawString(str, font, brush, new PointF(left, top), renderformat);
						left += strwidth + ((character == ' ') ? spaceextra : charextra);
					}
				}

				// The last line of text or a line with a CRLF break is rendered without justification
				else
				{
					StringFormat toplineformat = new StringFormat(StringFormat.GenericTypographic);
					toplineformat.FormatFlags |= StringFormatFlags.NoWrap;
					toplineformat.Trimming = StringTrimming.EllipsisWord;

					graphics.DrawString(lines[index].Replace("\r\n", ""), font, brush, new RectangleF(bounds.Left, top, bounds.Width, float.MaxValue), toplineformat);
				}

				// Move to the next line of text
				top += lineheight;
			}
		}

		//---------------------------------------------------------------------
		// Member Variables
		//---------------------------------------------------------------------

		/// <summary>
		/// The "top line" text to be displayed (Fusion Materials)
		/// </summary>
		private string m_topline = string.Empty;

		/// <summary>
		/// The text to be displayed
		/// </summary>
		private string m_text = string.Empty;
	}
}
