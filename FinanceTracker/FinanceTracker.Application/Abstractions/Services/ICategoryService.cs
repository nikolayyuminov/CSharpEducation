using FinanceTracker.Application.Categories.Commands;
using FinanceTracker.Application.Common.Validation;

namespace FinanceTracker.Application.Abstractions.Services;

/// <summary>
/// Интерфейс сервиса для работы с категориями.
/// </summary>
public interface ICategoryService
{
  /// <summary>
  /// Создание категории.
  /// </summary>
  /// <param name="command">Команда от пользователя для создания новой категории.</param>
  /// <returns>Коллекция сообщений об ошибках.</returns>
  ValidationResult CreateCategory(CreateCategoryCommand command);
}