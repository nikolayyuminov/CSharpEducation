using FinanceTracker.Application.Abstractions;
using FinanceTracker.Application.Abstractions.Repositories;
using FinanceTracker.Application.Abstractions.Services;
using FinanceTracker.Application.Transactions.Commands;
using FinanceTracker.Application.Transactions.Services;
using FinanceTracker.Application.Transactions.Validators;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;
using Moq;

namespace FinanceTrackerTests.Application;

[TestFixture]
public class TransactionServiceTests
{
    #region Поля

    private Mock<ITransactionRepository> _transactionRepository = null!;
    private Mock<IAccountRepository> _accountRepository = null!;
    private Mock<ICategoryRepository> _categoryRepository = null!;
    private Mock<IUnitOfWork> _unitOfWork = null!;

    private ITransactionService _transactionService = null!;

    #endregion

    #region Setup

    [SetUp]
    public void Setup()
    {
        _transactionRepository = new Mock<ITransactionRepository>();
        _accountRepository = new Mock<IAccountRepository>();
        _categoryRepository = new Mock<ICategoryRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();

        _transactionService = new TransactionService(
            _transactionRepository.Object,
            new CreateTransactionValidator(),
            new ChangeTransactionDescriptionValidator(),
            _accountRepository.Object,
            _categoryRepository.Object,
            _unitOfWork.Object);
    }

    #endregion

    #region Helpers

    private static CreateTransactionCommand CreateValidCreateCommand(
        long accountId = 1,
        long? categoryId = 1,
        decimal amount = 100,
        TransactionKind kind = TransactionKind.Expense,
        string? description = "Test transaction")
    {
        return new CreateTransactionCommand
        {
            AccountId = accountId,
            CategoryId = categoryId,
            Amount = amount,
            Kind = kind,
            Description = description
        };
    }

    private static void SetAccountId(Account account, int id)
    {
        typeof(Account).GetProperty("Id")?.SetValue(account, id);
    }

    private static void SetCategoryId(Category category, int id)
    {
        typeof(Category).GetProperty("Id")?.SetValue(category, id);
    }

    private static void SetTransactionId(Transaction transaction, int id)
    {
        typeof(Transaction).GetProperty("Id")?.SetValue(transaction, id);
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

    private static Category CreateCategory(
        string name = "Food",
        long? userId = 1,
        CategoryKind kind = CategoryKind.Expense,
        string? description = null,
        bool isArchived = false)
    {
        var category = new Category(name, description, userId, kind);
        typeof(Category).GetProperty("IsArchived")?.SetValue(category, isArchived);
        return category;
    }

    #endregion

    #region CreateTransaction Tests

    [Test]
    public void CreateTransaction_ValidExpenseCommand_ReturnsSuccess()
    {
        // Arrange
        var account = CreateDebitAccount(balance: 1000);
        SetAccountId(account, 1);

        var category = CreateCategory(kind: CategoryKind.Expense);
        SetCategoryId(category, 1);

        var command = CreateValidCreateCommand(
            accountId: account.Id,
            categoryId: category.Id,
            amount: 100,
            kind: TransactionKind.Expense);

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns(account);

        _categoryRepository
            .Setup(r => r.GetById(command.CategoryId!.Value))
            .Returns(category);

        // Act
        var result = _transactionService.CreateTransaction(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);
            Assert.That(account.Balance, Is.EqualTo(900)); // 1000 - 100

            _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Once);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    [Test]
    public void CreateTransaction_ValidIncomeCommand_ReturnsSuccess()
    {
        // Arrange
        var account = CreateDebitAccount(balance: 1000);
        SetAccountId(account, 1);

        var category = CreateCategory(kind: CategoryKind.Income);
        SetCategoryId(category, 1);

        var command = CreateValidCreateCommand(
            accountId: account.Id,
            categoryId: category.Id,
            amount: 100,
            kind: TransactionKind.Income);

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns(account);

        _categoryRepository
            .Setup(r => r.GetById(command.CategoryId!.Value))
            .Returns(category);

        // Act
        var result = _transactionService.CreateTransaction(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);
            Assert.That(account.Balance, Is.EqualTo(1100)); // 1000 + 100

            _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Once);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    [Test]
    public void CreateTransaction_InvalidCommand_DoNotCreateTransaction()
    {
        // Arrange
        var command = CreateValidCreateCommand(amount: -100); // Отрицательная сумма

        // Act
        var result = _transactionService.CreateTransaction(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);

            _accountRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Never);
            _categoryRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Never);
            _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Never);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void CreateTransaction_AccountNotFound_ReturnsValidationError()
    {
        // Arrange
        var command = CreateValidCreateCommand(accountId: 999);

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns((Account?)null);

        // Act
        var result = _transactionService.CreateTransaction(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);

            _categoryRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Never);
            _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Never);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void CreateTransaction_CategoryNotFound_ReturnsValidationError()
    {
        // Arrange
        var account = CreateDebitAccount(balance: 1000);
        SetAccountId(account, 1);

        var command = CreateValidCreateCommand(
            accountId: account.Id,
            categoryId: 999);

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns(account);

        _categoryRepository
            .Setup(r => r.GetById(command.CategoryId!.Value))
            .Returns((Category?)null);

        // Act
        var result = _transactionService.CreateTransaction(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);
            Assert.That(account.Balance, Is.EqualTo(1000)); // Баланс не изменился

            _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Never);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void CreateTransaction_CategoryIsArchived_ReturnsValidationError()
    {
        // Arrange
        var account = CreateDebitAccount(balance: 1000);
        SetAccountId(account, 1);

        var category = CreateCategory(isArchived: true);
        SetCategoryId(category, 1);

        var command = CreateValidCreateCommand(
            accountId: account.Id,
            categoryId: category.Id);

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns(account);

        _categoryRepository
            .Setup(r => r.GetById(command.CategoryId!.Value))
            .Returns(category);

        // Act
        var result = _transactionService.CreateTransaction(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);
            Assert.That(account.Balance, Is.EqualTo(1000)); // Баланс не изменился

            _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Never);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void CreateTransaction_WithoutCategory_Expense_ReturnsSuccess()
    {
        // Arrange
        var account = CreateDebitAccount(balance: 1000);
        SetAccountId(account, 1);

        var command = CreateValidCreateCommand(
            accountId: account.Id,
            categoryId: null,
            amount: 100,
            kind: TransactionKind.Expense);

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns(account);

        // Act
        var result = _transactionService.CreateTransaction(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);
            Assert.That(account.Balance, Is.EqualTo(900)); // 1000 - 100

            _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Once);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    [Test]
    public void CreateTransaction_WithoutCategory_Income_ReturnsSuccess()
    {
        // Arrange
        var account = CreateDebitAccount(balance: 1000);
        SetAccountId(account, 1);

        var command = CreateValidCreateCommand(
            accountId: account.Id,
            categoryId: null,
            amount: 100,
            kind: TransactionKind.Income);

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns(account);

        // Act
        var result = _transactionService.CreateTransaction(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);
            Assert.That(account.Balance, Is.EqualTo(1100)); // 1000 + 100

            _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Once);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    [Test]
    public void CreateTransaction_ExpenseWithInsufficientFunds_ShouldThrowException()
    {
        // Arrange
        var account = CreateDebitAccount(balance: 50);
        SetAccountId(account, 1);

        var command = CreateValidCreateCommand(
            accountId: account.Id,
            categoryId: null,
            amount: 100,
            kind: TransactionKind.Expense);

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns(account);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _transactionService.CreateTransaction(command));

        _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Never);
        _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
    }

    [Test]
    public void CreateTransaction_WithCategoryKindMismatch_UsesCategoryKind()
    {
        // Arrange
        var account = CreateDebitAccount(balance: 1000);
        SetAccountId(account, 1);

        var category = CreateCategory(kind: CategoryKind.Expense);
        SetCategoryId(category, 1);

        var command = CreateValidCreateCommand(
            accountId: account.Id,
            categoryId: category.Id,
            amount: 100,
            kind: TransactionKind.Income); // Указываем Income, но категория Expense

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns(account);

        _categoryRepository
            .Setup(r => r.GetById(command.CategoryId!.Value))
            .Returns(category);

        // Act
        var result = _transactionService.CreateTransaction(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);
            Assert.That(account.Balance, Is.EqualTo(900)); // Сработал Expense от категории

            _transactionRepository.Verify(r => r.Add(It.Is<Transaction>(t => t.Kind == TransactionKind.Expense)), Times.Once);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    [Test]
    public void CreateTransaction_CreditAccount_WithExpense_ReturnsSuccess()
    {
        // Arrange
        var account = CreateCreditAccount(balance: 1000, creditLimit: 10000);
        SetAccountId(account, 1);

        var command = CreateValidCreateCommand(
            accountId: account.Id,
            categoryId: null,
            amount: 100,
            kind: TransactionKind.Expense);

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns(account);

        // Act
        var result = _transactionService.CreateTransaction(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);
            Assert.That(account.Balance, Is.EqualTo(900)); // 1000 - 100

            _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Once);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    #endregion

    #region ChangeDescription Tests

    [Test]
    public void ChangeDescription_ValidCommand_ReturnsSuccess()
    {
        // Arrange
        var transaction = new Transaction(1, 1, 100, "Old description", TransactionKind.Expense);
        SetTransactionId(transaction, 1);

        var command = new ChangeTransactionDescriptionCommand
        {
            TransactionId = transaction.Id,
            NewDescription = "New description"
        };

        _transactionRepository
            .Setup(r => r.GetById(command.TransactionId))
            .Returns(transaction);

        // Act
        var result = _transactionService.ChangeDescription(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);
            Assert.That(transaction.Description, Is.EqualTo("New description"));

            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    [Test]
    public void ChangeDescription_InvalidCommand_DoNotChangeDescription()
    {
        // Arrange
        var command = new ChangeTransactionDescriptionCommand
        {
            TransactionId = 1,
            NewDescription = "" // Пустое описание
        };

        // Act
        var result = _transactionService.ChangeDescription(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);

            _transactionRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Never);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void ChangeDescription_TransactionNotFound_ReturnsValidationError()
    {
        // Arrange
        var command = new ChangeTransactionDescriptionCommand
        {
            TransactionId = 999,
            NewDescription = "New description"
        };

        _transactionRepository
            .Setup(r => r.GetById(command.TransactionId))
            .Returns((Transaction?)null);

        // Act
        var result = _transactionService.ChangeDescription(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);

            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void ChangeDescription_ToNull_ReturnsSuccess()
    {
        // Arrange
        var transaction = new Transaction(1, 1, 100, "Old description", TransactionKind.Expense);
        SetTransactionId(transaction, 1);

        var command = new ChangeTransactionDescriptionCommand
        {
            TransactionId = transaction.Id,
            NewDescription = null
        };

        _transactionRepository
            .Setup(r => r.GetById(command.TransactionId))
            .Returns(transaction);

        // Act
        var result = _transactionService.ChangeDescription(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);
            Assert.That(transaction.Description, Is.Null);

            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    #endregion

    #region Edge Cases and Integration Tests

    [Test]
    public void CreateTransaction_WithZeroAmount_ReturnsValidationError()
    {
        // Arrange
        var command = CreateValidCreateCommand(amount: 0);

        // Act
        var result = _transactionService.CreateTransaction(command);

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
    public void CreateTransaction_ExpenseWithExactBalance_ReturnsSuccess()
    {
        // Arrange
        var account = CreateDebitAccount(balance: 100);
        SetAccountId(account, 1);

        var command = CreateValidCreateCommand(
            accountId: account.Id,
            categoryId: null,
            amount: 100,
            kind: TransactionKind.Expense);

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns(account);

        // Act
        var result = _transactionService.CreateTransaction(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);
            Assert.That(account.Balance, Is.EqualTo(0));

            _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Once);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    [Test]
    public void CreateTransaction_LargeAmountOnCreditAccount_ReturnsSuccess()
    {
        // Arrange
        var account = CreateCreditAccount(balance: 1000, creditLimit: 10000);
        SetAccountId(account, 1);

        var command = CreateValidCreateCommand(
            accountId: account.Id,
            categoryId: null,
            amount: 5000,
            kind: TransactionKind.Expense);

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns(account);

        // Act
        var result = _transactionService.CreateTransaction(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);
            Assert.That(account.Balance, Is.EqualTo(-4000)); // 1000 - 5000

            _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Once);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    [Test]
    public void CreateTransaction_ExceedsCreditLimit_ShouldThrowException()
    {
        // Arrange
        var account = CreateCreditAccount(balance: 1000, creditLimit: 10000);
        SetAccountId(account, 1);

        var command = CreateValidCreateCommand(
            accountId: account.Id,
            categoryId: null,
            amount: 15000,
            kind: TransactionKind.Expense);

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns(account);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _transactionService.CreateTransaction(command));

        _transactionRepository.Verify(r => r.Add(It.IsAny<Transaction>()), Times.Never);
        _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
    }

    #endregion
}