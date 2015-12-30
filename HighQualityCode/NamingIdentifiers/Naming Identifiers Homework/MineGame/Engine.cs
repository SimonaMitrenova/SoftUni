using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MineGame.Interfaces;

namespace MineGame
{
    public class Engine : IEngine
    {
        private IDatabase data;
        private IUserInterface userInterface;
        const int freeSquares = 35;
        private char[,] playArea = CreatePlayArea();
        private char[,] bombsArea = GenerateBombs();
        private int turnsCounter = 0;
        private bool isDead = false;
        private int currentRow = 0;
        private int currentCol = 0;
        private bool isNewGame = true;
        private bool wonTheGame = false;
        private bool isGameEnded = false;

        public Engine(IDatabase data, IUserInterface userInterface)
        {
            this.Database = data;
            this.UserInterface = userInterface;
        }

        public IDatabase Database
        {
            get
            {
                return this.data;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Datebase cannot be null.");
                }
                this.data = value;
            }
        }
        public IUserInterface UserInterface 
        {
            get
            {
                return this.userInterface;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("User interface cannot be null.");
                }
                this.userInterface = value;
            }
        }

        public void Run()
        {
            do
            {
                if (isNewGame)
                {
                    UserInterface.WriteLine(Messages.StartingMassage);
                    GeneratePlayGroundWithBorders(playArea);
                    isNewGame = false;
                }

                UserInterface.Write(Messages.ChooseRowAndColumn);
                string[] inputLine = UserInterface.ReadLine().Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);

                string command = inputLine[0];

                if (inputLine.Length > 1)
                {
                    if (int.TryParse(inputLine[0].ToString(), out currentRow) && int.TryParse(inputLine[1].ToString(), out currentCol)
                        && currentRow <= playArea.GetLength(0) && currentCol <= playArea.GetLength(1))
                    {
                        command = "turn";
                    }
                }

                ExecuteCommand(command);

                if (isDead)
                {
                    GeneratePlayGroundWithBorders(bombsArea);
                    UserInterface.WriteLine(Messages.MineUncover, turnsCounter);
                    UserInterface.Write("{0}{1}", Environment.NewLine, Messages.GiveNickname);
                    string nickname = Console.ReadLine();
                    IPlayer player = new Player(nickname, turnsCounter);
                    if (Database.Players.Count() < 5)
                    {
                        Database.AddPlayer(player);
                    }
                    else
                    {
                        Database.InsertPlayer(player);
                    }

                    this.Database.SortPlayers();
                    string result = GetWinnersList(Database);
                    UserInterface.WriteLine(result);

                    this.playArea = CreatePlayArea();
                    this.bombsArea = GenerateBombs();
                    this.turnsCounter = 0;
                    this.isDead = false;
                    this.isNewGame = true;
                }

                if (wonTheGame)
                {
                    UserInterface.WriteLine(Messages.WinningMassage);
                    GeneratePlayGroundWithBorders(bombsArea);
                    UserInterface.WriteLine(Messages.GiveNickname);
                    string nickname = Console.ReadLine();
                    IPlayer player = new Player(nickname, this.turnsCounter);
                    this.Database.AddPlayer(player);
                    string result = GetWinnersList(this.Database);
                    this.playArea = CreatePlayArea();
                    this.bombsArea = GenerateBombs();
                    this.turnsCounter = 0;
                    this.wonTheGame = false;
                    this.isNewGame = true;
                }

            } while (!isGameEnded);

            UserInterface.WriteLine(Messages.EndMessage);
            Environment.Exit(0);
        }

        private void ExecuteCommand(string command)
        {
            switch (command)
            {
                case "top":
                    string result = GetWinnersList(Database);
                    UserInterface.WriteLine(result);
                    break;
                case "restart":
                    RestartTheGame();
                    break;
                case "exit":
                    this.isGameEnded = true;
                    break;
                case "turn":
                    ExecuteTurnCommand();
                    break;
                default:
                    Console.WriteLine("{0}{1}{0}", Environment.NewLine, Messages.UnknownCommand);
                    break;
            }
        }

        private void ExecuteTurnCommand()
        {
            if (bombsArea[currentRow, currentCol] != '*')
            {
                if (bombsArea[currentRow, currentCol] == '-')
                {
                    SetBombsCount(playArea, bombsArea, currentRow, currentCol);
                    turnsCounter++;
                }

                if (turnsCounter == freeSquares)
                {
                    wonTheGame = true;
                }
                else
                {
                    GeneratePlayGroundWithBorders(playArea);
                }
            }
            else
            {
                isDead = true;
            }
        }

        private static void SetBombsCount(char[,] playArea, char[,] bombsArea, int row, int column)
        {
            char kolkoBombi = CalculateBombsCount(bombsArea, row, column);
            bombsArea[row, column] = kolkoBombi;
            playArea[row, column] = kolkoBombi;
        }

        private static char CalculateBombsCount(char[,] bombsArea, int row, int column)
        {
            int bombsCount = 0;
            int allRows = bombsArea.GetLength(0);
            int allColumns = bombsArea.GetLength(1);

            if (row - 1 >= 0)
            {
                if (bombsArea[row - 1, column] == '*')
                {
                    bombsCount++;
                }
            }

            if (row + 1 < allRows)
            {
                if (bombsArea[row + 1, column] == '*')
                {
                    bombsCount++;
                }
            }

            if (column - 1 >= 0)
            {
                if (bombsArea[row, column - 1] == '*')
                {
                    bombsCount++;
                }
            }

            if (column + 1 < allColumns)
            {
                if (bombsArea[row, column + 1] == '*')
                {
                    bombsCount++;
                }
            }

            if ((row - 1 >= 0) && (column - 1 >= 0))
            {
                if (bombsArea[row - 1, column - 1] == '*')
                {
                    bombsCount++;
                }
            }

            if ((row - 1 >= 0) && (column + 1 < allColumns))
            {
                if (bombsArea[row - 1, column + 1] == '*')
                {
                    bombsCount++;
                }
            }

            if ((row + 1 < allRows) && (column - 1 >= 0))
            {
                if (bombsArea[row + 1, column - 1] == '*')
                {
                    bombsCount++;
                }
            }

            if ((row + 1 < allRows) && (column + 1 < allColumns))
            {
                if (bombsArea[row + 1, column + 1] == '*')
                {
                    bombsCount++;
                }
            }

            return char.Parse(bombsCount.ToString());
        }

        private static string GetWinnersList(IDatabase data)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(string.Format("{0}{1}", Environment.NewLine, "Points:"));

            if (data.Players.Any())
            {
                int count = 1;
                foreach (var player in data.Players)
                {
                    result.AppendLine(String.Format("{0}. {1} --> {2} squares", count, player.Name, player.Points));
                    count++;
                }

                result.AppendLine();
            }
            else
            {
                result.AppendLine(string.Format("{1}{0}{1}", Messages.EmptyWinnersList, Environment.NewLine));
            }

            return result.ToString();
        }

        private void RestartTheGame()
        {
            playArea = CreatePlayArea();
            bombsArea = GenerateBombs();
            GeneratePlayGroundWithBorders(playArea);
            isDead = false;
            isNewGame = false;
        }

        private static char[,] CreatePlayArea()
        {
            int boardRows = 5;
            int boardColumns = 10;
            char[,] board = new char[boardRows, boardColumns];
            for (int i = 0; i < boardRows; i++)
            {
                for (int j = 0; j < boardColumns; j++)
                {
                    board[i, j] = '?';
                }
            }

            return board;
        }

        private static char[,] GenerateBombs()
        {
            int rows = 5;
            int columns = 10;
            char[,] gameArea = new char[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    gameArea[i, j] = '-';
                }
            }

            List<int> randomNumberCollection = new List<int>();
            while (randomNumberCollection.Count < 15)
            {
                Random random = new Random();
                int nextNumber = random.Next(50);
                if (!randomNumberCollection.Contains(nextNumber))
                {
                    randomNumberCollection.Add(nextNumber);
                }
            }

            foreach (int number in randomNumberCollection)
            {
                int col = number / columns;
                int row = number % columns;
                if (row == 0 && number != 0)
                {
                    col--;
                    row = columns;
                }
                else
                {
                    row++;
                }

                gameArea[col, row - 1] = '*';
            }

            return gameArea;
        }

        private static void GeneratePlayGroundWithBorders(char[,] board)
        {
            int boardRows = board.GetLength(0);
            int boardCols = board.GetLength(1);
            Console.WriteLine("\n    0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");
            for (int i = 0; i < boardRows; i++)
            {
                Console.Write("{0} | ", i);
                for (int j = 0; j < boardCols; j++)
                {
                    Console.Write(string.Format("{0} ", board[i, j]));
                }

                Console.Write("|");
                Console.WriteLine();
            }

            Console.WriteLine("   ---------------------\n");
        }
    }
}
