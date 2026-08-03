using FinanceTracker.API.Contracts.Accounts;
using FinanceTracker.API.Mappers;
using FinanceTracker.Application.Abstractions.Services;
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

  #endregion

  #region Методы

  /// <summary>
  /// Создать счет. 
  /// </summary>
  /// <param name="request">Запрос пользователя для создания счета.</param>
  /// <returns>Результат запроса.</returns>
  [HttpPost]
  public IActionResult Create(CreateAccountRequest request)
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
  [HttpPost]
  public IActionResult Rename(RenameAccountRequest request)
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
  [HttpPost]
  public IActionResult Close(CloseAccountRequest request)
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
  [HttpPost]
  public IActionResult ChangeCreditLimit(ChangeCreditLimitRequest request)
  {
    var command = AccountMapper.ToChangeCreditLimitCommand(request);

    var result = _accountService.ChangeCreditLimit(command);

    if (result.HasErrors)
      return BadRequest(result.Errors);

    return Ok();
  }

  #endregion

  #region Конструкторы

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="accountService">Сервис работы со счетами.</param>
  public AccountsController(IAccountService accountService)
  {
    _accountService = accountService;
  }

  #endregion
}