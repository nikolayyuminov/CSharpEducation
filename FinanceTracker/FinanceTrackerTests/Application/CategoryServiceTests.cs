using FinanceTracker.Application.Abstractions;
using FinanceTracker.Application.Abstractions.Repositories;
using FinanceTracker.Application.Abstractions.Services;
using FinanceTracker.Application.Categories.Commands;
using FinanceTracker.Application.Categories.Services;
using FinanceTracker.Application.Categories.Validators;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;
using Moq;

namespace FinanceTrackerTests.Application;

[TestFixture]
public class CategoryServiceTests
{
  #region Поля

  private Mock<ICategoryRepository> _categoryRepository = null!;
  private Mock<IUnitOfWork> _unitOfWork = null!;

  private ICategoryService _categoryService = null!;

  #endregion

  #region Setup

  [SetUp]
  public void Setup()
  {
    _categoryRepository = new Mock<ICategoryRepository>();
    _unitOfWork = new Mock<IUnitOfWork>();

    _categoryService = new CategoryService(_categoryRepository.Object,
                                            new CreateCategoryValidator(),
                                            new RenameCategoryValidator(),
                                            new ArchiveCategoryValidator(),
                                            new ChangeDescriptionValidator(),
                                            _unitOfWork.Object);
  }

  #endregion

  #region Helpers

  private static CreateCategoryCommand CreateValidCommand(
    string name = "Food",
    long? userId = 1,
    CategoryKind kind = CategoryKind.Expense,
    string? description = null)
  {
    return new CreateCategoryCommand
    {
      Name = name,
      UserId = userId,
      CategoryKind = kind,
      Description = description
    };
  }

  #endregion
  
  [Test]
  public void CreateCategory_ValidCommand_ReturnsSuccess()
  {
    // Arrange

    var command = CreateValidCommand();

    _categoryRepository
      .Setup(r => r.GetByName(command.UserId, command.Name))
      .Returns((Category?)null);

    // Act

    var result = _categoryService.CreateCategory(command);

    // Assert

    using (Assert.EnterMultipleScope())
    {
      Assert.That(result.HasErrors, Is.False);

      _categoryRepository.Verify(r => r.Add(It.IsAny<Category>()), Times.Once);

      _unitOfWork.Verify(u => u.SaveChanges(), Times.Once);
    }
  }
  
  [Test]
  public void CreateCategory_InvalidCommand_DoNotAddCategory()
  {
    // Arrange

    var command = CreateValidCommand(name: "");

    // Act

    var result = _categoryService.CreateCategory(command);

    // Assert

    using (Assert.EnterMultipleScope())
    {
      Assert.That(result.HasErrors, Is.True);

      _categoryRepository.Verify(r => r.Add(It.IsAny<Category>()), Times.Never);

      _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
    }
  }
  
  [Test]
  public void CreateCategory_DuplicateName_ReturnsValidationError()
  {
    // Arrange

    var command = CreateValidCommand();

    var category = new Category(
      command.Name,
      command.Description,
      command.UserId,
      command.CategoryKind
      );

    _categoryRepository
      .Setup(r => r.GetByName(command.UserId, command.Name))
      .Returns(category);

    // Act

    var result = _categoryService.CreateCategory(command);

    // Assert

    using (Assert.EnterMultipleScope())
    {
      Assert.That(result.HasErrors, Is.True);

      _categoryRepository.Verify(r => r.Add(It.IsAny<Category>()), Times.Never);

      _unitOfWork.Verify(u => u.SaveChanges(), Times.Never);
    }
  }
  
  [Test]
  public void RenameCategory_ValidCommand_ReturnsSuccess()
  {
    // Arrange

    var category = new Category(
      "Food",
      null, 
      1,
      CategoryKind.Expense
      );
    
    var propertyInfo = typeof(Category).GetProperty("Id");
    if (propertyInfo != null && propertyInfo.CanWrite)
    {
      propertyInfo.SetValue(category, 1);
    }

    var command = new RenameCategoryCommand
    {
      UserId = 1,
      CategoryId = category.Id,
      NewName = "Products"
    };

    _categoryRepository
      .Setup(r => r.GetById(command.CategoryId))
      .Returns(category);

    _categoryRepository
      .Setup(r => r.GetByName(command.UserId, command.NewName!))
      .Returns((Category?)null);

    // Act

    var result = _categoryService.RenameCategory(command);

    // Assert

    using (Assert.EnterMultipleScope())
    {
      Assert.That(result.HasErrors, Is.False);
      Assert.That(category.Name, Is.EqualTo("Products"));

      _unitOfWork.Verify(
        u => u.SaveChanges(),
        Times.Once);
    }
  }
  
  [Test]
  public void RenameCategory_CategoryNotFound_ReturnsValidationError()
  {
    // Arrange

    var command = new RenameCategoryCommand
    {
      UserId = 1,
      CategoryId = 15,
      NewName = "Products"
    };

    _categoryRepository
      .Setup(r => r.GetById(command.CategoryId))
      .Returns((Category?)null);

    // Act

    var result = _categoryService.RenameCategory(command);

    // Assert

    using (Assert.EnterMultipleScope())
    {
      Assert.That(result.HasErrors, Is.True);

      _unitOfWork.Verify(
        u => u.SaveChanges(),
        Times.Never);
    }
  }
  
  [Test]
  public void RenameCategory_DuplicateName_ReturnsValidationError()
  {
    // Arrange

    var category = new Category(
      "Food",
      null,
      1,
      CategoryKind.Expense
    );

    var duplicate = new Category(
      "Products",
      null,
      1,
      CategoryKind.Expense
      );

    var command = new RenameCategoryCommand
    {
      UserId = 1,
      CategoryId = category.Id,
      NewName = "Products"
    };

    _categoryRepository
      .Setup(r => r.GetById(command.CategoryId))
      .Returns(category);

    _categoryRepository
      .Setup(r => r.GetByName(command.UserId, command.NewName!))
      .Returns(duplicate);

    // Act

    var result = _categoryService.RenameCategory(command);

    // Assert

    using (Assert.EnterMultipleScope())
    {
      Assert.That(result.HasErrors, Is.True);

      _unitOfWork.Verify(
        u => u.SaveChanges(),
        Times.Never);
    }
  }
  
  [Test]
  public void ChangeDescriptionCategory_ValidCommand_ReturnsSuccess()
  {
    // Arrange

    var category = new Category(
      "Food",
      null,
      1,
      CategoryKind.Expense
    );
    
    var propertyInfo = typeof(Category).GetProperty("Id");
    if (propertyInfo != null && propertyInfo.CanWrite)
    {
      propertyInfo.SetValue(category, 1);
    }

    var command = new ChangeDescriptionCommand
    {
      CategoryId = category.Id,
      NewDescription = "New Description"
    };

    _categoryRepository
      .Setup(r => r.GetById(command.CategoryId))
      .Returns(category);

    // Act

    var result = _categoryService.ChangeDescriptionCategory(command);

    // Assert

    using (Assert.EnterMultipleScope())
    {
      Assert.That(result.HasErrors, Is.False);
      Assert.That(category.Description, Is.EqualTo("New Description"));

      _unitOfWork.Verify(
        u => u.SaveChanges(),
        Times.Once);
    }
  }
  
  [Test]
  public void ChangeDescriptionCategory_CategoryNotFound_ReturnsValidationError()
  {
    // Arrange

    var command = new ChangeDescriptionCommand
    {
      CategoryId = 20,
      NewDescription = "Description"
    };

    _categoryRepository
      .Setup(r => r.GetById(command.CategoryId))
      .Returns((Category?)null);

    // Act

    var result = _categoryService.ChangeDescriptionCategory(command);

    // Assert

    using (Assert.EnterMultipleScope())
    {
      Assert.That(result.HasErrors, Is.True);

      _unitOfWork.Verify(
        u => u.SaveChanges(),
        Times.Never);
    }
  } 
  
  [Test]
  public void ArchiveCategory_ValidCommand_ReturnsSuccess()
  {
    // Arrange

    var newCategory = new Category(
      "Food",
      null,
      1,
      CategoryKind.Expense
    );
    
    var propertyInfo = typeof(Category).GetProperty("Id");
    if (propertyInfo != null && propertyInfo.CanWrite)
    {
      propertyInfo.SetValue(newCategory, 1);
    }
    
    _categoryRepository
      .Setup(r => r.GetById(newCategory.Id))
      .Returns(newCategory);

    var command = new ArchiveCategoryCommand
    {
      CategoryId = newCategory.Id
    };

    // Act

    var result = _categoryService.ArchiveCategory(command);

    // Assert

    using (Assert.EnterMultipleScope())
    {
      Assert.That(result.HasErrors, Is.False);
      Assert.That(newCategory.IsArchived, Is.True);

      _unitOfWork.Verify(
        u => u.SaveChanges(),
        Times.Once);
    }
  }
  
  [Test]
  public void ArchiveCategory_CategoryNotFound_ReturnsValidationError()
  {
    // Arrange

    var command = new ArchiveCategoryCommand
    {
      CategoryId = 10
    };

    _categoryRepository
      .Setup(r => r.GetById(command.CategoryId))
      .Returns((Category?)null);

    // Act

    var result = _categoryService.ArchiveCategory(command);

    // Assert

    using (Assert.EnterMultipleScope())
    {
      Assert.That(result.HasErrors, Is.True);

      _unitOfWork.Verify(
        u => u.SaveChanges(),
        Times.Never);
    }
  }
}