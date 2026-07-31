using FinanceTracker.Application.Abstractions.Repositories;
using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Infrastructure.Repositories;

/// <summary>
/// Репозиторий категорий.
/// </summary>
public class CategoryRepository : ICategoryRepository
{
  #region Поля и свойства

  /// <summary>
  /// Коллекция Категорий.
  /// </summary>
  private readonly List<Category> _categories;

  #endregion

  #region Методы

  /// <summary>
  /// Добавить новую категорию в репозиторий.
  /// </summary>
  /// <param name="category">Категория.</param>
  public void Add(Category category)
  {
    _categories.Add(category);
  }

  /// <summary>
  /// Получить категорию по Id.
  /// </summary>
  /// <param name="categoryId">Id категории.</param>
  /// <returns>Категория, может быть пустой.</returns>
  public Category? GetById(long categoryId)
  {
    return _categories.FirstOrDefault(c => c.Id == categoryId);
  }

  /// <summary>
  /// Получить пользовательскую или системную категорию по имени.
  /// </summary>
  /// <param name="userId">Id пользователя, которому принадлежит категория. Null - системная категория.</param>
  /// <param name="name">Имя категории.</param>
  /// <returns>Категория, может быть пустой.</returns>
  public Category? GetByName(long? userId, string name)
  {
    return _categories.FirstOrDefault(c => (c.UserId == userId || c.UserId == null) && c.Name == name);
  }

  #endregion

  #region Конструкторы

  public CategoryRepository()
  {
    _categories = [];
  }

  #endregion
}