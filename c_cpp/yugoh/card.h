#ifndef CARD_H
#define CARD_H

#include <iostream>
#include <string>

class Player; 
class Board;

class Card {
      
   
protected:
    std::string Name;
    Player* Owner;
    void* ownboard; 

public:
   
    Card(std::string name, Player* owner);
    Card();

    
    ~Card();

    void empty();

    void setname(const std::string name);
    std::string getname() const;

    void setowner(Player* owner);
    Player* getowner() const;


    virtual void Play();
    virtual void Display();
  
};

#endif // CARD_H