using FinanceTracker.Application.Abstractions.Repositories;
using FinanceTracker.Application.Abstractions.Services;
using FinanceTracker.Application.Abstractions.Validation;
using FinanceTracker.Application.Categories.Commands;
using FinanceTracker.Application.Common.Validation;
using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Categories.Services;

/// <summary>
/// Сервис для работы с категориями.
/// </summary>
public class CategoryService : ICategoryService
{
  #region Поля и свойтсва

  /// <summary>
  /// Репозиторий категорий.
  /// </summary>
  private readonly ICategoryRepository _categoryRepository;
  
  /// <summary>
  /// Валидатор создания новой категории.
  /// </summary>
  private readonly IValidator<CreateCategoryCommand> _createCategoryValidator;

  #endregion

  #region Методы

  /// <summary>
  /// Создание категории.
  /// </summary>
  /// <param name="command">Команда от пользователя для создания новой категории.</param>
  /// <returns>Коллекция сообщений об ошибках.</returns>
  public ValidationResult CreateCategory(CreateCategoryCommand command)
  {
    var validationResult = _createCategoryValidator.Validate(command);

    if (validationResult.HasErrors) return validationResult;
    
    var existingCategory = _categoryRepository.GetByName(command.UserId, command.Name);

    if (existingCategory != null)
    {
      validationResult.AddError(new ValidationError(nameof(command.Name), "Категория с таким именем уже существует."));
      return  validationResult;
    }
    
    var category = new Category(command.Name, command.Description, command.UserId, command.CategoryKind);
    
    _categoryRepository.Add(category);
    
    return validationResult;
  }

  #endregion

  #region Конструкторы

  public CategoryService(ICategoryRepository categoryRepository, 
                          IValidator<CreateCategoryCommand> createCategoryValidator)
  {
    _categoryRepository = categoryRepository;
    _createCategoryValidator = createCategoryValidator;
  }

  #endregion
}