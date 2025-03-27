#include <iostream>
#include "Spell.h"

Spell::Spell(std::string Name){
    setname(Name);
}
Spell::Spell(){
    setname("DEFAULT");
}
void Spell::copyspell(Spell* target){
    setname(target->getname());
    setowner(target->getowner());
}
void Spell::Display(){
    std::cout<<getname()<<std::endl;
}
void Spell::effect(){
    std::cout<<"Standard efffect called"<<std::endl;
}
Spell::~Spell(){

}