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

#ifndef __MONSTERCARD_H_
#define __MONSTERCARD_H_
#pragma once

#include "Card.h"
#include "MonsterAttribute.h"
#include "MonsterType.h"

#pragma warning(push, 4)

using namespace System;
using namespace System::Runtime::Serialization;
using namespace System::Security;
using namespace System::Security::Permissions;

namespace zuki::ronin::data {

//---------------------------------------------------------------------------
// Class MonsterCard
//
// Describes a monster card object
//---------------------------------------------------------------------------

public ref class MonsterCard : public Card
{
public:

	//-----------------------------------------------------------------------
	// Member Functions

	// GetObjectData
	//
	// Implements ISerializable::GetObjectData
	[SecurityCriticalAttribute]
	[PermissionSetAttribute(SecurityAction::LinkDemand, Unrestricted = true)]
	[PermissionSetAttribute(SecurityAction::InheritanceDemand, Unrestricted = true)]
	[SecurityPermissionAttribute(SecurityAction::Demand, SerializationFormatter = true)]
	virtual void GetObjectData(SerializationInfo^ info, StreamingContext context) override;

	//-----------------------------------------------------------------------
	// Properties

	// Attribute
	//
	// Gets the monster attribute
	property MonsterAttribute Attribute
	{
		MonsterAttribute get(void);
		internal: void set(MonsterAttribute value);
	}

	// Type
	//
	// Gets the monster type
	property MonsterType Type
	{
		MonsterType get(void) new;
		internal: void set(MonsterType value);
	}

	// Normal
	//
	// Gets the normal monster flag
	property bool Normal
	{
		bool get(void);
		internal: void set(bool value);
	}

internal:

	// Instance Constructor
	//
	MonsterCard();

	//-----------------------------------------------------------------------
	// Internal Member Functions

	// ParseAttribute (static)
	//
	// Parses a string into a MonsterAttribute
	static MonsterAttribute ParseAttribute(String^ value);

	// ParseType (static)
	//
	// Parses a string into a MonsterType
	static MonsterType ParseType(String^ value);

private:

	// Serialization Constructor
	//
	[SecurityPermissionAttribute(SecurityAction::Demand, SerializationFormatter = true)]
	MonsterCard(SerializationInfo^ info, StreamingContext context);

	//-----------------------------------------------------------------------
	// Member Variables

	MonsterAttribute	m_attribute;			// Attribute
	MonsterType			m_type;					// Type
	bool				m_normal = false;		// Normal monster
};

//---------------------------------------------------------------------------

} // zuki::ronin::data

#pragma warning(pop)

#endif	// __MONSTERCARD_H_
