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
// Extensions::ToString (MonsterType)
//
// Converts a MonsterType enumeration value into a String^
//
// Arguments:
//
//	type	- MonsterType to be converted

String^ Extensions::ToString(MonsterType type)
{
	switch(type) {

		case(MonsterType::Aqua): return "Aqua";
		case(MonsterType::Beast): return "Beast";
		case(MonsterType::BeastWarrior): return "Beast-Warrior";
		case(MonsterType::Dinosaur): return "Dinosaur";
		case(MonsterType::Dragon): return "Dragon";
		case(MonsterType::Fairy): return "Fairy";
		case(MonsterType::Fiend): return "Fiend";
		case(MonsterType::Fish): return "Fish";
		case(MonsterType::Insect): return "Insect";
		case(MonsterType::Machine): return "Machine";
		case(MonsterType::Plant): return "Plant";
		case(MonsterType::Pyro): return "Pyro";
		case(MonsterType::Reptile): return "Reptile";
		case(MonsterType::Rock): return "Rock";
		case(MonsterType::SeaSerpent): return "Sea Serpent";
		case(MonsterType::Spellcaster): return "Spellcaster";
		case(MonsterType::Thunder): return "Thunder";
		case(MonsterType::Warrior): return "Warrior";
		case(MonsterType::WingedBeast): return "Winged Beast";
		case(MonsterType::Zombie): return "Zombie";
	}

	return String::Empty;

}

//---------------------------------------------------------------------------

} // zuki::ronin::data

#pragma warning(pop)
