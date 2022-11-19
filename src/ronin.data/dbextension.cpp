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

#include <rpc.h>
#include <sqlite3ext.h>

#include "CardAttribute.h"
#include "CardType.h"
#include "MonsterType.h"
#include "PrintRarity.h"

extern "C" { SQLITE_EXTENSION_INIT1 };

using namespace zuki::ronin::data;

#pragma warning(push, 4)

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
	else if(wcscmp(str, L"Duel Terminal Normal Parallel Rare") == 0) return sqlite3_result_int(context, static_cast<int>(PrintRarity::DuelTerminalNormalParallelRare));
	else if(wcscmp(str, L"Duel Terminal Rare Parallel Rare") == 0) return sqlite3_result_int(context, static_cast<int>(PrintRarity::DuelTerminalRareParallelRare));
	else if(wcscmp(str, L"Duel Terminal Super Parallel Rare") == 0) return sqlite3_result_int(context, static_cast<int>(PrintRarity::DuelTerminalSuperParallelRare));
	else if(wcscmp(str, L"Gold Rare") == 0) return sqlite3_result_int(context, static_cast<int>(PrintRarity::GoldRare));
	else if(wcscmp(str, L"Parallel Rare") == 0) return sqlite3_result_int(context, static_cast<int>(PrintRarity::ParallelRare));
	else if(wcscmp(str, L"Prismatic Secret Rare") == 0) return sqlite3_result_int(context, static_cast<int>(PrintRarity::PrismaticSecretRare));
	else if(wcscmp(str, L"Rare") == 0) return sqlite3_result_int(context, static_cast<int>(PrintRarity::Rare));
	else if(wcscmp(str, L"Secret Rare") == 0) return sqlite3_result_int(context, static_cast<int>(PrintRarity::SecretRare));
	else if(wcscmp(str, L"Super Rare") == 0) return sqlite3_result_int(context, static_cast<int>(PrintRarity::SuperRare));
	else if(wcscmp(str, L"Ultra Parallel Rare") == 0) return sqlite3_result_int(context, static_cast<int>(PrintRarity::UltraParallelRare));
	else if(wcscmp(str, L"Ultra Rare") == 0) return sqlite3_result_int(context, static_cast<int>(PrintRarity::UltraRare));
	else if(wcscmp(str, L"Ultra Secret Rare") == 0) return sqlite3_result_int(context, static_cast<int>(PrintRarity::UltraSecretRare));

	// Input string was not a valid monster type
	return sqlite3_result_int(context, static_cast<int>(PrintRarity::None));
}

//---------------------------------------------------------------------------
// uuid (local)
//
// SQLite scalar function to generate a UUID
//
// Arguments:
//
//	context		- SQLite context object
//	argc		- Number of supplied arguments
//	argv		- Argument values

static void uuid(sqlite3_context* context, int argc, sqlite3_value** /*argv*/)
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

	// printrarity function
	//
	result = sqlite3_create_function16(db, L"printrarity", 1, SQLITE_UTF16, nullptr, printrarity, nullptr, nullptr);
	if(result != SQLITE_OK) { *errmsg = sqlite3_mprintf("Unable to register scalar function printrarity (%d)", result); return result; }

	// uuid function
	//
	result = sqlite3_create_function16(db, L"uuid", 0, SQLITE_UTF16, nullptr, uuid, nullptr, nullptr);
	if(result != SQLITE_OK) { *errmsg = sqlite3_mprintf("Unable to register scalar function uuid (%d)", result); return result; }

	return SQLITE_OK;
}

//---------------------------------------------------------------------------

#pragma warning(pop)
