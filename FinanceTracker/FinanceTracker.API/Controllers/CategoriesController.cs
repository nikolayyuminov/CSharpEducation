using FinanceTracker.API.Contracts.Categories;
using FinanceTracker.API.Mappers;
using FinanceTracker.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.API.Controllers;

/// <summary>
/// Контроллер для работы с категориями.
/// </summary>
[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
  #region Поля и свойства

  /// <summary>
  /// Сервис для работы с категориями.
  /// </summary>
  private readonly ICategoryService _categoryService;

  #endregion

  #region Методы

  /// <summary>
  /// Создать категорию. 
  /// </summary>
  /// <param name="request">Запрос пользователя для создания категории.</param>
  /// <returns>Результат запроса.</returns>
  [HttpPost]
  public ActionResult Create([FromBody] CreateCategoryRequest request)
  {
    var command = CategoryMapper.ToCreateCategoryCommand(request);

    var result = _categoryService.CreateCategory(command);

    if (result.HasErrors)
      return BadRequest(result.Errors);

    return Ok();
  }
  
  /// <summary>
  /// Переименовать категорию. 
  /// </summary>
  /// <param name="request">Запрос пользователя для переименования категории.</param>
  /// <returns>Результат запроса.</returns>
  [HttpPost("rename")]
  public ActionResult Rename([FromBody] RenameCategoryRequest request)
  {
    var command = CategoryMapper.ToRenameCategoryCommand(request);

    var result = _categoryService.RenameCategory(command);

    if (result.HasErrors)
      return BadRequest(result.Errors);

    return Ok();
  }
  
  /// <summary>
  /// Изменить описание категории. 
  /// </summary>
  /// <param name="request">Запрос пользователя для изменения описания категории.</param>
  /// <returns>Результат запроса.</returns>
  [HttpPost("change-category")]
  public ActionResult ChangeDescription([FromBody] ChangeDescriptionRequest request)
  {
    var command = CategoryMapper.ToChangeDescriptionCommand(request);

    var result = _categoryService.ChangeDescriptionCategory(command);

    if (result.HasErrors)
      return BadRequest(result.Errors);

    return Ok();
  }

  /// <summary>
  /// Архивировать категорию. 
  /// </summary>
  /// <param name="request">Запрос пользователя для архивирования категории.</param>
  /// <returns>Результат запроса.</returns>
  [HttpPost("archive")]
  public ActionResult Archive([FromBody] ArchiveCategoryRequest request)
  {
    var command = CategoryMapper.ToArchiveCategoryCommand(request);

    var result = _categoryService.ArchiveCategory(command);

    if (result.HasErrors)
      return BadRequest(result.Errors);

    return Ok();
  }
  #endregion
  
  #region Конструкторы

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="categoryService">Сервис работы с категориями.</param>
  public CategoriesController(ICategoryService categoryService)
  {
    _categoryService = categoryService;
  }

  #endregion
}