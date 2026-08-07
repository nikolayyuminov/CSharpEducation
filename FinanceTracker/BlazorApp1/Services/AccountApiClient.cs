using System.Net.Http.Json;
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
    var result = await _httpClient.GetFromJsonAsync<List<AccountModel>>
    (
      $"api/accounts"
    );

    return result ?? [];
  }
  
  /// <summary>
  /// Создать новый счет.
  /// </summary>
  /// <param name="model">Модель создания счета.</param>
  public async Task CreateAccountAsync(CreateAccountModel model)
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

    var response = await _httpClient.PostAsJsonAsync(
      "api/accounts",
      request);

    
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