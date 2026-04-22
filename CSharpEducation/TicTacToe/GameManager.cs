namespace TicTacToe;

public class GameManager
{
  #region Поля и свойства
  
  public static string? CurrentPlayer { get; set; }
  
  #endregion

  #region Методы

  public static void FirstMove(Player player, Computer computer, Board board)
  {
    if (player.Symbol == 'X')
    {
      player.Move(board);
      CurrentPlayer = player.ToString();
    }
    else
    {
      computer.Move(board);
      CurrentPlayer = computer.ToString();
    }
  }

  public static void SwitchPlayer(Player player, Computer computer)
  {
    CurrentPlayer = (CurrentPlayer == player.ToString()) ? computer.ToString() : player.ToString();
  }
  
  public static bool CheckWin(Player player, Computer computer, Board board)
  {
    char currentPlayerSymbol;
    if (CurrentPlayer == player.ToString())
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

  public static bool CheckGameraw(Board board)
  {
    foreach (char cell in board.Cell)
      if (cell == ' ')
        return false;

    return true;
  }

  #endregion
}