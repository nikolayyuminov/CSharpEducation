using System.Net;
using System.Net.Http.Json;
using FinanceTracker.API.Contracts.Accounts;
using FinanceTracker.Domain.Enums;
using FinanceTrackerTests.Integration.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerTests.Integration.Accounts;

public class CloseAccountTests : IntegrationTestBase
{
    [Test]
    public async Task CloseAccount_ZeroBalance_ReturnsOk()
    {
        // Arrange
        var accountId = await CreateAccount(
            "Account",
            0);


        var request = new CloseAccountRequest
        {
            AccountId = accountId
        };


        // Act
        var response = await Client.PostAsJsonAsync(
            "/api/accounts/close",
            request);


        // Assert
        Assert.That(
            response.StatusCode,
            Is.EqualTo(HttpStatusCode.OK));


        await ExecuteDbContextAsync(async db =>
        {
            var account = await db.Accounts
                .FirstAsync();


            Assert.That(account.IsClosed, Is.True);
        });
    }


    [Test]
    public async Task CloseAccount_WithBalance_ReturnsBadRequest()
    {
        // Arrange
        var accountId = await CreateAccount(
            "Account",
            1000);


        var request = new CloseAccountRequest
        {
            AccountId = accountId
        };


        // Act
        var response = await Client.PostAsJsonAsync(
            "/api/accounts/close",
            request);


        // Assert
        Assert.That(
            response.StatusCode,
            Is.EqualTo(HttpStatusCode.BadRequest));


        await ExecuteDbContextAsync(async db =>
        {
            var account = await db.Accounts
                .FirstAsync();


            Assert.That(account.IsClosed, Is.False);
        });
    }


    private async Task<long> CreateAccount(
        string name,
        decimal balance)
    {
        var request = new CreateAccountRequest
        {
            UserId = 1,
            Name = name,
            AccountType = AccountType.Debit,
            Currency = Currency.EUR,
            InitialBalance = balance
        };


        var response = await Client.PostAsJsonAsync(
            "/api/accounts",
            request);


        Assert.That(
            response.StatusCode,
            Is.EqualTo(HttpStatusCode.OK));


        long id = 0;


        await ExecuteDbContextAsync(async db =>
        {
            id = await db.Accounts
                .Select(x => x.Id)
                .FirstAsync();
        });


        return id;
    }
}