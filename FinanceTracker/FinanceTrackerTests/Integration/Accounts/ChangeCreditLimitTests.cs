using System.Net;
using System.Net.Http.Json;
using FinanceTracker.API.Contracts.Accounts;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;
using FinanceTrackerTests.Integration.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerTests.Integration.Accounts;

public class ChangeCreditLimitTests : IntegrationTestBase
{
    [Test]
    public async Task ChangeCreditLimit_CreditAccount_ReturnsOk()
    {
        // Arrange
        var accountId = await CreateCreditAccount();


        var request = new ChangeCreditLimitRequest
        {
            AccountId = accountId,
            NewCreditLimit = 5000
        };


        // Act
        var response = await Client.PostAsJsonAsync(
            "/api/accounts/change-credit-limit",
            request);


        // Assert
        Assert.That(
            response.StatusCode,
            Is.EqualTo(HttpStatusCode.OK));


        await ExecuteDbContextAsync(async db =>
        {
            var account = await db.Accounts
                .OfType<CreditAccount>()
                .FirstAsync();


            Assert.That(
                account.CreditLimit,
                Is.EqualTo(5000));
        });
    }


    [Test]
    public async Task ChangeCreditLimit_DebitAccount_ReturnsBadRequest()
    {
        // Arrange
        var accountId = await CreateDebitAccount();


        var request = new ChangeCreditLimitRequest
        {
            AccountId = accountId,
            NewCreditLimit = 5000
        };


        // Act
        var response = await Client.PostAsJsonAsync(
            "/api/accounts/change-credit-limit",
            request);


        // Assert
        Assert.That(
            response.StatusCode,
            Is.EqualTo(HttpStatusCode.BadRequest));


        await ExecuteDbContextAsync(async db =>
        {
            var account = await db.Accounts
                .FirstAsync();


            Assert.That(
                account,
                Is.TypeOf<DebitAccount>());
        });
    }


    private async Task<long> CreateCreditAccount()
    {
        var request = new CreateAccountRequest
        {
            UserId = 1,
            Name = "Credit account",
            AccountType = AccountType.Credit,
            Currency = Currency.EUR,
            InitialBalance = 0,
            CreditLimit = 1000
        };


        var response = await Client.PostAsJsonAsync(
            "/api/accounts",
            request);


        Assert.That(
            response.StatusCode,
            Is.EqualTo(HttpStatusCode.OK));


        return await GetAccountId();
    }


    private async Task<long> CreateDebitAccount()
    {
        var request = new CreateAccountRequest
        {
            UserId = 1,
            Name = "Debit account",
            AccountType = AccountType.Debit,
            Currency = Currency.EUR,
            InitialBalance = 0
        };


        var response = await Client.PostAsJsonAsync(
            "/api/accounts",
            request);


        Assert.That(
            response.StatusCode,
            Is.EqualTo(HttpStatusCode.OK));


        return await GetAccountId();
    }


    private async Task<long> GetAccountId()
    {
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