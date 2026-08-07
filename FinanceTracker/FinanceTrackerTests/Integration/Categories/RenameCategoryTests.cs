using System.Net;
using System.Net.Http.Json;
using FinanceTracker.API.Contracts.Categories;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;
using FinanceTrackerTests.Integration.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerTests.Integration.Categories;

/// <summary>
/// Интеграционные тесты переименования категорий.
/// </summary>
public class RenameCategoryTests : IntegrationTestBase
{
  [Test]
  public async Task RenameCategory_ValidRequest_ReturnsOk()
  {
    // Arrange
    long categoryId = 0;

    await ExecuteDbContextAsync(async db =>
    {
      var category = new Category(
        userId: 1,
        name: "Old category",
        categoryKind: CategoryKind.Expense,
        description: "Old description");

      db.Categories.Add(category);

      await db.SaveChangesAsync();

      categoryId = category.Id;
    });


    var request = new RenameCategoryRequest
    {
      UserId = 1,
      CategoryId = categoryId,
      NewName = "New category"
    };


    // Act
    var response = await Client.PostAsJsonAsync(
      "/api/categories/rename",
      request);


    var body = await response.Content.ReadAsStringAsync();

    TestContext.WriteLine(body);


    // Assert
    Assert.That(
      response.StatusCode,
      Is.EqualTo(HttpStatusCode.OK));


    await ExecuteDbContextAsync(async db =>
    {
      var category = await db.Categories
        .FirstAsync(x => x.Id == categoryId);


      Assert.That(
        category.Name,
        Is.EqualTo("New category"));


      Assert.That(
        category.UserId,
        Is.EqualTo(1));
    });
  }
}