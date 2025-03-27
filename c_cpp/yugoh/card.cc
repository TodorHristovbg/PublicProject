#include "card.h"

Card::Card(std::string name, Player* owner) : Name(name), Owner(owner) {
    // Initialization list used for setting Name and Owner
}

Card::Card() : Name("DEFAULT"), Owner(nullptr), ownboard(nullptr) {
    // Initialization list used for setting default values
}

Card::~Card() {
    // Destructor implementation if needed
}

void Card::setname(const std::string name) {
    Name = name;
}

std::string Card::getname() const {
    return Name;
}

void Card::setowner(Player* owner) {
    Owner = owner;
}

Player* Card::getowner() const {
    return Owner;
}

void Card::Play() {
    // Implementation
}

void Card::Display() {
    std::cout << "Empty Card" << std::endl;
}