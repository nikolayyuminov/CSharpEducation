namespace FinanceTracker.API.Contracts.Categories;

/// <summary>
/// Запрос от пользователя изменения описания категории.
/// </summary>
public class ChangeDescriptionRequest
{
  /// <summary>
  /// Id категории для изменения описания.
  /// </summary>
  public long CategoryId { get; init; }
  
  /// <summary>
  /// Новое описание для категории.
  /// </summary>
  public string? NewDescription { get; init; }
}