using System;

namespace TicTacToe;

public class Player : BasePlayer
{
  #region Поля и свойства
  
  private readonly string _name;
  
  public string? Name
  {
    get => _name;
    private init => _name = string.IsNullOrWhiteSpace(value) ? "EmptyMan" : value; 
  }

  #endregion

  #region Методы

  public override void Move(Board board)
  {
    Console.Clear();
    if (Symbol == GameManager.SymbolX)
    {
      Console.ForegroundColor = ConsoleColor.Red;
    }
    else
    {
      Console.ForegroundColor = ConsoleColor.Blue;
    }
    Console.WriteLine("Tic Tac Toe Game");
    board.PrintBoard();
    if (Symbol == GameManager.SymbolX)
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

  public Player(string name, char symbol) : base(symbol)
  {
    Name = name;
  }

  #endregion
}