using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;

namespace FinanceTrackerTests.Domain;

[TestFixture]
    public class DepositAccountTests
    {
        private DepositAccount _depositAccount;
        private const long UserId = 1;
        private const string AccountName = "Deposit Account";
        private const Currency CurrencyCode = Currency.USD;

        [SetUp]
        public void Setup()
        {
            _depositAccount = new DepositAccount(UserId, AccountName, CurrencyCode);
        }

        [Test]
        public void Constructor_WithDefaultBalance_ShouldSetBalanceToZero()
        {
            // Arrange & Act
            var account = new DepositAccount(UserId, AccountName, CurrencyCode);

            // Assert
            Assert.That(account.Balance, Is.EqualTo(0));
            Assert.That(account.AccountType, Is.EqualTo(AccountType.Deposit));
        }

        [Test]
        public void Constructor_WithValidBalance_ShouldSetBalanceCorrectly()
        {
            // Arrange
            const decimal initialBalance = 1000.50m;

            // Act
            var account = new DepositAccount(UserId, AccountName, CurrencyCode, initialBalance);

            // Assert
            Assert.That(account.Balance, Is.EqualTo(initialBalance));
        }

        [Test]
        public void Constructor_WithNegativeBalance_ShouldThrowInvalidOperationException()
        {
            // Arrange & Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                new DepositAccount(UserId, AccountName, CurrencyCode, -100));
        }

        [Test]
        public void Withdraw_WithSufficientFunds_ShouldDecreaseBalance()
        {
            // Arrange
            _depositAccount.Deposit(500);
            const decimal withdrawAmount = 200;

            // Act
            _depositAccount.Withdraw(withdrawAmount);

            // Assert
            Assert.That(_depositAccount.Balance, Is.EqualTo(300));
        }

        [Test]
        public void Withdraw_WithExactBalance_ShouldSetBalanceToZero()
        {
            // Arrange
            _depositAccount.Deposit(100);

            // Act
            _depositAccount.Withdraw(100);

            // Assert
            Assert.That(_depositAccount.Balance, Is.EqualTo(0));
        }

        [Test]
        public void Withdraw_WithInsufficientFunds_ShouldThrowInvalidOperationException()
        {
            // Arrange
            _depositAccount.Deposit(50);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _depositAccount.Withdraw(100));
        }

        [Test]
        public void Withdraw_NegativeAmount_ShouldThrowInvalidOperationException()
        {
            // Arrange
            _depositAccount.Deposit(100);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _depositAccount.Withdraw(-50));
        }

        [Test]
        public void Withdraw_WhenAccountClosed_ShouldThrowInvalidOperationException()
        {
            // Arrange
            _depositAccount.Deposit(100);
            _depositAccount.Close();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _depositAccount.Withdraw(50));
        }

        [Test]
        public void Deposit_ShouldWorkLikeBaseAccount()
        {
            // Arrange
            const decimal depositAmount = 250;

            // Act
            _depositAccount.Deposit(depositAmount);

            // Assert
            Assert.That(_depositAccount.Balance, Is.EqualTo(depositAmount));
        }
    }