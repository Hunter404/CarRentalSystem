namespace CarRentalSystem.Tests;

using App;
using App.Models;
using App.Services;
using Common;
using Microsoft.EntityFrameworkCore;
using Moq;

[TestFixture]
public class RentalRateServiceTests
{
    private readonly Mock<DbSet<RentalRate>> _rentalRateSet;
    private readonly Mock<ICarRentalDbContext> _dbContext;
    private readonly RentalRateService _service;

    public RentalRateServiceTests()
    {
        // Initialize the DbContext and DbSet mocks
        _dbContext = new ();
        _rentalRateSet = new ();
        
        // Setup the RentalRateService with the mock DbContext
        _service = new (_dbContext.Object);
    }

    [SetUp]
    public void SetUp()
    {
        var rentalRates = new List<RentalRate>
        {
            new () { Id = 1, Category = "Small car", BaseDayRental = 1m, DayRentalScale = 1m, BaseKmPrice = 0m, KmPriceScale = 0m },
            new () { Id = 2, Category = "Combi", BaseDayRental = 1m, DayRentalScale = 1.3m, BaseKmPrice = 1m, KmPriceScale = 1m },
            new () { Id = 3, Category = "Truck", BaseDayRental = 1m, DayRentalScale = 1.5m, BaseKmPrice = 1m, KmPriceScale = 1.5m },
        }.AsQueryable();

        _dbContext.Setup(m => m.RentalRates)
            .Returns(rentalRates.AsQueryable().BuildMockDbSet().Object);
    }

    [Test]
    public async Task GetAllRentalRates_ReturnsAllRentalRates()
    {
        // Arrange

        // Act
        var result = await _service.GetAllRentalRates();
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Count, Is.EqualTo(3));
        });
    }
    
    [Test]
    public async Task RentalRates_CalculatesCorrectBusinessLogic()
    {
        // Arrange
        var numberOfDays = 7;
        var numberOfKm = 1000;
        
        // Act
        var rentalRates = await _service.GetAllRentalRates();
        
        // Assert
        Assert.Multiple(() =>
        {
            // baseDayRental * numberOfDays
            var smallCarPrice = 1m * numberOfDays;
            var smallCarResult = rentalRates
                .First(x => x.Category == "Small car")
                .CalculateRentalPrice(numberOfDays, numberOfKm);
            
            Assert.That(smallCarResult, Is.EqualTo(smallCarPrice));
            
            // baseDayRental * numberOfDays * 1.3 + baseKmPrice * numberOfKm
            var combiPrice = 1m * numberOfDays * 1.3m + 1m * numberOfKm;
            var combiCarResult = rentalRates
                .First(x => x.Category == "Combi")
                .CalculateRentalPrice(numberOfDays, numberOfKm);
            
            Assert.That(combiPrice, Is.EqualTo(combiCarResult));

            // baseDayRental * numberOfDays * 1.5 + baseKmPrice * numberOfKm * 1.5
            var truckPrice = 1m * numberOfDays * 1.5m + 1m * numberOfKm * 1.5m;
            var truckResult = rentalRates
                .First(x => x.Category == "Truck")
                .CalculateRentalPrice(numberOfDays, numberOfKm);

            Assert.That(truckResult, Is.EqualTo(truckPrice));
        });
    }
}
