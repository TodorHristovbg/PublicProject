#include <iostream>
#include <time.h>  
#include "card.h"
#include "Monster.h"


   Monster::Monster(std::string name,int attack,int def, int level, Player* owner){
        
        setname(name);
        setattack(attack);
        setdef(def);
        setlevel(level);
        setowner(owner);
        
    }
    Monster:: Monster(){
        setname("DEFAULT");
        setattack(0);
        setdef(0);
        setlevel(0);
        
    }

    void  Monster::empty(){
        setname("DEFAULT");
        setattack(0);
        setdef(0);
        setlevel(0);
    }

    void  Monster::Display(){
    //    std::cout<<"THIS IS CALLED"<<std::endl;
        std::string position;
        if(attackposition==2){ position= " in attack";}
        else if(attackposition==1){ position=" in defense";}
        else {
            position="";
        }
        


        std::cout<<getname()<<": Atk - "<<getattack()<<" Def - "<<getdef()<<" Level: "<<getlevel()<<position<<std::endl;
    }

 void Monster:: addnumberofattacks(int i){
        attacks+=i;
    }
  void Monster::resetattacks(){
        attacks=0;
    }
   int  Monster:: getnumberofattacks(){
        return attacks;
    }

   void  Monster:: setattack(int attack){
        Attack=attack;
    }
  int    Monster::getattack(){
        return Attack;
    }
   void  Monster:: changeattack(int change){
        Attack+=change;
    }

  void   Monster:: setdef(int def){
        Def=def;
    }
   int Monster::  getdef(){
        return Def;
    }

   void Monster::  setlevel(int level){
        Level=level;
    }
   int Monster::  getlevel(){
        return Level;
    }  
    
    void  Monster:: setpositiontoattack(){
        attackposition=2;
    }
   void Monster::  setpositiontodef(){
        attackposition=1;
    }
    void Monster::  copyposition(int position){
        attackposition=position;
    }
    int Monster::  getposition(){
        return attackposition;
    }


  void  Monster:: copyMonster(Monster* temp){
        setname(temp->getname());
        setattack(temp->getattack());
        setdef(temp->getdef());
        setlevel(temp->getlevel());
        setowner(temp->getowner());
        copyposition(temp->getposition());
    }

  Monster:: ~Monster(){

     //   free(Attack);
    }
