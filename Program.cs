// Final Project - Connect 4
// Team Bangtan Sonyeodan
// Coded by Corazon Marie Palencia
// Course: SODV1202:Introduction to Object Oriented Programming-24JANMNTR1 

using System;
using System.Runtime.Serialization.Formatters;

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

    public void PromptForColumn()
    {
        Console.WriteLine("Enter column number (1-7): ");
    }

    public void DisplayWinner (char symbol)
    {
        Console.WriteLine($"Player {symbol} wins! ");
    }


    public int PromptForRestart()
    {
        int choice;
        while (true)
        {
            Console.WriteLine("Press 1 for a new game, press 2 to exit.");
            if (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
            {
                Console.WriteLine("Invalid choice. Please enter 1 or 2 only.");
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

                //display X and O in the board
                gameBoard.SelectColumns(column, playerSwitch.Symbol);

                //check for wins
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
                Console.WriteLine("Invalid column number. Please enter a number between 1-7.");
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
                Console.Write(grid[row, col] + " | ");
            }
            Console.WriteLine("+---+---+---+---+---+---+---+");
            Console.WriteLine("  1   2   3   4   5   6   7  ");
        }

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

    //method for game over
    public bool GameOverLoop()
    {
        return false;
    }

    // How to win: Horizontal, vertical, diagonal (/), diagonal (\)
    public bool whoWins()
    {
        //Horizontal
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                if (grid[row, col] != ' ' && grid[row, col] == grid[row, col + 1] && grid[row, col] == grid[row, col + 2] && grid[row, col] == grid[row, col + 3])
                {
                    return true;
                }
            }
        }


        //Vertical
        for (int row = 0; row < 3; row++)
        {
            for(int col = 0; col < 7; col++)
            {
                if (grid[row, col] != ' ' && grid[row, col] == grid[row + 1, col] && grid[row, col] == grid[row + 2, col] && grid[row, col] == grid[row + 3, col])
                {
                    return true;
                }
            }
        }

        //Diagonal left
        for (int row = 3; row <6; row++)
        {
            for (int col = 0; col <4 ; col++)
            {
                if (grid[row, col] != ' ' && grid[row, col] == grid[row - 1, col + 1] && grid[row, col] == grid[row - 2, col + 2] && grid[row, col] == grid[row - 3, col + 3])
                {
                    return true;
                }
            }
        }
        //Diagonal right
        for(int row = 0; row < 6; row++)
        {
            for (int col = 0; col <4; col++)
            {
                if (grid[row, col] != ' ' && grid[row, col] == grid[row + 1, col + 1] && grid[row, col] == grid[row + 2, col + 2] && grid[row, col] == grid[row + 3, col + 3])
                {
                    return true;
                }
            }
        }
        return false;

    }
}

// Class: MainGame
// Description: Main class 
class MainGame
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