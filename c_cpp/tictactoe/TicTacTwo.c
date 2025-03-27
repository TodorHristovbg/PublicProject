#include <stdio.h>
#include <string.h>
#include <stdbool.h>

void playerturn(unsigned char *board);
bool gamestate(unsigned char *board,unsigned char lastmove);
void printBoard(unsigned char board[9]);


int main (){
    unsigned char i=0;
    unsigned char board[9];
    memset(board,0,sizeof(board));
    playerturn(&board[0]);


    return 0;
}

void playerturn(unsigned char *board){
    bool gameover=false;
    bool turn = false;
    unsigned char index=10;
    unsigned char counter = 0;
    unsigned char lastmove = 0;
    while (gamestate(&board[0],lastmove)==false)
    {
        printBoard(&board[0]);
        while (index>=10)
        {
        printf("Enter index of your move ");
        scanf("%d",&index);
        index--;

    
        if(board[index]!=0){
        
            index=10;
        } else {
              
            board[index]=turn+1;
        }
        }
        
      //  gamestate(&board[0],index);
        turn=!turn;
        counter++;
        lastmove = index;
        index = 10;
        if(counter==9){
            printf("game is a draw");
            break;
        }


    }
    
 printBoard(&board[0]);


}

bool gamestate(unsigned char *board,unsigned char lastmove){


     

    switch ((lastmove)%3)
    {
    case 0: if((board[0]==board[3] && board[0]==board[6]) || (board[lastmove]==board[lastmove+1] && board[lastmove]==board[lastmove+2]))
     { if(board[0]!=0) { printf("Player %d wins:\n",board[0]);  return true;}} break;

    case 1: if((board[1]==board[4] && board[1]==board[7]) || (board[lastmove]==board[lastmove-1] && board[lastmove]==board[lastmove+1]))
     { if(board[1]!=0) { printf("Player %d wins:\n",board[1]); return true;}} break;

    case 2: if((board[2]==board[5] && board[2]==board[8]) || (board[lastmove]==board[lastmove-1] && board[lastmove]==board[lastmove-2]))
     { if(board[3]!=0) { printf("Player %d wins:\n",board[2]); return true;}} break;
      
    default:
        break;
    }
    
    if((board[0]==board[4] && board[0]==board[8]) || (board[2]==board[4] && board[2]==board[6])){
    if(board[4]!=0){
    printf("Player %d wins:\n",board[4]);
    return true;
    }
    }

    return false;
}


void printBoard(unsigned char board[9]){

    char line[22] = "";
    
     unsigned char i = 1;

       for(i=1; i<=9 ;i++){
            switch (board[i-1])
            {
            case 1: 
             strcat(line,"X");
                break;
            case 2: 
             strcat(line,"O");
             break;

            default:

                strcat(line," ");
                break;
            }
           
           strcat(line,"|");
        
         if(i%3==0){strcat(line, "\n");}
       }
       printf("%s",line);

} 
