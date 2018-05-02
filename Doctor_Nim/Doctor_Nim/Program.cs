using static System.Console;
using static System.ConsoleColor;
using static System.Threading.Thread;

namespace Doctor_Nim
{
    class Program
    {
        static void Main(string[] args)
        {
            bool equalizer = false;
            //creates a new game of Nim and starts it
            if(args.Length != 0)
            {
                if (args[0] == "-equalizer" || args[0] == "e") equalizer = true;
            }
            var nim = new Nim();
            nim.InitializeGame(equalizer);
        }
    }

    public class Nim
    {
        //Sets the number of stones used for the game. Must be a multiple of 4 for the ai to work correctly
        const int DEFAULT_STONE_NUMBER = 12;

        int stonesRemaining;
        bool gameRunning, playersTurn = true, isEqual;

        public void InitializeGame(bool equalizer)
        {
            isEqual = equalizer;
            Clear();
            //sets the variables and begins the game loop
            WriteLine("I am the unbeatable Dr. Nim!");
            stonesRemaining = DEFAULT_STONE_NUMBER;
            gameRunning = true;
            StartGameLoop();
        }

        public void StartGameLoop()
        {
            DrawStones();
            //game loop
            while (gameRunning)
            {
                if (playersTurn)
                {
                    WriteLine("Take 1-3 stones!");
                    string userInput = "";

                    while (userInput == "")
                    {
                        //takes the entered key as a string that's converted into an int
                        userInput = ReadKey(true).KeyChar.ToString();
                        if (int.TryParse(userInput, out int intInput) && intInput >= 1 && intInput <= 3 && intInput <= stonesRemaining)
                        {
                            //takes out the number of stones from the total and redraws them. It'll then check to see if the player won.
                            WriteLine("You take {0} stones", intInput);
                            stonesRemaining -= intInput;
                            DrawStones();
                            CheckIfWon();
                            playersTurn = false;
                        }
                        else
                        {
                            WriteLine("Invalid input.");
                        }
                    }

                }
                else
                {
                    WriteLine("I take {0} stones!", AiProcess());
                    DrawStones();
                    CheckIfWon();
                    playersTurn = true;
                }
            }

            GameOver();
        }

        public void GameOver()
        {
            WriteLine();
            WriteLine("Game Over!");
            WriteLine("Would you like to retry? (Y/N)");
            var input = ReadKey(true).KeyChar.ToString();
            if(input == "y")
            {
                InitializeGame(false);
            }
        }

        public void DrawStones()
        {
            ForegroundColor = DarkGray; 

            for (int i = 0; i < stonesRemaining; i++)
            {
                Sleep(30);
                Write("O");
            }

            for (int i = stonesRemaining; i < DEFAULT_STONE_NUMBER; i++)
            {
                Sleep(30);
                Write("*");
            }

            ForegroundColor = Gray;
            WriteLine();
        }

        public int AiProcess()
        {
            var rand = new System.Random();
            int draw;

            if (stonesRemaining > 3 && stonesRemaining % 4 <=3 && stonesRemaining % 4 >= 1)
            {
                draw = stonesRemaining % 4;
            }
            else if (stonesRemaining <= 3)
            {
                draw = stonesRemaining;
            }
            else
            {
                draw = rand.Next(1, 4);
            }

            if (isEqual)
            {
                while (draw == stonesRemaining % 4)
                {
                    draw = rand.Next(1, 4);
                }
                isEqual = false;
            }

            stonesRemaining -= draw;
            return draw;
        }

        public void CheckIfWon()
        {
            if (stonesRemaining == 0)
            {
                WriteLine();
                for (int i = 0; i < 3; i++)
                {
                    Sleep(500);
                    Write(".");
                }

                gameRunning = false;

                if (!playersTurn)
                {
                    WriteLine("I win!");
                }
                else
                {
                    WriteLine("I lost?");
                }
            }
        }

    }
}