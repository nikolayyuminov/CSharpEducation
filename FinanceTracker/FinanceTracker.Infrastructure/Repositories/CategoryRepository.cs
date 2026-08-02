using FinanceTracker.Application.Abstractions.Repositories;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Infrastructure.Persistence;

namespace FinanceTracker.Infrastructure.Repositories;

/// <summary>
/// Репозиторий категорий.
/// </summary>
public class CategoryRepository : ICategoryRepository
{
  #region Поля и свойства

  /// <summary>
  /// Контекст БД.
  /// </summary>
  private readonly FinanceTrackerDbContext _dbContext;

  #endregion

  #region Методы

  /// <summary>
  /// Добавить новую категорию в репозиторий.
  /// </summary>
  /// <param name="category">Категория.</param>
  public void Add(Category category)
  {
    _dbContext.Categories.Add(category);
  }

  /// <summary>
  /// Получить категорию по Id.
  /// </summary>
  /// <param name="categoryId">Id категории.</param>
  /// <returns>Категория, может быть пустой.</returns>
  public Category? GetById(long categoryId)
  {
    return _dbContext.Categories.Find(categoryId);
  }

  /// <summary>
  /// Получить пользовательскую или системную категорию по имени.
  /// </summary>
  /// <param name="userId">Id пользователя, которому принадлежит категория. Null - системная категория.</param>
  /// <param name="name">Имя категории.</param>
  /// <returns>Категория, может быть пустой.</returns>
  public Category? GetByName(long? userId, string name)
  {
    return _dbContext.Categories
                     .FirstOrDefault(c => (c.UserId == userId || c.UserId == null) && c.Name == name);
  }

  #endregion

  #region Конструкторы

  public CategoryRepository(FinanceTrackerDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  #endregion
}