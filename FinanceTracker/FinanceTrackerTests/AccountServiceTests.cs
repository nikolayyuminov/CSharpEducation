using FinanceTracker.Application.Abstractions.Factories;
using FinanceTracker.Application.Abstractions.Repositories;
using FinanceTracker.Application.Abstractions.Services;
using FinanceTracker.Application.Accounts.Commands;
using FinanceTracker.Application.Accounts.Services;
using FinanceTracker.Application.Accounts.Validators;
using FinanceTracker.Application.Factories;
using FinanceTracker.Domain.Enums;
using FinanceTracker.Infrastructure.Repositories;

namespace FinanceTrackerTests;

public class AccountServiceTests
{
  private IAccountService _accountService;
  private IAccountRepository _accountRepository;
  private IAccountFactory _accountFactory;

  private CreateAccountCommand CreateValidCommand(
    string? name = "Test",
    AccountType accountType = AccountType.Debit,
    decimal initialBalance = 0,
    Currency currency = Currency.EUR,
    decimal? creditLimit = null,
    long userId = 1)
  {
    return new CreateAccountCommand
    {
      Name = name,
      AccountType = accountType,
      InitialBalance = initialBalance,
      CreditLimit = creditLimit,
      Currency = currency,
      UserId = userId
    };
  }

  [SetUp]
  public void Setup()
  {
    _accountRepository = new AccountRepository();
    _accountFactory = new AccountFactory();
    _accountService = new AccountService(_accountRepository, new CreateAccountValidator(), _accountFactory);
  }
  
  [Test]
  public void CreateAccount_ValidCommand_ReturnsSuccess()
  {
    // Arrange 
    var command = CreateValidCommand();
    
    // Act
    var result = _accountService.CreateAccount(command);
    
    // Assert
    Assert.That(result.HasErrors, Is.False);
  }

  [Test]
  public void CreateAccount_InvalidCommand_DoNotAddAccountInRepository()
  {
    // Arrange
    var command = CreateValidCommand(initialBalance: -100);
    
    // Act
    var result = _accountService.CreateAccount(command);
    using (Assert.EnterMultipleScope())
    {
      // Assert
      Assert.That(result.HasErrors, Is.True);
      Assert.That(_accountRepository.ExistsWithName(command.UserId, command.Name), Is.False);
    }
  }

  [Test]
  public void CreateAccount_DuplicateName_ReturnsValidationError()
  {
    // Arrange 
    var command = CreateValidCommand();
    _accountService.CreateAccount(command);
    
    // Act
    var result = _accountService.CreateAccount(command);
    using (Assert.EnterMultipleScope())
    {
      // Assert
      Assert.That(result.HasErrors, Is.True);
    }
  }
}