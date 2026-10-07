using FinanceTracker.Application.Categories.Queries.GetCategories;

namespace FinanceTracker.Application.Abstractions.Queries;

/// <summary>
/// Определяет операции для получения данных категорий.
/// </summary>
public interface ICategoryQueries
{
  /// <summary>
  /// Получить категории пользователя и системные категории.
  /// </summary>
  /// <param name="userId">
  /// Идентификатор пользователя.
  /// </param>
  /// <returns>
  /// Коллекция категорий.
  /// </returns>
  IReadOnlyCollection<CategoryListItemDto> GetAll(long userId);
}