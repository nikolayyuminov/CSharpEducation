using System;

namespace TicTacToe;

public static class GameManager
{
  public const char SymbolX = 'X';
  public const char Symbol0 = '0';
  #region Поля и свойства
  
  public static BasePlayer CurrentPlayer { get; set; } = null!;

  #endregion

  #region Методы

  public static void FirstMove(Player player, Computer computer, Board board)
  {
    if (player.Symbol == SymbolX)
    {
      player.Move(board);
      CurrentPlayer = player;
    }
    else
    {
      computer.Move(board);
      CurrentPlayer = computer;
    }
  }

  public static void SwitchPlayer(Player player, Computer computer)
  {
    CurrentPlayer = (CurrentPlayer == player) ? computer : player;
  }
  
  public static bool CheckWin(Player player, Computer computer, Board board)
  {
    char currentPlayerSymbol;
    if (CurrentPlayer == player)
    {
      currentPlayerSymbol = player.Symbol;
    }
    else
    {
      currentPlayerSymbol = computer.Symbol;
    }
    for (int i = 0; i < 3; i++)
    {
      // проверка горизонтали
      if (board.Cell[i, 0] == currentPlayerSymbol &&
          board.Cell[i, 1] == currentPlayerSymbol &&
          board.Cell[i, 2] == currentPlayerSymbol)
        return true;
      // проверка вертикали
      if (board.Cell[0, i] == currentPlayerSymbol &&
          board.Cell[1, i] == currentPlayerSymbol &&
          board.Cell[2, i] == currentPlayerSymbol)
        return true;
    }
      //Проверка диагоналей
    if (board.Cell[0, 0] == currentPlayerSymbol &&
        board.Cell[1, 1] == currentPlayerSymbol &&
        board.Cell[2, 2] == currentPlayerSymbol)
      return true;

    if (board.Cell[0, 2] == currentPlayerSymbol &&
        board.Cell[1, 1] == currentPlayerSymbol &&
        board.Cell[2, 0] == currentPlayerSymbol)
      return true;

    return false;
  }

  public static bool CheckGameRaw(Board board)
  {
    foreach (char cell in board.Cell)
      if (cell == ' ')
        return false;

    return true;
  }

  public static char SetPlayerSymbol()
  {
    char playerChar;
    Console.Write($"Чем будешь играть?({SymbolX} или {Symbol0}): ");
    do
    {
      playerChar = Console.ReadKey().KeyChar;
      Console.WriteLine();
      if (playerChar != SymbolX && playerChar != Symbol0)
      {
        Console.Write($"Не правильный символ, введите {SymbolX} или {Symbol0}:");
      }
    } while (playerChar != SymbolX && playerChar != Symbol0);
    
    return playerChar;
  }

  public static char SetComputerSymbol(Player player)
  {
    if  (player.Symbol == SymbolX)
    {
      return Symbol0;
    }
    return SymbolX;
  }

  public static void GameBody(Player player, Computer computer, Board board)
  {
    Console.Write("Нажмите любую клавишу для продолжения или E для выхода из игры:");
    var keyInfo = Console.ReadKey(true);
    while (keyInfo.Key != ConsoleKey.E)
    {
      if (CheckWin(player, computer, board))
      {
        Console.Clear();
        Console.WriteLine("Tic Tac Toe Game");
        board.PrintBoard();
        if (CurrentPlayer == player)
        {
          Console.WriteLine($"Победил игрок {player.Name}!");
        }
        else
        {
          Console.WriteLine($"Победил Компьютер!");
        }
        break;
      }
      if (CheckGameRaw(board))
      {
        Console.Clear();
        Console.WriteLine("Tic Tac Toe Game");
        board.PrintBoard();
        Console.WriteLine("Ничья!");
        break;
      }
      SwitchPlayer(player, computer);
  
      if (CurrentPlayer == player)
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
  }

  #endregion
}