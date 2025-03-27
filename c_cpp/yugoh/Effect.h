#ifndef EFFECT_H
#define EFFECT_H

class Player;
class Board;

class Effect{

public:
Effect();
virtual void activate()=0;
virtual void display()=0;
virtual ~Effect()=0;
virtual bool requirestargetboard()=0;
virtual bool requirestargetplayer()=0;
virtual void settargetboard(Board* targetboard)=0;
virtual void settargetplayer(Player* targetplayer)=0;
};


#endif