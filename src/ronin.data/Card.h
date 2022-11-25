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

#ifndef __CARD_H_
#define __CARD_H_
#pragma once

#include "CardType.h"

#pragma warning(push, 4)

using namespace System;
using namespace System::Collections::Generic;

namespace zuki::ronin::data {

// FORWARD DECLARATIONS
//
ref class Artwork;
ref class Database;
ref class Print;

//---------------------------------------------------------------------------
// Class Card
//
// Describes a card object
//---------------------------------------------------------------------------

public ref class Card abstract
{
public:

	//-----------------------------------------------------------------------
	// Overloaded Operators

	// operator== (static)
	//
	static bool operator==(Card^ lhs, Card^ rhs);

	// operator!= (static)
	//
	static bool operator!=(Card^ lhs, Card^ rhs);

	//-----------------------------------------------------------------------
	// Member Functions

	// AddArtwork
	//
	// Adds new artwork for the card
	void AddArtwork(String^ format, int width, int height, array<Byte>^ image);

	// Equals
	//
	// Overrides Object::Equals()
	virtual bool Equals(Object^ rhs) override;

	// Equals
	//
	// Compares this Card instance to another Card instance
	bool Equals(Card^ rhs);

	// GetArtworks
	//
	// Gets all the artwork associated with the card
	List<Artwork^>^ GetArtworks(void);

	// GetDefaultArtwork
	//
	// Gets the default artwork associated with the card
	Artwork^ GetDefaultArtwork(void);

	// GetHashCode
	//
	// Overrides Object::GetHashCode()
	virtual int GetHashCode(void) override;

	// GetPrints
	//
	// Gets the Print objects associated with this Card
	List<Print^>^ GetPrints(void);

	// Refresh
	//
	// Refreshes the information for this Card from the database
	void Refresh(void);

	// ToString
	//
	// Overrides Object::ToString()
	virtual String^ ToString(void) override;

	// UpdateText
	//
	// Updates the text for this card in the database
	void UpdateText(String^ text);

	//-----------------------------------------------------------------------
	// Properties

	// ArtworkID
	//
	// Gets the artwork unique identifier
	property Guid ArtworkID
	{
		Guid get(void);
		internal: void set(Guid value);
	}

	// CardID
	//
	// Gets the card unique identifier
	property Guid CardID
	{
		Guid get(void);
		internal: void set(Guid value);
	}

	// Name
	//
	// Gets the card name
	property String^ Name
	{
		String^ get(void);
		internal: void set(String^ value);
	}

	// Passcode
	//
	// Gets the card passcode
	property String^ Passcode
	{
		String^ get(void);
		internal: void set(String^ value);
	}

	// Text
	//
	// Gets the card text
	property String^ Text
	{
		String^ get(void);
		internal: void set(String^ value);
	}

	// Type
	//
	// Gets the card type
	property CardType Type
	{
		CardType get(void);
	}

protected:

	// Instance Constructor
	//
	Card(Database^ database, CardType type);

private:

	//-----------------------------------------------------------------------
	// Member Variables

	initonly CardType		m_type;						// Card type
	initonly Database^		m_database;					// Database instance

	Guid					m_cardid;					// Unique identifier
	String^					m_name = String::Empty;		// Card name
	String^					m_passcode = String::Empty;	// Card passcode
	String^					m_text = String::Empty;		// Card text
	Guid					m_artworkid;				// Default artwork ID
};

//---------------------------------------------------------------------------

} // zuki::ronin::data

#pragma warning(pop)

#endif	// __CARD_H_
