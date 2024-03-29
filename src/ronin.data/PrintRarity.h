//---------------------------------------------------------------------------
// Copyright (c) 2004-2024 Michael G. Brehm
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

#ifndef __PRINTRARITY_H_
#define __PRINTRARITY_H_
#pragma once

#pragma warning(push, 4)

using namespace System;
using namespace System::ComponentModel;

namespace zuki::ronin::data {

//---------------------------------------------------------------------------
// Enum PrintRarity
//
// Describes the rarity of a Print
//---------------------------------------------------------------------------

public enum class PrintRarity
{
	[DescriptionAttribute("None")]
	None = 0,

	[DescriptionAttribute("Common")]
	Common,
	
	[DescriptionAttribute("Gold Rare")]
	GoldRare,

	[DescriptionAttribute("Parallel Rare")]
	ParallelRare,

	[DescriptionAttribute("Prismatic Secret Rare")]
	PrismaticSecretRare,

	[DescriptionAttribute("Rare")]
	Rare,

	[DescriptionAttribute("Secret Rare")]
	SecretRare,

	[DescriptionAttribute("Super Rare")]
	SuperRare,

	[DescriptionAttribute("Ultra Parallel Rare")]
	UltraParallelRare,

	[DescriptionAttribute("Ultra Rare")]
	UltraRare,
};

//---------------------------------------------------------------------------

} // zuki::ronin::data

#pragma warning(pop)

#endif	// __PRINTRARITY_H_
