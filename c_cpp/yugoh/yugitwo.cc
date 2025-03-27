#include <iostream>
#include <time.h>  
#include "card.h"
#include "Monster.h"
#include "Spell.h"
#include "EquipSpell.h"
#include "Board.h"
#include "Player.h"
#include "gameplay.h"
#include "Effects.h"


int main(){

    
   std::string name;

    std::cout<<"Name of first player"<<std::endl;
    std::cin>>name; 
    Player player1(name);

                                                      //ERROR SOMEWHERE IN DISPLAYBOARD V BOARD.CC NZ

    std::cout<<"Name of second player"<<std::endl;
    std::cin>>name; 
    Player player2(name);

    whogoesfirst(&player1,&player2);

    return 0;
}
