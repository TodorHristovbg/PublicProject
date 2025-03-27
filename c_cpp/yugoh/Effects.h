#ifndef EFFECTS_H
#define EFFECTS_H

#include "Effect.h"
#include "Monster.h"
#include "Player.h"
#include "Board.h"



class ChangeAttackEffect : public Effect{
    char Change;
    Monster* TMonster;

    Board* TBoard;
    bool needstargetboard = true;

    public:

   // ChangeAttackEffect(char change,Monster* target);

    ChangeAttackEffect(char change);
    void setchange(char change);       
    char getchange();
    void settarget(Monster* target);
    Monster* gettarget();
   
    bool requirestargetboard();
    void settargetboard(Board* targetboard);

    bool requirestargetplayer();


    void settargetplayer(Player* targetplayer);
    void activate();
    void display();
    ~ChangeAttackEffect();
};




#endif