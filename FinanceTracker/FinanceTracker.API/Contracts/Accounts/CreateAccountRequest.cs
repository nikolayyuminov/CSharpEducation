using FinanceTracker.Domain.Enums;

namespace FinanceTracker.API.Contracts.Accounts;

/// <summary>
/// Запрос пользователя для создания счета. 
/// </summary>
public sealed record CreateAccountRequest
{
  /// <summary>
  /// Id пользователя, которому будет принадлежать счет.
  /// </summary>
  public long UserId { get; init; }

  /// <summary>
  /// Имя создаваемого счета.
  /// </summary>
  public string Name { get; init; } = string.Empty;

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
}