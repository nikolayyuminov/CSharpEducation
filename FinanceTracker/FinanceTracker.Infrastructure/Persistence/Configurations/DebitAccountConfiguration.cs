using FinanceTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceTracker.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="DebitAccount"/> для Entity Framework Core.
/// Определяет таблицу, ключи, ограничения и правила хранения полей.
/// </summary>
public class DebitAccountConfiguration : IEntityTypeConfiguration<DebitAccount>
{
  /// <summary>
  /// Настроить отображение сущности <see cref="DebitAccount"/> в базе данных.
  /// </summary>
  /// <param name="builder"> Построитель конфигурации сущности.</param>
  public void Configure(EntityTypeBuilder<DebitAccount> builder)
  {
  }
}