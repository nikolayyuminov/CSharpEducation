using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;

namespace FinanceTrackerTests.Domain
{
    [TestFixture]
    public class AccountTests
    {
        private TestAccount _account;
        private const long UserId = 1;
        private const string AccountName = "Test Account";
        private const Currency CurrencyCode = Currency.USD;

        private class TestAccount : Account
        {
            public TestAccount(long userId, string name, AccountType accountType, Currency currency)
                : base(userId, name, accountType, currency)
            {
            }

            public override void Withdraw(decimal amount)
            {
                EnsureAccountIsOpen();
                EnsurePositiveAmount(amount);
                Balance -= amount;
            }
        }

        [SetUp]
        public void Setup()
        {
            _account = new TestAccount(UserId, AccountName, AccountType.Debit, CurrencyCode);
        }

        [Test]
        public void Rename_ValidName_ShouldUpdateName()
        {
            // Arrange
            const string newName = "Updated Account Name";

            // Act
            _account.Rename(newName);

            // Assert
            Assert.That(_account.Name, Is.EqualTo(newName));
        }

        [Test]
        public void Rename_WithSameName_ShouldNotChangeAnything()
        {
            // Arrange
            var originalName = _account.Name;

            // Act
            _account.Rename(originalName);

            // Assert
            Assert.That(_account.Name, Is.EqualTo(originalName));
        }

        [Test]
        public void Rename_WithEmptyName_ShouldThrowInvalidOperationException()
        {
            // Arrange & Act & Assert
            Assert.Throws<InvalidOperationException>(() => _account.Rename(""));
        }

        [Test]
        public void Rename_WithNullName_ShouldThrowInvalidOperationException()
        {
            // Arrange & Act & Assert
            Assert.Throws<InvalidOperationException>(() => _account.Rename(null));
        }

        [Test]
        public void Rename_WhenAccountClosed_ShouldThrowInvalidOperationException()
        {
            // Arrange
            _account.Close();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _account.Rename("New Name"));
        }

        [Test]
        public void Close_ShouldSetIsClosedToTrue()
        {
            // Act
            _account.Close();

            // Assert
            Assert.That(_account.IsClosed, Is.True);
        }

        [Test]
        public void Close_WhenAlreadyClosed_ShouldThrowInvalidOperationException()
        {
            // Arrange
            _account.Close();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _account.Close());
        }

        [Test]
        public void Deposit_WithValidAmount_ShouldIncreaseBalance()
        {
            // Arrange
            const decimal depositAmount = 100.50m;

            // Act
            _account.Deposit(depositAmount);

            // Assert
            Assert.That(_account.Balance, Is.EqualTo(depositAmount));
        }

        [Test]
        public void Deposit_WithZeroAmount_ShouldThrowInvalidOperationException()
        {
            // Arrange
            const decimal initialBalance = 0;

            // Act && Assert
            Assert.Throws(typeof(InvalidOperationException), () => _account.Deposit(initialBalance));
        }

        [Test]
        public void Deposit_WithNegativeAmount_ShouldThrowInvalidOperationException()
        {
            // Arrange & Act & Assert
            Assert.Throws<InvalidOperationException>(() => _account.Deposit(-50));
        }

        [Test]
        public void Deposit_WhenAccountClosed_ShouldThrowInvalidOperationException()
        {
            // Arrange
            _account.Close();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _account.Deposit(100));
        }
    }
}