namespace FinanceTracker.Application.Accounts.Commands;

/// <summary>
/// Команда для переименования счета
/// </summary>
public class RenameAccountCommand
{
  #region Поля и свойства

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

  #endregion
}