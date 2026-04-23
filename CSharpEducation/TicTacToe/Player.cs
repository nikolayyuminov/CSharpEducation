using System;

namespace TicTacToe;

public class Player
{
  #region Поля и свойства
  
  private readonly string _name;
  
  public string? Name
  {
    get => _name;
    private init => _name = string.IsNullOrWhiteSpace(value) ? "EmptyMan" : value; 
  }
  
  private readonly char _symbol;
  public char Symbol
  {
    get => _symbol;
    private init
    {
      if (value == 'o' || value == 'O')
      {
        _symbol = '0';
      }
      else if  (value != 'X' && value != '0')
      {
        _symbol = 'X';
      }
      else
      {
        _symbol = value;
      }
    }
  }
  #endregion

  #region Методы

  public void Move(Board board)
  {
    Console.Clear();
    if (Symbol == 'X')
    {
      Console.ForegroundColor = ConsoleColor.Red;
    }
    else
    {
      Console.ForegroundColor = ConsoleColor.Blue;
    }
    Console.WriteLine("Tic Tac Toe Game");
    board.PrintBoard();
    if (Symbol == 'X')
    {
      Console.ForegroundColor = ConsoleColor.Red;
    }
    else
    {
      Console.ForegroundColor = ConsoleColor.Blue;
    }
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

      if (board.Cell[row, col] != ' ')
      {
        Console.WriteLine("Клетка занята!");
        continue;
      }
      board.Cell[row, col] = Symbol;
      break;
    }
    Console.Clear();
    Console.WriteLine("Tic Tac Toe Game");
    Console.WriteLine($"Ход игрока {Name}... ");
    board.PrintBoard();
    Console.ResetColor();
  }

  #endregion

  #region Конструкторы

  public Player(string name, char symbol)
  {
    Name = name;
    Symbol = symbol;
  }

  #endregion
}