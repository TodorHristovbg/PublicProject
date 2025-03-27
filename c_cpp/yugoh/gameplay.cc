#include <iostream>
#include "gameplay.h"
#include "EquipSpell.h"
#include "Board.h"
#include <time.h>  


void randomizeseed(){
    srand(rand()%31+time(NULL));
}
void generateMonster(Player *owner){

    int Atk = (rand()%20)*100;
    int Def = (rand()%20)*100;

    Monster* monster = new  Monster("Mysterious monster", Atk,Def, 4,owner);
    owner->Draw(monster);

}

    void generateEquip(Player *owner){

    int change = (rand()%6)*100+300;
    EquipSpell* equip = new EquipSpell("Empowering chant",change,0);
    owner->Draw(equip);

 }

    void drawcard(Player* player){
randomizeseed();
int type = rand()%3;
if(type==0){
    generateEquip(player);
} else {
    generateMonster(player);
}



}

void fillhand(Player* player){

  while (!player->ishandfull())
  {
    drawcard(player);
  }
  

};


void whogoesfirst(Player* player1, Player* player2){
bool turn = rand()%2;   //0 - player1 stays player1

std::cout<<"\n"<<std::endl;
if(turn){
    std::cout<<player2->getname()<<" will go first"<<std::endl;
    getyourgameon(player2,player1);
 

} else{
    std::cout<<player1->getname()<<" will go first"<<std::endl;
    getyourgameon(player1,player2);
}



}


bool playacard(){

 int play;
 std::cout<<"Play a card? 1 - yes    2 - no"<<std::endl;
 std::cin>>play;
    if(play==1){
        return true;
    } else {
        
        return false;
    }


}


void MainPhase1(Player* player){

    if(player->ishandempty()){
        std::cout<<player->getname()<<" has an empty hand"<<std::endl;
    }
   std::cout<<player->getlifepoints()<<std::endl;
    player->displayhand();
    player->displayboard();


    int index;
    player->addnormalsummon(1);
    while (playacard())
    {
        int index;
        std::cout<<"Which card do you want to play?"<<std::endl;
        std::cin>>index;
        index--;
       
          while (!(player->legalmove(index)))
         {
          std::cout<<"try a different move or return by entering 0"<<std::endl;
          std::cin>>index;
          index--;
          if(index==-1){
            break;
          }
         }
         if(index!=-1) {player->Play(index);}
    }
    
    std::cout<<player->getname()<<"'s MainPhaseOne has ended\n"<<std::endl;
    player->displayhand();
    player->displayboard();

}

bool declareattack(){
    int play;
 std::cout<<"Declare an attack? 1 - yes    2 - no"<<std::endl;
 std::cin>>play;
    if(play==1){
        return true;
    } else {
        
        return false;
    }

}


void BattlePhase(Player* attacker, Player* defender){

    attacker->addBPattacks();
    
    while (declareattack())
    {
        int attackindex;
        int defindex;
        bool valid = false;
        
        while (!valid)
        {   
               attacker->displayboard();

              std::cout<<"Which monster are you attacking with?"<<std::endl;
              std::cin>>attackindex;
              attackindex--;
              if(attackindex==-1){
                break;
              }
              if(attacker->hasmonsterat(attackindex)){
                if(attacker->getmonsterat(attackindex)->getnumberofattacks()>0){
                    valid=!valid;
                } else{
                    std::cout<<"This mosnter doesnt have any attacks left"<<std::endl;
                }
              } else {
                std::cout<<"Please enter a valid monster index or 0 to quit"<<std::endl;
              }

        }
            if(attackindex==-1){continue;}


        if(defender->ismonsterzoneempty()){
            std::cout<<attacker->getmonsterat(attackindex)->getname()<<" attacks directly for "<<attacker->getmonsterat(attackindex)->getattack()<<" damage"<<std::endl;
            defender->removelifepoints(attacker->getmonsterat(attackindex)->getattack());
            std::cout<<defender->getname()<<" has "<<defender->getlifepoints()<<" lifepoints left"<<std::endl;
            attacker->getmonsterat(attackindex)->resetattacks();
        } else {

        while (valid)
        {
            defender->displaymonsterzone();

            std::cout<<"Which monster do you want to attack?"<<std::endl;
              std::cin>>defindex;
              defindex--;
            if(defender->hasmonsterat(defindex)){
                valid=!valid;
            } else {
                 std::cout<<"Please enter a valid monster index"<<std::endl;
            }

        }
        

            int importantstat;
            bool takedamage;
            if(defender->getmonsterat(defindex)->getposition()==2){
                importantstat=defender->getmonsterat(defindex)->getattack();
                takedamage=true;
            } else {
                 importantstat=defender->getmonsterat(defindex)->getdef();
                 takedamage=false;
            }
        if(attacker->getmonsterat(attackindex)->getattack()>importantstat){

            if(takedamage){
                defender->removelifepoints(attacker->getmonsterat(attackindex)->getattack()-importantstat);
            }
            defender->destroymonster(defindex);
        } else {
            attacker->removelifepoints(importantstat-attacker->getmonsterat(attackindex)->getattack());
            if(attacker->getmonsterat(attackindex)->getattack()==importantstat){
            defender->destroymonster(defindex);
            }
            attacker->destroymonster(attackindex);
            
        }

         attacker->getmonsterat(attackindex)->resetattacks();
    }
    }
    std::cout<<attacker->getname()<<"'s battlephase has ended"<<std::endl;
    attacker->resetnumberofattacks();



}



void getyourgameon(Player* player1, Player* player2){


std::cout<<player1->getname()<<" versus " <<player2->getname()<<std::endl;
std::cout<<std::endl;

fillhand(player1);
//player1->displayhand();
Board p1board;
player1->setownboard(&p1board);

std::cout<<"\n"<<std::endl;
fillhand(player2);
//player2->displayhand();
Board p2board;
player2->setownboard(&p2board);

int turn=1;

while (player1->getlifepoints()>0 && player2->getlifepoints()>0)
{

    Player* attacker;
    Player* defender;


    if(turn%2==1){
        std::cout<<player1->getname()<<"'s turn."<<std::endl;
       attacker=player1;
       defender=player2;
    } else {
        std::cout<<player2->getname()<<"'s turn."<<std::endl;
        attacker=player2;
        defender=player1;
    }

    MainPhase1(attacker);
    BattlePhase(attacker,defender);
    turn++;

}

if(player1->getlifepoints()<=0){
    std::cout<<player2->getname()<<" WINS"<<std::endl;
    delete player1;
}
if(player2->getlifepoints()<=0){
    std::cout<<player1->getname()<<" WINS"<<std::endl;
    delete player2;
}

}
