using FinanceTracker.API.Contracts.Categories;
using FinanceTracker.API.Mappers;
using FinanceTracker.Application.Abstractions.Queries;
using FinanceTracker.Application.Abstractions.Services;
using FinanceTracker.Application.Categories.Queries.GetCategories;
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
  
  /// <summary>
  /// Запросы для получения категорий.
  /// </summary>
  private readonly ICategoryQueries _categoryQueries;

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
  
  /// <summary>
  /// Получить категории пользователя и системные категории.
  /// </summary>
  /// <param name="userId"> Id пользователя. </param>
  /// <returns> Коллекция категорий. </returns>
  [HttpGet]
  public ActionResult<IReadOnlyCollection<CategoryListItemDto>> GetAll()
  {
    // TODO: заменить на получение пользователя из авторизации.
    const long userId = 1;
    
    return Ok(_categoryQueries.GetAll(userId));
  }
  #endregion
  
  #region Конструкторы

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="categoryService">Сервис работы с категориями.</param>
  /// <param name="categoryQueries">Запросы для получения категорий.</param>
  public CategoriesController(ICategoryService categoryService, ICategoryQueries categoryQueries)
  {
    _categoryService = categoryService;
    _categoryQueries = categoryQueries;
  }

  #endregion
}