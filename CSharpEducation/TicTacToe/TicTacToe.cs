using System;

namespace TicTacToe;

    public class TicTacToe
    {
        public const char SymbolX = 'X';
        public const char Symbol0 = '0';
        
        public static void StartGame()
        {
            Console.Clear();
            Console.WriteLine("Tic Tac Toe Game");
            
            Console.Write("Введите имя игрока: ");
            var name = Console.ReadLine();
            char playerChar;
            Console.Write($"Чем будешь играть?(X или 0): ");
            do
            {
                playerChar = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (playerChar != SymbolX && playerChar != Symbol0)
                {
                    Console.Write("Не правильный символ, введите 'X' или '0':");
                }
            } while (playerChar != SymbolX && playerChar != Symbol0);
            
            Player player = new Player(name, playerChar);
            Console.WriteLine($"Привет, {player.Name}! Ты играешь за {player.Symbol}");
            
            Computer computer = new Computer(player.Symbol);
            
            var board = new Board(3,3);
            
            Console.WriteLine($"Готов? Нажми любую клавишу чтобы начать игру!");
            Console.ReadKey();

            GameManager.FirstMove(player, computer, board);
            Console.Write("Нажмите любую клавишу для продолжения или E для выхода из игры:");
            var keyInfo = Console.ReadKey(true);
            while (keyInfo.Key != ConsoleKey.E)
            {
                 if (GameManager.CheckWin(player, computer, board))
                 {
                     Console.Clear();
                     Console.WriteLine("Tic Tac Toe Game");
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
                 if (GameManager.CheckGameRaw(board))
                 {
                     Console.Clear();
                     Console.WriteLine("Tic Tac Toe Game");
                     board.PrintBoard();
                     Console.WriteLine("Ничья!");
                     break;
                 }
                 GameManager.SwitchPlayer(player, computer);
  
                 if (GameManager.CurrentPlayer == player.ToString())
                 {
                     player.Move(board);
                     Console.Write("Нажмите любую клавишу для продолжения или E для выхода из игры:");
                     keyInfo = Console.ReadKey(true);
                 }
                 else
                 {
                     computer.Move(board);
                     Console.Write("Нажмите любую клавишу для продолжения или E для выхода из игры:");
                     keyInfo = Console.ReadKey(true);
                 }
            }
            Console.WriteLine();
            Console.WriteLine("Игра завершена. Нажмите любую клавишу для выхода...");
            Console.ReadKey();

        }
    }
