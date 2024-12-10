using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.Test.DALTests;

[TestFixture]
public class CategoryDaoTests
{
    private CategoryDAO _categoryDao;
    private readonly string _testSuffix = "_Test";
    private readonly List<int> _categoryIdsCreated = new();

    [SetUp]
    public void SetUp()
    {
        _categoryDao = new CategoryDAO(DBConnection.ConnectionString());
    }

    [TearDown]
    public async Task TearDownAsync()
    {
        foreach (var categoryId in _categoryIdsCreated)
        {
            await _categoryDao.DeleteAsync(categoryId);
        }
    }

    [Test]
    public async Task CreateAsync_WhenCategoryExists_ShouldThrowInvalidOperationException()
    {
        var categoryName = $"Category{_testSuffix}";

        var category = new Category { Name = categoryName, IsDeleted = false };

        var categoryId = await _categoryDao.CreateAsync(category);
        _categoryIdsCreated.Add(categoryId);

        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _categoryDao.CreateAsync(category));

        Assert.That(exception.Message, Is.EqualTo($"Category with name '{categoryName}' already exists."));
    }

    [Test]
    public void CreateAsync_WhenCategoryNameIsNull_ShouldThrowArgumentException()
    {
        var category = new Category { Name = null, IsDeleted = false };

        var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _categoryDao.CreateAsync(category));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception.Message, Does.Contain("Category name cant be null or empty."));
    }
}
