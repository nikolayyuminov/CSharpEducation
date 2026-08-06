namespace FinanceTracker.API.Contracts.Accounts;

/// <summary>
/// Запрос пользователя на закрытие счета.
/// </summary>
public sealed record CloseAccountRequest
{
  /// <summary>
  /// Id счета, который станет закрытым.
  /// </summary>
  public long  AccountId { get; init; }
}