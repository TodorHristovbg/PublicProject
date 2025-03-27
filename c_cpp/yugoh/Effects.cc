#include <iostream>
#include <string>
#include "Effects.h"
#include "card.h"
#include "Board.h"

/*
ChangeAttackEffect::ChangeAttackEffect(char change, Monster* target){
    setchange(change);
    settarget(target);
}
*/
Effect::Effect(){
    std::cout<<"bobec"<<std::endl;
}
Effect::~Effect(){
    std::cout<<"EFFECT DESTRUCTOR CALLED?"<<std::endl;
}
ChangeAttackEffect::ChangeAttackEffect(char change){
    setchange(change*100);
}
void ChangeAttackEffect::setchange(char change){
    Change=change;
}
char ChangeAttackEffect::getchange(){
    return Change;
}

void ChangeAttackEffect::settarget(Monster* target){
    TMonster=target;
}
Monster* ChangeAttackEffect::gettarget(){
    return TMonster;
}
bool ChangeAttackEffect::requirestargetboard(){
    return true;
}
bool ChangeAttackEffect::requirestargetplayer(){
    return false;
}

void ChangeAttackEffect::settargetboard(Board* targetboard){
    TBoard=targetboard;
}
void ChangeAttackEffect::activate(){
    std::cout<<"CHANGEFFECT ACTIVATE CALLED"<<std::endl;
    int index;
    TBoard->displayboard();
    std::cout<<"Enter index of target"<<std::endl;
    std::cin>>index;
    index--;
    while (!TBoard->hasmonsterat(index))
    {
        std::cout<<"Please enter a valid index"<<std::endl;
        TBoard->displaymonsterzone();
        // TBoard->displayboard();
        std::cin>>index;
        index--;

    }

    settarget(TBoard->getmonsterat(index));
    TMonster->changeattack(Change);
}

void ChangeAttackEffect::display(){
    std::cout<< "modifies attack by "<<Change<<std::endl;
}

void ChangeAttackEffect::settargetplayer(Player* targetplayer){
    std::cout<<"WHY IS THIS GETTING CALLED?"<<std::endl;
}

ChangeAttackEffect::~ChangeAttackEffect(){

    TMonster->changeattack(-Change);
    
}
