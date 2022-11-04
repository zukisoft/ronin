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

#include "SQLiteException.h"

using namespace System::IO;

#pragma warning(push, 4)

namespace zuki::ronin::data {

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

		// Finalize the statement
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

		// Finalize the statement
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
// Database Destructor

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
	pin_ptr<const wchar_t> pinpath = PtrToStringChars(path);
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
	if(dbversion == 0) {

		// table: card
		//
		// cardid(pk) | name(u) | type | passcode(u) | text
		execute_non_query(instance, L"create table card(cardid blob not null, name text unique not null, type integer not null, "
			"passcode text unique not null, text text not null, primary key(cardid), "
			"check(type in (0, 1, 2)))");

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
}

//---------------------------------------------------------------------------

} // zuki::ronin::data

#pragma warning(pop)
