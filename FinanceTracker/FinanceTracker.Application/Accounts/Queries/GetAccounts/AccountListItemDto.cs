using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Application.Accounts.Queries.GetAccounts;

/// <summary>
/// Информация о счете для отображения в списке.
/// </summary>
public sealed class AccountListItemDto
{
  /// <summary>
  /// Id счета.
  /// </summary>
  public long Id { get; init; }

  /// <summary>
  /// Имя счета.
  /// </summary>
  public string Name { get; init; } = string.Empty;

  /// <summary>
  /// Баланс счета.
  /// </summary>
  public decimal Balance { get; init; }

  /// <summary>
  /// Тип счета.
  /// </summary>
  public AccountType AccountType { get; init; }

  /// <summary>
  /// Закрыт ли счет.
  /// </summary>
  public bool IsClosed { get; init; }
}