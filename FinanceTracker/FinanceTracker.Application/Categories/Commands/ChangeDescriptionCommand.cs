namespace FinanceTracker.Application.Categories.Commands;

/// <summary>
/// Команда изменения описания категории.
/// </summary>
public class ChangeDescriptionCommand
{
  #region Поля и свойства
  
  /// <summary>
  /// Id категории для изменения описания.
  /// </summary>
  public long CategoryId { get; init; }
  
  /// <summary>
  /// Новое описание для категории.
  /// </summary>
  public string? NewDescription { get; init; }

  #endregion
}