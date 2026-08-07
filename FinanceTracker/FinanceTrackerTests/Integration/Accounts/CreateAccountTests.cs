using System.Net;
using System.Net.Http.Json;
using FinanceTracker.API.Contracts.Accounts;
using FinanceTracker.API;
using FinanceTracker.Domain.Enums;
using FinanceTrackerTests.Integration.Infrastructure;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerTests.Integration.Accounts;

public class CreateAccountTests : IntegrationTestBase
{
  [Test]
  public async Task CreateAccount_ValidRequest_ReturnsOk()
  {
    // Arrange
    var request = new CreateAccountRequest
    {
      UserId = 1,
      Name = "Main account",
      AccountType = AccountType.Debit,
      Currency = Currency.EUR,
      InitialBalance = 1000
    };

    // Act
    var response = await Client.PostAsJsonAsync(
      "/api/accounts",
      request);


    // Assert HTTP
    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    /*
    var content = await response.Content.ReadAsStringAsync();

    Assert.That(
      response.StatusCode,
      Is.EqualTo(HttpStatusCode.OK),
      $"Response body: {content}");
    */
    // Act&Assert
    await ExecuteDbContextAsync(async db =>
    {
      var account = await db.Accounts.FirstOrDefaultAsync();

      Assert.That(account, Is.Not.Null);

      Assert.That(account!.Name, Is.EqualTo("Main account"));

      Assert.That(account.Balance, Is.EqualTo(1000));
    });
  }
}