#ifndef GAMEPLAY_H
#define GAMEPLAY_H
#include "Player.h"


void generateMonster(Player *owner);
void generateEquip(Player *owner);
void drawcard(Player* player);
void fillhand(Player* player);
void whogoesfirst(Player* player1, Player* player2);
bool playacard();
void MainPhase1(Player* player);
void getyourgameon(Player* player1, Player* player2);

#endif
