using System.Net;
using System.Net.Http.Json;
using FinanceTracker.API.Contracts.Categories;
using FinanceTracker.Domain.Enums;
using FinanceTrackerTests.Integration.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerTests.Integration.Categories;

/// <summary>
/// Интеграционные тесты создания категорий.
/// </summary>
public class CreateCategoryTests : IntegrationTestBase
{
  [Test]
  public async Task CreateCategory_ValidRequest_ReturnsOk()
  {
    // Arrange
    var request = new CreateCategoryRequest
    {
      UserId = 1,
      Name = "Food",
      CategoryKind = CategoryKind.Expense,
      Description = "Food expenses"
    };


    // Act
    var response = await Client.PostAsJsonAsync(
      "/api/categories",
      request);


    // Assert
    Assert.That(
      response.StatusCode,
      Is.EqualTo(HttpStatusCode.OK));


    await ExecuteDbContextAsync(async db =>
    {
      var category = await db.Categories
        .FirstOrDefaultAsync();


      Assert.That(
        category,
        Is.Not.Null);


      Assert.That(
        category!.Name,
        Is.EqualTo("Food"));


      Assert.That(
        category.CategoryKind,
        Is.EqualTo(CategoryKind.Expense));


      Assert.That(
        category.Description,
        Is.EqualTo("Food expenses"));


      Assert.That(
        category.UserId,
        Is.EqualTo(1));
    });
  }
}