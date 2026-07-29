using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;

namespace FinanceTrackerTests;

[TestFixture]
    public class CreditAccountTests
    {
        private CreditAccount _creditAccount;
        private const long UserId = 1;
        private const string AccountName = "Credit Account";
        private const Currency CurrencyCode = Currency.USD;
        private const decimal CreditLimitAmount = 1000;

        [SetUp]
        public void Setup()
        {
            _creditAccount = new CreditAccount(UserId, AccountName, CurrencyCode, CreditLimitAmount);
        }

        [Test]
        public void Constructor_WithValidParameters_ShouldInitializeCorrectly()
        {
            // Arrange & Act
            var account = new CreditAccount(UserId, AccountName, CurrencyCode, CreditLimitAmount);

            // Assert
            Assert.That(account.AccountType, Is.EqualTo(AccountType.Credit));
            Assert.That(account.CreditLimit, Is.EqualTo(CreditLimitAmount));
            Assert.That(account.Balance, Is.EqualTo(0));
        }

        [Test]
        public void Constructor_WithBalanceWithinLimit_ShouldSetBalanceCorrectly()
        {
            // Arrange
            const decimal initialBalance = -500m;

            // Act
            var account = new CreditAccount(UserId, AccountName, CurrencyCode, CreditLimitAmount, initialBalance);

            // Assert
            Assert.That(account.Balance, Is.EqualTo(initialBalance));
        }

        [Test]
        public void Constructor_WithBalanceExceedingLimit_ShouldThrowInvalidOperationException()
        {
            // Arrange & Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                new CreditAccount(UserId, AccountName, CurrencyCode, CreditLimitAmount, -1500));
        }

        [Test]
        public void Constructor_WithZeroCreditLimit_ShouldSetCreditLimitToZero()
        {
            // Arrange & Act
            var account = new CreditAccount(UserId, AccountName, CurrencyCode, 0);

            // Assert
            Assert.That(account.CreditLimit, Is.EqualTo(0));
            Assert.That(account.Balance, Is.EqualTo(0));
        }

        [Test]
        public void Constructor_WithNullCreditLimit_ShouldThrowInvalidOperationException()
        {
            // Arrange & Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                new CreditAccount(UserId, AccountName, CurrencyCode, null));
        }

        [Test]
        public void Withdraw_WithinCreditLimit_ShouldDecreaseBalance()
        {
            // Arrange
            const decimal withdrawAmount = 500;

            // Act
            _creditAccount.Withdraw(withdrawAmount);

            // Assert
            Assert.That(_creditAccount.Balance, Is.EqualTo(-500));
        }

        [Test]
        public void Withdraw_ExactCreditLimit_ShouldSetBalanceToNegativeLimit()
        {
            // Arrange
            const decimal withdrawAmount = CreditLimitAmount;

            // Act
            _creditAccount.Withdraw(withdrawAmount);

            // Assert
            Assert.That(_creditAccount.Balance, Is.EqualTo(-CreditLimitAmount));
        }

        [Test]
        public void Withdraw_ExceedingCreditLimit_ShouldThrowInvalidOperationException()
        {
            // Arrange
            const decimal withdrawAmount = CreditLimitAmount + 100;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _creditAccount.Withdraw(withdrawAmount));
        }

        [Test]
        public void Withdraw_AfterDeposit_ShouldCorrectlyTrackBalance()
        {
            // Arrange
            _creditAccount.Deposit(200);
            const decimal withdrawAmount = 800;

            // Act
            _creditAccount.Withdraw(withdrawAmount);

            // Assert
            Assert.That(_creditAccount.Balance, Is.EqualTo(-600));
        }

        [Test]
        public void ChangeCreditLimit_ShouldUpdateCreditLimit()
        {
            // Arrange
            const decimal newLimit = 2000;

            // Act
            _creditAccount.ChangeCreditLimit(newLimit);

            // Assert
            Assert.That(_creditAccount.CreditLimit, Is.EqualTo(newLimit));
        }

        [Test]
        public void ChangeCreditLimit_ToNull_ShouldThrowInvalidOperationException()
        {
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _creditAccount.ChangeCreditLimit(null));
        }

        [Test]
        public void ChangeCreditLimit_ToNegative_ShouldThrowInvalidOperationException()
        {
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _creditAccount.ChangeCreditLimit(-100));
        }

        [Test]
        public void ChangeCreditLimit_WhenAccountClosed_ShouldThrowInvalidOperationException()
        {
            // Arrange
            _creditAccount.Close();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _creditAccount.ChangeCreditLimit(1500));
        }

        [Test]
        public void Deposit_OnCreditAccount_ShouldIncreaseBalance()
        {
            // Arrange
            _creditAccount.Withdraw(300);
            const decimal depositAmount = 100;

            // Act
            _creditAccount.Deposit(depositAmount);

            // Assert
            Assert.That(_creditAccount.Balance, Is.EqualTo(-200));
        }
    }