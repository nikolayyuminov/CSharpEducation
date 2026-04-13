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
      if  (value != 'X' && value != '0')
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
    Console.ForegroundColor = ConsoleColor.Red;
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

      board.Cell[row, col] = this.Symbol;
      Console.ResetColor();
      break;
    }
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