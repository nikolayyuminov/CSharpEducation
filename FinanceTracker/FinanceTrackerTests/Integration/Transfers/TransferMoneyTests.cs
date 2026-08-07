using System.Net;
using System.Net.Http.Json;
using FinanceTracker.API.Contracts.Transfers;
using FinanceTracker.API.Contracts.Accounts;
using FinanceTracker.Domain.Enums;
using FinanceTrackerTests.Integration.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerTests.Integration.Transfers;

public class TransferMoneyTests : IntegrationTestBase
{
    [Test]
    public async Task TransferMoney_ValidRequest_ReturnsOk()
    {
        // Arrange
        var fromAccountId = await CreateAccount(
            "Source account",
            1000);

        var toAccountId = await CreateAccount(
            "Target account",
            100);


        var request = new TransferMoneyRequest
        {
            FromAccountId = fromAccountId,
            ToAccountId = toAccountId,
            Amount = 300,
            Description = "Test transfer"
        };


        // Act
        var response = await Client.PostAsJsonAsync(
            "/api/transfers",
            request);
        
        // Assert
        Assert.That(
            response.StatusCode,
            Is.EqualTo(HttpStatusCode.OK));


        await ExecuteDbContextAsync(async db =>
        {
            var fromAccount = await db.Accounts
                .FindAsync(fromAccountId);

            var toAccount = await db.Accounts
                .FindAsync(toAccountId);


            Assert.That(
                fromAccount!.Balance,
                Is.EqualTo(700));


            Assert.That(
                toAccount!.Balance,
                Is.EqualTo(400));
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
                .Where(x => x.Name == name)
                .Select(x => x.Id)
                .FirstAsync();
        });


        return id;
    }
}