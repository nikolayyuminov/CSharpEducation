using System;

namespace TicTacToe;

public class Board
{
  #region Поля и свойства

  private static char[,]? _board;
  public char[,] Cell { get { return _board; } set { _board = value; } }

  #endregion
  
  #region Методы

  public void PrintBoard()
  {
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("     0   1   2");
    Console.WriteLine("    ---+---+---");
    for (int i = 0; i < _board.GetLength(0); i++)
    {
      Console.Write(" " + i + " |");
      for (int j = 0; j < _board.GetLength(1) ; j++)
      {
        Console.Write(" ");
        if (_board[i, j] == 'X')
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.Write('X');
        }
        else if (_board[i, j] == '0')
        {
          Console.ForegroundColor = ConsoleColor.Blue;
          Console.Write('0');
        }
        else
        {
          Console.ForegroundColor = ConsoleColor.DarkGray;
          Console.Write(' ');
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(" ");
        if (j < _board.GetLength(1)) Console.Write("|");
      }
      Console.WriteLine();

      if (i < _board.GetLength(0))
        Console.WriteLine("    ---+---+---");
    }
    Console.ResetColor();
  }
  #endregion
  
  #region Конструкторы
  
  public Board(int row, int col)
  {
    _board = new char[row, col];
    for (int i = 0; i < row; i++)
    {
      for (int j = 0; j < col; j++)
      {
        _board[i, j] = ' ';
      }
    }
  }

  #endregion
}