using System;

namespace TicTacToe;

    public class TicTacToe
    {
        public static void StartGame()
        {
            Console.Clear();
            Console.WriteLine("Tic Tac Toe Game");
            
            Console.Write("Введите имя игрока: ");
            var name = Console.ReadLine();
            Console.Write($"Чем будешь играть?(X или 0): ");
            if (!char.TryParse(Console.ReadLine(), out var playerChar))
            {
                playerChar = 'X';
            }
            Player player = new Player(name, playerChar);
            Console.WriteLine($"Привет, {player.Name}! Ты играешь за {player.Symbol}");
            
            Computer computer = new Computer(player.Symbol);
            
            var board = new Board(3,3);
            
            Console.WriteLine($"Нажми любую клавишу чтобы начать игру!");
            Console.ReadKey();

            GameManager.FirstMove(player, computer, board);
            
            GameManager.SwitchPlayer(player, computer);
            if (GameManager.CheckWin(player, computer, board))
            {
                    Console.Clear();
                    board.PrintBoard();
                    if (GameManager.CurrentPlayer == player.ToString())
                    {
                        Console.WriteLine($"Победил игрок {player.Name}!");
                    }
                    else
                    {
                        Console.WriteLine($"Победил Компьютер!");
                    }
                    break;
            }
            if (GameManager.CheckGameraw(board))
            {
                Console.Clear();
                board.PrintBoard();
                Console.WriteLine("Ничья!");
                break;
            }

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
            }

            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }
*/
        }
    }
