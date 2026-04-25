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

            var playerChar = GameManager.SetPlayerSymbol();
            
            var player = new Player(name, playerChar);
            Console.WriteLine($"Привет, {player.Name}! Ты играешь за {player.Symbol}");
            
            var computerChar = GameManager.SetComputerSymbol(player);

            var computer = new Computer(computerChar);
            
            var board = new Board(3,3);
            
            Console.WriteLine($"Готов? Нажми любую клавишу чтобы начать игру!");
            Console.ReadKey();

            GameManager.FirstMove(player, computer, board);
            
            GameManager.GameBody(player, computer, board);
            
            Console.WriteLine();
            Console.WriteLine("Игра завершена. Нажмите любую клавишу для выхода...");
            Console.ReadKey();

        }
    }
