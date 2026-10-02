namespace BlazorApp1.Models;

/// <summary>
/// Модель изменения кредитного лимита.
/// </summary>
public class ChangeCreditLimitModel
{
  public long AccountId { get; init; }
  
  public decimal NewCreditLimit { get; set; }
}