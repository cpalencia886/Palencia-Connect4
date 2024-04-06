// Final Project - Connect 4
// Team Bangtan Sonyeodan
// Coded by Corazon Marie Palencia
// Course: SODV1202:Introduction to Object Oriented Programming-24JANMNTR1 

using System;

// Interface: MyBoard 
// To define methods for game info display

public interface MyBoard
{
    void DisplayWelcome ();
    void DisplayPlayerTurn(char symbol);
    void DisplayBoard(Board gameBoard, Player playerSwitch);
    void PromptForColumn();
    void DisplayWinner(char symbol);
    int PromptForRestart();

}

// Class: ConsoleDisplay
// Description: Console display of MyBoard interface

public class ConsoleDisplay : MyBoard
{
    public void DisplayWelcome()
    {
        Console.WriteLine("Welcome to Cor's Connect Four Game!");
    }


    public void DisplayPlayerTurn(char symbol)
    {
        Console.WriteLine($"It's {symbol}'s turn");
    }

    public void DisplayBoard(Board gameBoard, Player playerSwitch)
    {
        Console.Clear();
        DisplayWelcome();
        DisplayPlayerTurn(playerSwitch.Symbol);
        gameBoard.GameBoard();

    }
}