using FinanceTracker.Application.Abstractions;
using FinanceTracker.Application.Abstractions.Repositories;
using FinanceTracker.Application.Abstractions.Services;
using FinanceTracker.Application.Transfers.Commands;
using FinanceTracker.Application.Transfers.Services;
using FinanceTracker.Application.Transfers.Validators;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;
using Moq;

namespace FinanceTrackerTests.Application;

[TestFixture]
public class TransferServiceTests
{
    #region Поля

    private Mock<IAccountRepository> _accountRepository = null!;
    private Mock<ITransactionRepository> _transactionRepository = null!;
    private Mock<IUnitOfWork> _unitOfWork = null!;

    private ITransferService _transferService = null!;

    #endregion

    #region Setup

    [SetUp]
    public void Setup()
    {
        _accountRepository = new Mock<IAccountRepository>();
        _transactionRepository = new Mock<ITransactionRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();

        _transferService = new TransferService(
            _accountRepository.Object,
            new TransferMoneyValidator(),
            _transactionRepository.Object,
            _unitOfWork.Object);
    }

    #endregion

    #region Helpers

    private static TransferMoneyCommand CreateValidTransferCommand(
        long fromAccountId = 1,
        long toAccountId = 2,
        decimal amount = 100,
        string? description = "Test transfer")
    {
        return new TransferMoneyCommand
        {
            FromAccountId = fromAccountId,
            ToAccountId = toAccountId,
            Amount = amount,
            Description = description
        };
    }

    private static void SetAccountId(Account account, int id)
    {
        typeof(Account).GetProperty("Id")?.SetValue(account, id);
    }

    private static DebitAccount CreateDebitAccount(
        string name = "Main Account",
        long userId = 1,
        AccountType accountType = AccountType.Debit,
        decimal balance = 0,
        Currency currency = Currency.RUB)
    {
        return new DebitAccount(userId, name, accountType, currency, balance);
    }

    private static CreditAccount CreateCreditAccount(
        string name = "Credit Account",
        long userId = 1,
        decimal balance = 0,
        decimal creditLimit = 10000,
        Currency currency = Currency.RUB)
    {
        return new CreditAccount(userId, name, currency, creditLimit, balance);
    }

    #endregion

    #region Transfer Tests

    [Test]
    public void Transfer_ValidCommand_DebitToDebit_ReturnsSuccess()
    {
        // Arrange
        var senderAccount = CreateDebitAccount(balance: 1000);
        SetAccountId(senderAccount, 1);

        var receiverAccount = CreateDebitAccount(balance: 500);
        SetAccountId(receiverAccount, 2);

        var command = CreateValidTransferCommand(
            fromAccountId: senderAccount.Id,
            toAccountId: receiverAccount.Id,
            amount: 100);

        _accountRepository
            .Setup(r => r.GetById(command.FromAccountId))
            .Returns(senderAccount);

        _accountRepository
            .Setup(r => r.GetById(command.ToAccountId))
            .Returns(receiverAccount);

        // Act
        var result = _transferService.Transfer(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);
            Assert.That(senderAccount.Balance, Is.EqualTo(900)); // 1000 - 100
            Assert.That(receiverAccount.Balance, Is.EqualTo(600)); // 500 + 100

            _transactionRepository.Verify(r => r.Add(It.Is<Transaction>(t => 
                t.AccountId == command.FromAccountId && 
                t.Kind == TransactionKind.Expense)), Times.Once);

            _transactionRepository.Verify(r => r.Add(It.Is<Transaction>(t => 
                t.AccountId == command.ToAccountId && 
                t.Kind == TransactionKind.Income)), Times.Once);

            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    [Test]
    public void Transfer_ValidCommand_CreditToDebit_ReturnsSuccess()
    {
        // Arrange
        var senderAccount = CreateCreditAccount(balance: 1000, creditLimit: 10000);
        SetAccountId(senderAccount, 1);

        var receiverAccount = CreateDebitAccount(balance: 500);
        SetAccountId(receiverAccount, 2);

        var command = CreateValidTransferCommand(
            fromAccountId: senderAccount.Id,
            toAccountId: receiverAccount.Id,
            amount: 100);

        _accountRepository
            .Setup(r => r.GetById(command.FromAccountId))
            .Returns(senderAccount);

        _accountRepository
            .Setup(r => r.GetById(command.ToAccountId))
            .Returns(receiverAccount);

        // Act
        var result = _transferService.Transfer(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);
            Assert.That(senderAccount.Balance, Is.EqualTo(900)); // 1000 - 100
            Assert.That(receiverAccount.Balance, Is.EqualTo(600)); // 500 + 100

            _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Exactly(2));
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    [Test]
    public void Transfer_ValidCommand_DebitToCredit_ReturnsSuccess()
    {
        // Arrange
        var senderAccount = CreateDebitAccount(balance: 1000);
        SetAccountId(senderAccount, 1);

        var receiverAccount = CreateCreditAccount(balance: 500, creditLimit: 10000);
        SetAccountId(receiverAccount, 2);

        var command = CreateValidTransferCommand(
            fromAccountId: senderAccount.Id,
            toAccountId: receiverAccount.Id,
            amount: 100);

        _accountRepository
            .Setup(r => r.GetById(command.FromAccountId))
            .Returns(senderAccount);

        _accountRepository
            .Setup(r => r.GetById(command.ToAccountId))
            .Returns(receiverAccount);

        // Act
        var result = _transferService.Transfer(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);
            Assert.That(senderAccount.Balance, Is.EqualTo(900)); // 1000 - 100
            Assert.That(receiverAccount.Balance, Is.EqualTo(600)); // 500 + 100

            _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Exactly(2));
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    [Test]
    public void Transfer_InvalidCommand_DoNotTransfer()
    {
        // Arrange
        var command = CreateValidTransferCommand(amount: -100); // Отрицательная сумма

        // Act
        var result = _transferService.Transfer(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);

            _accountRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Never);
            _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Never);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void Transfer_ZeroAmount_ReturnsValidationError()
    {
        // Arrange
        var command = CreateValidTransferCommand(amount: 0);

        // Act
        var result = _transferService.Transfer(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);

            _accountRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Never);
            _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Never);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void Transfer_SenderAccountNotFound_ReturnsValidationError()
    {
        // Arrange
        var command = CreateValidTransferCommand(fromAccountId: 999);

        _accountRepository
            .Setup(r => r.GetById(command.FromAccountId))
            .Returns((Account?)null);

        // Act
        var result = _transferService.Transfer(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);

            _accountRepository.Verify(r => r.GetById(command.ToAccountId), Times.Never);
            _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Never);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void Transfer_ReceiverAccountNotFound_ReturnsValidationError()
    {
        // Arrange
        var senderAccount = CreateDebitAccount(balance: 1000);
        SetAccountId(senderAccount, 1);

        var command = CreateValidTransferCommand(
            fromAccountId: senderAccount.Id,
            toAccountId: 999);

        _accountRepository
            .Setup(r => r.GetById(command.FromAccountId))
            .Returns(senderAccount);

        _accountRepository
            .Setup(r => r.GetById(command.ToAccountId))
            .Returns((Account?)null);

        // Act
        var result = _transferService.Transfer(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);
            Assert.That(senderAccount.Balance, Is.EqualTo(1000)); // Баланс не изменился

            _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Never);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void Transfer_SenderAndReceiverSameAccount_ReturnsValidationError()
    {
        // Arrange
        var account = CreateDebitAccount(balance: 1000);
        SetAccountId(account, 1);

        var command = CreateValidTransferCommand(
            fromAccountId: account.Id,
            toAccountId: account.Id,
            amount: 100);

        // Act
        var result = _transferService.Transfer(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);

            _accountRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Never);
            _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Never);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void Transfer_InsufficientFundsOnSender_ShouldThrowException()
    {
        // Arrange
        var senderAccount = CreateDebitAccount(balance: 50);
        SetAccountId(senderAccount, 1);

        var receiverAccount = CreateDebitAccount(balance: 500);
        SetAccountId(receiverAccount, 2);

        var command = CreateValidTransferCommand(
            fromAccountId: senderAccount.Id,
            toAccountId: receiverAccount.Id,
            amount: 100);

        _accountRepository
            .Setup(r => r.GetById(command.FromAccountId))
            .Returns(senderAccount);

        _accountRepository
            .Setup(r => r.GetById(command.ToAccountId))
            .Returns(receiverAccount);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _transferService.Transfer(command));

        Assert.That(senderAccount.Balance, Is.EqualTo(50)); // Баланс не изменился
        Assert.That(receiverAccount.Balance, Is.EqualTo(500)); // Баланс не изменился

        _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Never);
        _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
    }

    [Test]
    public void Transfer_ExceedsCreditLimit_ShouldThrowException()
    {
        // Arrange
        var senderAccount = CreateCreditAccount(balance: 1000, creditLimit: 10000);
        SetAccountId(senderAccount, 1);

        var receiverAccount = CreateDebitAccount(balance: 500);
        SetAccountId(receiverAccount, 2);

        var command = CreateValidTransferCommand(
            fromAccountId: senderAccount.Id,
            toAccountId: receiverAccount.Id,
            amount: 15000);

        _accountRepository
            .Setup(r => r.GetById(command.FromAccountId))
            .Returns(senderAccount);

        _accountRepository
            .Setup(r => r.GetById(command.ToAccountId))
            .Returns(receiverAccount);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _transferService.Transfer(command));

        Assert.That(senderAccount.Balance, Is.EqualTo(1000)); // Баланс не изменился
        Assert.That(receiverAccount.Balance, Is.EqualTo(500)); // Баланс не изменился

        _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Never);
        _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
    }

    [Test]
    public void Transfer_WithDescription_ReturnsSuccess()
    {
        // Arrange
        var senderAccount = CreateDebitAccount(balance: 1000);
        SetAccountId(senderAccount, 1);

        var receiverAccount = CreateDebitAccount(balance: 500);
        SetAccountId(receiverAccount, 2);

        var description = "Monthly rent payment";
        var command = CreateValidTransferCommand(
            fromAccountId: senderAccount.Id,
            toAccountId: receiverAccount.Id,
            amount: 100,
            description: description);

        _accountRepository
            .Setup(r => r.GetById(command.FromAccountId))
            .Returns(senderAccount);

        _accountRepository
            .Setup(r => r.GetById(command.ToAccountId))
            .Returns(receiverAccount);

        // Act
        var result = _transferService.Transfer(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);

            _transactionRepository.Verify(r => r.Add(It.Is<Transaction>(t => 
                t.Description == description)), Times.Exactly(2));

            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    [Test]
    public void Transfer_WithoutDescription_ReturnsSuccess()
    {
        // Arrange
        var senderAccount = CreateDebitAccount(balance: 1000);
        SetAccountId(senderAccount, 1);

        var receiverAccount = CreateDebitAccount(balance: 500);
        SetAccountId(receiverAccount, 2);

        var command = CreateValidTransferCommand(
            fromAccountId: senderAccount.Id,
            toAccountId: receiverAccount.Id,
            amount: 100,
            description: null);

        _accountRepository
            .Setup(r => r.GetById(command.FromAccountId))
            .Returns(senderAccount);

        _accountRepository
            .Setup(r => r.GetById(command.ToAccountId))
            .Returns(receiverAccount);

        // Act
        var result = _transferService.Transfer(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);

            _transactionRepository.Verify(r => r.Add(It.Is<Transaction>(t => 
                t.Description == null)), Times.Exactly(2));

            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    [Test]
    public void Transfer_ExactBalanceOnSender_ReturnsSuccess()
    {
        // Arrange
        var senderAccount = CreateDebitAccount(balance: 100);
        SetAccountId(senderAccount, 1);

        var receiverAccount = CreateDebitAccount(balance: 500);
        SetAccountId(receiverAccount, 2);

        var command = CreateValidTransferCommand(
            fromAccountId: senderAccount.Id,
            toAccountId: receiverAccount.Id,
            amount: 100);

        _accountRepository
            .Setup(r => r.GetById(command.FromAccountId))
            .Returns(senderAccount);

        _accountRepository
            .Setup(r => r.GetById(command.ToAccountId))
            .Returns(receiverAccount);

        // Act
        var result = _transferService.Transfer(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);
            Assert.That(senderAccount.Balance, Is.EqualTo(0));
            Assert.That(receiverAccount.Balance, Is.EqualTo(600)); // 500 + 100

            _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Exactly(2));
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    [Test]
    public void Transfer_BothAccountsCredit_ReturnsSuccess()
    {
        // Arrange
        var senderAccount = CreateCreditAccount(balance: 1000, creditLimit: 10000);
        SetAccountId(senderAccount, 1);

        var receiverAccount = CreateCreditAccount(balance: 500, creditLimit: 5000);
        SetAccountId(receiverAccount, 2);

        var command = CreateValidTransferCommand(
            fromAccountId: senderAccount.Id,
            toAccountId: receiverAccount.Id,
            amount: 100);

        _accountRepository
            .Setup(r => r.GetById(command.FromAccountId))
            .Returns(senderAccount);

        _accountRepository
            .Setup(r => r.GetById(command.ToAccountId))
            .Returns(receiverAccount);

        // Act
        var result = _transferService.Transfer(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);
            Assert.That(senderAccount.Balance, Is.EqualTo(900));
            Assert.That(receiverAccount.Balance, Is.EqualTo(600));

            _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Exactly(2));
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    #endregion
}