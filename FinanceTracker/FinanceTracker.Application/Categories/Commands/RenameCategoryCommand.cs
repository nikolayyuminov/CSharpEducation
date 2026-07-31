namespace FinanceTracker.Application.Categories.Commands;

/// <summary>
/// Команда переименования категории.
/// </summary>
public class RenameCategoryCommand
{
  #region Поля и свойства

  /// <summary>
  /// Id пользователя, которому принадлежит категория.
  /// </summary>
  public long UserId { get; init; }
  
  /// <summary>
  /// Id категории для переименования.
  /// </summary>
  public long CategoryId { get; init; }
  
  /// <summary>
  /// Новое имя для категории.
  /// </summary>
  public string? NewName { get; init; }

  #endregion
}