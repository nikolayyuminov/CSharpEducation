using FinanceTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceTracker.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="CreditAccount"/> для Entity Framework Core.
/// Определяет таблицу, ключи, ограничения и правила хранения полей.
/// </summary>
public class CreditAccountConfiguration : IEntityTypeConfiguration<CreditAccount>
{
  /// <summary>
  /// Настроить отображение сущности <see cref="CreditAccount"/> в базе данных.
  /// </summary>
  /// <param name="builder"> Построитель конфигурации сущности.</param>
  public void Configure(EntityTypeBuilder<CreditAccount> builder)
  {
    builder.Property(a => a.CreditLimit).HasPrecision(18,2);
  }
}