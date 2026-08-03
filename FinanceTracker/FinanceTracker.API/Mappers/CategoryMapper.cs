using FinanceTracker.API.Contracts.Categories;
using FinanceTracker.Application.Categories.Commands;

namespace FinanceTracker.API.Mappers;

/// <summary>
/// Маппер для преобразования моделей категорий между API и слоем Application.
/// </summary>
public static class CategoryMapper
{
  /// <summary>
  /// Преобразовать HTTP-запрос на создание категории
  /// в команду слоя Application.
  /// </summary>
  /// <param name="request">Запрос пользователя.</param>
  /// <returns>Команда создания категории.</returns>
  public static CreateCategoryCommand ToCreateCategoryCommand(CreateCategoryRequest request)
  {
    return new CreateCategoryCommand()
    {
      UserId = request.UserId,
      Name = request.Name,
      Description =  request.Description,
      CategoryKind =  request.CategoryKind
    };
  }
  
  /// <summary>
  /// Преобразовать HTTP-запрос на переименование категории
  /// в команду слоя Application.
  /// </summary>
  /// <param name="request">Запрос пользователя.</param>
  /// <returns>Команда переименования категории.</returns>
  public static RenameCategoryCommand ToRenameCategoryCommand(RenameCategoryRequest request)
  {
    return new RenameCategoryCommand()
    {
      UserId = request.UserId,
      CategoryId =  request.CategoryId,
      NewName =  request.NewName
    };
  }
  
  /// <summary>
  /// Преобразовать HTTP-запрос на изменение описания категории
  /// в команду слоя Application.
  /// </summary>
  /// <param name="request">Запрос пользователя.</param>
  /// <returns>Команда изменения описания категории.</returns>
  public static ChangeDescriptionCommand ToChangeDescriptionCommand(ChangeDescriptionRequest request)
  {
    return new ChangeDescriptionCommand()
    {
      CategoryId =  request.CategoryId,
      NewDescription =  request.NewDescription
    };
  }
  
  /// <summary>
  /// Преобразовать HTTP-запрос на архивирование категории
  /// в команду слоя Application.
  /// </summary>
  /// <param name="request">Запрос пользователя.</param>
  /// <returns>Команда архивирования категории.</returns>
  public static ArchiveCategoryCommand ToArchiveCategoryCommand(ArchiveCategoryRequest request)
  {
    return new ArchiveCategoryCommand()
    {
      CategoryId =  request.CategoryId
    };
  }
}