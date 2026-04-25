using System;

namespace TicTacToe;

public class Computer : BasePlayer
{
  #region Методы

  public override void Move(Board board)
  {
    Random rand = new Random();
    Console.Clear();
    if (Symbol == TicTacToe.SymbolX)
    {
      Console.ForegroundColor = ConsoleColor.Red;
    }
    else
    {
      Console.ForegroundColor = ConsoleColor.Blue;
    }
    Console.WriteLine("Tic Tac Toe Game");
    Console.Write("Ход компьютера");
    System.Threading.Thread.Sleep(500);
    Console.Write(".");
    System.Threading.Thread.Sleep(500);
    Console.Write(".");
    System.Threading.Thread.Sleep(500);
    Console.Write(".");
    Console.WriteLine();
    
    while (true)
    {
      int row = rand.Next(0, 3);
      int col = rand.Next(0, 3);

      if (board.Cell[row, col] == ' ')
      {
        board.Cell[row, col] = Symbol;
        Console.ResetColor();
        break;
      }
    }
    board.PrintBoard();
  }

  #endregion

  #region Конструкторы

  public Computer(char symbol) : base(symbol) { }

  #endregion
}