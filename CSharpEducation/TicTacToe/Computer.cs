using System;

namespace TicTacToe;

public class Computer
{
  #region Поля и свойства
  
  private readonly char _symbol;
  public char Symbol
  {
    get => _symbol;
    private init
    {
      if  (value == 'X')
      {
        _symbol = '0';
      }
      else
      {
        _symbol = 'X';
      }
    }
  }
  
  #endregion

  #region Методы

  public void Move(Board board)
  {
    Random rand = new Random();
    Console.Clear();
    Console.WriteLine("Tic Tac Toe Game");
    Console.ForegroundColor = ConsoleColor.Blue;
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

  public Computer(char symbol)
  {
    Symbol = symbol;
  }

  #endregion
}