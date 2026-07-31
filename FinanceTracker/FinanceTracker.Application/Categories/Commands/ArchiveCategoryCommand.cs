namespace FinanceTracker.Application.Categories.Commands;

/// <summary>
/// Команда архивирования категории.
/// </summary>
public class ArchiveCategoryCommand
{
  #region Поля и свойства

  /// <summary>
  /// Id категории, которую необходимо архивировать.
  /// </summary>
  public long CategoryId { get; init; }

  #endregion
}