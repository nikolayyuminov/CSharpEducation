using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;

namespace FinanceTrackerTests.Domain;

[TestFixture]
public class DebitAccountTests
{
    private DebitAccount _debitAccount;
    private const long UserId = 1;
    private const string AccountName = "Debit Account";
    private const AccountType AccountType = FinanceTracker.Domain.Enums.AccountType.Debit;
    private const Currency CurrencyCode = Currency.RUB;

    [SetUp]
    public void Setup()
    {
        _debitAccount = new DebitAccount(UserId, AccountName, AccountType, CurrencyCode);
    }

    [Test]
    public void Constructor_WithDefaultBalance_ShouldSetBalanceToZero()
    {
        // Arrange & Act
        var account = new DebitAccount(UserId, AccountName, AccountType, CurrencyCode);

        // Assert
        Assert.That(account.Balance, Is.EqualTo(0));
        Assert.That(account.AccountType, Is.EqualTo(AccountType.Debit));
    }

    [Test]
    public void Constructor_WithValidBalance_ShouldSetBalanceCorrectly()
    {
        // Arrange
        const decimal initialBalance = 500.75m;

        // Act
        var account = new DebitAccount(UserId, AccountName, AccountType, CurrencyCode, initialBalance);

        // Assert
        Assert.That(account.Balance, Is.EqualTo(initialBalance));
    }

    [Test]
    public void Constructor_WithNegativeBalance_ShouldThrowInvalidOperationException()
    {
        // Arrange & Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
            new DebitAccount(UserId, AccountName, AccountType, CurrencyCode, -100));
    }

    [Test]
    public void Withdraw_WithSufficientFunds_ShouldDecreaseBalance()
    {
        // Arrange
        _debitAccount.Deposit(500);
        const decimal withdrawAmount = 200;

        // Act
        _debitAccount.Withdraw(withdrawAmount);

        // Assert
        Assert.That(_debitAccount.Balance, Is.EqualTo(300));
    }

    [Test]
    public void Withdraw_WithExactBalance_ShouldSetBalanceToZero()
    {
        // Arrange
        _debitAccount.Deposit(100);

        // Act
        _debitAccount.Withdraw(100);

        // Assert
        Assert.That(_debitAccount.Balance, Is.EqualTo(0));
    }

    [Test]
    public void Withdraw_WithInsufficientFunds_ShouldThrowInvalidOperationException()
    {
        // Arrange
        _debitAccount.Deposit(50);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _debitAccount.Withdraw(100));
    }

    [Test]
    public void Withdraw_NegativeAmount_ShouldThrowInvalidOperationException()
    {
        // Arrange
        _debitAccount.Deposit(100);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _debitAccount.Withdraw(-50));
    }

    [Test]
    public void Withdraw_WhenAccountClosed_ShouldThrowInvalidOperationException()
    {
        // Arrange
        _debitAccount.Deposit(100);
        _debitAccount.Close();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _debitAccount.Withdraw(50));
    }
}