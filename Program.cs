// Final Project - Connect 4
// Team Bangtan Sonyeodan
// Coded by Corazon Marie Palencia
// Course: SODV1202:Introduction to Object Oriented Programming-24JANMNTR1 

using System;
using System.Runtime.CompilerServices;

//Class 2: ProjectConnectFour
// Connect four game flow

public class ProjectConnectFour {
    private Board gameBoard; // to hold my gameboard
    private Player playerSwitch; //to swicth currentr player
    private Player player1; 
    private Player player2;

//to set 2 players
public class ProjectConnectFive (Player player1, Player player2 ){

    this.player1 = player1;
    this.player2 = player2;
    this.playerSwitch = player1;
    this.gameBoard = new Board();
    
}
}
//Class 1: MaIN Game
class MainGame
{ //Changed name Program to MainGame

    static void Main(string[] args)
    {

        Player player1 = new Player('X');
        Player player2 = new player1('0');
        ProjectConnectFour game = new ProjectConnectFour(player1, player2);

        game.letsStart()

    }
}