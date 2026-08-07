using FinanceTracker.Domain.Enums;

namespace BlazorApp1.Models;

public sealed class AccountModel
{
  public long Id { get; set; }

  public string Name { get; set; } = string.Empty;

  public decimal Balance { get; set; }

  public Currency Currency { get; set; }

  public AccountType AccountType { get; set; }

  public bool IsClosed { get; set; }
}