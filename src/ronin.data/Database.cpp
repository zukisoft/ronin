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

#include "Database.h"

#include "MonsterCard.h"
#include "SpellCard.h"
#include "SQLiteException.h"
#include "TrapCard.h"

using namespace System::IO;
using namespace System::Runtime::InteropServices;

#pragma warning(push, 4)

namespace zuki::ronin::data {

//---------------------------------------------------------------------------
// bind_parameter (local)
//
// Used by execute_non_query to bind a String^ parameter
//
// Arguments:
//
//	statement		- SQL statement instance
//	paramindex		- Index of the parameter to bind; will be incremented
//	value			- Value to bind as the parameter

static void bind_parameter(sqlite3_stmt* statement, int& paramindex, String^ value)
{
	int					result;				// Result from binding operation

	if(CLRISNOTNULL(value)) {

		// Pin the String and specify SQLITE_TRANSIENT to have SQLite copy the string
		pin_ptr<const wchar_t> pintext = PtrToStringChars(value);
		result = sqlite3_bind_text16(statement, paramindex++, pintext, -1, SQLITE_TRANSIENT);
	}

	else result = sqlite3_bind_null(statement, paramindex++);

	if(result != SQLITE_OK) throw gcnew SQLiteException(result);
}

//---------------------------------------------------------------------------
// bind_parameter (local)
//
// Used by execute_non_query to bind a Guid parameter
//
// Arguments:
//
//	statement		- SQL statement instance
//	paramindex		- Index of the parameter to bind; will be incremented
//	value			- Value to bind as the parameter

static void bind_parameter(sqlite3_stmt* statement, int& paramindex, Guid value)
{
	int					result;				// Result from binding operation

	// Convert the Guid into a byte array and pin it
	array<Byte>^ guid = value.ToByteArray();
	pin_ptr<Byte> pinguid = &guid[0];

	// Specify SQLITE_TRANSIENT to have SQLite copy the data
	result = sqlite3_bind_blob(statement, paramindex++, pinguid, guid->Length, SQLITE_TRANSIENT);
	if(result != SQLITE_OK) throw gcnew SQLiteException(result);
}

//---------------------------------------------------------------------------
// column_guid (local)
//
// Converts a SQLite BLOB result column into a System::Guid
//
// Arguments:
//
//	instance		- Database instance
	
static Guid column_guid(sqlite3_stmt* statement, int index)
{
	int bloblen = sqlite3_column_bytes(statement, index);
	if(bloblen == 0) return Guid::Empty;
	if(bloblen != sizeof(UUID)) throw gcnew Exception("Invalid BLOB length for conversion to System::Guid");

	array<byte>^ blob = gcnew array<byte>(bloblen);
	Marshal::Copy(IntPtr(const_cast<void*>(sqlite3_column_blob(statement, index))), blob, 0, bloblen);

	return Guid(blob);
}

//---------------------------------------------------------------------------
// execute_non_query (local)
//
// Executes a database query and returns the number of rows affected
//
// Arguments:
//
//	instance		- Database instance
//	sql				- SQL query to execute
//	parameters		- Parameters to be bound to the query

template<typename... _parameters>
static int execute_non_query(sqlite3* instance, wchar_t const* sql, _parameters&&... parameters)
{
	sqlite3_stmt* statement = nullptr;
	int	paramindex = 1;

	if(instance == nullptr) throw gcnew ArgumentNullException("instance");
	if(sql == nullptr) throw gcnew ArgumentNullException("sql");

	// Suppress unreferenced local variable warning when there are no parameters to bind
	(void)paramindex;

	// Prepare the statement
	int result = sqlite3_prepare16_v2(instance, sql, -1, &statement, nullptr);
	if(result != SQLITE_OK) throw gcnew SQLiteException(result, sqlite3_errmsg(instance));

	try {

		// Bind the provided query parameter(s) by unpacking the parameter pack
		int unpack[] = { 0, (static_cast<void>(bind_parameter(statement, paramindex, parameters)), 0) ... };
		(void)unpack;

		// Execute the query; ignore any rows that are returned
		do result = sqlite3_step(statement);
		while(result == SQLITE_ROW);

		// The final result from sqlite3_step should be SQLITE_DONE
		if(result != SQLITE_DONE) throw gcnew SQLiteException(result, sqlite3_errmsg(instance));

		sqlite3_finalize(statement);

		// Return the number of changes made by the statement
		return sqlite3_changes(instance);
	}

	catch(Exception^) { sqlite3_finalize(statement); throw; }
}

//---------------------------------------------------------------------------
// execute_scalar_int (local)
//
// Executes a database query and returns a scalar integer result
//
// Arguments:
//
//	instance		- Database instance
//	sql				- SQL query to execute
//	parameters		- Parameters to be bound to the query

template<typename... _parameters>
static int execute_scalar_int(sqlite3* instance, wchar_t const* sql, _parameters&&... parameters)
{
	sqlite3_stmt* statement = nullptr;
	int	paramindex = 1;
	int	value = 0;

	if(instance == nullptr) throw gcnew ArgumentNullException("instance");
	if(sql == nullptr) throw gcnew ArgumentNullException("sql");

	// Suppress unreferenced local variable warning when there are no parameters to bind
	(void)paramindex;

	// Prepare the statement
	int result = sqlite3_prepare16_v2(instance, sql, -1, &statement, nullptr);
	if(result != SQLITE_OK) throw gcnew SQLiteException(result, sqlite3_errmsg(instance));

	try {

		// Bind the provided query parameter(s) by unpacking the parameter pack
		int unpack[] = { 0, (static_cast<void>(bind_parameter(statement, paramindex, parameters)), 0) ... };
		(void)unpack;

		// Execute the query; only the first row returned will be used
		result = sqlite3_step(statement);

		if(result == SQLITE_ROW) value = sqlite3_column_int(statement, 0);
		else if(result != SQLITE_DONE) throw gcnew SQLiteException(result, sqlite3_errmsg(instance));

		sqlite3_finalize(statement);

		// Return the resultant value from the scalar query
		return value;
	}

	catch(Exception^) { sqlite3_finalize(statement); throw; }
}

//---------------------------------------------------------------------------
// execute_scalar_int64 (local)
//
// Executes a database query and returns a scalar 64-bit integer result
//
// Arguments:
//
//	instance		- Database instance
//	sql				- SQL query to execute
//	parameters		- Parameters to be bound to the query

template<typename... _parameters>
static int64_t execute_scalar_int64(sqlite3* instance, wchar_t const* sql, _parameters&&... parameters)
{
	sqlite3_stmt* statement = nullptr;
	int	paramindex = 1;
	int64_t	value = 0;

	if(instance == nullptr) throw gcnew ArgumentNullException("instance");
	if(sql == nullptr) throw gcnew ArgumentNullException("sql");

	// Suppress unreferenced local variable warning when there are no parameters to bind
	(void)paramindex;

	// Prepare the statement
	int result = sqlite3_prepare16_v2(instance, sql, -1, &statement, nullptr);
	if(result != SQLITE_OK) throw gcnew SQLiteException(result, sqlite3_errmsg(instance));

	try {

		// Bind the provided query parameter(s) by unpacking the parameter pack
		int unpack[] = { 0, (static_cast<void>(bind_parameter(statement, paramindex, parameters)), 0) ... };
		(void)unpack;

		// Execute the query; only the first row returned will be used
		result = sqlite3_step(statement);

		if(result == SQLITE_ROW) value = sqlite3_column_int64(statement, 0);
		else if(result != SQLITE_DONE) throw gcnew SQLiteException(result, sqlite3_errmsg(instance));

		sqlite3_finalize(statement);

		// Return the resultant value from the scalar query
		return value;
	}

	catch(Exception^) { sqlite3_finalize(statement); throw; }
}

//---------------------------------------------------------------------------
// Database Static Constructor (private)
//
// Arguments:
//
//	NONE
	
static Database::Database()
{
	// Automatically register the built-in database extension library functions
	s_result = sqlite3_auto_extension(reinterpret_cast<void(*)()>(sqlite3_extension_init));
}

//---------------------------------------------------------------------------
// Database Constructor (private)
//
// Arguments:
//
//	handle		- SQLiteSafeHandle instance

Database::Database(SQLiteSafeHandle^ handle) : m_handle(handle)
{
	if(CLRISNULL(handle)) throw gcnew ArgumentNullException("handle");

	// Ensure that the static initialization completed successfully
	if(s_result != SQLITE_OK)
		throw gcnew Exception("Static initialization failed", gcnew SQLiteException(s_result));

	// Initialize the database instance
	InitializeInstance(handle);
}

//---------------------------------------------------------------------------
// Database Destructor (private)

Database::~Database()
{
	if(m_disposed) return;

	delete m_handle;					// Release the safe handle
	m_disposed = true;					// Object is now in a disposed state
}

//---------------------------------------------------------------------------
// Database::Create (static)
//
// Creates a new database file and opens it
//
// Arguments:
//
//	path		- Path on which to create a new database file

Database^ Database::Create(String^ path)
{
	sqlite3* instance = nullptr;

	// If the specified path is null/empty use an in-memory database,
	// otherwise canonicalize the path to prevent traversal
	if(String::IsNullOrEmpty(path)) path = gcnew String(":memory:");
	else path = Path::GetFullPath(path);

	// Attempt to create or open the database at the specified path
	pin_ptr<wchar_t const> pinpath = PtrToStringChars(path);
	int result = sqlite3_open16(pinpath, &instance);
	if(result != SQLITE_OK) {

		if(instance != nullptr) sqlite3_close(instance);
		throw gcnew SQLiteException(result);
	}

	// Create the safe handle wrapper around the sqlite3*
	SQLiteSafeHandle^ handle = gcnew SQLiteSafeHandle(std::move(instance));
	CLRASSERT(instance == nullptr);
	
	// Delete the safe handle on a construction failure
	try { return gcnew Database(handle); }
	catch(Exception^) { delete handle; throw; }
}

//---------------------------------------------------------------------------
// Database::GetSize
//
// Gets the current size of the database
//
// Arguments:
//
//	NONE

int64_t Database::GetSize(void)
{
	CHECK_DISPOSED(m_disposed);
	CLRASSERT(CLRISNOTNULL(m_handle) && (m_handle->IsClosed == false));

	SQLiteSafeHandle::Reference instance(m_handle);		// Instance handle

	// The database size is the page size multiplied by the page count
	int pagesize = execute_scalar_int(instance, L"pragma page_size");
	int64_t pagecount = execute_scalar_int64(instance, L"pragma page_count");

	return pagecount * pagesize;
}

//---------------------------------------------------------------------------
// Database::InitializeInstance (private, static)
//
// Initializes the database instance for use
//
// Arguments:
//
//	handle		- SQLiteSafeHandle instance

void Database::InitializeInstance(SQLiteSafeHandle^ handle)
{
	if(CLRISNULL(handle)) throw gcnew ArgumentNullException("handle");
	
	SQLiteSafeHandle::Reference instance(handle);

	// Set the instance to report extended error codes
	sqlite3_extended_result_codes(instance, TRUE);

	// Set a busy timeout handler for this connection
	sqlite3_busy_timeout(instance, 5000);

	// Switch the database to write-ahead logging
	execute_non_query(instance, L"pragma journal_mode=wal");

	// Switch the database to UTF-16 encoding
	execute_non_query(instance, L"pragma encoding='UTF-16'");

	// Enable foreign key constraints
	execute_non_query(instance, L"pragma foreign_keys=ON");

	// Get the database schema version
	int dbversion = execute_scalar_int(instance, L"pragma user_version");

	// SCHEMA VERSION 0 -> VERSION 1
	//
	// Original database schema
	if(dbversion == 0) {

		// table: card
		//
		// cardid(pk) | name(u) | type | passcode(u) | text
		execute_non_query(instance, L"create table card(cardid blob not null, name text unique not null, type text not null, "
			"passcode text unique not null, text text not null, primary key(cardid), "
			"check(type in ('Monster', 'Spell', 'Trap')))");

		// table: monster
		//
		// cardid(fk) | attribute | level | type | attack | defense | normal | effect | fusion | ritual | toon | union | spirit | gemini 
		execute_non_query(instance, L"create table monster(cardid blob not null, attribute text not null, level integer not null, "
			"type text not null, attack integer not null, defense integer not null, normal integer not null, effect integer not null, "
			"fusion integer not null, ritual integer not null, toon integer not null, [union] integer not null, spirit integer not null, "
			"gemini integer not null, foreign key(cardid) references card(cardid), "
			"check(attribute in ('DARK', 'EARTH', 'FIRE', 'LIGHT', 'WATER', 'WIND')), "
			"check(level between 1 and 12), check(attack between -1 and 5000), check(defense between -1 and 5000), "
			"check(type in ('Aqua', 'Beast', 'Beast-Warrior', 'Dinosaur', 'Dragon', 'Fairy', 'Fiend', 'Fish', 'Insect', 'Machine', "
			"  'Plant', 'Pyro', 'Reptile', 'Rock', 'Sea Serpent', 'Spellcaster', 'Thunder', 'Warrior', 'Winged Beast', 'Zombie')), "
			"check(normal = 0 or (effect + fusion + ritual + toon + [union] + spirit + gemini = 0)), "
			"check(fusion = 0 or (toon + [union] + spirit + gemini = 0)), "
			"check(ritual = 0 or (toon + [union] + spirit + gemini = 0)), "
			"check(toon = 0 or effect = 1), check([union] = 0 or effect = 1), check(spirit = 0 or effect = 1), "
			"check(gemini = 0 or effect = 1))");
		execute_non_query(instance, L"create index monster_attribute on monster(attribute)");
		execute_non_query(instance, L"create index monster_level on monster(level)");
		execute_non_query(instance, L"create index monster_type on monster(type)");
		execute_non_query(instance, L"create index monster_attackdefense on monster(attack, defense)");
		execute_non_query(instance, L"create index monster_flag on monster(normal, effect, fusion, ritual, toon, [union], spirit, gemini)");

		// table: spell
		//
		// cardid(fk) | normal | continuous | equip | field | quickplay | ritual
		execute_non_query(instance, L"create table spell(cardid blob not null, normal integer not null, continuous integer not null, "
			"equip integer not null, field integer not null, quickplay integer not null, ritual integer not null, "
			"foreign key(cardid) references card(cardid), "
			"check(normal + continuous + equip + field + quickplay + ritual = 1))");
		execute_non_query(instance, L"create index spell_flag on spell(normal, continuous, equip, field, quickplay, ritual)");

		// table: trap
		//
		// cardid(fk) | normal | continuous | counter
		execute_non_query(instance, L"create table trap(cardid blob not null, normal integer not null, continuous integer not null, "
			"counter integer not null, foreign key(cardid) references card(cardid), "
			"check(normal + continuous + counter = 1))");
		execute_non_query(instance, L"create index trap_flag on trap(normal, continuous, counter)");

		// table: artwork
		//
		// artworkid(pk) | cardid(fk) | format | height | width | image
		execute_non_query(instance, L"create table artwork(artworkid blob not null, cardid blob not null, format text not null,"
			"height integer not null, width integer not null, image blob not null, primary key(artworkid), "
			"foreign key(cardid) references card(cardid))");

		// table: defaultartwork
		//
		// cardid(pk,fk) | artworkid(pk,fk)
		execute_non_query(instance, L"create table defaultartwork(cardid blob not null, artworkid blob null, "
			"primary key(cardid, artworkid), foreign key(cardid) references card(cardid), foreign key(artworkid) references artwork(artworkid))");

		// table: series
		//
		// seriesid(pk) | code(u) | name(u) | releasedate
		execute_non_query(instance, L"create table series(seriesid blob not null, code text unique not null, name text unique not null, "
			"releasedate text null, primary key(seriesid))");
		execute_non_query(instance, L"create index series_releasedate on series(releasedate)");

		// table: print
		//
		// printid(pk) | cardid(fk) | seriesid(fk) | artworkid(fk) | code | language | number | rarity | releasedate
		execute_non_query(instance, L"create table print(printid blob not null, cardid blob not null, seriesid blob not null, "
			"artworkid blob null, code text not null, language text null, number text not null, rarity text not null, releasedate not null, "
			"primary key(printid), foreign key(cardid) references card(cardid), foreign key(seriesid) references series(seriesid), "
			"foreign key(artworkid) references artwork(artworkid))");
		// TODO: missing check constraint on rarity
		execute_non_query(instance, L"create unique index print_code on print(code, language, number)");
		execute_non_query(instance, L"create index print_rarity on print(rarity)");
		execute_non_query(instance, L"create index print_releasedate on print(releasedate)");

		// table: restrictionlist
		//
		// restrictionlistid(pk) | effective
		execute_non_query(instance, L"create table restrictionlist(restrictionlistid blob not null, effective text unique not null, "
			"primary key(restrictionlistid))");

		// table: restriction
		//
		// restrictionlistid(pk,fk) | cardid(pk,fk) | restriction
		execute_non_query(instance, L"create table restriction(restrictionlistid blob not null, cardid blob not null, restriction integer not null, "
			"primary key(restrictionlistid, cardid), foreign key(restrictionlistid) references restrictionlist(restrictionlistid), "
			"foreign key(cardid) references card(cardid))");

		// table: ruling
		//
		// cardid(fk) | sequence | ruling
		execute_non_query(instance, L"create table ruling(cardid blob not null, sequence integer not null, ruling text not null, "
			"foreign key(cardid) references card(cardid))");

		execute_non_query(instance, L"pragma user_version = 1");
		dbversion = 1;
	}

	// SCHEMA VERSION 1 -> VERSION 2
	//
	// Corrects an incorrect PRIMARY KEY constraint on the defaultartwork table
	if(dbversion == 1) {

		// table: defaultartwork_v1
		//
		// cardid(pk,fk) | artworkid(pk,fk)
		execute_non_query(instance, L"alter table defaultartwork rename to defaultartwork_v1");

		// table: defaultartwork
		//
		// cardid(pk,fk) | artworkid(fk)
		execute_non_query(instance, L"create table defaultartwork(cardid blob not null, artworkid blob not null, "
			"primary key(cardid), foreign key(cardid) references card(cardid), foreign key(artworkid) references artwork(artworkid))");

		// Remove any NULL artworkid values from the old table, move the data into the new table, and drop the old table
		execute_non_query(instance, L"delete from defaultartwork_v1 where artworkid is null");
		execute_non_query(instance, L"insert into defaultartwork select v1.cardid, v1.artworkid from defaultartwork_v1 as v1");
		execute_non_query(instance, L"drop table defaultartwork_v1");

		execute_non_query(instance, L"pragma user_version = 2");
		dbversion = 2;
	}
}

//---------------------------------------------------------------------------
// Database::InsertArtwork
//
// Inserts a new artwork image into the database
//
// Arguments:
//
//	cardid		- CardID for the artwork
//	format		- Image format
//	width		- Image width
//	height		- Image height
//	image		- Image data

Guid Database::InsertArtwork(Guid cardid, String^ format, int width, int height, array<Byte>^ image)
{
	CHECK_DISPOSED(m_disposed);
	CLRASSERT(CLRISNOTNULL(m_handle) && (m_handle->IsClosed == false));

	if(CLRISNULL(format)) throw gcnew ArgumentNullException("format");
	if(CLRISNULL(image)) throw gcnew ArgumentNullException("image");

	SQLiteSafeHandle::Reference instance(m_handle);		// Instance handle
	sqlite3_stmt* statement;							// Statement handle

	auto sql = L"insert into artwork values(?1, ?2, ?3, ?4, ?5, ?6)";

	// Create a new Guid to represent the artworkid and pin it
	Guid artworkid = Guid::NewGuid();
	array<Byte>^ _artworkid = artworkid.ToByteArray();
	pin_ptr<Byte> pinartworkid = &_artworkid[0];

	// Convert the cardid into a byte array and pin it
	array<Byte>^ _cardid = cardid.ToByteArray();
	pin_ptr<Byte> pincardid = &_cardid[0];

	// Pin the format string
	pin_ptr<wchar_t const> pinformat = PtrToStringChars(format);

	// Pin the image data
	pin_ptr<Byte> pinimage = &image[0];

	// Prepare the query
	int result = sqlite3_prepare16_v2(instance, sql, -1, &statement, nullptr);
	if(result != SQLITE_OK) throw gcnew SQLiteException(result, sqlite3_errmsg(instance));

	try {

		// Bind the query parameter(s)
		result = sqlite3_bind_blob(statement, 1, pinartworkid, _artworkid->Length, SQLITE_STATIC);
		if(result == SQLITE_OK) result = sqlite3_bind_blob(statement, 2, pincardid, _cardid->Length, SQLITE_STATIC);
		if(result == SQLITE_OK) result = sqlite3_bind_text16(statement, 3, pinformat, -1, SQLITE_STATIC);
		if(result == SQLITE_OK) result = sqlite3_bind_int(statement, 4, height);
		if(result == SQLITE_OK) result = sqlite3_bind_int(statement, 5, width);
		if(result == SQLITE_OK) result = sqlite3_bind_blob(statement, 6, pinimage, image->Length, SQLITE_STATIC);
		if(result != SQLITE_OK) throw gcnew SQLiteException(result);

		// Execute the query; no rows are expected to be returned
		result = sqlite3_step(statement);
		if(result != SQLITE_DONE) SQLiteException(result, sqlite3_errmsg(instance));
	}

	finally { sqlite3_finalize(statement); }

	return artworkid;
}

//---------------------------------------------------------------------------
// Database::SelectArtwork
//
// Selects an artwork object from the database
//
// Arguments:
//
//	artworkid	- Artwork identifier

Artwork^ Database::SelectArtwork(Guid artworkid)
{
	CHECK_DISPOSED(m_disposed);
	CLRASSERT(CLRISNOTNULL(m_handle) && (m_handle->IsClosed == false));

	SQLiteSafeHandle::Reference instance(m_handle);		// Instance handle
	sqlite3_stmt* statement;							// Statement handle

	Artwork^ artwork = nullptr;
		
	auto sql = L"select cardid, format, width, height, image from artwork where artworkid = ?1";

	// Convert the artworkid into a byte array and pin it
	array<Byte>^ guid = artworkid.ToByteArray();
	pin_ptr<Byte> pinguid = &guid[0];

	// Prepare the query
	int result = sqlite3_prepare16_v2(instance, sql, -1, &statement, nullptr);
	if(result != SQLITE_OK) throw gcnew SQLiteException(result, sqlite3_errmsg(instance));

	try {

		// Bind the query parameter(s)
		result = sqlite3_bind_blob(statement, 1, pinguid, guid->Length, SQLITE_STATIC);
		if(result != SQLITE_OK) throw gcnew SQLiteException(result);

		// Execute the query; there should be at most one row returned
		if(sqlite3_step(statement) == SQLITE_ROW) {

			artwork = gcnew Artwork(this);

			// artworkid
			artwork->ArtworkID = artworkid;

			// cardid
			artwork->CardID = column_guid(statement, 0);

			// passcode
			wchar_t const* formatptr = reinterpret_cast<wchar_t const*>(sqlite3_column_text16(statement, 1));
			artwork->Format = (formatptr == nullptr) ? String::Empty : gcnew String(formatptr);

			// width
			artwork->Width = sqlite3_column_int(statement, 2);

			// height
			artwork->Height = sqlite3_column_int(statement, 3);

			// image
			int length = sqlite3_column_bytes(statement, 4);
			if(length > 0) {

				array<Byte>^ image = gcnew array<Byte>(length);

				void const* blob = sqlite3_column_blob(statement, 4);
				if(blob != nullptr) {

					Marshal::Copy(IntPtr(const_cast<void*>(blob)), image, 0, length);
					artwork->Image = image;
				}
			}
		}
	}

	finally { sqlite3_finalize(statement); }

	return artwork;
}

//---------------------------------------------------------------------------
// Database::SelectArtworks
//
// Selects artwork objects from the database
//
// Arguments:
//
//	cardid	- Card identifier

List<Artwork^>^ Database::SelectArtworks(Guid cardid)
{
	CHECK_DISPOSED(m_disposed);
	CLRASSERT(CLRISNOTNULL(m_handle) && (m_handle->IsClosed == false));

	SQLiteSafeHandle::Reference instance(m_handle);		// Instance handle
	sqlite3_stmt* statement;							// Statement handle

	List<Artwork^>^ artworks = gcnew List<Artwork^>();

	auto sql = L"select artworkid, format, width, height, image from artwork where cardid = ?1";

	// Convert the cardid into a byte array and pin it
	array<Byte>^ guid = cardid.ToByteArray();
	pin_ptr<Byte> pinguid = &guid[0];

	// Prepare the query
	int result = sqlite3_prepare16_v2(instance, sql, -1, &statement, nullptr);
	if(result != SQLITE_OK) throw gcnew SQLiteException(result, sqlite3_errmsg(instance));

	try {

		// Bind the query parameter(s)
		result = sqlite3_bind_blob(statement, 1, pinguid, guid->Length, SQLITE_STATIC);
		if(result != SQLITE_OK) throw gcnew SQLiteException(result);

		// Execute the query and iterate over all returned rows
		result = sqlite3_step(statement);
		while(result == SQLITE_ROW) {

			Artwork^ artwork = gcnew Artwork(this);

			// artworkid
			artwork->ArtworkID = column_guid(statement, 0);

			// cardid
			artwork->CardID = cardid;

			// passcode
			wchar_t const* formatptr = reinterpret_cast<wchar_t const*>(sqlite3_column_text16(statement, 1));
			artwork->Format = (formatptr == nullptr) ? String::Empty : gcnew String(formatptr);

			// width
			artwork->Width = sqlite3_column_int(statement, 2);

			// height
			artwork->Height = sqlite3_column_int(statement, 3);

			// image
			int length = sqlite3_column_bytes(statement, 4);
			if(length > 0) {

				array<Byte>^ image = gcnew array<Byte>(length);

				void const* blob = sqlite3_column_blob(statement, 4);
				if(blob != nullptr) {

					Marshal::Copy(IntPtr(const_cast<void*>(blob)), image, 0, length);
					artwork->Image = image;
				}
			}

			artworks->Add(artwork);						// Add the Card instance
			result = sqlite3_step(statement);			// Move to the next result set row
		}

		// If the final result of the query was not SQLITE_DONE, something bad happened
		if(result != SQLITE_DONE) throw gcnew SQLiteException(result, sqlite3_errmsg(instance));
	}

	finally { sqlite3_finalize(statement); }

	return artworks;
}

//---------------------------------------------------------------------------
// Database::SelectCard
//
// Selects a single card object from the database
//
// Arguments:
//
//	cardid		- Card identifier

Card^ Database::SelectCard(Guid cardid)
{
	CHECK_DISPOSED(m_disposed);
	CLRASSERT(CLRISNOTNULL(m_handle) && (m_handle->IsClosed == false));

	SQLiteSafeHandle::Reference instance(m_handle);		// Instance handle
	sqlite3_stmt* statement;							// Statement handle

	Card^ card = nullptr;

	// { 00-05 } type | cardid | name | passcode | text | artworkid 
	// { 06-18 } attribute | level | type | attack | defense | normal | effect | fusion | ritual | toon | union | spirit | gemini |
	// { 19-24 } normal | continuous | equip | field | quickplay | ritual |
	// { 25-27 } normal | continuous | counter
	auto sql = L"select cardtype(card.type), card.cardid, card.name, card.passcode, card.text, defaultartwork.artworkid, "
		"monsterattribute(monster.attribute), monster.level, monstertype(monster.type), monster.attack, "
		"monster.defense, monster.normal, monster.effect, monster.fusion, monster.ritual,  "
		"monster.toon, monster.[union], monster.spirit, monster.gemini, "
		"spell.normal, spell.continuous, spell.equip, spell.field, spell.quickplay, spell.ritual, "
		"trap.normal, trap.continuous, trap.counter from card "
		"left outer join defaultartwork on card.cardid = defaultartwork.cardid "
		"left outer join monster on card.cardid = monster.cardid "
		"left outer join spell on card.cardid = spell.cardid "
		"left outer join trap on card.cardid = trap.cardid "
		"where card.cardid = ?1";

	// Convert the cardid into a byte array and pin it
	array<Byte>^ guid = cardid.ToByteArray();
	pin_ptr<Byte> pinguid = &guid[0];

	int result = sqlite3_prepare16_v2(instance, sql, -1, &statement, nullptr);
	if(result != SQLITE_OK) throw gcnew SQLiteException(result, sqlite3_errmsg(instance));

	try {

		// Bind the query parameter(s)
		result = sqlite3_bind_blob(statement, 1, pinguid, guid->Length, SQLITE_STATIC);
		if(result != SQLITE_OK) throw gcnew SQLiteException(result);

		// Execute the query; there should be at most one row returned
		if(sqlite3_step(statement) == SQLITE_ROW) {

			// Get the type of card being iterated here in order to instantiate the
			// proper derivation of the Card class
			CardType type = static_cast<CardType>(sqlite3_column_int(statement, 0));

			// MonsterCard
			if(type == CardType::Monster) {

				MonsterCard^ monster = gcnew MonsterCard(this);

				monster->Attribute = static_cast<CardAttribute>(sqlite3_column_int(statement, 6));
				monster->Level = sqlite3_column_int(statement, 7);
				monster->Type = static_cast<MonsterType>(sqlite3_column_int(statement, 8));
				monster->Attack = sqlite3_column_int(statement, 9);
				monster->Defense = sqlite3_column_int(statement, 10);
				monster->Normal = (sqlite3_column_int(statement, 11) != 0);
				monster->Effect = (sqlite3_column_int(statement, 12) != 0);
				monster->Fusion = (sqlite3_column_int(statement, 13) != 0);
				monster->Ritual = (sqlite3_column_int(statement, 14) != 0);
				monster->Toon = (sqlite3_column_int(statement, 15) != 0);
				monster->Union = (sqlite3_column_int(statement, 16) != 0);
				monster->Spirit = (sqlite3_column_int(statement, 17) != 0);
				monster->Gemini = (sqlite3_column_int(statement, 18) != 0);

				card = static_cast<Card^>(monster);
			}

			// SpellCard
			else if(type == CardType::Spell) {

				SpellCard^ spell = gcnew SpellCard(this);

				spell->Normal = (sqlite3_column_int(statement, 19) != 0);
				spell->Continuous = (sqlite3_column_int(statement, 20) != 0);
				spell->Equip = (sqlite3_column_int(statement, 21) != 0);
				spell->Field = (sqlite3_column_int(statement, 22) != 0);
				spell->QuickPlay = (sqlite3_column_int(statement, 23) != 0);
				spell->Ritual = (sqlite3_column_int(statement, 24) != 0);

				card = static_cast<Card^>(spell);
			}

			// TrapCard
			else if(type == CardType::Trap) {

				TrapCard^ trap = gcnew TrapCard(this);

				trap->Normal = (sqlite3_column_int(statement, 25) != 0);
				trap->Continuous = (sqlite3_column_int(statement, 26) != 0);
				trap->Counter = (sqlite3_column_int(statement, 27) != 0);

				card = static_cast<Card^>(trap);
			}

			else throw gcnew Exception("Invalid card.type value");

			// The base class reference should have been set above
			CLRASSERT(card != nullptr);

			// cardid
			card->CardID = column_guid(statement, 1);

			// name
			wchar_t const* nameptr = reinterpret_cast<wchar_t const*>(sqlite3_column_text16(statement, 2));
			card->Name = (nameptr == nullptr) ? String::Empty : gcnew String(nameptr);

			// passcode
			wchar_t const* passcodeptr = reinterpret_cast<wchar_t const*>(sqlite3_column_text16(statement, 3));
			card->Passcode = (passcodeptr == nullptr) ? String::Empty : gcnew String(passcodeptr);

			// text
			wchar_t const* textptr = reinterpret_cast<wchar_t const*>(sqlite3_column_text16(statement, 4));
			card->Text = (textptr == nullptr) ? String::Empty : gcnew String(textptr);

			// artworkid
			card->ArtworkID = column_guid(statement, 5);
		}
	}

	finally { sqlite3_finalize(statement); }

	return card;
}

//---------------------------------------------------------------------------
// Database::SelectCards
//
// Selects Card objects from the database
//
// Arguments:
//
//	NONE

List<Card^>^ Database::SelectCards(void)
{
	CHECK_DISPOSED(m_disposed);
	CLRASSERT(CLRISNOTNULL(m_handle) && (m_handle->IsClosed == false));

	SQLiteSafeHandle::Reference instance(m_handle);		// Instance handle
	sqlite3_stmt*				statement;				// Statement handle

	List<Card^>^ cards = gcnew List<Card^>();

	// { 00-05 } type | cardid | name | passcode | text | artworkid
	// { 06-18 } attribute | level | type | attack | defense | normal | effect | fusion | ritual | toon | union | spirit | gemini |
	// { 19-24 } normal | continuous | equip | field | quickplay | ritual |
	// { 25-27 } normal | continuous | counter
	auto sql = L"select cardtype(card.type), card.cardid, card.name, card.passcode, card.text, defaultartwork.artworkid, "
		"monsterattribute(monster.attribute), monster.level, monstertype(monster.type), monster.attack, "
		"monster.defense, monster.normal, monster.effect, monster.fusion, monster.ritual,  "
		"monster.toon, monster.[union], monster.spirit, monster.gemini, "
		"spell.normal, spell.continuous, spell.equip, spell.field, spell.quickplay, spell.ritual, "
		"trap.normal, trap.continuous, trap.counter from card "
		"left outer join defaultartwork on card.cardid = defaultartwork.cardid "
		"left outer join monster on card.cardid = monster.cardid "
		"left outer join spell on card.cardid = spell.cardid "
		"left outer join trap on card.cardid = trap.cardid "
		"order by card.name asc";

	int result = sqlite3_prepare16_v2(instance, sql, -1, &statement, nullptr);
	if(result != SQLITE_OK) throw gcnew SQLiteException(result, sqlite3_errmsg(instance));

	try {

		// Execute the query and iterate over all returned rows
		result = sqlite3_step(statement);
		while(result == SQLITE_ROW) {

			Card^ card = nullptr;

			// Get the type of card being iterated here in order to instantiate the
			// proper derivation of the Card class
			CardType type = static_cast<CardType>(sqlite3_column_int(statement, 0));
			
			// MonsterCard
			if(type == CardType::Monster) {

				MonsterCard^ monster = gcnew MonsterCard(this);

				monster->Attribute	= static_cast<CardAttribute>(sqlite3_column_int(statement, 6));
				monster->Level		= sqlite3_column_int(statement, 7);
				monster->Type		= static_cast<MonsterType>(sqlite3_column_int(statement, 8));
				monster->Attack		= sqlite3_column_int(statement, 9);
				monster->Defense	= sqlite3_column_int(statement, 10);
				monster->Normal		= (sqlite3_column_int(statement, 11) != 0);
				monster->Effect		= (sqlite3_column_int(statement, 12) != 0);
				monster->Fusion		= (sqlite3_column_int(statement, 13) != 0);
				monster->Ritual		= (sqlite3_column_int(statement, 14) != 0);
				monster->Toon		= (sqlite3_column_int(statement, 15) != 0);
				monster->Union		= (sqlite3_column_int(statement, 16) != 0);
				monster->Spirit		= (sqlite3_column_int(statement, 17) != 0);
				monster->Gemini		= (sqlite3_column_int(statement, 18) != 0);

				card = static_cast<Card^>(monster);
			}

			// SpellCard
			else if(type == CardType::Spell) {

				SpellCard^ spell = gcnew SpellCard(this);

				spell->Normal		= (sqlite3_column_int(statement, 19) != 0);
				spell->Continuous	= (sqlite3_column_int(statement, 20) != 0);
				spell->Equip		= (sqlite3_column_int(statement, 21) != 0);
				spell->Field		= (sqlite3_column_int(statement, 22) != 0);
				spell->QuickPlay	= (sqlite3_column_int(statement, 23) != 0);
				spell->Ritual		= (sqlite3_column_int(statement, 24) != 0);

				card = static_cast<Card^>(spell);
			}

			// TrapCard
			else if(type == CardType::Trap) {

				TrapCard^ trap = gcnew TrapCard(this);

				trap->Normal		= (sqlite3_column_int(statement, 25) != 0);
				trap->Continuous	= (sqlite3_column_int(statement, 26) != 0);
				trap->Counter		= (sqlite3_column_int(statement, 27) != 0);

				card = static_cast<Card^>(trap);
			}

			else throw gcnew Exception("Invalid card.type value");

			// The base class reference should have been set above
			CLRASSERT(card != nullptr);

			// cardid
			card->CardID = column_guid(statement, 1);

			// name
			wchar_t const* nameptr = reinterpret_cast<wchar_t const*>(sqlite3_column_text16(statement, 2));
			card->Name = (nameptr == nullptr) ? String::Empty : gcnew String(nameptr);

			// passcode
			wchar_t const* passcodeptr = reinterpret_cast<wchar_t const*>(sqlite3_column_text16(statement, 3));
			card->Passcode = (passcodeptr == nullptr) ? String::Empty : gcnew String(passcodeptr);

			// text
			wchar_t const* textptr = reinterpret_cast<wchar_t const*>(sqlite3_column_text16(statement, 4));
			card->Text = (textptr == nullptr) ? String::Empty : gcnew String(textptr);

			// artworkid
			card->ArtworkID = column_guid(statement, 5);

			cards->Add(card);							// Add the Card instance
			result = sqlite3_step(statement);			// Move to the next result set row
		}

		// If the final result of the query was not SQLITE_DONE, something bad happened
		if(result != SQLITE_DONE) throw gcnew SQLiteException(result, sqlite3_errmsg(instance));
	}

	finally { sqlite3_finalize(statement); }

	return cards;
}

//---------------------------------------------------------------------------
// Database::SelectCards
//
// Selects Card objects from the database
//
// Arguments:
//
//	filter		- CardFilter instance on which to filter the results

List<Card^>^ Database::SelectCards(CardFilter^ filter)
{
	CHECK_DISPOSED(m_disposed);
	CLRASSERT(CLRISNOTNULL(m_handle) && (m_handle->IsClosed == false));

	// Passing in a null filter would just select all cards
	if(CLRISNULL(filter)) return SelectCards();

	// TODO
	return gcnew List<Card^>();
}

//---------------------------------------------------------------------------
// Database::SelectPrints
//
// Selects Print objects from the database
//
// Arguments:
//
//	cardid		- Card identifier on which to filter the results

List<Print^>^ Database::SelectPrints(Guid cardid)
{
	CHECK_DISPOSED(m_disposed);
	CLRASSERT(CLRISNOTNULL(m_handle) && (m_handle->IsClosed == false));

	SQLiteSafeHandle::Reference instance(m_handle);		// Instance handle
	sqlite3_stmt* statement;							// Statement handle

	List<Print^>^ prints = gcnew List<Print^>();

	// printid | cardid | seriesid | artworkid | code | language | number | rarity | releasedate
	auto sql = L"select print.printid, print.cardid, print.seriesid, print.artworkid, print.code, print.language, "
		"print.number, printrarity(print.rarity), print.releasedate from print where print.cardid = ?1 "
		"order by print.releasedate asc";

	// Convert the cardid into a byte array and pin it
	array<Byte>^ guid = cardid.ToByteArray();
	pin_ptr<Byte> pinguid = &guid[0];

	int result = sqlite3_prepare16_v2(instance, sql, -1, &statement, nullptr);
	if(result != SQLITE_OK) throw gcnew SQLiteException(result, sqlite3_errmsg(instance));

	try {

		// Bind the query parameter(s)
		result = sqlite3_bind_blob(statement, 1, pinguid, guid->Length, SQLITE_STATIC);
		if(result != SQLITE_OK) throw gcnew SQLiteException(result);

		// Execute the query and iterate over all returned rows
		result = sqlite3_step(statement);
		while(result == SQLITE_ROW) {

			Print^ print = gcnew Print(this);

			// printid
			print->PrintID = column_guid(statement, 0);

			// cardid
			print->CardID = column_guid(statement, 1);

			// seriesid
			print->SeriesID = column_guid(statement, 2);

			// artworkid
			print->ArtworkID = column_guid(statement, 3);

			// code
			wchar_t const* codeptr = reinterpret_cast<wchar_t const*>(sqlite3_column_text16(statement, 4));
			print->Code = (codeptr == nullptr) ? String::Empty : gcnew String(codeptr);

			// language
			wchar_t const* languageptr = reinterpret_cast<wchar_t const*>(sqlite3_column_text16(statement, 5));
			print->Language = (languageptr == nullptr) ? String::Empty : gcnew String(languageptr);

			// number
			wchar_t const* numberptr = reinterpret_cast<wchar_t const*>(sqlite3_column_text16(statement, 6));
			print->Number = (numberptr == nullptr) ? String::Empty : gcnew String(numberptr);

			// rarity
			print->Rarity = static_cast<PrintRarity>(sqlite3_column_int(statement, 7));

			// releasedate
			wchar_t const* releasedateptr = reinterpret_cast<wchar_t const*>(sqlite3_column_text16(statement, 8));
			print->ReleaseDate = (releasedateptr == nullptr) ? Nullable<DateTime>() : DateTime::Parse(gcnew String(releasedateptr));

			prints->Add(print);							// Add the Print instance
			result = sqlite3_step(statement);			// Move to the next result set row
		}

		// If the final result of the query was not SQLITE_DONE, something bad happened
		if(result != SQLITE_DONE) throw gcnew SQLiteException(result, sqlite3_errmsg(instance));
	}

	finally { sqlite3_finalize(statement); }

	return prints;
}

//---------------------------------------------------------------------------
// Database::UpdateArtwork
//
// Updates an artwork image in the database
//
// Arguments:
//
//	artworkid	- Artwork identifier
//	format		- Image format
//	width		- Image width
//	height		- Image height
//	image		- Image data

void Database::UpdateArtwork(Guid artworkid, String^ format, int width, int height, array<Byte>^ image)
{
	CHECK_DISPOSED(m_disposed);
	CLRASSERT(CLRISNOTNULL(m_handle) && (m_handle->IsClosed == false));

	if(CLRISNULL(format)) throw gcnew ArgumentNullException("format");
	if(CLRISNULL(image)) throw gcnew ArgumentNullException("image");

	SQLiteSafeHandle::Reference instance(m_handle);		// Instance handle
	sqlite3_stmt* statement;							// Statement handle

	auto sql = L"update artwork set format = ?1, width = ?2, height = ?3, image = ?4 where artworkid = ?5";

	// Convert the artworkid into a byte array and pin it
	array<Byte>^ _artworkid = artworkid.ToByteArray();
	pin_ptr<Byte> pinartworkid = &_artworkid[0];

	// Pin the format string
	pin_ptr<wchar_t const> pinformat = PtrToStringChars(format);

	// Pin the image data
	pin_ptr<Byte> pinimage = &image[0];

	// Prepare the query
	int result = sqlite3_prepare16_v2(instance, sql, -1, &statement, nullptr);
	if(result != SQLITE_OK) throw gcnew SQLiteException(result, sqlite3_errmsg(instance));

	try {

		// Bind the query parameter(s)
		result = sqlite3_bind_text16(statement, 1, pinformat, -1, SQLITE_STATIC);
		if(result == SQLITE_OK) result = sqlite3_bind_int(statement, 2, width);
		if(result == SQLITE_OK) result = sqlite3_bind_int(statement, 3, height);
		if(result == SQLITE_OK) result = sqlite3_bind_blob(statement, 4, pinimage, image->Length, SQLITE_STATIC);
		if(result == SQLITE_OK) result = sqlite3_bind_blob(statement, 5, pinartworkid, _artworkid->Length, SQLITE_STATIC);
		if(result != SQLITE_OK) throw gcnew SQLiteException(result);

		// Execute the query; no rows are expected to be returned
		result = sqlite3_step(statement);
		if(result != SQLITE_DONE) SQLiteException(result, sqlite3_errmsg(instance));
	}

	finally { sqlite3_finalize(statement); }
}

//---------------------------------------------------------------------------
// Database::UpdateCardText (internal)
//
// Updates the text for a Card in the database
//
// Arguments:
//
//	card		- Card instance to retrieve the artwork for

void Database::UpdateCardText(Guid cardid, String^ text)
{
	CHECK_DISPOSED(m_disposed);
	CLRASSERT(CLRISNOTNULL(m_handle) && (m_handle->IsClosed == false));

	if(CLRISNULL(text)) throw gcnew ArgumentNullException("text");

	SQLiteSafeHandle::Reference instance(m_handle);

	execute_non_query(instance, L"update card set text = ?1 where cardid = ?2", text, cardid);
}

//---------------------------------------------------------------------------
// Database::UpdateDefaultArtwork
//
// Updates the default artwork for a card in the database
//
// Arguments:
//
//	cardid		- Card unique identifier
//	artworkid	- Artwork unique identifier

void Database::UpdateDefaultArtwork(Guid cardid, Guid artworkid)
{
	CHECK_DISPOSED(m_disposed);
	CLRASSERT(CLRISNOTNULL(m_handle) && (m_handle->IsClosed == false));

	SQLiteSafeHandle::Reference instance(m_handle);

	execute_non_query(instance, L"insert into defaultartwork values(?1, ?2) "
		"on conflict(cardid) do update set artworkid = excluded.artworkid", cardid, artworkid);
}

//---------------------------------------------------------------------------
// Database::Vacuum
//
// Vacuums the database
//
// Arguments:
//
//	NONE

void Database::Vacuum(void)
{
	CHECK_DISPOSED(m_disposed);
	CLRASSERT(CLRISNOTNULL(m_handle) && (m_handle->IsClosed == false));

	SQLiteSafeHandle::Reference instance(m_handle);

	execute_non_query(instance, L"vacuum");
}

//---------------------------------------------------------------------------

} // zuki::ronin::data

#pragma warning(pop)
