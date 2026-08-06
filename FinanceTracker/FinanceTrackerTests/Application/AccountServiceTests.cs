using FinanceTracker.Application.Abstractions;
using FinanceTracker.Application.Abstractions.Repositories;
using FinanceTracker.Application.Abstractions.Services;
using FinanceTracker.Application.Accounts.Commands;
using FinanceTracker.Application.Accounts.Services;
using FinanceTracker.Application.Accounts.Validators;
using FinanceTracker.Application.Factories;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;
using Moq;

namespace FinanceTrackerTests.Application;

[TestFixture]
public class AccountServiceTests
{
    #region Поля

    private Mock<IAccountRepository> _accountRepository = null!;
    private Mock<IUnitOfWork> _unitOfWork = null!;

    private IAccountService _accountService = null!;

    #endregion

    #region Setup

    [SetUp]
    public void Setup()
    {
        _accountRepository = new Mock<IAccountRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();

        _accountService = new AccountService(
            _accountRepository.Object,
            new CreateAccountValidator(),
            new AccountFactory(),
            new RenameAccountValidator(),
            new CloseAccountValidator(),
            new ChangeCreditLimitValidator(),
            _unitOfWork.Object);
    }

    #endregion

    #region Helpers

    private static CreateAccountCommand CreateValidCreateCommand(
        string name = "Main Account",
        long userId = 1,
        AccountType accountType = AccountType.Debit,
        decimal initialBalance = 0,
        decimal? creditLimit = null,
        Currency currency = Currency.EUR)
    {
        return new CreateAccountCommand
        {
            Name = name,
            UserId = userId,
            AccountType = accountType,
            InitialBalance = initialBalance,
            CreditLimit = creditLimit,
            Currency = currency
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

    #region CreateAccount Tests

    [Test]
    public void CreateAccount_ValidCommand_ReturnsSuccess()
    {
        // Arrange
        var command = CreateValidCreateCommand();

        _accountRepository
            .Setup(r => r.GetByName(command.UserId, command.Name))
            .Returns((Account?)null);

        // Act
        var result = _accountService.CreateAccount(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);

            _accountRepository.Verify(r => r.Add(It.IsAny<Account>()), Times.Once);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    [Test]
    public void CreateAccount_InvalidCommand_DoNotAddAccount()
    {
        // Arrange
        var command = CreateValidCreateCommand(name: "");

        // Act
        var result = _accountService.CreateAccount(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);

            _accountRepository.Verify(r => r.Add(It.IsAny<Account>()), Times.Never);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void CreateAccount_DuplicateName_ReturnsValidationError()
    {
        // Arrange
        var command = CreateValidCreateCommand();
        var existingAccount = CreateDebitAccount(name: command.Name, userId: command.UserId);
        SetAccountId(existingAccount, 1);

        _accountRepository
            .Setup(r => r.GetByName(command.UserId, command.Name))
            .Returns(existingAccount);

        // Act
        var result = _accountService.CreateAccount(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);

            _accountRepository.Verify(r => r.Add(It.IsAny<Account>()), Times.Never);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void CreateAccount_ValidCommandWithInitialBalance_ReturnsSuccess()
    {
        // Arrange
        var command = CreateValidCreateCommand(initialBalance: 1000);

        _accountRepository
            .Setup(r => r.GetByName(command.UserId, command.Name))
            .Returns((Account?)null);

        // Act
        var result = _accountService.CreateAccount(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);

            _accountRepository.Verify(r => r.Add(It.Is<Account>(a => a.Balance == 1000)), Times.Once);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    [Test]
    public void CreateAccount_CreditAccountWithCreditLimit_ReturnsSuccess()
    {
        // Arrange
        var command = CreateValidCreateCommand(
            accountType: AccountType.Credit,
            creditLimit: 10000);

        _accountRepository
            .Setup(r => r.GetByName(command.UserId, command.Name))
            .Returns((Account?)null);

        // Act
        var result = _accountService.CreateAccount(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);

            _accountRepository.Verify(r => r.Add(It.Is<Account>(a => 
                a.AccountType == AccountType.Credit && 
                ((CreditAccount)a).CreditLimit == 10000)), Times.Once);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    #endregion

    #region RenameAccount Tests

    [Test]
    public void RenameAccount_ValidCommand_ReturnsSuccess()
    {
        // Arrange
        var account = CreateDebitAccount(name: "Main Account", userId: 1);
        SetAccountId(account, 1);

        var command = new RenameAccountCommand
        {
            UserId = 1,
            AccountId = account.Id,
            NewName = "New Account Name"
        };

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns(account);

        _accountRepository
            .Setup(r => r.GetByName(command.UserId, command.NewName!))
            .Returns((Account?)null);

        // Act
        var result = _accountService.RenameAccount(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);
            Assert.That(account.Name, Is.EqualTo("New Account Name"));

            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    [Test]
    public void RenameAccount_InvalidCommand_DoNotRenameAccount()
    {
        // Arrange
        var command = new RenameAccountCommand
        {
            UserId = 1,
            AccountId = 1,
            NewName = ""
        };

        // Act
        var result = _accountService.RenameAccount(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);

            _accountRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Never);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void RenameAccount_AccountNotFound_ReturnsValidationError()
    {
        // Arrange
        var command = new RenameAccountCommand
        {
            UserId = 1,
            AccountId = 999,
            NewName = "New Account Name"
        };

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns((Account?)null);

        // Act
        var result = _accountService.RenameAccount(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);

            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void RenameAccount_DuplicateName_ReturnsValidationError()
    {
        // Arrange
        var account = CreateDebitAccount(name: "Main Account", userId: 1);
        SetAccountId(account, 1);

        var existingAccount = CreateDebitAccount(name: "Existing Account", userId: 1);
        SetAccountId(existingAccount, 2);

        var command = new RenameAccountCommand
        {
            UserId = 1,
            AccountId = account.Id,
            NewName = "Existing Account"
        };

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns(account);

        _accountRepository
            .Setup(r => r.GetByName(command.UserId, command.NewName!))
            .Returns(existingAccount);

        // Act
        var result = _accountService.RenameAccount(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);
            Assert.That(account.Name, Is.EqualTo("Main Account")); // Имя не изменилось

            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void RenameAccount_SameName_ReturnsSuccess()
    {
        // Arrange
        var account = CreateDebitAccount(name: "Main Account", userId: 1);
        SetAccountId(account, 1);

        var command = new RenameAccountCommand
        {
            UserId = 1,
            AccountId = account.Id,
            NewName = "Main Account" // То же самое имя
        };

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns(account);

        _accountRepository
            .Setup(r => r.GetByName(command.UserId, command.NewName!))
            .Returns(account); // Возвращаем этот же аккаунт

        // Act
        var result = _accountService.RenameAccount(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);
            Assert.That(account.Name, Is.EqualTo("Main Account"));

            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    #endregion

    #region CloseAccount Tests

    [Test]
    public void CloseAccount_ValidCommand_ReturnsSuccess()
    {
        // Arrange
        var account = CreateDebitAccount(balance: 0);
        SetAccountId(account, 1);

        var command = new CloseAccountCommand
        {
            AccountId = account.Id
        };

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns(account);

        // Act
        var result = _accountService.CloseAccount(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);
            Assert.That(account.IsClosed, Is.True);

            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    [Test]
    public void CloseAccount_InvalidCommand_DoNotCloseAccount()
    {
        // Arrange
        var command = new CloseAccountCommand
        {
            AccountId = 0 // Невалидный ID
        };

        // Act
        var result = _accountService.CloseAccount(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);

            _accountRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Never);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void CloseAccount_AccountNotFound_ReturnsValidationError()
    {
        // Arrange
        var command = new CloseAccountCommand
        {
            AccountId = 999
        };

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns((Account?)null);

        // Act
        var result = _accountService.CloseAccount(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);

            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void CloseAccount_BalanceNotZero_ReturnsValidationError()
    {
        // Arrange
        var account = CreateDebitAccount(balance: 1000);
        SetAccountId(account, 1);

        var command = new CloseAccountCommand
        {
            AccountId = account.Id
        };

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns(account);

        // Act
        var result = _accountService.CloseAccount(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);
            Assert.That(account.IsClosed, Is.False);

            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void CloseAccount_CreditAccountWithZeroBalance_ReturnsSuccess()
    {
        // Arrange
        var account = CreateCreditAccount(balance: 0);
        SetAccountId(account, 1);

        var command = new CloseAccountCommand
        {
            AccountId = account.Id
        };

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns(account);

        // Act
        var result = _accountService.CloseAccount(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);
            Assert.That(account.IsClosed, Is.True);

            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    #endregion

    #region ChangeCreditLimit Tests

    [Test]
    public void ChangeCreditLimit_ValidCommand_ReturnsSuccess()
    {
        // Arrange
        var account = CreateCreditAccount(creditLimit: 10000);
        SetAccountId(account, 1);

        var command = new ChangeCreditLimitCommand
        {
            AccountId = account.Id,
            NewCreditLimit = 15000
        };

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns(account);

        // Act
        var result = _accountService.ChangeCreditLimit(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);
            Assert.That(account.CreditLimit, Is.EqualTo(15000));

            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    [Test]
    public void ChangeCreditLimit_InvalidCommand_DoNotChangeCreditLimit()
    {
        // Arrange
        var command = new ChangeCreditLimitCommand
        {
            AccountId = 1,
            NewCreditLimit = -1000 // Невалидный лимит (отрицательный)
        };

        // Act
        var result = _accountService.ChangeCreditLimit(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);

            _accountRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Never);
            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void ChangeCreditLimit_AccountNotFound_ReturnsValidationError()
    {
        // Arrange
        var command = new ChangeCreditLimitCommand
        {
            AccountId = 999,
            NewCreditLimit = 15000
        };

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns((Account?)null);

        // Act
        var result = _accountService.ChangeCreditLimit(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);

            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void ChangeCreditLimit_NotCreditAccount_ReturnsValidationError()
    {
        // Arrange
        var account = CreateDebitAccount();
        SetAccountId(account, 1);

        var command = new ChangeCreditLimitCommand
        {
            AccountId = account.Id,
            NewCreditLimit = 15000
        };

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns(account);

        // Act
        var result = _accountService.ChangeCreditLimit(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.True);

            _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
        }
    }

    [Test]
    public void ChangeCreditLimit_ZeroCreditLimit_ReturnsSuccess()
    {
        // Arrange
        var account = CreateCreditAccount(creditLimit: 10000);
        SetAccountId(account, 1);

        var command = new ChangeCreditLimitCommand
        {
            AccountId = account.Id,
            NewCreditLimit = 0
        };

        _accountRepository
            .Setup(r => r.GetById(command.AccountId))
            .Returns(account);

        // Act
        var result = _accountService.ChangeCreditLimit(command);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.HasErrors, Is.False);
            Assert.That(account.CreditLimit, Is.EqualTo(0));

            _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
        }
    }

    #endregion
}