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
#include "Artwork.h"

#include "Card.h"
#include "Database.h"

#pragma warning(push, 4)

using namespace System::IO;

namespace zuki::ronin::data {

//---------------------------------------------------------------------------
// Artwork Constructor (internal)
//
// Arguments:
//
//	database	- Underlying Database instance

Artwork::Artwork(Database^ database) : m_database(database)
{
	if(CLRISNULL(database)) throw gcnew ArgumentNullException("database");
}

//---------------------------------------------------------------------------
// Artwork Destructor (private)

Artwork::~Artwork()
{
	if(m_disposed) return;

	if(CLRISNOTNULL(m_image)) delete m_image;

	m_disposed = true;
}

//---------------------------------------------------------------------------
// Artwork::operator == (static)

bool Artwork::operator==(Artwork^ lhs, Artwork^ rhs)
{
	if(Object::ReferenceEquals(lhs, rhs)) return true;
	if(Object::ReferenceEquals(lhs, nullptr) || Object::ReferenceEquals(rhs, nullptr)) return false;

	return lhs->ArtworkID == rhs->ArtworkID;
}

//---------------------------------------------------------------------------
// Artwork::operator != (static)

bool Artwork::operator!=(Artwork^ lhs, Artwork^ rhs)
{
	if(Object::ReferenceEquals(lhs, rhs)) return false;
	if(Object::ReferenceEquals(lhs, nullptr) || Object::ReferenceEquals(rhs, nullptr)) return true;

	return lhs->ArtworkID != rhs->ArtworkID;
}

//---------------------------------------------------------------------------
// Artwork::ArtworkID::get
//
// Gets the artwork unique identifier

Guid Artwork::ArtworkID::get(void)
{
	CHECK_DISPOSED(m_disposed);
	return m_artworkid;
}

//---------------------------------------------------------------------------
// Artwork::ArtworkID::set (internal)
//
// Sets the artwork unique identifier

void Artwork::ArtworkID::set(Guid value)
{
	CHECK_DISPOSED(m_disposed);
	m_artworkid = value;
}

//---------------------------------------------------------------------------
// Artwork::CardID::get
//
// Gets the card unique identifier

Guid Artwork::CardID::get(void)
{
	CHECK_DISPOSED(m_disposed);
	return m_cardid;
}

//---------------------------------------------------------------------------
// Artwork::CardID::set (internal)
//
// Sets the card unique identifier

void Artwork::CardID::set(Guid value)
{
	CHECK_DISPOSED(m_disposed);
	m_cardid = value;
}

//---------------------------------------------------------------------------
// Artwork::Equals
//
// Compares this Artwork instance to another Artwork instance
//
// Arguments:
//
//	rhs		- Right-hand Artwork instance to compare against

bool Artwork::Equals(Artwork^ rhs)
{
	CHECK_DISPOSED(m_disposed);
	return (this == rhs);
}

//---------------------------------------------------------------------------
// Artwork::Equals
//
// Overrides Object::Equals()
//
// Arguments:
//
//	rhs		- Right-hand object instance to compare against

bool Artwork::Equals(Object^ rhs)
{
	CHECK_DISPOSED(m_disposed);

	if(Object::ReferenceEquals(rhs, nullptr)) return false;

	// Convert the provided object into a Artwork instance
	Artwork^ rhsref = dynamic_cast<Artwork^>(rhs);
	if(rhsref == nullptr) return false;

	return (this == rhsref);
}

//---------------------------------------------------------------------------
// Artwork::Format::get
//
// Gets the artwork format

String^ Artwork::Format::get(void)
{
	CHECK_DISPOSED(m_disposed);
	return m_format;
}

//---------------------------------------------------------------------------
// Artwork::Format::set (internal)
//
// Sets the artwork format

void Artwork::Format::set(String^ value)
{
	CHECK_DISPOSED(m_disposed);
	m_format = value;
}

//---------------------------------------------------------------------------
// Artwork::GetCard
//
// Gets the card associated with the artwork
//
// Arguments:
//
//	NONE

Card^ Artwork::GetCard(void)
{
	CHECK_DISPOSED(m_disposed);
	CLRASSERT(CLRISNOTNULL(m_database));

	return m_database->SelectCard(m_cardid);
}

//---------------------------------------------------------------------------
// Artwork::GetHashCode
//
// Overrides Object::GetHashCode()
//
// Arguments:
//
//	NONE

int Artwork::GetHashCode(void)
{
	CHECK_DISPOSED(m_disposed);
	return m_cardid.GetHashCode();
}

//---------------------------------------------------------------------------
// Artwork::Height::get
//
// Gets the artwork height

int Artwork::Height::get(void)
{
	CHECK_DISPOSED(m_disposed);
	return m_height;
}

//---------------------------------------------------------------------------
// Artwork::Height::set (internal)
//
// Sets the artwork height

void Artwork::Height::set(int value)
{
	CHECK_DISPOSED(m_disposed);
	m_height = value;
}

//---------------------------------------------------------------------------
// Artwork::Image::get
//
// Gets the artwork image

Bitmap^ Artwork::Image::get(void)
{
	CHECK_DISPOSED(m_disposed);
	return m_image;
}

//---------------------------------------------------------------------------
// Artwork::Image::set (internal)
//
// Sets the artwork image

void Artwork::Image::set(Bitmap^ value)
{
	CHECK_DISPOSED(m_disposed);
	m_image = value;
}

//---------------------------------------------------------------------------
// Artwork::SetDefault
//
// Sets the artwork as the default for the card
//
// Arguments:
//
//	NONE

void Artwork::SetDefault(void)
{
	CHECK_DISPOSED(m_disposed);
	CLRASSERT(CLRISNOTNULL(m_database));

	m_database->UpdateDefaultArtwork(m_cardid, m_artworkid);
}

//---------------------------------------------------------------------------
// Artwork::ToString
//
// Overrides Object::ToString()
//
// Arguments:
//
//	NONE

String^ Artwork::ToString(void)
{
	CHECK_DISPOSED(m_disposed);
	return m_artworkid.ToString();
}

//---------------------------------------------------------------------------
// Artwork::UpdateImage
//
// Updates the artwork image
//
// Arguments:
//
//	format		- Image format
//	width		- Image width
//	height		- Image height
//	image		- Image data

void Artwork::UpdateImage(String^ format, int width, int height, array<Byte>^ image)
{
	CHECK_DISPOSED(m_disposed);
	CLRASSERT(CLRISNOTNULL(m_database));

	if(CLRISNULL(format)) throw gcnew ArgumentNullException("format");
	if(CLRISNULL(image)) throw gcnew ArgumentNullException("format");

	// Attempt to update the image in the database
	m_database->UpdateArtwork(m_artworkid, format, width, height, image);

	m_format = format;
	m_width = width;
	m_height = height;

	if(CLRISNOTNULL(m_image)) delete m_image;
	m_image = gcnew Bitmap(gcnew MemoryStream(image));
}

//---------------------------------------------------------------------------
// Artwork::Width::get
//
// Gets the artwork width

int Artwork::Width::get(void)
{
	CHECK_DISPOSED(m_disposed);
	return m_width;
}

//---------------------------------------------------------------------------
// Artwork::Width::set (internal)
//
// Sets the artwork width

void Artwork::Width::set(int value)
{
	CHECK_DISPOSED(m_disposed);
	m_width = value;
}

//---------------------------------------------------------------------------

} // zuki::ronin::data

#pragma warning(pop)
