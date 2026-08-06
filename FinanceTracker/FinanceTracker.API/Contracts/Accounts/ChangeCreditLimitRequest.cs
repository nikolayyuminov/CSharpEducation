namespace FinanceTracker.API.Contracts.Accounts;

/// <summary>
/// Запрос пользователя для изменения кредитного лимита.
/// </summary>
public sealed record ChangeCreditLimitRequest
{
  /// <summary>
  /// Id счета, которому нужно изменить кредитный лимит.
  /// </summary>
  public long AccountId { get; init; }
  
  /// <summary>
  /// Новое значение кредитного лимита.
  /// </summary>
  public decimal NewCreditLimit { get; init; }
}