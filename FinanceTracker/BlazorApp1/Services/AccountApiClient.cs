using System.Text.Json;
using BlazorApp1.Models;
using FinanceTracker.API.Contracts.Accounts;
using FinanceTracker.Domain.Enums;

namespace BlazorApp1.Services;

/// <summary>
/// Клиент для работы со счетами.
/// </summary>
public class AccountApiClient
{
  #region Поля

  /// <summary>
  /// Http клиент.
  /// </summary>
  private readonly HttpClient _httpClient;

  /// <summary>
  /// Текущий пользователь.
  /// </summary>
  private readonly CurrentUserService _currentUser;

  #endregion

  #region Методы

  /// <summary>
  /// Получить счета текущего пользователя.
  /// </summary>
  public async Task<IReadOnlyCollection<AccountModel>> GetAccountsAsync()
  {
    var result = await _httpClient.GetFromJsonAsync<List<AccountModel>>($"api/accounts");

    return result ?? [];
  }
  
  /// <summary>
  /// Создать новый счет.
  /// </summary>
  /// <param name="model">Модель создания счета.</param>
  /// <returns>HTTP-ответ API.</returns>
  public async Task<HttpResponseMessage> CreateAccountAsync(CreateAccountModel model)
  {
    var request = new CreateAccountRequest
    {
      UserId = 1, // DemoUser
      Name = model.Name,
      AccountType = model.AccountType,
      Currency = model.Currency,
      InitialBalance = model.InitialBalance,
      CreditLimit = model.AccountType == AccountType.Credit
        ? model.CreditLimit
        : null
    };

    var response = await _httpClient.PostAsJsonAsync("api/accounts", request);

    if (!response.IsSuccessStatusCode)
    {
      var error = await response.Content.ReadAsStringAsync();
      var errorMessage = JsonSerializer.Deserialize<ValidationErrorModel[]>(error);
      throw new Exception(error);
    }

    return response;
  }

  /// <summary>
  /// Переименовать счет.
  /// </summary>
  public async Task RenameAccountAsync(long accountId, string newName)
  {
    var request = new RenameAccountRequest
    {
      UserId = 1, // DemoUser
      AccountId = accountId,
      NewName = newName
    };

    var response = await _httpClient.PostAsJsonAsync("api/accounts/rename", request);

    if (!response.IsSuccessStatusCode)
    {
      var error = await response.Content.ReadAsStringAsync();

      throw new Exception(error);
    }
  }
  
  /// <summary>
  /// Закрыть счет.
  /// </summary>
  /// <param name="accountId">Id счета.</param>
  public async Task CloseAccountAsync(long accountId)
  {
    var request = new CloseAccountRequest
    {
      AccountId = accountId
    };

    var response = await _httpClient.PostAsJsonAsync("api/accounts/close", request);

    if (!response.IsSuccessStatusCode)
    {
      var error = await response.Content.ReadAsStringAsync();

      throw new Exception(error);
    }
  }
  #endregion

  #region Конструкторы

  public AccountApiClient(
    IHttpClientFactory factory,
    CurrentUserService currentUser)
  {
    _httpClient = factory.CreateClient("FinanceTrackerApi");
    _currentUser = currentUser;
  }

  #endregion
}