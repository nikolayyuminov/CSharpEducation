using System.Net;
using System.Net.Http.Json;
using FinanceTracker.API.Contracts.Accounts;
using FinanceTracker.Domain.Enums;
using FinanceTrackerTests.Integration.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerTests.Integration.Accounts;

public class RenameAccountTests : IntegrationTestBase
{
  [Test]
  public async Task RenameAccount_ValidRequest_ReturnsOk()
  {
    // Arrange
    var createRequest = new CreateAccountRequest
    {
      UserId = 1,
      Name = "Old name",
      AccountType = AccountType.Debit,
      Currency = Currency.EUR,
      InitialBalance = 1000
    };


    var createResponse = await Client.PostAsJsonAsync(
      "/api/accounts",
      createRequest);


    Assert.That(
      createResponse.StatusCode,
      Is.EqualTo(HttpStatusCode.OK));


    long accountId = 0;


    await ExecuteDbContextAsync(async db =>
    {
      var account = await db.Accounts
        .FirstAsync();

      accountId = account.Id;
    });


    var renameRequest = new RenameAccountRequest
    {
      UserId = 1,
      AccountId = accountId,
      NewName = "New name"
    };


    // Act
    var response = await Client.PostAsJsonAsync(
      "/api/accounts/rename",
      renameRequest);


    // Assert
    Assert.That(
      response.StatusCode,
      Is.EqualTo(HttpStatusCode.OK));


    await ExecuteDbContextAsync(async db =>
    {
      var account = await db.Accounts
        .FirstAsync();


      Assert.That(account.Name,
        Is.EqualTo("New name"));
    });
  }
}