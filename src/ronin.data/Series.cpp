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
#include "Series.h"

#include "Extensions.h"

#pragma warning(push, 4)

namespace zuki::ronin::data {

//---------------------------------------------------------------------------
// Series Constructor (internal)
//
// Arguments:
//
//	NONE

Series::Series()
{
}

//---------------------------------------------------------------------------
// Series Constructor (private)
//
// Arguments:
//
//	info		- Serialization information
//	context		- Serialization context

Series::Series(SerializationInfo^ info, StreamingContext /*context*/)
{
	if(CLRISNULL(info)) throw gcnew ArgumentNullException("info");

	m_seriesid.Parse(info->GetString("@m_seriesid"));
	m_code = info->GetString("@m_code");
	m_name = info->GetString("@m_name");

	Object^ releasedate = Extensions::GetValueNoThrow(info, "@m_releasedate", DateTime::typeid);
	if(CLRISNOTNULL(releasedate)) m_releasedate = safe_cast<DateTime>(releasedate);
}

//---------------------------------------------------------------------------
// Series::operator == (static)

bool Series::operator==(Series^ lhs, Series^ rhs)
{
	if(Object::ReferenceEquals(lhs, rhs)) return true;
	if(Object::ReferenceEquals(lhs, nullptr) || Object::ReferenceEquals(rhs, nullptr)) return false;

	return lhs->SeriesID == rhs->SeriesID;
}

//---------------------------------------------------------------------------
// Series::operator != (static)

bool Series::operator!=(Series^ lhs, Series^ rhs)
{
	if(Object::ReferenceEquals(lhs, rhs)) return false;
	if(Object::ReferenceEquals(lhs, nullptr) || Object::ReferenceEquals(rhs, nullptr)) return true;

	return lhs->SeriesID != rhs->SeriesID;
}

//---------------------------------------------------------------------------
// Series::Code::get
//
// Gets the series code

String^ Series::Code::get(void)
{
	return m_code;
}

//---------------------------------------------------------------------------
// Series::Code::set (internal)
//
// Sets the series code

void Series::Code::set(String^ value)
{
	m_code = value;
}

//---------------------------------------------------------------------------
// Series::Equals
//
// Compares this Series instance to another Series instance
//
// Arguments:
//
//	rhs		- Right-hand Series instance to compare against

bool Series::Equals(Series^ rhs)
{
	return (this == rhs);
}

//---------------------------------------------------------------------------
// Series::Equals
//
// Overrides Object::Equals()
//
// Arguments:
//
//	rhs		- Right-hand object instance to compare against

bool Series::Equals(Object^ rhs)
{
	if(Object::ReferenceEquals(rhs, nullptr)) return false;

	// Convert the provided object into a Series instance
	Series^ rhsref = dynamic_cast<Series^>(rhs);
	if(rhsref == nullptr) return false;

	return (this == rhsref);
}

//---------------------------------------------------------------------------
// Series::GetHashCode
//
// Overrides Object::GetHashCode()
//
// Arguments:
//
//	NONE

int Series::GetHashCode(void)
{
	return m_seriesid.GetHashCode();
}

//---------------------------------------------------------------------------
// Series::GetObjectData
//
// Implements ISerializable::GetObjectData
//
// Arguments:
//
//	info		- Serialization information
//	context		- Serialization context

void Series::GetObjectData(SerializationInfo^ info, StreamingContext /*context*/)
{
	if(CLRISNULL(info)) throw gcnew ArgumentNullException("info");

	info->AddValue("@m_seriesid", m_seriesid.ToString());
	info->AddValue("@m_code", m_code);
	info->AddValue("@m_name", m_name);
	if(m_releasedate.HasValue) info->AddValue("@m_releasedate", m_releasedate.Value);
}

//---------------------------------------------------------------------------
// Series::Name::get
//
// Gets the series name

String^ Series::Name::get(void)
{
	return m_name;
}

//---------------------------------------------------------------------------
// Series::Name::set (internal)
//
// Sets the series name

void Series::Name::set(String^ value)
{
	m_name = value;
}

//---------------------------------------------------------------------------
// Series::ReleaseDate::get
//
// Gets the series release date

Nullable<DateTime> Series::ReleaseDate::get(void)
{
	return m_releasedate;
}

//---------------------------------------------------------------------------
// Series::ReleaseDate::set (internal)
//
// Sets the series release date

void Series::ReleaseDate::set(Nullable<DateTime> value)
{
	m_releasedate = value;
}

//---------------------------------------------------------------------------
// Series::SeriesID::get
//
// Gets the series unique identifier

Guid Series::SeriesID::get(void)
{
	return m_seriesid;
}

//---------------------------------------------------------------------------
// Series::SeriesID::set (internal)
//
// Sets the series unique identifier

void Series::SeriesID::set(Guid value)
{
	m_seriesid = value;
}

//---------------------------------------------------------------------------
// Series::ToString
//
// Overrides Object::ToString()
//
// Arguments:
//
//	NONE

String^ Series::ToString(void)
{
	return m_name;
}

//---------------------------------------------------------------------------

} // zuki::ronin::data

#pragma warning(pop)
