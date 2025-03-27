#ifndef PLAYER_H
#define PLAYER_H

#include <iostream>
#include "card.h"
#include "Spell.h"
#include "Monster.h"

class Player final{

 std::string Name;
    int lifepoints=0;
    Card *hand[6];
    Board*  ownboard;
    Board*  enemyboard;
    
    int normalsummon=0;

public:
Player(std::string name);
void addnormalsummon(int i);
void subtractnormalsummon(int i);
int getnormalsummon();
void setname(std::string name);
std::string getname();
void addlifepoints(int change);
void removelifepoints(int change);
int getlifepoints();
void setownboard(Board* own);
Board* getownboard();
void setenemyboard(Board* enemy);
Board* getenemyboard();
bool ishandfull();
bool ishandempty();
bool ismonsterzoneempty();
void Draw(Card *card);
bool monsterzonefull();
bool spellzonefull();
bool legalmove(int indexinhand);
void Play(int indexinhand);
void displaymonsterzone();
void displaybackrow();
void displayboard();
void displayhand();
bool hasbackrowat(int index);
Spell* getspellat(int index);
bool hasmonsterat(int index);
Monster* getmonsterat(int index);
void addBPattacks();
void resetnumberofattacks();
void playMonster(int indexinhand);
void Tribute(int tributes);
void playSpell(int indexinhand);
void destroymonster(int index);
void destroybackrow(int index);
void normalizehand(int index);
~Player();
};

#endif