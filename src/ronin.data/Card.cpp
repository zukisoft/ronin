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

#include "stdafx.h"
#include "Card.h"

#include "Artwork.h"
#include "Database.h"

#pragma warning(push, 4)

namespace zuki::ronin::data {

//---------------------------------------------------------------------------
// Card Constructor (protected)
//
// Arguments:
//
//	database	- Underlying Database instance
//	type		- Card type being constructed

Card::Card(Database^ database, CardType type) : m_database(database), m_type(type)
{
	if(CLRISNULL(database)) throw gcnew ArgumentNullException("database");
}

//---------------------------------------------------------------------------
// Card::operator == (static)

bool Card::operator==(Card^ lhs, Card^ rhs)
{
	if(Object::ReferenceEquals(lhs, rhs)) return true;
	if(Object::ReferenceEquals(lhs, nullptr) || Object::ReferenceEquals(rhs, nullptr)) return false;

	return lhs->CardID == rhs->CardID;
}

//---------------------------------------------------------------------------
// Card::operator != (static)

bool Card::operator!=(Card^ lhs, Card^ rhs)
{
	if(Object::ReferenceEquals(lhs, rhs)) return false;
	if(Object::ReferenceEquals(lhs, nullptr) || Object::ReferenceEquals(rhs, nullptr)) return true;

	return lhs->CardID != rhs->CardID;
}

//---------------------------------------------------------------------------
// Card::AddArtwork
//
// Adds new artwork for the card
//
// Arguments:
//
//	format		- Artwork image format
//	width		- Artwork image width
//	height		- Artwork image height
//	image		- Artwork image data

void Card::AddArtwork(String^ format, int width, int height, array<Byte>^ image)
{
	CLRASSERT(CLRISNOTNULL(m_database));
	m_database->InsertArtwork(m_cardid, format, width, height, image);
}

//---------------------------------------------------------------------------
// Card::ArtworkID::get
//
// Gets the default artwork unique identifier

Guid Card::ArtworkID::get(void)
{
	return m_artworkid;
}

//---------------------------------------------------------------------------
// Card::ArtworkID::set (internal)
//
// Sets the default artwork unique identifier

void Card::ArtworkID::set(Guid value)
{
	m_artworkid = value;
}

//---------------------------------------------------------------------------
// Card::CardID::get
//
// Gets the card unique identifier

Guid Card::CardID::get(void)
{
	return m_cardid;
}

//---------------------------------------------------------------------------
// Card::CardID::set (internal)
//
// Sets the card unique identifier

void Card::CardID::set(Guid value)
{
	m_cardid = value;
}

//---------------------------------------------------------------------------
// Card::Equals
//
// Compares this Card instance to another Card instance
//
// Arguments:
//
//	rhs		- Right-hand Card instance to compare against

bool Card::Equals(Card^ rhs)
{
	return (this == rhs);
}

//---------------------------------------------------------------------------
// Card::Equals
//
// Overrides Object::Equals()
//
// Arguments:
//
//	rhs		- Right-hand object instance to compare against

bool Card::Equals(Object^ rhs)
{
	if(Object::ReferenceEquals(rhs, nullptr)) return false;

	// Convert the provided object into a Card instance
	Card^ rhsref = dynamic_cast<Card^>(rhs);
	if(rhsref == nullptr) return false;

	return (this == rhsref);
}

//---------------------------------------------------------------------------
// Card::GetArtworks
//
// Gets all the artwork associated with the card
//
// Arguments:
//
//	NONE

List<Artwork^>^ Card::GetArtworks(void)
{
	CLRASSERT(CLRISNOTNULL(m_database));
	return m_database->SelectArtworks(m_cardid);
}

//---------------------------------------------------------------------------
// Card::GetDefaultArtwork
//
// Gets the default artwork associated with the card
//
// Arguments:
//
//	NONE

Artwork^ Card::GetDefaultArtwork(void)
{
	CLRASSERT(CLRISNOTNULL(m_database));
	return m_database->SelectArtwork(m_artworkid);
}

//---------------------------------------------------------------------------
// Card::GetHashCode
//
// Overrides Object::GetHashCode()
//
// Arguments:
//
//	NONE

int Card::GetHashCode(void)
{
	return m_cardid.GetHashCode();
}

//---------------------------------------------------------------------------
// Card::GetPrints
//
// Gets the Print objects associated with this Card
//
// Arguments:
//
//	NONE

List<Print^>^ Card::GetPrints(void)
{
	CLRASSERT(CLRISNOTNULL(m_database));
	return m_database->SelectPrints(this->CardID);
}

//---------------------------------------------------------------------------
// Card::Name::get
//
// Gets the card name

String^ Card::Name::get(void)
{
	return m_name;
}

//---------------------------------------------------------------------------
// Card::Name::set (internal)
//
// Sets the card name

void Card::Name::set(String^ value)
{
	m_name = value;
}

//---------------------------------------------------------------------------
// Card::Passcode::get
//
// Gets the card passcode

String^ Card::Passcode::get(void)
{
	return m_passcode;
}

//---------------------------------------------------------------------------
// Card::Passcode::set (internal)
//
// Sets the card passcode

void Card::Passcode::set(String^ value)
{
	m_passcode = value;
}

//---------------------------------------------------------------------------
// Card::Refresh
//
// ReRefreshes the information for this Card from the database
//
// Arguments:
//
//	NONE

void Card::Refresh(void)
{
	CLRASSERT(CLRISNOTNULL(m_database));

	// Re-select this Card from the database
	Card^ card = m_database->SelectCard(m_cardid);

	// Update the member variables to reflect the new information
	m_name = card->Name;
	m_passcode = card->Passcode;
	m_text = card->Text;
	m_artworkid = card->ArtworkID;
}

//---------------------------------------------------------------------------
// Card::Text::get
//
// Gets the card text

String^ Card::Text::get(void)
{
	return m_text;
}

//---------------------------------------------------------------------------
// Card::Text::set (internal)
//
// Sets the card text

void Card::Text::set(String^ value)
{
	m_text = value;
}

//---------------------------------------------------------------------------
// Card::ToString
//
// Overrides Object::ToString()
//
// Arguments:
//
//	NONE

String^ Card::ToString(void)
{
	return m_name;
}

//---------------------------------------------------------------------------
// Card::Type::get
//
// Gets the card type

CardType Card::Type::get(void)
{
	return m_type;
}

//---------------------------------------------------------------------------
// Card::UpdateText
//
// Updates the text for this card in the database
//
// Arguments:
//
//	text		- New text to assign to the card

void Card::UpdateText(String^ text)
{
	CLRASSERT(CLRISNOTNULL(m_database));

	m_database->UpdateCardText(m_cardid, text);
	m_text = (CLRISNULL(text)) ? "" : text;
}

//---------------------------------------------------------------------------

} // zuki::ronin::data

#pragma warning(pop)
