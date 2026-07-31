namespace FinanceTracker.Application.Accounts.Commands;

/// <summary>
/// Команда для изменения кредитного лимита.
/// </summary>
public class ChangeCreditLimitCommand
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