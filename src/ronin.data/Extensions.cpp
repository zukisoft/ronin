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
#include "Extensions.h"

#pragma warning(push, 4)

namespace zuki::ronin::data {

//---------------------------------------------------------------------------
// Extensions Static Constructor (private)
//
// Arguments:
//
//	NONE

static Extensions::Extensions()
{
	// Use Reflection to get at SerializationInfo::GetValueNoThrow()
	s_getvaluenothrow = SerializationInfo::typeid->GetMethod("GetValueNoThrow",
		BindingFlags::Instance | BindingFlags::NonPublic);
}

//---------------------------------------------------------------------------
// Extensions::GetValueNoThrow (static)
//
// Extends SerializationInfo to include a GetValueNoThrow method
//
// Arguments:
//
//	info		- SerializationInfo instance
//	name		- Name of the property to retrieve
//	type		- Type of the property to retrieve

Object^ Extensions::GetValueNoThrow(SerializationInfo^ info, String^ name, Type^ type)
{
	if(CLRISNULL(info)) throw gcnew ArgumentNullException("info");
	if(CLRISNULL(s_getvaluenothrow)) throw gcnew Exception("Reflection failed for SerializationInfo::GetValueNoThrow");

	return s_getvaluenothrow->Invoke(info, gcnew array<Object^>{ name, type });
}

//---------------------------------------------------------------------------

} // zuki::ronin::data

#pragma warning(pop)
