#ifndef MONSTER_H
#define MONSTER_H

#include <iostream>
#include <string>
#include "card.h"


class Player; // Forward declaration
class Board;

class Monster: public Card {

int Attack;
int Def;
int Level;
int attackposition=0;
int attacks=0;

public:
Monster(std::string name,int attack,int def, int level, Player* owner);
Monster();
void empty();
void Display();
void addnumberofattacks(int i);
void resetattacks();
int getnumberofattacks();
void setattack(int attack);
int getattack();
void changeattack(int change);
void setdef(int def);
int getdef();
void setlevel(int level);
int getlevel();
void setpositiontoattack();
void setpositiontodef();
void copyposition(int position);
int getposition();
void copyMonster(Monster* temp);
 ~Monster();
};

#endif // MONSTER_H