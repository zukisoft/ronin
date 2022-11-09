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
// Card::GetArtwork
//
// Gets the artwork associated with the card
//
// Arguments:
//
//	NONE

Bitmap^ Card::GetArtwork(void)
{
	return m_database->SelectArtwork(this);
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

} // zuki::ronin::data

#pragma warning(pop)
