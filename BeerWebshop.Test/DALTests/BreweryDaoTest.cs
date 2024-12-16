using BeerWebshop.DAL.DATA.DAO.DAOClasses;
using BeerWebshop.DAL.DATA.Entities;

namespace BeerWebshop.Test.DALTests;

[TestFixture]
public class BreweryDaoTest
{
    private BreweryDAO _breweryDAO;
    private readonly string _testSuffix = "_Test";
    private readonly List<int> _breweryIdsCreated = new();

    [SetUp]
    public void SetUp()
    {
        _breweryDAO = new BreweryDAO(DBConnection.ConnectionString());
    }

    [TearDown]
    public async Task TearDownAsync()
    {
        foreach (var breweryId in _breweryIdsCreated)
        {
            await _breweryDAO.DeleteAsync(breweryId);
        }
    }

    [Test]
    public async Task CreateAsync_WhenBreweryDoesNotExist_ShouldCreateBrewery()
    {
        var breweryName = $"Brewery{_testSuffix}";
        var brewery = new Brewery { Name = breweryName };

        var breweryId = await _breweryDAO.CreateAsync(brewery);
        _breweryIdsCreated.Add(breweryId);

        Assert.That(breweryId, Is.GreaterThan(0));

        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _breweryDAO.CreateAsync(brewery));
        Assert.That(exception.Message, Is.EqualTo($"Brewery with name '{breweryName}' already exists."));
    }

    [Test]
    public async Task CreateAsync_WhenBreweryExists_ShouldThrowInvalidOperationException()
    {
        var breweryName = $"Brewery{_testSuffix}";
        var brewery = new Brewery { Name = breweryName };

        var breweryId = await _breweryDAO.CreateAsync(brewery);
        _breweryIdsCreated.Add(breweryId);

        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _breweryDAO.CreateAsync(brewery));
        Assert.That(exception.Message, Does.Contain($"Brewery with name '{breweryName}' already exists."));
    }
    [Test]
    public async Task DeleteBrewery_WhenBreweryExists_ShouldDeleteBrewery()
    {
        //Arrange
        var breweryName = $"Brewery{_testSuffix}";
        var brewery = new Brewery { Name = breweryName };
        //Act
        var breweryId = await _breweryDAO.CreateAsync(brewery);
        _breweryIdsCreated.Add(breweryId);
        var deleteResult = await _breweryDAO.DeleteAsync(breweryId);
        //Assert
        Assert.IsTrue(deleteResult, "DeleteAsync should return true when a product is successfully deleted.");
        var deletedBrewery = await _breweryDAO.GetByIdAsync(breweryId);
        Assert.IsNull(deletedBrewery, "GetByIdAsync should return null for a deleted product.");
    }
    [Test]
    public async Task GetByIdAsync_WhenBreweryExists_ShouldReturnBrewery()
    {
        //Arrange
        var breweryName = $"Brewery{_testSuffix}";
        var brewery = new Brewery { Name = breweryName };
        //Act
        var breweryId = await _breweryDAO.CreateAsync(brewery);
        _breweryIdsCreated.Add(breweryId);
        var breweryReturned = await _breweryDAO.GetByIdAsync(breweryId);
        //Assert
        Assert.That(breweryReturned, Is.Not.Null);
        Assert.That(brewery.Name, Is.EqualTo(breweryName));
    }
    [Test]
    public async Task GetBreweryIdByName_WhenBreweryExists_ShouldReturnNameFromId()
    {
        //Arrange
        var breweryName = $"Brewery{_testSuffix}";
        var brewery = new Brewery { Name = breweryName };
        //Act
        var breweryId = await _breweryDAO.CreateAsync(brewery);
        _breweryIdsCreated.Add(breweryId);
        var breweryIdFromName = await _breweryDAO.GetBreweryIdByName(breweryName);
        //Assert
        Assert.That(breweryIdFromName, Is.EqualTo(breweryId));

    }
}
