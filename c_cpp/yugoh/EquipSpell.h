#ifndef EQUIP_H
#define EQUIP_H
#include <iostream>
#include <string>
#include <vector>
#include "Spell.h"
#include "Monster.h"
#include "Effects.h"
#include "Player.h"


class EquipSpell final: public Spell{

int Change;
Monster* Target;
bool Effectsenemy;  //0 - effects ally, 1 - effects enemy

std::vector<Effect*> eList;


public:
EquipSpell(std::string Name,int change, bool enemy);
EquipSpell();
void setboard(Board* own);
void setchange(int change);
int getchange();
void settarget(bool enemy);
bool gettarget();
void effect(Monster* target);
void Display();
bool validtargetexists();
void TESTEFFECT();
void TESTADDEFFECT(Effect* addnew);
~EquipSpell();

};

#endif 