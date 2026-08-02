using FinanceTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceTracker.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="Category"/> для Entity Framework Core.
/// Определяет таблицу, ключи, ограничения и правила хранения полей.
/// </summary>
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
  /// <summary>
  /// Настроить отображение сущности <see cref="Category"/> в базе данных.
  /// </summary>
  /// <param name="builder"> Построитель конфигурации сущности.</param>
  public void Configure(EntityTypeBuilder<Category> builder)
  {
    builder.ToTable("Categories");
    
    builder.HasKey(c => c.Id);
    
    builder.Property(c => c.Id).ValueGeneratedOnAdd();
    
    builder.Property(c => c.Name).IsRequired().HasMaxLength(256);
    
    builder.Property(c => c.UserId).IsRequired(false);
    
    builder.Property(c => c.IsArchived).IsRequired();
    
    builder.Property(c => c.CategoryKind).IsRequired();
    
    builder.Property(c => c.Description).HasMaxLength(1000);
    
    builder.HasIndex(c => new
      {
        c.UserId,
        c.Name
      })
      .IsUnique();
  }
}