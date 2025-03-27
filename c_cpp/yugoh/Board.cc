#include <iostream>
#include <string>
#include "Board.h"
#include "EquipSpell.h"

Board::Board(){
    for(int i = 0;i<6;i++){
        monsterzone[i]= new Monster();
        spellzone[i]=new Spell();
    }
}

void Board::destroymonster(int index){
    std::cout<<monsterzone[index]->getname()<<"Has been destroyed"<<std::endl;
    delete monsterzone[index];
    monsterzone[index]=new Monster();
    //

    //afasfsa
}

void Board::destroybackrow(int index){

    if(dynamic_cast<EquipSpell*>(spellzone[index])!=nullptr){
        std::cout<<"WOWZA AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"<<std::endl;
    }

    delete spellzone[index];
    spellzone[index]=new Spell();
}


bool Board::hasmonsterat(int index){
    if(index<0 || index>5){return false;}
    if(monsterzone[index]->getname()=="DEFAULT"){
        return false;
    }
    return true;
}

Monster* Board::getmonsterat(int index){
    if(hasmonsterat(index)){
        return monsterzone[index];
    }
    return nullptr;
}

bool Board::hasbackrow(int index){

    if(spellzone[index]==nullptr){
        std::cout<<"NULL POINTER DETECTED?"<<std::endl;
    }
    if(spellzone[index]->getname()=="DEFAULT"){
        return false;
    }
    return true;

}

Spell* Board::getspellat(int index){
    if(hasbackrow(index)){
        return spellzone[index];
    }
}



void Board::triggerspell(int index){

    if(dynamic_cast<EquipSpell*>(spellzone[index])!=nullptr){
        int index;
        std::cout<<"Enter index of monster to be equipped"<<std::endl;
        std::cin>>index;
        while (!hasmonsterat(index))
        {
             std::cout<<"Please enter a valid index of monster to be equipped"<<std::endl;
             std::cin>>index;
        }
        
        dynamic_cast<EquipSpell*>(spellzone[index])->effect(getmonsterat(index));

    }


}


void Board::displaymonsterzone(){
   
       int i=0;
    while (i<6)
    {

        getmonsterat(i)->Display();
 
        i++;
    }
    std::cout<<std::endl;
}

void Board::displaybackrow(){
     
    int i=0;
    while (i<6)
    {
    getspellat(i)->Display();
 
        i++;
    }
    std::cout<<std::endl;
}

void Board::displayboard(){
    displaymonsterzone();
    displaybackrow();
}


Board::~Board(){
    std::cout<<"BOARD DESTRUCTOR CALLED"<<std::endl;
    for(int i=0;i<6;i++){
        delete monsterzone[i];
        delete spellzone[i];
    }
}

