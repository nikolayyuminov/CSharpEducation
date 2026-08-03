using FinanceTracker.Application.Abstractions;
using FinanceTracker.Application.Abstractions.Repositories;
using FinanceTracker.Infrastructure.Persistence;
using FinanceTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceTracker.Infrastructure.DependencyInjection;

/// <summary>
/// Регистрация сервисов слоя Infrastructure.
/// </summary>
public static class ServiceCollectionExtensions
{
  /// <summary>
  /// Зарегистрировать зависимости слоя Infrastructure.
  /// </summary>
  /// <param name="services">Коллекция сервисов.</param>
  /// <returns>Коллекция сервисов.</returns>
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    // регистрация DbContext
    services.AddDbContext<FinanceTrackerDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("FinanceTracker")));
    
    // регистрация UnitOfWork
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    
    // регистрация Repository
    services.AddScoped<IAccountRepository, AccountRepository>();
    
    services.AddScoped<ICategoryRepository, CategoryRepository>();
    
    services.AddScoped<ITransactionRepository, TransactionRepository>();

    return services;
  }
}