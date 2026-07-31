using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория категорий
/// </summary>
public interface ICategoryRepository
{
  /// <summary>
  /// Добавить новую категорию в репозиторий.
  /// </summary>
  /// <param name="category">Категория.</param>
  void Add(Domain.Entities.Category category);

  /// <summary>
  /// Получить категорию по Id.
  /// </summary>
  /// <param name="categoryId">Id категории.</param>
  /// <returns>Категория, может быть пустой.</returns>
  Domain.Entities.Category? GetById(long categoryId);

  /// <summary>
  /// Получить категорию по имени.
  /// </summary>
  /// <param name="userId">Id пользователя, которому принадлежит категория. Null - системная категория.</param>
  /// <param name="name">Имя категории.</param>
  /// <returns>Категория, может быть пустой.</returns>
  Domain.Entities.Category? GetByName(long? userId, string name);
}