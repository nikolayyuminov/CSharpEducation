using System;

namespace TicTacToe
{
    public class TicTacToe
    {
        static Random rand = new Random();

        public static void StartGame()
        {
            Console.Clear();
            Console.WriteLine("Tic Tac Toe Game");

            
            Console.Write("Введите имя игрока: ");
            var name = Console.ReadLine();
            Console.Write($"Чем будешь играть?(X или 0): ");
            if (!char.TryParse(Console.ReadLine(), out var playerChar))
            {
                playerChar = ' ';
            }
            Player player = new Player(name, playerChar);
            Console.WriteLine($"Привет, {player.Name}! Ты играешь за {player.Symbol}");
            
            var board = new Board(3,3);
            board.PrintBoard();
            
            player.Move(board);
            Console.Clear();
            board.PrintBoard();

/*
            while (true)
            {
                Console.Clear();
                board.PrintBoard();

                if (currentPlayer == 'X')
                {
                    Console.WriteLine("Ход игрока X");
                    PlayerMove();
                }
                else
                {
                    Console.WriteLine("Ход компьютера O...");
                    System.Threading.Thread.Sleep(700);
                    ComputerMove();
                }

                if (CheckWin())
                {
                    Console.Clear();
                    board.PrintBoard();
                    Console.WriteLine($"Победил {(currentPlayer == 'X' ? "игрок" : "компьютер")}!");
                    break;
                }

                if (CheckDraw())
                {
                    Console.Clear();
                    board.PrintBoard();
                    Console.WriteLine("Ничья!");
                    break;
                }

                SwitchPlayer();
            }

            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }


        static void PlayerMove()
        {
            while (true)
            {
                Console.Write("Введите ход (строка столбец): ");
                string input = Console.ReadLine();

                string[] parts = input.Split(' ');

                if (parts.Length != 2 ||
                    !int.TryParse(parts[0], out int row) ||
                    !int.TryParse(parts[1], out int col))
                {
                    Console.WriteLine("Неверный формат! Пример: 1 2");
                    continue;
                }

                if (row < 0 || row > 2 || col < 0 || col > 2)
                {
                    Console.WriteLine("Координаты должны быть от 0 до 2!");
                    continue;
                }

                if (board[row, col] != ' ')
                {
                    Console.WriteLine("Клетка занята!");
                    continue;
                }

                board[row, col] = currentPlayer;
                break;
            }
        }

        static void ComputerMove()
        {
            while (true)
            {
                int row = rand.Next(0, 3);
                int col = rand.Next(0, 3);

                if (board[row, col] == ' ')
                {
                    board[row, col] = currentPlayer;
                    break;
                }
            }
        }

        static void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }

        static bool CheckWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == currentPlayer &&
                    board[i, 1] == currentPlayer &&
                    board[i, 2] == currentPlayer)
                    return true;

                if (board[0, i] == currentPlayer &&
                    board[1, i] == currentPlayer &&
                    board[2, i] == currentPlayer)
                    return true;
            }

            if (board[0, 0] == currentPlayer &&
                board[1, 1] == currentPlayer &&
                board[2, 2] == currentPlayer)
                return true;

            if (board[0, 2] == currentPlayer &&
                board[1, 1] == currentPlayer &&
                board[2, 0] == currentPlayer)
                return true;

            return false;
        }

        static bool CheckDraw()
        {
            foreach (char cell in board)
                if (cell == ' ')
                    return false;

            return true;*/
        }
    }
}