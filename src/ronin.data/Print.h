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

#ifndef __PRINT_H_
#define __PRINT_H_
#pragma once

#include "PrintRarity.h"
#include "Series.h"

#pragma warning(push, 4)

using namespace System;
using namespace System::Drawing;

namespace zuki::ronin::data {

// FORWARD DECLARATIONS
//
ref class Card;
ref class Database;

//---------------------------------------------------------------------------
// Class Print
//
// Describes a print object
//---------------------------------------------------------------------------

public ref class Print
{
public:

	//-----------------------------------------------------------------------
	// Overloaded Operators

	// operator== (static)
	//
	static bool operator==(Print^ lhs, Print^ rhs);

	// operator!= (static)
	//
	static bool operator!=(Print^ lhs, Print^ rhs);

	//-----------------------------------------------------------------------
	// Member Functions

	// Equals
	//
	// Overrides Object::Equals()
	virtual bool Equals(Object^ rhs) override;

	// Equals
	//
	// Compares this Print instance to another Print instance
	bool Equals(Print^ rhs);

	// GetArtwork
	//
	// Gets the artwork associated with the print
	Bitmap^ GetArtwork(void);

	// GetCard
	//
	// Gets the Card associated with the print
	Card^ GetCard(void);

	// GetHashCode
	//
	// Overrides Object::GetHashCode()
	virtual int GetHashCode(void) override;

	// GetSeries
	//
	// Gets the Series associated with the print
	Series^ GetSeries(void);

	// ToString
	//
	// Overrides Object::ToString()
	virtual String^ ToString(void) override;

	//-----------------------------------------------------------------------
	// Properties

	// CardID
	//
	// Gets the card identifier
	property Guid CardID
	{
		Guid get(void);
		internal: void set(Guid value);
	}

	// Code
	//
	// Gets the print code
	property String^ Code
	{
		String^ get(void);
		internal: void set(String^ value);
	}

	// Language
	//
	// Gets the print language
	property String^ Language
	{
		String^ get(void);
		internal: void set(String^ value);
	}

	// Number
	//
	// Gets the print number
	property String^ Number
	{
		String^ get(void);
		internal: void set(String^ value);
	}

	// PrintID
	//
	// Gets the print identifier
	property Guid PrintID
	{
		Guid get(void);
		internal: void set(Guid value);
	}

	// Rarity
	//
	// Gets the print rarity
	property PrintRarity Rarity
	{
		PrintRarity get(void);
		internal: void set(PrintRarity value);
	}

	// ReleaseDate
	//
	// Gets the print release date
	property Nullable<DateTime> ReleaseDate
	{
		Nullable<DateTime> get(void);
		internal: void set(Nullable<DateTime> value);
	}

	// SeriesID
	//
	// Gets the series unique identifier
	property Guid SeriesID
	{
		Guid get(void);
		internal: void set(Guid value);
	}

internal:

	// Instance Constructor
	//
	Print(Database^ database);

	//-----------------------------------------------------------------------
	// Internal Member Functions

	// ParseRarity (static)
	//
	// Parses a string into a PrintRarity
	static PrintRarity ParseRarity(String^ value);

private:

	//-----------------------------------------------------------------------
	// Member Variables

	initonly Database^		m_database;					// Database instance

	Guid					m_printid;					// Unique identifier
	Guid					m_cardid;					// Card unique identifer
	Guid					m_seriesid;					// Series unique identifier
	String^					m_code = String::Empty;		// Print code
	String^					m_language = String::Empty;	// Language code
	String^					m_number = String::Empty;	// Print number
	PrintRarity				m_rarity;					// Print rarity
	Nullable<DateTime>		m_releasedate;				// Print release date
};

//---------------------------------------------------------------------------

} // zuki::ronin::data

#pragma warning(pop)

#endif	// __PRINT_H_
