using FinanceTracker.Domain.Enums;

namespace BlazorApp1.Models;

public sealed class CreateAccountModel
{
  public string Name { get; set; } = string.Empty;

  public AccountType AccountType { get; set; } = AccountType.Debit;

  public Currency Currency { get; set; } = Currency.EUR;

  public decimal InitialBalance { get; set; }

  public decimal? CreditLimit { get; set; }
}