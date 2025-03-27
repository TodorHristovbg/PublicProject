#ifndef BOARD_H
#define BOARD_H
#include "Monster.h"
#include "Spell.h"
#include "EquipSpell.h"

class Board final{
public:

Monster* monsterzone[6];
Spell* spellzone[6];

Board();
void destroymonster(int index);
void destroybackrow(int index);
bool hasmonsterat(int index);
Monster* getmonsterat(int index);
bool hasbackrow(int index);
Spell* getspellat(int index);
void triggerspell(int index);
void displaymonsterzone();
void displaybackrow();
void displayboard();
~Board();

};
#endif