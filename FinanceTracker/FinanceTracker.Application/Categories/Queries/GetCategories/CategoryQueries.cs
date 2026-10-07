using FinanceTracker.Application.Abstractions.Queries;
using FinanceTracker.Application.Abstractions.Repositories;

namespace FinanceTracker.Application.Categories.Queries.GetCategories;

/// <summary>
/// Запросы для чтения категорий.
/// </summary>
public class CategoryQueries : ICategoryQueries
{
  #region Поля

  /// <summary>
  /// Репозиторий категорий.
  /// </summary>
  private readonly ICategoryRepository _categoryRepository;

  #endregion

  #region Методы

  /// <inheritdoc />
  public IReadOnlyCollection<CategoryListItemDto> GetAll(long userId)
  {
    return _categoryRepository
      .GetAll(userId)
      .Select(x => new CategoryListItemDto
      {
        Id = x.Id,
        Name = x.Name,
        UserId = x.UserId,
        IsArchived = x.IsArchived,
        CategoryKind = x.CategoryKind,
        Description = x.Description
      })
      .ToList();
  }

  #endregion

  #region Конструктор

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="categoryRepository"> Репозиторий категорий. </param>
  public CategoryQueries(ICategoryRepository categoryRepository)
  {
    _categoryRepository = categoryRepository;
  }

  #endregion
}