#include <iostream>
#include "Player.h"
#include "EquipSpell.h"
#include "Board.h"


Player::Player(std::string name){

        setname(name);
        addlifepoints(8000);
        for(int i=0;i<6;i++){
            hand[i]=new Card();

        }

    }

void Player::addnormalsummon(int i){
        normalsummon+=i;
    }
void Player::subtractnormalsummon(int i){
        normalsummon-=i;
    }
int Player::getnormalsummon(){
        return normalsummon;
    }
void Player::setname(std::string name){
        Name=name;
        
    }  
    
std::string Player::getname(){
        return Name;
    }
void Player::addlifepoints(int change){
        lifepoints+=change;
    }
void Player::removelifepoints(int change){
        lifepoints-=change;
    }
int Player::getlifepoints(){
        return lifepoints;
    }
void Player::setownboard(Board* own){
         ownboard=own;
    }
Board* Player::getownboard(){
    return ownboard;
}
void Player::setenemyboard(Board* enemy){
        enemyboard=enemy;
    }
Board* Player::getenemyboard(){
    return enemyboard;
}

bool Player::ishandfull(){
        if(hand[5]->getname()=="DEFAULT"){
            return false;
        }
        return true;
    }
bool Player::ishandempty(){
        if(hand[0]->getname()=="DEFAULT"){
            return true;
        }
        return false;
    }
bool Player::ismonsterzoneempty(){
        int i=0;
        while(i<6){
            if(hasmonsterat(i)){
                return false;
            }
            i++;
        }
        return true;
    }

void Player::Draw(Card *card){
        int i=0;


        while (i<6)
        {   
            if(hand[i]->getname()=="DEFAULT"){

          

                hand[i]=card;
        
                  std::cout<<Name<<" has drawn ";
                  hand[i]->Display();
            
                return;
            }
            i++;
        }
        std::cout<<Name<<" has a full hand and cant draw."<<std::endl;
    
    }

bool Player::monsterzonefull(){
       
        int i=0;
        while (i<6)
        {
            if(!hasmonsterat(i)){
              
                return false;
            }
            i++;
        }
        std::cout<<"Monster zone is full??"<<std::endl;
        return true;
    }

bool Player::spellzonefull(){
         int i=0;
        while (i<6) 
        {
           
            if(hasbackrowat(i)==false){

               
                return false;
            }
            i++;
        }
        return true;
    }

bool Player::legalmove(int indexinhand){

      if(indexinhand<0 || indexinhand>5 || hand[indexinhand]->getname()=="DEFAULT")
        {
            std::cout<<"This is an illegal move"<<std::endl;
            return false;
        }
        

        if(dynamic_cast<Monster*>(hand[indexinhand])!= nullptr){
            if(getnormalsummon()<1){
                std::cout<<"You do not have any normal summons left"<<std::endl;
                return false;
            }
         
            if(monsterzonefull()){
                std::cout<<"Your monsterzone is full so you cant summon more monsters"<<std::endl;
                return false;
            }
        }


        if(dynamic_cast<Spell*>(hand[indexinhand])!= nullptr){
            if(spellzonefull()){
                std::cout<<"Your backrow is full and you cant play more spells/traps"<<std::endl;
            }
        }


    return true;

    }
void Player::Play(int indexinhand){



       if(dynamic_cast<Monster*>(hand[indexinhand])!= nullptr){ 
        playMonster(indexinhand); 
        }
       
       if(dynamic_cast<EquipSpell*>(hand[indexinhand])!= nullptr){ 
        
        dynamic_cast<EquipSpell*>(hand[indexinhand])->setboard(ownboard);
        
        playSpell(indexinhand); }

       normalizehand(indexinhand);
       displayhand();
  
    }


void Player::displaymonsterzone(){
    std::cout<<"Displaying "<<Name<<"'s monsterzone:"<<std::endl;
    ownboard->displaymonsterzone();
     
    }

void Player::displaybackrow(){
      std::cout<<"Displaying "<<Name<<"'s backrow:"<<std::endl;
      ownboard->displaybackrow();

      
    }


void Player::displayboard(){
  
    ownboard->displayboard();
   
   }

    
void Player::displayhand(){
        int i = 0;
        std::cout<<"Displaying "<<Name<<"'s hand: \n"<<std::endl;
    while (i<6)
    {
     //   if(hand[i]->getname()!="DEFAULT"){
            
            hand[i]->Display();
   //     }
        i++;
    }
    std::cout<<std::endl;
    }
    
bool Player::hasbackrowat(int index){
    ownboard->hasbackrow(index);
   }

Spell* Player::getspellat(int index){
   ownboard->getspellat(index);
   }


bool Player::hasmonsterat(int index){

        ownboard->hasmonsterat(index);
    }

Monster* Player::getmonsterat(int index){

        ownboard->getmonsterat(index);
    }

void Player::addBPattacks(){
        int i=0;
        while(i<6){
            if(hasmonsterat(i)){
                if(getmonsterat(i)->getposition()==2){
                getmonsterat(i)->addnumberofattacks(1);
                }
            }
            i++;
        }
    }

void Player::resetnumberofattacks(){
         int i=0;
        while(i<6){
            if(hasmonsterat(i)){
            getmonsterat(i)->resetattacks();
            }
                

            i++;
        }
    }

 void Player::playMonster(int indexinhand){

     

        if(dynamic_cast<Monster*>(hand[indexinhand])->getlevel()>4){
                        
                        if(dynamic_cast<Monster*>(hand[indexinhand])->getlevel()>7){
                            Tribute(2);
                        } else {
                            Tribute(1);
                        }
                    }

        int i=0;
         while(i<6){

        

           if(!hasmonsterat(i)){
          
                  delete ownboard->monsterzone[i];
                  ownboard->monsterzone[i]=dynamic_cast<Monster*>(hand[indexinhand]);
             

                int attackposition;
                std::cout<<"1 - attack position     2 - defense position"<<std::endl;
                std::cin>>attackposition;
                if(attackposition==1){
                    ownboard->monsterzone[i]->setpositiontoattack();
                } else{
                    ownboard->monsterzone[i]->setpositiontodef();
                    
                }

                std::cout<<Name<<" has summoned ";
            
                
                ownboard->monsterzone[i]->Display();
                subtractnormalsummon(1);

                hand[indexinhand]=new Card();
                return;
              
            }
            i++;
        }
        std::cout<<Name<<" cannot summon more monsters"<<std::endl;
    }


void Player::Tribute(int tributes){
        int i=0;
        int temp=tributes;
        while(i<6){
            if(ownboard->monsterzone[i]->getname()!="DEFAULT"){
               temp--;
            }
            i++;
        }
        if(temp>0){
            std::cout<<"Not enough tributes on field"<<std::endl;
            return;
        }else{
            for(int i=0;i<tributes;i++){
                bool valid=false;
                int index;
                while (!valid)
                {
               
                std::cout<<"Enter a valid index of monster to be tributed"<<std::endl;
                std::cin>>index;
                if(ownboard->monsterzone[index]->getname()!="DEFAULT"){valid=true;}
                }
                
                ownboard->destroymonster(index);
            }
        }
    }

void Player::playSpell(int indexinhand){

        int i =0;
        while (i<6)
        {
            if(!hasbackrowat(i)){
                delete ownboard->spellzone[i];
                ownboard->spellzone[i]=dynamic_cast<EquipSpell*>(hand[indexinhand]);
                
                std::cout<<Name<<" has activated ";
                ownboard->spellzone[i]->Display();
                
                if(dynamic_cast<EquipSpell*>(ownboard->spellzone[i])!=nullptr){

      
                    
                    hand[indexinhand]=new Card();
                    ownboard->triggerspell(i);
                    ownboard->spellzone[i]->Display();
                    return;
                }
                
                ownboard->spellzone[i]->effect();
                
                return;
            }
            i++;
        }
        

        std::cout<<Name<<" cannot play more spells"<<std::endl;
    }

void Player::destroymonster(int index){
        ownboard->destroymonster(index);
    }
void Player::destroybackrow(int index){
        ownboard->destroybackrow(index);
     
    }


void Player::normalizehand(int index){


        int i=index;
        while (i<5)
        {
              if(hand[i+1]->getname()!="Default"){
        //    std::cout<<array[i+1]->getname()<<"is name of next"<<std::endl;

                if(dynamic_cast<Monster*>(hand[i+1])!= nullptr){
                
                delete hand[i];
                hand[i]=hand[i+1];
                //new Monster();
              //  dynamic_cast<Monster*>(hand[i])->copyMonster(dynamic_cast<Monster*>(hand[i+1]));


                }

                if(dynamic_cast<EquipSpell*>(hand[i+1])!=nullptr){
                

                delete hand[i];
                hand[i]=hand[i+1];
   
            
                }


             //  delete hand[i+1];
               hand[i+1]=new Card();
               i++;
}

        }


}

Player::~Player(){
    for(int i =0;i<6;i++){
        delete hand[i];
    }
   // ownboard->~Board();
    delete ownboard;
    delete enemyboard;
}