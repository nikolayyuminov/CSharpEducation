using FinanceTracker.API.Contracts.Accounts;
using FinanceTracker.API.Mappers;
using FinanceTracker.Application.Abstractions.Services;
using FinanceTracker.Application.Accounts.Queries.GetAccounts;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.API.Controllers;

/// <summary>
/// Контроллер для работы со счетами.
/// </summary>
[ApiController]
[Route("api/accounts")]
public class AccountsController : ControllerBase
{
  #region Поля и свойства

  /// <summary>
  /// Сервис для работы со счетами.
  /// </summary>
  private readonly IAccountService _accountService;
  
  /// <summary>
  /// Кверик для чтения счетов.
  /// </summary>
  private readonly IAccountQueries _accountQueries;

  #endregion

  #region Методы

  /// <summary>
  /// Создать счет. 
  /// </summary>
  /// <param name="request">Запрос пользователя для создания счета.</param>
  /// <returns>Результат запроса.</returns>
  [HttpPost]
  public ActionResult Create([FromBody] CreateAccountRequest request)
  {
    var command = AccountMapper.ToCreateAccountCommand(request);

    var result = _accountService.CreateAccount(command);

    if (result.HasErrors)
      return BadRequest(result.Errors);

    return Ok();
  }
  
  /// <summary>
  /// Переименовать счет. 
  /// </summary>
  /// <param name="request">Запрос пользователя для создания счета.</param>
  /// <returns>Результат запроса.</returns>
  [HttpPost("rename")]
  public ActionResult Rename([FromBody] RenameAccountRequest request)
  {
    var command = AccountMapper.ToRenameAccountCommand(request);

    var result = _accountService.RenameAccount(command);

    if (result.HasErrors)
      return BadRequest(result.Errors);

    return Ok();
  }
  
  /// <summary>
  /// Закрыть счет. 
  /// </summary>
  /// <param name="request">Запрос пользователя для закрытия счета.</param>
  /// <returns>Результат запроса.</returns>
  [HttpPost("close")]
  public ActionResult Close([FromBody] CloseAccountRequest request)
  {
    var command = AccountMapper.ToCloseAccountCommand(request);

    var result = _accountService.CloseAccount(command);

    if (result.HasErrors)
      return BadRequest(result.Errors);

    return Ok();
  }
  
  /// <summary>
  /// Изменить кредитный лимит счета. 
  /// </summary>
  /// <param name="request">Запрос пользователя для изменения кредитного лимита.</param>
  /// <returns>Результат запроса.</returns>
  [HttpPost("change-credit-limit")]
  public ActionResult ChangeCreditLimit([FromBody] ChangeCreditLimitRequest request)
  {
    var command = AccountMapper.ToChangeCreditLimitCommand(request);

    var result = _accountService.ChangeCreditLimit(command);

    if (result.HasErrors)
      return BadRequest(result.Errors);

    return Ok();
  }
  
  /// <summary>
  /// Получить список счетов пользователя.
  /// </summary>
  /// <param name="userId">Id пользователя.</param>
  /// <returns>Список счетов.</returns>
  [HttpGet]
  public ActionResult<IReadOnlyCollection<AccountListItemDto>> GetAll()
  {
    // TODO: заменить на получение пользователя из авторизации.
    const long userId = 1;
    
    return Ok(_accountQueries.GetAll(userId));
  }

  #endregion

  #region Конструкторы

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="accountService">Сервис работы со счетами.</param>
  public AccountsController(IAccountService accountService, IAccountQueries accountQueries)
  {
    _accountService = accountService;
    _accountQueries = accountQueries;
  }

  #endregion
}