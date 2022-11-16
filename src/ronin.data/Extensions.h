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

#ifndef __EXTENSIONS_H_
#define __EXTENSIONS_H_
#pragma once

#include "MonsterType.h"

#pragma warning(push, 4)

using namespace System;
using namespace System::Runtime::CompilerServices;

namespace zuki::ronin::data {

//---------------------------------------------------------------------------
// Class Extensions
//
// Implements extension methods
//---------------------------------------------------------------------------

[ExtensionAttribute]
public ref class Extensions abstract sealed
{
public:

	//-----------------------------------------------------------------------
	// Member Functions
	//-----------------------------------------------------------------------

	// ToString (MonsterType)
	//
	// Converts a MonsterType enumeration value into a string
	[ExtensionAttribute]
	static String^ ToString(MonsterType type);
};

//---------------------------------------------------------------------------

} // zuki::ronin::data

#pragma warning(pop)

#endif	// __EXTENSIONS_H_
