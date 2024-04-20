// Final Project - Connect 4
// Team Bangtan Sonyeodan
// Coded by Corazon Marie Palencia
// Course: SODV1202:Introduction to Object Oriented Programming-24JANMNTR1 

// Code logic, color and other references: 
//https://www.ifnamemain.com/posts/2014/Oct/09/csharp_connect4/
//https://www.geeksforgeeks.org/console-clear-method-in-c-sharp/
//https://www.nuget.org/packages/Pastel#readme-body-tab


using System;
using System.Runtime.Serialization.Formatters;
using System.Drawing;
using Console = Colorful.Console;
using Pastel;

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
{//
    public void DisplayWelcome()
    {
                                                               //title with backgroung
        Console.WriteLine("Welcome to Cor's Connect Four Game!".Pastel(Color.Black).PastelBg(Color.FromArgb(123, 153, 253))); 
    }


    public void DisplayPlayerTurn(char symbol)
    {
        Console.Write("It's ");
        Console.Write($"{symbol}'s".Pastel(Color.FromArgb(123, 153, 253)));
        Console.WriteLine(" turn");
    }

    public void DisplayBoard(Board gameBoard, Player playerSwitch)
    {
        Console.Clear();
        DisplayWelcome();
        DisplayPlayerTurn(playerSwitch.Symbol);
        gameBoard.GameBoard();

    }

    public void PromptForColumn()
    {
        Console.Write("Enter column number (1-7): ");
    }

    public void DisplayWinner (char symbol)
    {
        Console.WriteLine($"Player {symbol.ToString().Pastel(Color.FromArgb(93, 222, 82))} wins! ");
    }


    public int PromptForRestart()
    {
        int choice;
        while (true)
        {
            Console.WriteLine("Press 1 for a new game, press 2 to exit.".Pastel(Color.FromArgb(247, 144, 144)));
            if (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
            {
                Console.WriteLine("Invalid choice. Please enter 1 or 2 only.".Pastel(Color.FromArgb(247, 144, 144)));
            }
            else
            {
                return choice;
            }
            
            }
        }
    }

// Class: ProjectConnetFour
// Description: The game flow

        public class ProjectConnectFour
        {
    private Board gameBoard; //game board holder
    private Player playerSwitch; //track current player
    private Player player1; //storing player 1
    private Player player2; //storing player 2
    private MyBoard display; // displaying interface
        

    public ProjectConnectFour(Player player1, Player player2, MyBoard display)
    {
        this.player1 = player1;
        this.player2 = player2;
        this.playerSwitch = player1; // start player
        this.gameBoard = new Board();
        this.display = display;
    }

    public void letsStart()
    {
        display.DisplayWelcome();
        gameBoard.resetBoard();

        while (true)
        {
            while (!gameBoard.GameOverLoop())
            {
                display.DisplayBoard(gameBoard, playerSwitch); // display gameboard and player's turn
                int column = checkColumn();

                // Place the player's piece in the selected column
                gameBoard.SelectColumns(column, playerSwitch.Symbol);

                // Check if the board is full
                if (gameBoard.BoardFull())
                {
                    Console.WriteLine("Board is full. It's a draw!".Pastel(Color.FromArgb(247, 144, 144)));
                    int choice = display.PromptForRestart();
                    if (choice == 1)
                    {
                        gameBoard.resetBoard();
                        break;
                    }
                    else if (choice == 2)
                    {
                        return;
                    }
                }

                // Check for wins
                if (gameBoard.whoWins())
                {
                    display.DisplayWinner(playerSwitch.Symbol);
                    int choice = display.PromptForRestart();
                    if (choice == 1)
                    {
                        gameBoard.resetBoard();
                        break;
                    }
                    else if (choice == 2)
                    {
                        return;
                    }
                }

                playerSwitch = (playerSwitch == player1) ? player2 : player1;
            }
        }
    }

    //get column number from current player
    private int checkColumn()
    {
        int column;
        while (true)
        {
            display.PromptForColumn(); //display input in concole
            if (!int.TryParse(Console.ReadLine(), out column) || column < 1 || column > 7 )
            {
                Console.WriteLine("Invalid column number. Please enter a number between 1-7.".Pastel(Color.FromArgb(247, 144, 144)));
            }

            else if (gameBoard.ColumnFull(column))
            {
                Console.WriteLine("Column is full. Please choose another column.".Pastel(Color.FromArgb(247, 144, 144)));
            }

            else
            {
                return column;
            }
        }
    }


}



// Class: Player
// Description: players and symbols

public class Player
{
    public char Symbol { get; } // for X and O
    public Player (char symbol)
    {
        Symbol = symbol;
    }
}


// Class: Board
// Description: My gameboard <3

public class Board
{
    private char[,] grid; //array for my board

    public Board()
    {
        grid = new char[6, 7]; // 6 row, 7 col

    }

    //method for empty cells

    public void resetBoard()
    {
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                grid[row, col] = ' ';
            }
        }
    }
    //method for current board
    public void GameBoard()
    {
        for (int row = 0; row < 6; row++)
        {
            Console.Write("| ");
            for (int col = 0; col < 7; col++)
            {
                char symbol = grid[row, col];
                Color color = (symbol == 'X') ? Color.FromArgb(119, 205, 249) : Color.FromArgb(233, 249, 119); // Blue for X, Red for O
                Console.Write(symbol.ToString().Pastel(color) + " | ".Pastel(Color.FromArgb(255, 255, 255)));
            }
            Console.WriteLine();
        }
        Console.WriteLine("+---+---+---+---+---+---+---+");
        Console.WriteLine("  1   2   3   4   5   6   7  ".Pastel(Color.FromArgb(123, 153, 253)));
}


    // method to show symbols in a specific col
    public void SelectColumns(int column, char symbol)
    {
        for (int row = 5; row >= 0; row--)
        {
            if (grid[row, column - 1] == ' ')
            {
                grid[row, column - 1] = symbol;
                break;
            }
        }
    }


    public bool BoardFull()
    {
        for (int col = 0; col < 7; col++)
        {
            if (grid[0, col] == ' ')
            {
                return false; // If any cell in the top row of a column is empty, the board is not full
            }
        }
        return true; // All cells in the top row of every column are occupied, so the board is full
    }

    public bool ColumnFull(int column)
    {
        return grid[0, column - 1] != ' '; 
    }

    //method for game over
    public bool GameOverLoop()
    {
        return false;
    }

    // How to win: Horizontal, vertical, diagonal (/), diagonal (\)
    public bool whoWins()
    {
        //Horizontal
        for(int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                if (grid[row, col] != ' ' && grid[row, col] == grid[row, col + 1] && grid[row, col] == grid[row, col + 2] && grid[row, col] == grid[row, col + 3])
                {
                    return true;
                }
            }
        }

        // Vertical
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                if (grid[row, col] != ' ' && grid[row, col] == grid[row + 1, col] && grid[row, col] == grid[row + 2, col] && grid[row, col] == grid[row + 3, col])
                {
                    return true;
                }
            }
        }

        // Diagonal right
        for (int row = 3; row < 6; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                if (grid[row, col] != ' ' && grid[row, col] == grid[row - 1, col + 1] && grid[row, col] == grid[row - 2, col + 2] && grid[row, col] == grid[row - 3, col + 3])
                {
                    return true;
                }
            }
        }


        // Diagonal Left
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                if (grid[row, col] != ' ' && grid[row, col] == grid[row + 1, col + 1] && grid[row, col] == grid[row + 2, col + 2] && grid[row, col] == grid[row + 3, col + 3])
                {
                    return true; // Diagonal win (descending)
                }
            }
        }
        return false;

    }
}

// Class: Main Game
// Description: Main class 
class Program
{
    static void Main(string[] args)
    {

        Player player1 = new Player('X'); //player 1 symbol
        Player player2 = new Player('O');
        MyBoard display = new ConsoleDisplay();
        ProjectConnectFour game = new ProjectConnectFour(player1, player2, display);
        game.letsStart();
    }
}