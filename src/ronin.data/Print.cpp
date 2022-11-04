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
#include "Print.h"

#pragma warning(push, 4)

namespace zuki::ronin::data {

//---------------------------------------------------------------------------
// Print Constructor (internal)
//
// Arguments:
//
//	NONE

Print::Print()
{
}

//---------------------------------------------------------------------------
// Print::operator == (static)

bool Print::operator==(Print^ lhs, Print^ rhs)
{
	if(Object::ReferenceEquals(lhs, rhs)) return true;
	if(Object::ReferenceEquals(lhs, nullptr) || Object::ReferenceEquals(rhs, nullptr)) return false;

	return lhs->PrintID == rhs->PrintID;
}

//---------------------------------------------------------------------------
// Print::operator != (static)

bool Print::operator!=(Print^ lhs, Print^ rhs)
{
	if(Object::ReferenceEquals(lhs, rhs)) return false;
	if(Object::ReferenceEquals(lhs, nullptr) || Object::ReferenceEquals(rhs, nullptr)) return true;

	return lhs->PrintID != rhs->PrintID;
}

//---------------------------------------------------------------------------
// Print::CardID::get
//
// Gets the card unique identifier

Guid Print::CardID::get(void)
{
	return m_cardid;
}

//---------------------------------------------------------------------------
// Print::CardID::set (internal)
//
// Sets the card unique identifier

void Print::CardID::set(Guid value)
{
	m_cardid = value;
}

//---------------------------------------------------------------------------
// Print::Code::get
//
// Gets the print code

String^ Print::Code::get(void)
{
	return m_code;
}

//---------------------------------------------------------------------------
// Print::Code::set (internal)
//
// Sets the print code

void Print::Code::set(String^ value)
{
	m_code = value;
}

//---------------------------------------------------------------------------
// Print::Equals
//
// Compares this Print instance to another Print instance
//
// Arguments:
//
//	rhs		- Right-hand Print instance to compare against

bool Print::Equals(Print^ rhs)
{
	return (this == rhs);
}

//---------------------------------------------------------------------------
// Print::Equals
//
// Overrides Object::Equals()
//
// Arguments:
//
//	rhs		- Right-hand object instance to compare against

bool Print::Equals(Object^ rhs)
{
	if(Object::ReferenceEquals(rhs, nullptr)) return false;

	// Convert the provided object into a Print instance
	Print^ rhsref = dynamic_cast<Print^>(rhs);
	if(rhsref == nullptr) return false;

	return (this == rhsref);
}

//---------------------------------------------------------------------------
// Print::GetHashCode
//
// Overrides Object::GetHashCode()
//
// Arguments:
//
//	NONE

int Print::GetHashCode(void)
{
	return m_printid.GetHashCode();
}

//---------------------------------------------------------------------------
// Print::Language::get
//
// Gets the print language

String^ Print::Language::get(void)
{
	return m_language;
}

//---------------------------------------------------------------------------
// Print::Language::set (internal)
//
// Sets the print language

void Print::Language::set(String^ value)
{
	m_language = value;
}

//---------------------------------------------------------------------------
// Print::Number::get
//
// Gets the print number

String^ Print::Number::get(void)
{
	return m_number;
}

//---------------------------------------------------------------------------
// Print::Number::set (internal)
//
// Sets the print number

void Print::Number::set(String^ value)
{
	m_number = value;
}

//---------------------------------------------------------------------------
// Print::ParseRarity (internal, static)
//
// Parses a string into a PrintRarity
//
// Arguments:
//
//	value		- String to be parsed into a PrintRarity

PrintRarity Print::ParseRarity(String^ /*value*/)
{
	// TODO
	return PrintRarity::Common;
}

//---------------------------------------------------------------------------
// Print::PrintID::get
//
// Gets the print unique identifier

Guid Print::PrintID::get(void)
{
	return m_printid;
}

//---------------------------------------------------------------------------
// Print::PrintID::set (internal)
//
// Sets the print unique identifier

void Print::PrintID::set(Guid value)
{
	m_printid = value;
}

//---------------------------------------------------------------------------
// Print::Rarity::get
//
// Gets the print rarity

PrintRarity Print::Rarity::get(void)
{
	return m_rarity;
}

//---------------------------------------------------------------------------
// Print::Rarity::set (internal)
//
// Sets the print rarity

void Print::Rarity::set(PrintRarity value)
{
	m_rarity = value;
}

//---------------------------------------------------------------------------
// Print::ReleaseDate::get
//
// Gets the print release date

Nullable<DateTime> Print::ReleaseDate::get(void)
{
	return m_releasedate;
}

//---------------------------------------------------------------------------
// Print::ReleaseDate::set (internal)
//
// Sets the print release date

void Print::ReleaseDate::set(Nullable<DateTime> value)
{
	m_releasedate = value;
}

//---------------------------------------------------------------------------
// Print::SeriesID::get
//
// Gets the series unique identifier

Guid Print::SeriesID::get(void)
{
	return m_seriesid;
}

//---------------------------------------------------------------------------
// Print::SeriesID::set (internal)
//
// Sets the series unique identifier

void Print::SeriesID::set(Guid value)
{
	m_seriesid = value;
}

//---------------------------------------------------------------------------
// Print::ToString
//
// Overrides Object::ToString()
//
// Arguments:
//
//	NONE

String^ Print::ToString(void)
{
	// PSV-000
	// FOTB-EN001
	return String::Concat(m_code, "-", String::IsNullOrEmpty(m_language) ? "" : 
		m_language, m_number);
}

//---------------------------------------------------------------------------

} // zuki::ronin::data

#pragma warning(pop)
