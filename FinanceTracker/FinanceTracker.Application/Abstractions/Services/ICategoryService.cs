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
    
  /// <summary>
  /// Переименовать категорию.
  /// </summary>
  /// <param name="command">Команда от пользователя на переименование категории.</param>
  /// <returns>Ошибки при переименовании. Если ошибок нет, категория переименовалась успешно.</returns>
  ValidationResult RenameCategory(RenameCategoryCommand command);
  
  /// <summary>
  /// Архивирование категории.
  /// </summary>
  /// <param name="command">Команда от пользователя на архивирование категории.</param>
  /// <returns>Ошибки при переименовании. Если ошибок нет, категория архивировалась успешно.</returns>
  ValidationResult ArchiveCategory(ArchiveCategoryCommand command);

  /// <summary>
  /// Изменение описания категории.
  /// </summary>
  /// <param name="command">Команда от пользователя на изменение описания категории.</param>
  /// <returns>Ошибки при изменении описания. Если ошибок нет, описание изменено успешно.</returns>
  ValidationResult ChangeDescriptionCategory(ChangeDescriptionCommand command);
}