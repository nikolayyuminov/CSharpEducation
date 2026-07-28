using FinanceTracker.Application.Accounts.Commands;
using FinanceTracker.Application.Accounts.Validators;
using FinanceTracker.Domain.Enums;

namespace FinanceTrackerTests;

public class CreateAccountValidatorTests
{
  private readonly CreateAccountValidator _validator = new();

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

  [Test]
  public void EmptyName_ReturnsError()
  {
    // Arrange 
    var command = CreateValidCommand(name: "");
    
    // Act
    var result = _validator.Validate(command);
    var nameError = result.Errors.FirstOrDefault(e => e.PropertyName == nameof(command.Name) 
                                                                && e.ErrorMessage.Contains("не может быть пустым"));
    
    // Assert
    Assert.That(nameError, Is.Not.Null);
  }

  [Test]
  public void NegativeBalanceForDebit_ReturnsError()
  {
    // Arrange 
    var command = CreateValidCommand(initialBalance: -100);
    
    // Act
    var result = _validator.Validate(command);
    var initialBalanceError = result.Errors.FirstOrDefault(e => e.PropertyName == nameof(command.InitialBalance));
    
    // Assert
    Assert.That(initialBalanceError, Is.Not.Null);
  }

  [Test]
  public void CreditLimitRequiredForCreditAccount_ReturnsError()
  {
    // Arrange 
    var command = CreateValidCommand(accountType: AccountType.Credit, creditLimit: null);
    
    // Act
    var result = _validator.Validate(command);
    var creditLimitError = result.Errors.FirstOrDefault(e => e.PropertyName == nameof(command.CreditLimit) 
                                                             && e.ErrorMessage.Contains(" не может быть пустым"));
    
    // Assert
    Assert.That(creditLimitError, Is.Not.Null);
  }

  [Test]
  public void ValidCommand_ReturnsSuccess()
  {
    // Arrange 
    var validCommand = CreateValidCommand();
    
    // Act
    var result = _validator.Validate(validCommand);
    using (Assert.EnterMultipleScope())
    {
        // Assert
        Assert.That(result.HasErrors, Is.False);
        Assert.That(result.Errors, Is.Empty);
    }
  } 
}