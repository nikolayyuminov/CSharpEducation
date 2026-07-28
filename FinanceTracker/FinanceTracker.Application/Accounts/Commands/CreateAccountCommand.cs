using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Application.Accounts.Commands;

/// <summary>
/// Данные для создания аккаунта.
/// </summary>
public class CreateAccountCommand
{
  #region Поля и свойства
  
  /// <summary>
  /// Id пользователя, которому будет принадлежать счет.
  /// </summary>
  public long UserId { get; init; }
  
  /// <summary>
  /// Имя создаваемого счета.
  /// </summary>
  public required string Name { get; init; }
  
  /// <summary>
  /// Тип счета.
  /// </summary>
  public AccountType AccountType { get; init; }
  
  /// <summary>
  /// Начальное значение баланса счета.
  /// </summary>
  public decimal InitialBalance { get; init; }
  
  /// <summary>
  /// Валюта счета.
  /// </summary>
  public Currency Currency { get; init; }
  
  /// <summary>
  /// Кредитный лимит, если счет кредитный.
  /// </summary>
  public decimal? CreditLimit { get; init; }

  #endregion
}