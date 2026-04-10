using System;

namespace TicTacToe;

public class Player
{
  #region Поля и свойства
  
  private string _playerName;
  
  public string? PlayerName
  {
    get => _playerName;
  }
  
  private  char _playerSymbol;
  public char PlayerSymbol
  {
    get => _playerSymbol;
    set => _playerSymbol = value;
  }
  #endregion

  #region Методы

  public void PlayerMove(char symbol)
  {
    
  }

  #endregion

  #region Конструкторы

  public Player(string playerName, char playerSymbol)
  {
    _playerName = playerName;
    _playerSymbol = playerSymbol;
  }

  #endregion
}