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
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using zuki.ronin.data;
using zuki.ronin.renderer;

namespace zuki.ronin.ui
{
	/// <summary>
	/// Implements the card information view
	/// </summary>
	public partial class CardInfo : UserControlBase
	{
		/// <summary>
		/// Instance Constructor
		/// </summary>
		public CardInfo()
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
			m_name.Text = (card == null) ? "" : card.Name.Replace("&", "&&");
			label1.Text = GenerateCardStats(card);
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
			m_name.BackColor = label1.BackColor = ApplicationTheme.PanelBackColor;
			m_name.ForeColor = label1.ForeColor = ApplicationTheme.PanelForeColor;
		}

		/// <summary>
		/// Invoked when the control has been loaded
		/// </summary>
		/// <param name="sender">Object raising this event</param>
		/// <param name="args">Standard event arguments</param>
		private void OnLoad(object sender, EventArgs args)
		{
			//m_name.Location = new Point(12.ScaleDPI(ApplicationTheme.ScalingFactor), m_name.Top);
			m_name.Size = new Size(Width - 2 * 12.ScaleDPI(ApplicationTheme.ScalingFactor), m_name.Height);
			//label1.Location = new Point(12.ScaleDPI(ApplicationTheme.ScalingFactor), label1.Top);
			label1.Size = new Size(Width - 2 * 12.ScaleDPI(ApplicationTheme.ScalingFactor), label1.Height);


			//m_image.Location = new Point(12.ScaleDPI(ApplicationTheme.ScalingFactor), 12.ScaleDPI(ApplicationTheme.ScalingFactor));
			//m_image.Size = new Size(Width - 2 * 12.ScaleDPI(ApplicationTheme.ScalingFactor), Height - 12 * 6.ScaleDPI(ApplicationTheme.ScalingFactor));
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
				args.Graphics.ResetClip();
			}
		}

		//---------------------------------------------------------------------
		// Private Member Functions
		//---------------------------------------------------------------------

		private string GenerateCardStats(Card card)
		{
			if(card == null) return string.Empty;

			if(card is MonsterCard monstercard) return GenerateMonsterCardStats(monstercard);
			else if(card is SpellCard spellcard) return GenerateSpellCardStats(spellcard);
			else if(card is TrapCard trapcard) return GenerateTrapCardStats(trapcard);

			return string.Empty;
		}

		private string GenerateMonsterCardStats(MonsterCard card)
		{
			string statline;			// Generated monster card stat line

			// Normal monster
			if(card.Normal) statline = "Normal Monster";

			// Fusion [/Effect] monster
			else if(card.Fusion)
			{
				if(card.Effect) statline = "Fusion Effect Monster";
				else statline = "Fusion Monster";
			}

			// Ritual [/Effect] monster
			else if(card.Ritual)
			{
				if(card.Effect) statline = "Ritual Effect Monster";
				else statline = "Ritual Monster";
			}

			// Spirit monster
			else if(card.Spirit) statline = "Spirit Monster";

			// Toon monster
			else if(card.Toon) statline = "Toon Monster";

			// Union monster
			else if(card.Union) statline = "Union Monster";

			// Gemini monster
			else if(card.Gemini) statline = "Gemini Monster";

			// Effect monster
			else statline = "Effect Monster";

			// (Type
			statline += " (" + card.Type.EnumDescription();

			// Attribute
			statline += " / " + card.Attribute.EnumDescription();

			// Level
			statline += " / " + card.Level.ToString() + ((card.Level == 1) ? " Star" : " Stars");

			// Attack
			statline += " / ATK " + ((card.Attack < 0) ? "?" : card.Attack.ToString());

			// Defense)
			statline += " / DEF " + ((card.Defense < 0) ? "?" : card.Defense.ToString()) + ")";

			return statline;
		}

		private string GenerateSpellCardStats(SpellCard card)
		{
			if(card.Normal) return "Normal Spell";
			else if(card.Continuous) return "Continuous Spell";
			else if(card.Equip) return "Equip Spell";
			else if(card.Field) return "Field Spell";
			else if(card.QuickPlay) return "Quick-Play Spell";
			else if(card.Ritual) return "Ritual Spell";

			return string.Empty;
		}

		private string GenerateTrapCardStats(TrapCard card)
		{
			if(card.Normal) return "Normal Trap";
			else if(card.Continuous) return "Continuous Trap";
			else if(card.Counter) return "Counter Trap";

			return string.Empty;
		}
	}
}
