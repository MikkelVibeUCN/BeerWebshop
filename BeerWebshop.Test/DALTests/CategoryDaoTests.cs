using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.Test.DALTests;

[TestFixture]
public class CategoryDaoTests
{
	private CategoryDAO _categoryDao;
	private string _testSuffix = "_Test";

	[SetUp]

	public void SetUp()
	{
		_categoryDao = new CategoryDAO(DBConnection.ConnectionString());
	}

	[Test]
	public async Task CreateAsync_WhenCategoryExist_ShouldThrowSqlException()
	{
		var categoryName = $"Category{_testSuffix}";

		var category = new Category{Name = categoryName, IsDeleted = false};

		var accountId = await _categoryDao.CreateAsync(category);

		var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _categoryDao.CreateAsync(category));

		Assert.That(exception.Message, Is.EqualTo($"Category with name '{categoryName}' already exists."));

		await _categoryDao.DeleteAsync(accountId);
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
