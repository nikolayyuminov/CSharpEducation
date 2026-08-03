namespace FinanceTracker.API.Contracts.Accounts;

/// <summary>
/// Запрос пользователя для переименования счета.
/// </summary>
public class RenameAccountRequest
{
  /// <summary>
  /// Id пользователя, которому принадлежит счет.
  /// </summary>
  public long UserId { get; init; }
  
  /// <summary>
  /// Id Счета для переименования.
  /// </summary>
  public long AccountId { get; init; }
  
  /// <summary>
  /// Новое имя для счета.
  /// </summary>
  public string? NewName { get; init; }
}