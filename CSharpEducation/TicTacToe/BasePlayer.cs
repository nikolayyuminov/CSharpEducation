namespace TicTacToe;

public abstract class BasePlayer
{
  #region Поля и свойства
  
  public char Symbol { get; private init; }
  
  #endregion

  #region Методы

  public abstract void Move(Board board);

  #endregion
  
  #region Конструктор

  public BasePlayer(char symbol)
  {
    Symbol = symbol;
  }

  #endregion

}