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

#ifndef __ARTWORK_H_
#define __ARTWORK_H_
#pragma once

#pragma warning(push, 4)

using namespace System;
using namespace System::Drawing;

namespace zuki::ronin::data {

// FORWARD DECLARATIONS
//
ref class Card;
ref class Database;

//---------------------------------------------------------------------------
// Class Artwork
//
// Describes an artwork object
//---------------------------------------------------------------------------

public ref class Artwork
{
public:

	//-----------------------------------------------------------------------
	// Overloaded Operators

	// operator== (static)
	//
	static bool operator==(Artwork^ lhs, Artwork^ rhs);

	// operator!= (static)
	//
	static bool operator!=(Artwork^ lhs, Artwork^ rhs);

	//-----------------------------------------------------------------------
	// Member Functions

	// Equals
	//
	// Overrides Object::Equals()
	virtual bool Equals(Object^ rhs) override;

	// Equals
	//
	// Compares this Artwork instance to another Artwork instance
	bool Equals(Artwork^ rhs);

	// GetCard
	//
	// Gets the Card associated with the Artwork
	Card^ GetCard(void);

	// GetHashCode
	//
	// Overrides Object::GetHashCode()
	virtual int GetHashCode(void) override;

	// SetDefault
	//
	// Sets the artwork as the default for the Card
	void SetDefault(void);

	// ToBitmap
	//
	// Converts the raw image data into a Bitmap
	Bitmap^ ToBitmap(void);

	// ToString
	//
	// Overrides Object::ToString()
	virtual String^ ToString(void) override;

	// UpdateImage
	//
	// Updates the artwork image
	void UpdateImage(String^ format, int width, int height, array<Byte>^ image);

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

	// Format
	//
	// Gets the image format
	property String^ Format
	{
		String^ get(void);
		internal: void set(String^ value);
	}

	// Height
	//
	// Gets the image height
	property int Height
	{
		int get(void);
		internal: void set(int value);
	}

	// Image
	//
	// Gets the artwork image
	property array<Byte>^ Image
	{
		array<Byte>^ get(void);
		internal: void set(array<Byte>^ value);
	}

	// Width
	//
	// Gets the image width
	property int Width
	{
		int get(void);
		internal: void set(int value);
	}

internal:

	// Instance Constructor
	//
	Artwork(Database^ database);

private:

	// Destructor
	//
	~Artwork();

	//-----------------------------------------------------------------------
	// Member Variables

	initonly Database^		m_database;					// Database instance
	bool					m_disposed = false;			// Object disposal flag

	Guid					m_artworkid;				// Unique identifier
	Guid					m_cardid;					// Unique identifier
	String^					m_format = String::Empty;	// Image format
	int						m_height = 0;				// Image height
	array<Byte>^			m_image;					// Image
	int						m_width = 0;				// Image width
};

//---------------------------------------------------------------------------

} // zuki::ronin::data

#pragma warning(pop)

#endif	// __ARTWORK_H_
