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

#include <assert.h>
#include <rpc.h>
#include <sqlite3ext.h>
#include <stdexcept>
#include <string>
#include <vector>

#include "CardAttribute.h"
#include "CardType.h"
#include "MonsterType.h"
#include "PrintRarity.h"

extern "C" { SQLITE_EXTENSION_INIT1 };

using namespace zuki::ronin::data;

#pragma warning(push, 4)

//---------------------------------------------------------------------------
// FUNCTION PROTOTYPES
//---------------------------------------------------------------------------

// uuids virtual table functions
//
int uuids_bestindex(sqlite3_vtab* vtab, sqlite3_index_info* info);
int uuids_close(sqlite3_vtab_cursor* cursor);
int uuids_column(sqlite3_vtab_cursor* cursor, sqlite3_context* context, int ordinal);
int uuids_connect(sqlite3* instance, void* aux, int argc, const char* const* argv, sqlite3_vtab** vtab, char** err);
int uuids_disconnect(sqlite3_vtab* vtab);
int uuids_eof(sqlite3_vtab_cursor* cursor);
int uuids_filter(sqlite3_vtab_cursor* cursor, int indexnum, char const* indexstr, int argc, sqlite3_value** argv);
int uuids_next(sqlite3_vtab_cursor* cursor);
int uuids_open(sqlite3_vtab* vtab, sqlite3_vtab_cursor** cursor);
int uuids_rowid(sqlite3_vtab_cursor* cursor, sqlite_int64* rowid);

//---------------------------------------------------------------------------
// TYPE DECLARATIONS
//---------------------------------------------------------------------------

// uuids_vtab_columns
//
// Constants indicating the uuids virtual table column ordinals
enum class uuids_vtab_columns {

	input,					// input text hidden
	uuid,					// uuid blob not null
};

// uuids_vtab
//
// Subclassed version of sqlite3_vtab for the uuids virtual table
struct uuids_vtab : public sqlite3_vtab
{
	// Instance Constructor
	//
	uuids_vtab() { memset(static_cast<sqlite3_vtab*>(this), 0, sizeof(sqlite3_vtab)); }
};

// uuids_vtab_cursor
//
// Subclassed version of sqlite3_vtab_cursor for the uuids virtual table
struct uuids_vtab_cursor : public sqlite3_vtab_cursor
{
	// Instance Constructor
	//
	uuids_vtab_cursor() { memset(static_cast<sqlite3_vtab_cursor*>(this), 0, sizeof(sqlite3_vtab_cursor)); }

	// Fields
	//
	std::vector<UUID>		uuids;				// vector<> of binary UUIDs
	sqlite3_int64			rowid = 0;			// Current SQLite rowid
	bool					eof = false;		// EOF flag
};

//---------------------------------------------------------------------------
// GLOBAL VARIABLES
//---------------------------------------------------------------------------

// g_uuids_module
//
// Defines the entry points for the uuids virtual table
static sqlite3_module g_uuids_module = {

	0,							// iVersion
	nullptr,					// xCreate
	uuids_connect,				// xConnect
	uuids_bestindex,			// xBestIndex
	uuids_disconnect,			// xDisconnect
	nullptr,					// xDestroy
	uuids_open,					// xOpen
	uuids_close,				// xClose
	uuids_filter,				// xFilter
	uuids_next,					// xNext
	uuids_eof,					// xEof
	uuids_column,				// xColumn
	uuids_rowid,				// xRowid
	nullptr,					// xUpdate
	nullptr,					// xBegin
	nullptr,					// xSync
	nullptr,					// xCommit
	nullptr,					// xRollback
	nullptr,					// xFindMethod
	nullptr,					// xRename
	nullptr,					// xSavepoint
	nullptr,					// xRelease
	nullptr,					// xRollbackTo
	nullptr						// xShadowName
};

//---------------------------------------------------------------------------
// cardtype (local)
//
// SQLite scalar function to convert a card type string into a CardType
//
// Arguments:
//
//	context		- SQLite context object
//	argc		- Number of supplied arguments
//	argv		- Argument values

static void cardtype(sqlite3_context* context, int argc, sqlite3_value** argv)
{
	if((argc != 1) || (argv[0] == nullptr)) return sqlite3_result_error(context, "invalid arguments", -1);

	// Null or zero-length input string results in CardType::None
	wchar_t const* str = reinterpret_cast<wchar_t const*>(sqlite3_value_text16(argv[0]));
	if((str == nullptr) || (*str == L'\0')) return sqlite3_result_int(context, static_cast<int>(CardType::None));

	// The strings are case-sensitive and enforced by a CHECK CONSTRAINT
	if(wcscmp(str, L"Monster") == 0) return sqlite3_result_int(context, static_cast<int>(CardType::Monster));
	else if(wcscmp(str, L"Spell") == 0) return sqlite3_result_int(context, static_cast<int>(CardType::Spell));
	else if(wcscmp(str, L"Trap") == 0) return sqlite3_result_int(context, static_cast<int>(CardType::Trap));

	// Input string was not a valid monster attribute
	return sqlite3_result_int(context, static_cast<int>(CardType::None));
}

//---------------------------------------------------------------------------
// monsterattribute (local)
//
// SQLite scalar function to convert an attribute string into a CardAttribute
//
// Arguments:
//
//	context		- SQLite context object
//	argc		- Number of supplied arguments
//	argv		- Argument values

static void monsterattribute(sqlite3_context* context, int argc, sqlite3_value** argv)
{
	if((argc != 1) || (argv[0] == nullptr)) return sqlite3_result_error(context, "invalid arguments", -1);

	// Null or zero-length input string results in CardAttribute::None
	wchar_t const* str = reinterpret_cast<wchar_t const*>(sqlite3_value_text16(argv[0]));
	if((str == nullptr) || (*str == L'\0')) return sqlite3_result_int(context, static_cast<int>(CardAttribute::None));

	// The strings are case-sensitive and enforced by a CHECK CONSTRAINT
	if(wcscmp(str, L"DARK") == 0) return sqlite3_result_int(context, static_cast<int>(CardAttribute::Dark));
	else if(wcscmp(str, L"EARTH") == 0) return sqlite3_result_int(context, static_cast<int>(CardAttribute::Earth));
	else if(wcscmp(str, L"FIRE") == 0) return sqlite3_result_int(context, static_cast<int>(CardAttribute::Fire));
	else if(wcscmp(str, L"LIGHT") == 0) return sqlite3_result_int(context, static_cast<int>(CardAttribute::Light));
	else if(wcscmp(str, L"WATER") == 0) return sqlite3_result_int(context, static_cast<int>(CardAttribute::Water));
	else if(wcscmp(str, L"WIND") == 0) return sqlite3_result_int(context, static_cast<int>(CardAttribute::Wind));

	// Input string was not a valid monster attribute
	return sqlite3_result_int(context, static_cast<int>(CardAttribute::None));
}

//---------------------------------------------------------------------------
// monstertype (local)
//
// SQLite scalar function to convert a type string into a MonsterType
//
// Arguments:
//
//	context		- SQLite context object
//	argc		- Number of supplied arguments
//	argv		- Argument values

static void monstertype(sqlite3_context* context, int argc, sqlite3_value** argv)
{
	if((argc != 1) || (argv[0] == nullptr)) return sqlite3_result_error(context, "invalid arguments", -1);

	// Null or zero-length input string results in MonsterType::None
	wchar_t const* str = reinterpret_cast<wchar_t const*>(sqlite3_value_text16(argv[0]));
	if((str == nullptr) || (*str == L'\0')) return sqlite3_result_int(context, static_cast<int>(MonsterType::None));

	// The strings are case-sensitive and enforced by a CHECK CONSTRAINT
	if(wcscmp(str, L"Aqua") == 0) return sqlite3_result_int(context, static_cast<int>(MonsterType::Aqua));
	else if(wcscmp(str, L"Beast") == 0) return sqlite3_result_int(context, static_cast<int>(MonsterType::Beast));
	else if(wcscmp(str, L"Beast-Warrior") == 0) return sqlite3_result_int(context, static_cast<int>(MonsterType::BeastWarrior));
	else if(wcscmp(str, L"Dinosaur") == 0) return sqlite3_result_int(context, static_cast<int>(MonsterType::Dinosaur));
	else if(wcscmp(str, L"Dragon") == 0) return sqlite3_result_int(context, static_cast<int>(MonsterType::Dragon));
	else if(wcscmp(str, L"Fairy") == 0) return sqlite3_result_int(context, static_cast<int>(MonsterType::Fairy));
	else if(wcscmp(str, L"Fiend") == 0) return sqlite3_result_int(context, static_cast<int>(MonsterType::Fiend));
	else if(wcscmp(str, L"Fish") == 0) return sqlite3_result_int(context, static_cast<int>(MonsterType::Fish));
	else if(wcscmp(str, L"Insect") == 0) return sqlite3_result_int(context, static_cast<int>(MonsterType::Insect));
	else if(wcscmp(str, L"Machine") == 0) return sqlite3_result_int(context, static_cast<int>(MonsterType::Machine));
	else if(wcscmp(str, L"Plant") == 0) return sqlite3_result_int(context, static_cast<int>(MonsterType::Plant));
	else if(wcscmp(str, L"Pyro") == 0) return sqlite3_result_int(context, static_cast<int>(MonsterType::Pyro));
	else if(wcscmp(str, L"Reptile") == 0) return sqlite3_result_int(context, static_cast<int>(MonsterType::Reptile));
	else if(wcscmp(str, L"Rock") == 0) return sqlite3_result_int(context, static_cast<int>(MonsterType::Rock));
	else if(wcscmp(str, L"Sea Serpent") == 0) return sqlite3_result_int(context, static_cast<int>(MonsterType::SeaSerpent));
	else if(wcscmp(str, L"Spellcaster") == 0) return sqlite3_result_int(context, static_cast<int>(MonsterType::Spellcaster));
	else if(wcscmp(str, L"Thunder") == 0) return sqlite3_result_int(context, static_cast<int>(MonsterType::Thunder));
	else if(wcscmp(str, L"Warrior") == 0) return sqlite3_result_int(context, static_cast<int>(MonsterType::Warrior));
	else if(wcscmp(str, L"Winged Beast") == 0) return sqlite3_result_int(context, static_cast<int>(MonsterType::WingedBeast));
	else if(wcscmp(str, L"Zombie") == 0) return sqlite3_result_int(context, static_cast<int>(MonsterType::Zombie));

	// Input string was not a valid monster type
	return sqlite3_result_int(context, static_cast<int>(CardAttribute::None));
}

//---------------------------------------------------------------------------
// newid (local)
//
// SQLite scalar function to generate a UUID
//
// Arguments:
//
//	context		- SQLite context object
//	argc		- Number of supplied arguments
//	argv		- Argument values

static void newid(sqlite3_context* context, int argc, sqlite3_value** /*argv*/)
{
	UUID uuid = {};

	if(argc != 0) return sqlite3_result_error16(context, L"invalid argument", -1);

	// Create a new UUID using Windows RPC
	RPC_STATUS status = UuidCreate(&uuid);
	if(status != RPC_S_OK) { /* Suppress C6031 */ }

	// Return the UUID back as a 16-byte blob
	return sqlite3_result_blob(context, &uuid, sizeof(UUID), SQLITE_TRANSIENT);
}

//---------------------------------------------------------------------------
// printrarity (local)
//
// SQLite scalar function to convert a rarity string into a PrintRarity
//
// Arguments:
//
//	context		- SQLite context object
//	argc		- Number of supplied arguments
//	argv		- Argument values

static void printrarity(sqlite3_context* context, int argc, sqlite3_value** argv)
{
	if((argc != 1) || (argv[0] == nullptr)) return sqlite3_result_error(context, "invalid arguments", -1);

	// Null or zero-length input string results in PrintRarity::None
	wchar_t const* str = reinterpret_cast<wchar_t const*>(sqlite3_value_text16(argv[0]));
	if((str == nullptr) || (*str == L'\0')) return sqlite3_result_int(context, static_cast<int>(PrintRarity::None));

	// The strings are case-sensitive and enforced by a CHECK CONSTRAINT
	if(wcscmp(str, L"Common") == 0) return sqlite3_result_int(context, static_cast<int>(PrintRarity::Common));
	else if(wcscmp(str, L"Gold Rare") == 0) return sqlite3_result_int(context, static_cast<int>(PrintRarity::GoldRare));
	else if(wcscmp(str, L"Parallel Rare") == 0) return sqlite3_result_int(context, static_cast<int>(PrintRarity::ParallelRare));
	else if(wcscmp(str, L"Prismatic Secret Rare") == 0) return sqlite3_result_int(context, static_cast<int>(PrintRarity::PrismaticSecretRare));
	else if(wcscmp(str, L"Rare") == 0) return sqlite3_result_int(context, static_cast<int>(PrintRarity::Rare));
	else if(wcscmp(str, L"Secret Rare") == 0) return sqlite3_result_int(context, static_cast<int>(PrintRarity::SecretRare));
	else if(wcscmp(str, L"Super Rare") == 0) return sqlite3_result_int(context, static_cast<int>(PrintRarity::SuperRare));
	else if(wcscmp(str, L"Ultra Parallel Rare") == 0) return sqlite3_result_int(context, static_cast<int>(PrintRarity::UltraParallelRare));
	else if(wcscmp(str, L"Ultra Rare") == 0) return sqlite3_result_int(context, static_cast<int>(PrintRarity::UltraRare));

	// Input string was not a valid monster type
	return sqlite3_result_int(context, static_cast<int>(PrintRarity::None));
}

//---------------------------------------------------------------------------
// uuids_bestindex
//
// Determines the best index to use when querying the virtual table
//
// Arguments:
//
//	vtab	- Virtual Table instance
//	info	- Selected index information to populate

int uuids_bestindex(sqlite3_vtab* /*vtab*/, sqlite3_index_info* info)
{
	// usable_constraint_index (local)
	//
	// Finds the first usable constraint for the specified column ordinal
	auto usable_constraint_index = [](sqlite3_index_info* info, int ordinal) -> int {

		// The constraints aren't necessarily in the order specified by the table, loop to find it
		for(int index = 0; index < info->nConstraint; index++) {

			auto constraint = &info->aConstraint[index];
			if(constraint->iColumn == ordinal) return ((constraint->usable) && (constraint->op == SQLITE_INDEX_CONSTRAINT_EQ)) ? index : -1;
		}

		return -1;
	};

	// argv[1] - input; required
	int input  = usable_constraint_index(info, static_cast<int>(uuids_vtab_columns::input));
	if(input < 0) return SQLITE_CONSTRAINT;
	info->aConstraintUsage[input].argvIndex = 1;
	info->aConstraintUsage[input].omit = 1;

	// There are no viable indexes on this virtual table, force the cost to 1
	info->estimatedCost = 1.0;

	return SQLITE_OK;
}

//---------------------------------------------------------------------------
// uuids_close
//
// Closes and deallocates a virtual table cursor instance
//
// Arguments:
//
//	cursor		- Cursor instance allocated by xOpen

int uuids_close(sqlite3_vtab_cursor* cursor)
{
	// Cast the provided generic cursor instance back into an uuids_vtab_cursor instance
	uuids_vtab_cursor* uuidscursor = reinterpret_cast<uuids_vtab_cursor*>(cursor);
	assert(uuidscursor != nullptr);

	delete uuidscursor;

	return SQLITE_OK;
}

//---------------------------------------------------------------------------
// uuids_column
//
// Accesses the data in the specified column of the current cursor row
//
// Arguments:
//
//	cursor		- Virtual table cursor instance
//	context		- Result context object
//	ordinal		- Ordinal of the column being accessed

int uuids_column(sqlite3_vtab_cursor* cursor, sqlite3_context* context, int ordinal)
{
	// Cast the provided generic cursor instance back into an uuids_vtab_cursor instance
	uuids_vtab_cursor* uuidscursor = reinterpret_cast<uuids_vtab_cursor*>(cursor);
	assert(uuidscursor != nullptr);

	// Assume that the result will be NULL to avoid adding this for each column test below
	sqlite3_result_null(context);

	// Extract the requested value from the current <programme> element in the XML document
	switch(static_cast<uuids_vtab_columns>(ordinal)) {

		case uuids_vtab_columns::uuid:
			assert(static_cast<size_t>(uuidscursor->rowid) <= uuidscursor->uuids.size());
			sqlite3_result_blob(context, &uuidscursor->uuids[static_cast<size_t>(uuidscursor->rowid - 1)], 
				sizeof(UUID), SQLITE_TRANSIENT);
			break;
	}

	return SQLITE_OK;
}

//---------------------------------------------------------------------------
// uuids_connect
//
// Connects to the uuids table
//
// Arguments:
//
//	instance	- SQLite database instance handle
//	aux			- Client data pointer provided to sqlite3_create_module[_v2]()
//	argc		- Number of provided metadata strings
//	argv		- Metadata strings
//	vtab		- On success contains the allocated virtual table instance
//	err			- On error contains a string-based error message

int uuids_connect(sqlite3* instance, void* /*aux*/, int /*argc*/, const char* const* /*argv*/, sqlite3_vtab** vtab, char** err)
{
	// Declare the schema for the virtual table, use hidden columns for all of the filter criteria
 	int result = sqlite3_declare_vtab(instance, "create table uuids(input text hidden, uuid blob not null)");
	if(result != SQLITE_OK) return result;

	// Allocate and initialize the custom virtual table class
	try { *vtab = static_cast<sqlite3_vtab*>(new uuids_vtab()); } 
	catch(std::exception const& ex) { *err = sqlite3_mprintf("%s", ex.what()); return SQLITE_ERROR; } 
	catch(...) { return SQLITE_ERROR; }

	return (*vtab == nullptr) ? SQLITE_NOMEM : SQLITE_OK;
}

//---------------------------------------------------------------------------
// uuids_disconnect
//
// Disconnects from the uuids virtual table
//
// Arguments:
//
//	vtab		- Virtual table instance allocated by xConnect

int uuids_disconnect(sqlite3_vtab* vtab)
{
	if(vtab != nullptr) delete reinterpret_cast<uuids_vtab*>(vtab);

	return SQLITE_OK;
}

//---------------------------------------------------------------------------
// uuids_eof
//
// Determines if the specified cursor has moved beyond the last row of data
//
// Arguments:
//
//	cursor		- Virtual table cursor instance

int uuids_eof(sqlite3_vtab_cursor* cursor)
{
	// Cast the provided generic cursor instance back into an uuids_vtab_cursor instance
	uuids_vtab_cursor* uuidscursor = reinterpret_cast<uuids_vtab_cursor*>(cursor);
	assert(uuidscursor != nullptr);

	return (uuidscursor->eof) ? 1 : 0;
}

//---------------------------------------------------------------------------
// uuids_filter
//
// Executes a search of the virtual table
//
// Arguments:
//
//	cursor		- Virtual table cursor instance
//	indexnum	- Virtual table index number from xBestIndex()
//	indexstr	- Virtual table index string from xBestIndex()
//	argc		- Number of arguments assigned by xBestIndex()
//	argv		- Argument data assigned by xBestIndex()

int uuids_filter(sqlite3_vtab_cursor* cursor, int /*indexnum*/, char const* /*indexstr*/, int argc, sqlite3_value** argv)
{
	// Cast the provided generic cursor instance back into an uuids_vtab_cursor instance
	uuids_vtab_cursor* uuidscursor = reinterpret_cast<uuids_vtab_cursor*>(cursor);
	assert(uuidscursor != nullptr);

	// The input argument must have been specified by xBestIndex
	if(argc < 1) {

		uuidscursor->pVtab->zErrMsg = sqlite3_mprintf("invalid argument count provided by xBestIndex"); 
		return SQLITE_ERROR;
	}

	// Parse the input string into individual UUIDs; use managed code here even though it's going to be
	// slower than native code would be to take advantage of String::Split, String::Trim, and Guid::TryParse
	wchar_t const* inputptr = reinterpret_cast<wchar_t const*>(sqlite3_value_text16(argv[0]));
	if(inputptr != nullptr) {

		String^ input = gcnew String(inputptr);
		for each(String^ string in input->Split(gcnew array<wchar_t>{ L',' }, StringSplitOptions::RemoveEmptyEntries)) {

			Guid uuid = Guid::Empty;
			if(Guid::TryParse(string->Trim(), uuid)) {

				// If the Guid parsed, push it into the cursor vector<> as a UUID
				array<Byte>^ bytes = uuid.ToByteArray();
				pin_ptr<Byte> pinbytes = &bytes[0];
				uuidscursor->uuids.push_back(*reinterpret_cast<UUID*>(pinbytes));
			}
		}
	}	
	
	// xFilter should position the cursor at the first row of the result set or at EOF
	return uuids_next(uuidscursor);
}

//---------------------------------------------------------------------------
// uuids_next
//
// Advances the virtual table cursor to the next row
//
// Arguments:
//
//	cursor		- Virtual table cusror instance

int uuids_next(sqlite3_vtab_cursor* cursor)
{
	// Cast the provided generic cursor instance back into an uuids_vtab_cursor instance
	uuids_vtab_cursor* uuidscursor = reinterpret_cast<uuids_vtab_cursor*>(cursor);
	assert(uuidscursor != nullptr);

	// Check for EOF
	if(static_cast<size_t>(uuidscursor->rowid) == uuidscursor->uuids.size()) {

		uuidscursor->eof = true;
		return SQLITE_OK;
	}

	// Increment the ROWID value to be returned to SQLite when asked
	uuidscursor->rowid++;

	return SQLITE_OK;
}

//---------------------------------------------------------------------------
// uuids_open
//
// Creates and intializes a new virtual table cursor instance
//
// Arguments:
//
//	vtab		- Virtual table instance
//	cursor		- On success contains the allocated virtual table cursor instance

int uuids_open(sqlite3_vtab* /*vtab*/, sqlite3_vtab_cursor** cursor)
{
	// Allocate and initialize the custom virtual table cursor class
	try { *cursor = static_cast<sqlite3_vtab_cursor*>(new uuids_vtab_cursor()); }
	catch(...) { return SQLITE_ERROR; }

	return (*cursor == nullptr) ? SQLITE_NOMEM : SQLITE_OK;
}

//---------------------------------------------------------------------------
// uuids_rowid
//
// Retrieves the ROWID for the current virtual table cursor row
//
// Arguments:
//
//	cursor		- Virtual table cursor instance
//	rowid		- On success contains the ROWID for the current row

int uuids_rowid(sqlite3_vtab_cursor* cursor, sqlite_int64* rowid)
{
	// Cast the provided generic cursor instance back into an uuids_vtab_cursor instance
	uuids_vtab_cursor* uuidscursor = reinterpret_cast<uuids_vtab_cursor*>(cursor);
	assert(uuidscursor != nullptr);

	*rowid = uuidscursor->rowid;
	return SQLITE_OK;
}

//---------------------------------------------------------------------------
// uuidstr (local)
//
// SQLite scalar function to convert a binary UUID into a string
//
// Arguments:
//
//	context		- SQLite context object
//	argc		- Number of supplied arguments
//	argv		- Argument values

static void uuidstr(sqlite3_context* context, int argc, sqlite3_value** argv)
{
	if((argc != 1) || (argv[0] == nullptr)) return sqlite3_result_error(context, "invalid arguments", -1);

	// The length of the blob must match the size of a UUID
	int length = sqlite3_value_bytes(argv[0]);
	if(length == sizeof(UUID)) {

		// Use RPC to convert the UUID blob into a string (.NET "D" format)
		RPC_WSTR uuidstr = nullptr;
		RPC_STATUS status = UuidToString(reinterpret_cast<UUID const*>(sqlite3_value_blob(argv[0])), &uuidstr);
		if(status == RPC_S_OK) {

			sqlite3_result_text16(context, uuidstr, -1, SQLITE_TRANSIENT);
			RpcStringFree(&uuidstr);
			return;
		}
	}

	return sqlite3_result_null(context);
}

//---------------------------------------------------------------------------
// sqlite3_extension_init
//
// SQLite Extension Library entry point
//
// Arguments:
//
//	db		- SQLite database instance
//	errmsg	- On failure set to the error message (use sqlite3_malloc() to allocate)
//	api		- Pointer to the SQLite API functions

extern "C" int sqlite3_extension_init(sqlite3* db, char** errmsg, const sqlite3_api_routines* api)
{
	SQLITE_EXTENSION_INIT2(api);

	*errmsg = nullptr;							// Initialize [out] variable

	// cardtype function
	//
	int result = sqlite3_create_function16(db, L"cardtype", 1, SQLITE_UTF16, nullptr, cardtype, nullptr, nullptr);
	if(result != SQLITE_OK) { *errmsg = sqlite3_mprintf("Unable to register scalar function cardtype (%d)", result); return result; }

	// monsterattribute function
	//
	result = sqlite3_create_function16(db, L"monsterattribute", 1, SQLITE_UTF16, nullptr, monsterattribute, nullptr, nullptr);
	if(result != SQLITE_OK) { *errmsg = sqlite3_mprintf("Unable to register scalar function monsterattribute (%d)", result); return result; }

	// monstertype function
	//
	result = sqlite3_create_function16(db, L"monstertype", 1, SQLITE_UTF16, nullptr, monstertype, nullptr, nullptr);
	if(result != SQLITE_OK) { *errmsg = sqlite3_mprintf("Unable to register scalar function monstertype (%d)", result); return result; }

	// newid function
	//
	result = sqlite3_create_function16(db, L"newid", 0, SQLITE_UTF16, nullptr, newid, nullptr, nullptr);
	if(result != SQLITE_OK) { *errmsg = sqlite3_mprintf("Unable to register scalar function newid (%d)", result); return result; }

	// printrarity function
	//
	result = sqlite3_create_function16(db, L"printrarity", 1, SQLITE_UTF16, nullptr, printrarity, nullptr, nullptr);
	if(result != SQLITE_OK) { *errmsg = sqlite3_mprintf("Unable to register scalar function printrarity (%d)", result); return result; }

	// uuidstr function
	//
	result = sqlite3_create_function16(db, L"uuidstr", 1, SQLITE_UTF16, nullptr, uuidstr, nullptr, nullptr);
	if(result != SQLITE_OK) { *errmsg = sqlite3_mprintf("Unable to register scalar function uuidstr (%d)", result); return result; }

	// uuids virtual table
	//
	result = sqlite3_create_module_v2(db, "uuids", &g_uuids_module, nullptr, nullptr);
	if(result != SQLITE_OK) { *errmsg = sqlite3_mprintf("Unable to register virtual table module uuids (%d)", result); return result; }

	return SQLITE_OK;
}

//---------------------------------------------------------------------------

#pragma warning(pop)
