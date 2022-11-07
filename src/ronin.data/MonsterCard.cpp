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
#include "MonsterCard.h"

#pragma warning(push, 4)

namespace zuki::ronin::data {

//---------------------------------------------------------------------------
// MonsterCard Constructor (internal)
//
// Arguments:
//
//	NONE

MonsterCard::MonsterCard() : Card(CardType::Monster)
{
}

//---------------------------------------------------------------------------
// MonsterCard Constructor (private)
//
// Arguments:
//
//	info		- Serialization information
//	context		- Serialization context

MonsterCard::MonsterCard(SerializationInfo^ info, StreamingContext context) : Card(info, context)
{
	if(CLRISNULL(info)) throw gcnew ArgumentNullException("info");

	m_attribute = static_cast<MonsterAttribute>(info->GetInt32("@monster_m_attribute"));
	m_type = static_cast<MonsterType>(info->GetInt32("@monster_m_type"));
	m_normal = info->GetBoolean("@monster_m_normal");
}

//---------------------------------------------------------------------------
// MonsterCard::Attribute::get
//
// Gets the monster attribute

MonsterAttribute MonsterCard::Attribute::get(void)
{
	return m_attribute;
}

//---------------------------------------------------------------------------
// MonsterCard::Attribute::set (internal)
//
// Sets the monster attribute

void MonsterCard::Attribute::set(MonsterAttribute value)
{
	m_attribute = value;
}

//---------------------------------------------------------------------------
// MonsterCard::GetObjectData
//
// Implements ISerializable::GetObjectData
//
// Arguments:
//
//	info		- Serialization information
//	context		- Serialization context

void MonsterCard::GetObjectData(SerializationInfo^ info, StreamingContext context)
{
	if(CLRISNULL(info)) throw gcnew ArgumentNullException("info");

	Card::GetObjectData(info, context);
	info->AddValue("@monster_m_attribute", static_cast<int>(m_attribute));
	info->AddValue("@monster_m_type", static_cast<int>(m_type));
	info->AddValue("@monster_m_normal", m_normal);
}

//---------------------------------------------------------------------------
// MonsterCard::Normal::get
//
// Gets the normal monster flag

bool MonsterCard::Normal::get(void)
{
	return m_normal;
}

//---------------------------------------------------------------------------
// MonsterCard::Normal::set (internal)
//
// Sets the normal monster flag

void MonsterCard::Normal::set(bool value)
{
	m_normal = value;
}

//---------------------------------------------------------------------------
// MonsterCard::Type::get
//
// Gets the monster type

MonsterType MonsterCard::Type::get(void)
{
	return m_type;
}

//---------------------------------------------------------------------------
// MonsterCard::Type::set (internal)
//
// Sets the monster type

void MonsterCard::Type::set(MonsterType value)
{
	m_type = value;
}

//---------------------------------------------------------------------------
// MonsterCard::ParseAttribute (internal, static)
//
// Parses a string into a MonsterAttribute
//
// Arguments:
//
//	value		- String to be parsed into a MonsterAttribute

MonsterAttribute MonsterCard::ParseAttribute(String^ /*value*/)
{
	// TODO
	return MonsterAttribute::Dark;
}

//---------------------------------------------------------------------------
// MonsterCard::ParseType (internal, static)
//
// Parses a string into a MonsterType
//
// Arguments:
//
//	value		- String to be parsed into a MonsterType

MonsterType MonsterCard::ParseType(String^ /*value*/)
{
	// TODO
	return MonsterType::Aqua;
}

//---------------------------------------------------------------------------

} // zuki::ronin::data

#pragma warning(pop)
