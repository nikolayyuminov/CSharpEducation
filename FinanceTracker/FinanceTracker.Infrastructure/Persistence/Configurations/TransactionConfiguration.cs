using FinanceTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceTracker.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="Transaction"/> для Entity Framework Core.
/// Определяет таблицу, ключи, ограничения и правила хранения полей.
/// </summary>
public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
  /// <summary>
  /// Настроить отображение сущности <see cref="Transaction"/> в базе данных.
  /// </summary>
  /// <param name="builder"> Построитель конфигурации сущности.</param>
  public void Configure(EntityTypeBuilder<Transaction> builder)
  {
    builder.ToTable("Transactions");
    
    builder.HasKey(t => t.Id);
    
    builder.Property(t => t.Id).ValueGeneratedOnAdd();
    
    builder.Property(t => t.TransferId).IsRequired(false);
    
    builder.Property(t => t.Amount).IsRequired().HasPrecision(18, 2);

    builder.Property(t => t.Kind).IsRequired();
    
    builder.Property(t => t.CreatedAt).IsRequired();
    
    builder.Property(t => t.Description).IsRequired(false).HasMaxLength(1000);
    
    builder.HasOne(t => t.Account)
           .WithMany(a => a.Transactions)
           .HasForeignKey(t => t.AccountId)
           .OnDelete(DeleteBehavior.Restrict);
    
    builder.HasOne(t => t.Category)
           .WithMany(c =>c.Transactions)
           .HasForeignKey(t => t.CategoryId)
           .OnDelete(DeleteBehavior.Restrict);
    
    builder.HasIndex(t => t.AccountId);

    builder.HasIndex(t => t.CategoryId);

    builder.HasIndex(t => t.TransferId);
  }
}