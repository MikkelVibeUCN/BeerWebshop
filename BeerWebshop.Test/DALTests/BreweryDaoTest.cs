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
}
