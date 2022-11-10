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
// Database::SelectArtwork (internal)
//
// Gets the default artwork for a Card
//
// Arguments:
//
//	card		- Card instance to retrieve the artwork for

Bitmap^ Database::SelectArtwork(Card^ card)
{
	CHECK_DISPOSED(m_disposed);
	CLRASSERT(CLRISNOTNULL(m_handle) && (m_handle->IsClosed == false));

	SQLiteSafeHandle::Reference instance(m_handle);		// Instance handle
	sqlite3_stmt* statement;							// Statement handle

	auto sql = L"select artwork.image from artwork "
		"inner join defaultartwork on defaultartwork.artworkid = artwork.artworkid "
		"where defaultartwork.cardid = ?1";

	// Convert the cardid into a byte array and pin it
	array<Byte>^ cardid = card->CardID.ToByteArray();
	pin_ptr<Byte> pincardid = &cardid[0];

	// Prepare the query
	int result = sqlite3_prepare16_v2(instance, sql, -1, &statement, nullptr);
	if(result != SQLITE_OK) throw gcnew SQLiteException(result, sqlite3_errmsg(instance));

	try {

		// Bind the query parameter(s)
		result = sqlite3_bind_blob(statement, 1, pincardid, cardid->Length, SQLITE_TRANSIENT);
		if(result != SQLITE_OK) throw gcnew SQLiteException(result);

		// Execute the query; there should be at most one row returned
		if(sqlite3_step(statement) == SQLITE_ROW) {

			// Get the length of the BLOB data
			int length = sqlite3_column_bytes(statement, 0);
			if(length > 0) {

				// Convert the BLOB data into a new Bitmap instance
				void const* blob = sqlite3_column_blob(statement, 0);
				if(blob != nullptr) {

					// Wrap the BLOB in a read-only UnmanagedMemoryStream and create a Bitmap from it
					Byte* blobptr = reinterpret_cast<unsigned char*>(const_cast<void*>(blob));
					msclr::auto_handle<UnmanagedMemoryStream> stream(gcnew UnmanagedMemoryStream(blobptr, length, length, FileAccess::Read));
					return gcnew Bitmap(stream.get());
				}
			}
		}
	}

	finally { sqlite3_finalize(statement); }

	return nullptr;
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

	// { 00-04 } type | cardid | name | passcode | text |
	// { 05-17 } attribute | level | type | attack | defense | normal | effect | fusion | ritual | toon | union | spirit | gemini |
	// { 18-23 } normal | continuous | equip | field | quickplay | ritual |
	// { 24-26 } normal | continuous | counter
	auto sql = L"select cardtype(card.type), card.cardid, card.name, card.passcode, card.text, "
		"monsterattribute(monster.attribute), monster.level, monstertype(monster.type), monster.attack, "
		"monster.defense, monster.normal, monster.effect, monster.fusion, monster.ritual,  "
		"monster.toon, monster.[union], monster.spirit, monster.gemini, "
		"spell.normal, spell.continuous, spell.equip, spell.field, spell.quickplay, spell.ritual, "
		"trap.normal, trap.continuous, trap.counter from card "
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

				monster->Attribute	= static_cast<CardAttribute>(sqlite3_column_int(statement, 5));
				monster->Level		= sqlite3_column_int(statement, 6);
				monster->Type		= static_cast<MonsterType>(sqlite3_column_int(statement, 7));
				monster->Attack		= sqlite3_column_int(statement, 8);
				monster->Defense	= sqlite3_column_int(statement, 9);
				monster->Normal		= (sqlite3_column_int(statement, 10) != 0);
				monster->Effect		= (sqlite3_column_int(statement, 11) != 0);
				monster->Fusion		= (sqlite3_column_int(statement, 12) != 0);
				monster->Ritual		= (sqlite3_column_int(statement, 13) != 0);
				monster->Toon		= (sqlite3_column_int(statement, 14) != 0);
				monster->Union		= (sqlite3_column_int(statement, 15) != 0);
				monster->Spirit		= (sqlite3_column_int(statement, 16) != 0);
				monster->Gemini		= (sqlite3_column_int(statement, 17) != 0);

				card = static_cast<Card^>(monster);
			}

			// SpellCard
			else if(type == CardType::Spell) {

				SpellCard^ spell = gcnew SpellCard(this);

				spell->Normal		= (sqlite3_column_int(statement, 18) != 0);
				spell->Continuous	= (sqlite3_column_int(statement, 19) != 0);
				spell->Equip		= (sqlite3_column_int(statement, 20) != 0);
				spell->Field		= (sqlite3_column_int(statement, 21) != 0);
				spell->QuickPlay	= (sqlite3_column_int(statement, 22) != 0);
				spell->Ritual		= (sqlite3_column_int(statement, 23) != 0);

				card = static_cast<Card^>(spell);
			}

			// TrapCard
			else if(type == CardType::Trap) {

				TrapCard^ trap = gcnew TrapCard(this);

				trap->Normal		= (sqlite3_column_int(statement, 24) != 0);
				trap->Continuous	= (sqlite3_column_int(statement, 25) != 0);
				trap->Counter		= (sqlite3_column_int(statement, 26) != 0);

				card = static_cast<Card^>(trap);
			}

			else throw gcnew Exception("Invalid card.type value");

			// The base class reference should have been set above
			CLRASSERT(card != nullptr);

			// cardid
			// TODO: test efficiency of converting the BLOB to a string in SQLite with
			// an extension function; probably faster than this method
			int cardidlen = sqlite3_column_bytes(statement, 1);
			if(cardidlen != sizeof(UUID)) throw gcnew Exception("Invalid card.cardid length");
			array<byte>^ cardidblob = gcnew array<byte>(16);
			Marshal::Copy(IntPtr(const_cast<void*>(sqlite3_column_blob(statement, 1))), cardidblob, 0, cardidlen);
			card->CardID = Guid(cardidblob);

			// name
			wchar_t const* nameptr = reinterpret_cast<wchar_t const*>(sqlite3_column_text16(statement, 2));
			card->Name = (nameptr == nullptr) ? String::Empty : gcnew String(nameptr);

			// passcode
			wchar_t const* passcodeptr = reinterpret_cast<wchar_t const*>(sqlite3_column_text16(statement, 3));
			card->Passcode = (passcodeptr == nullptr) ? String::Empty : gcnew String(passcodeptr);

			// text
			wchar_t const* textptr = reinterpret_cast<wchar_t const*>(sqlite3_column_text16(statement, 4));
			card->Text = (textptr == nullptr) ? String::Empty : gcnew String(textptr);

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

} // zuki::ronin::data

#pragma warning(pop)
