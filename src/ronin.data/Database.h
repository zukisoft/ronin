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

#ifndef __DATABASE_H_
#define __DATABASE_H_
#pragma once

#pragma warning(push, 4)

#include "Artwork.h"
#include "Card.h"
#include "CardFilter.h"
#include "Print.h"
#include "SQLiteSafeHandle.h"

using namespace System;
using namespace System::Collections::Generic;

// dbextension.cpp
//
extern "C" int sqlite3_extension_init(sqlite3* db, char** errmsg, const sqlite3_api_routines* api);

namespace zuki::ronin::data {

//---------------------------------------------------------------------------
// Class Database
//
// Implements the backing store database operations
//---------------------------------------------------------------------------

public ref class Database
{
public:

	//-----------------------------------------------------------------------
	// Member Functions

	// Create
	//
	// Creates a new database instance
	static Database^ Create(String^ path);

	// EnumerateArtwork
	//
	// Enumerates Artwork from the database
	void EnumerateArtwork(Action<Artwork^>^ callback);

	// EnumerateCards
	//
	// Enumerates Cards from the database
	void EnumerateCards(Action<Card^>^ callback);

	// EnumeratePrints
	//
	// Enumerates Prints from the database
	void EnumeratePrints(Action<Print^>^ callback);

	// GetSize
	//
	// Gets the current size of the database
	int64_t GetSize(void);

	// SelectArtwork
	//
	// Selects an artwork object from the database
	Artwork^ SelectArtwork(Guid artworkid);

	// SelectArtworks
	//
	// Selects artwork objects from the database
	List<Artwork^>^ SelectArtworks(Guid cardid);

	// SelectCard
	//
	// Selects a single Card object from the database
	Card^ SelectCard(Guid cardid);

	// SelectCards
	//
	// Selects Card objects from the database
	List<Card^>^ SelectCards(void);
	List<Card^>^ SelectCards(CardFilter^ filter);

	// SelectPrints
	//
	// Selects Print objects from the database
	List<Print^>^ SelectPrints(Guid cardid);

	// Vacuum
	//
	// Vacuums the database
	void Vacuum(void);

internal:

	// InsertArtwork
	//
	// Inserts a new artwork image into the database
	Guid InsertArtwork(Guid cardid, String^ format, int width, int height, array<Byte>^ image);

	// UpdateArtwork
	//
	// Updates an artwork image in the database
	void UpdateArtwork(Guid artworkid, String^ format, int width, int height, array<Byte>^ image);

	// UpdateCardText
	//
	// Updates the text for a Card in the database
	void UpdateCardText(Guid cardid, String^ text);

	// UpdateDefaultArtwork
	//
	// Updates the default artwork for a card in the database
	void UpdateDefaultArtwork(Guid cardid, Guid artworkid);

private:

	// Static Constructor
	//
	static Database();

	// Instance Constructor
	//
	Database(SQLiteSafeHandle^ handle);

	// Destructor
	//
	~Database();

	//-----------------------------------------------------------------------
	// Private Member Functions

	// InitializeInstance (static)
	//
	// Initializes the database instance for use
	static void InitializeInstance(SQLiteSafeHandle^ handle);

	//-----------------------------------------------------------------------
	// Member Variables

	bool					m_disposed = false;		// Object disposal flag
	SQLiteSafeHandle^		m_handle;				// Database safe handle
	
	static int				s_result = SQLITE_OK;	// Result from static init
};

//---------------------------------------------------------------------------

} // zuki::ronin::data

#pragma warning(pop)

#endif	// __DATABASE_H_
