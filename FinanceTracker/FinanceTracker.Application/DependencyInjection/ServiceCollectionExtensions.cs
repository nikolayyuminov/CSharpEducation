using FinanceTracker.Application.Abstractions.Factories;
using FinanceTracker.Application.Abstractions.Services;
using FinanceTracker.Application.Abstractions.Validation;
using FinanceTracker.Application.Accounts.Commands;
using FinanceTracker.Application.Accounts.Services;
using FinanceTracker.Application.Accounts.Validators;
using FinanceTracker.Application.Categories.Commands;
using FinanceTracker.Application.Categories.Services;
using FinanceTracker.Application.Categories.Validators;
using FinanceTracker.Application.Factories;
using FinanceTracker.Application.Transactions.Commands;
using FinanceTracker.Application.Transactions.Services;
using FinanceTracker.Application.Transactions.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceTracker.Application.DependencyInjection;

/// <summary>
/// Регистрация сервисов слоя Application.
/// </summary>
public static class ServiceCollectionExtensions
{
  /// <summary>
  /// Зарегистрировать зависимости слоя Application.
  /// </summary>
  /// <param name="services">Коллекция сервисов.</param>
  /// <returns>Коллекция сервисов.</returns>
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    // Регистрация сервисов
    services.AddScoped<IAccountService, AccountService>();

    services.AddScoped<ICategoryService, CategoryService>();

    services.AddScoped<ITransactionService, TransactionService>();

    services.AddScoped<ITransferService, TransferService>();
    
    // Регистрация фабрик
    services.AddScoped<IAccountFactory, AccountFactory>();

    // Регистрация валидаторов счетов
    services.AddScoped<IValidator<CreateAccountCommand>, CreateAccountValidator>();

    services.AddScoped<IValidator<RenameAccountCommand>, RenameAccountValidator>();

    services.AddScoped<IValidator<CloseAccountCommand>, CloseAccountValidator>();
    
    services.AddScoped<IValidator<ChangeCreditLimitCommand>, ChangeCreditLimitValidator>();
    
    services.AddScoped<IValidator<TransferMoneyCommand>, TransferMoneyValidator>();
    
    // Регистрация валидаторов категорий
    services.AddScoped<IValidator<ArchiveCategoryCommand>, ArchiveCategoryValidator>();

    services.AddScoped<IValidator<ChangeDescriptionCommand>, ChangeDescriptionValidator>();
    
    services.AddScoped<IValidator<CreateCategoryCommand>, CreateCategoryValidator>();
    
    services.AddScoped<IValidator<RenameCategoryCommand>, RenameCategoryValidator>();
    
    // Регистрация валидаторов транзакций
    services.AddScoped<IValidator<ChangeTransactionDescriptionCommand>, ChangeTransactionDescriptionValidator>();
    
    services.AddScoped<IValidator<CreateTransactionCommand>, CreateTransactionValidator>();

    return services;
  }
}