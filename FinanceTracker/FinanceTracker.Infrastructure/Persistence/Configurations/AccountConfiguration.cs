using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceTracker.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="Account"/> для Entity Framework Core.
/// Определяет таблицу, ключи, ограничения и правила хранения полей.
/// </summary>
public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
  #region Методы

  /// <summary>
  /// Настроить отображение сущности <see cref="Account"/> в базе данных.
  /// </summary>
  /// <param name="builder"> Построитель конфигурации сущности.</param>
  public void Configure(EntityTypeBuilder<Account> builder)
  {
    // Имя таблицы
    builder.ToTable("Accounts");

    // Первичный ключ
    builder.HasKey(a => a.Id);

    // Id генерируется базой данных
    builder.Property(a => a.Id).ValueGeneratedOnAdd();

    // Имя счета
    builder.Property(a => a.Name).IsRequired().HasMaxLength(256);

    // Пользователь-владелец
    builder.Property(a => a.UserId).IsRequired();

    // Баланс
    builder.Property(a => a.Balance).HasPrecision(18, 2).IsRequired();

    // Валюта
    builder.Property(a => a.Currency).IsRequired();

    // Признак закрытого счета
    builder.Property(a => a.IsClosed).IsRequired();

    // Имя счета должно быть уникальным в рамках пользователя
    builder.HasIndex(a => new
      {
        a.UserId,
        a.Name
      })
      .IsUnique();
    
    // Определение типа счета.
    builder.HasDiscriminator<AccountType>("AccountType")
      .HasValue<DebitAccount>(AccountType.Debit)
      .HasValue<CreditAccount>(AccountType.Credit)
      .HasValue<DepositAccount>(AccountType.Deposit);
  }

  #endregion
}