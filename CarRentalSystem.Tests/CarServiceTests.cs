namespace CarRentalSystem.Tests;

using App;
using App.Models;
using App.Services;
using Common;
using Microsoft.EntityFrameworkCore;
using Moq;

[TestFixture]
public class CarServiceTests
{
    private readonly Mock<DbSet<Car>> _carSet;
    private readonly Mock<ICarRentalDbContext> _dbContext;
    private readonly CarService _service;

    public CarServiceTests()
    {
        // Initialize the DbContext and DbSet mocks
        _dbContext = new Mock<ICarRentalDbContext>();
        _carSet = new Mock<DbSet<Car>>();
        
        // Setup the RentalRegistrationService with the mock DbContext
        _service = new CarService(_dbContext.Object);
    }

    [SetUp]
    public void SetUp()
    {
        var cars = new List<Car>
        {
            new () { Id = 1, RegistrationNumber = "XYZ123", RentalRate = new RentalRate() },
            new () { Id = 2, RegistrationNumber = "ABC456", RentalRate = new RentalRate() },
        }.AsQueryable();

        _dbContext.Setup(m => m.Cars)
            .Returns(cars.AsQueryable().BuildMockDbSet().Object);
    }

    [Test]
    public async Task GetAllCarsWithRentalRates_ReturnsAllCarsWithRentalRates()
    {
        // Arrange

        // Act
        var result = await _service.GetAllCarsWithRentalRates();
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.All(car => car.RentalRate != null), Is.True);
        });
    }
}