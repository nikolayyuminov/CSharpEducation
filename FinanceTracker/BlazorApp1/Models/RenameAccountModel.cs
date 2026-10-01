namespace BlazorApp1.Models;

/// <summary>
/// Модель для переименования счета.
/// </summary>
public sealed record RenameAccountModel
{
  public long AccountId { get; init; }

  public string NewName { get; set; } = string.Empty;
}