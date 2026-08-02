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
  
  /// <summary>
  /// Валидатор переименования категории.
  /// </summary>
  private readonly IValidator<RenameCategoryCommand> _renameCategoryValidator;
  
  /// <summary>
  /// Валидатор архивирования категории.
  /// </summary>
  private readonly IValidator<ArchiveCategoryCommand> _archiveCategoryValidator;
  
  /// <summary>
  /// Валидатор изменения описания категории.
  /// </summary>
  private readonly IValidator<ChangeDescriptionCommand> _changeDescriptionCategoryValidator;

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

  /// <summary>
  /// Переименовать категорию.
  /// </summary>
  /// <param name="command">Команда от пользователя на переименование категории.</param>
  /// <returns>Ошибки при переименовании. Если ошибок нет, категория переименовалась успешно.</returns>
  public ValidationResult RenameCategory(RenameCategoryCommand command)
  {
    var validationResult = _renameCategoryValidator.Validate(command);
    
    if (validationResult.HasErrors) return validationResult;
    
    var category = _categoryRepository.GetById(command.CategoryId);

    if (category == null)
    {
      validationResult.AddError(new ValidationError(nameof(command.CategoryId), "Категория не найдена."));
      return  validationResult;
    }
    
    var existingCategory  = _categoryRepository.GetByName(command.UserId, command.NewName!);
    
    if (existingCategory != null && existingCategory.Id != category.Id)
    {
      validationResult.AddError(new ValidationError(nameof(command.NewName), "Категория с таким именем уже существует."));
      return validationResult;
    }
    
    category.Rename(command.NewName!);
    
    return validationResult;
  }

  /// <summary>
  /// Архивирование категории.
  /// </summary>
  /// <param name="command">Команда от пользователя на архивирование категории.</param>
  /// <returns>Ошибки при переименовании. Если ошибок нет, категория архивировалась успешно.</returns>
  public ValidationResult ArchiveCategory(ArchiveCategoryCommand command)
  {
    var validationResult = _archiveCategoryValidator.Validate(command);
    
    if (validationResult.HasErrors) return validationResult;
    
    var category = _categoryRepository.GetById(command.CategoryId);
    if (category == null)
    {
      validationResult.AddError(new ValidationError(nameof(command.CategoryId), "Категория не найдена."));
      return validationResult;
    }
    
    category.Archive();
    
    return validationResult;
  }

  /// <summary>
  /// Изменение описания категории.
  /// </summary>
  /// <param name="command">Команда от пользователя на изменение описания категории.</param>
  /// <returns>Ошибки при изменении описания. Если ошибок нет, описание изменено успешно.</returns>
  public ValidationResult ChangeDescriptionCategory(ChangeDescriptionCommand command)
  {
    var validationResult = _changeDescriptionCategoryValidator.Validate(command);
    
    if (validationResult.HasErrors) return validationResult;
    
    var category = _categoryRepository.GetById(command.CategoryId);

    if (category == null)
    {
      validationResult.AddError(new ValidationError(nameof(command.CategoryId), "Категория не существует."));
      return  validationResult;
    }
    
    category.ChangeDescription(command.NewDescription);
    
    return validationResult;
  }

  #endregion

  #region Конструкторы

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="categoryRepository">Репозиторий категорий.</param>
  /// <param name="createCategoryValidator">Валидатор создания категории.</param>
  /// <param name="renameCategoryValidator">Валидатор переименования категории.</param>
  /// <param name="archiveCategoryValidator">Валидатор архивирования категории.</param>
  /// <param name="changeDescriptionCategoryValidator">Валидатор изменения описания категории.</param>
  public CategoryService(ICategoryRepository categoryRepository, 
                          IValidator<CreateCategoryCommand> createCategoryValidator, 
                          IValidator<RenameCategoryCommand> renameCategoryValidator, 
                          IValidator<ArchiveCategoryCommand> archiveCategoryValidator, 
                          IValidator<ChangeDescriptionCommand> changeDescriptionCategoryValidator)
  {
    _categoryRepository = categoryRepository;
    _createCategoryValidator = createCategoryValidator;
    _renameCategoryValidator = renameCategoryValidator;
    _archiveCategoryValidator = archiveCategoryValidator;
    _changeDescriptionCategoryValidator = changeDescriptionCategoryValidator;
  }

  #endregion
}