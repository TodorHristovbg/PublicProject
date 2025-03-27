#ifndef SPELL_H
#define SPELL_H


#include <iostream>
#include "card.h"

class Spell : public Card{


public:

Spell(std::string Name);
Spell();

virtual void copyspell(Spell* target);

void Display();
virtual void effect();
virtual ~Spell();   

};

#endif //SPELL_H
