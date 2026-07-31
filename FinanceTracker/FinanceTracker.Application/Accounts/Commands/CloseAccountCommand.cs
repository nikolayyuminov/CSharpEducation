namespace FinanceTracker.Application.Accounts.Commands;

/// <summary>
/// Команда для закрытия счета.
/// </summary>
public class CloseAccountCommand
{
  /// <summary>
  /// Id счета, который станет закрытым.
  /// </summary>
  public long  AccountId { get; init; }
}