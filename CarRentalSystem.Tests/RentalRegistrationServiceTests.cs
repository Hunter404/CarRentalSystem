namespace CarRentalSystem.Tests;

using App;
using App.Models;
using App.Services;
using Common;
using Microsoft.EntityFrameworkCore;
using Moq;

[TestFixture]
public class RentalRegistrationServiceTests
{
    private readonly Mock<DbSet<RentalRegistration>> _rentalRegistrationSet;
    private readonly Mock<ICarRentalDbContext> _dbContext;
    private readonly RentalRegistrationService _service;

    public RentalRegistrationServiceTests()
    {
        // Initialize the DbContext and DbSet mocks
        _dbContext = new ();
        _rentalRegistrationSet = new ();
        
        // Setup the RentalRegistrationService with the mock DbContext
        _service = new (_dbContext.Object);
    }

    [SetUp]
    public void SetUp()
    {
        var rentalRegistrations = new List<RentalRegistration>
        {
            new () { Id = 1, BookingNumber = "BN1", CarId = 1, CustomerId = 1, PickupDateAndTime = DateTime.Now, ReturnDateAndTime = DateTime.Now.AddDays(7), PickupMeterReadingKm = 1000, ReturnMeterReadingKm = 2000 },
            new () { Id = 2, BookingNumber = "BN2", CarId = 2, CustomerId = 2, PickupDateAndTime = DateTime.Now, ReturnDateAndTime = DateTime.Now.AddDays(5), PickupMeterReadingKm = 1500, ReturnMeterReadingKm = 2500 }
        }.AsQueryable();

        _dbContext.Setup(m => m.RentalRegistrations)
            .Returns(rentalRegistrations.AsQueryable().BuildMockDbSet().Object);
    }

    [Test]
    public async Task GetRentalRegistrationByBookingNumber_ReturnsCorrectRentalRegistration()
    {
        // Arrange
        var bookingNumber = "BN1";
        
        // Act
        var result = await _service.GetRentalRegistrationByBookingNumber(bookingNumber);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.BookingNumber, Is.EqualTo(bookingNumber));
        });
    }
    
    [Test]
    public async Task CreateRentalRegistration_AddsAndSavesRentalRegistration()
    {
        // Arrange
        var newRentalRegistration = new RentalRegistration { Id = 3, BookingNumber = "BN3", CarId = 3, CustomerId = 3, PickupDateAndTime = DateTime.Now, ReturnDateAndTime = DateTime.Now.AddDays(4), PickupMeterReadingKm = 1000, ReturnMeterReadingKm = 2000 };
        
        // Act
        var result = await _service.CreateRentalRegistration(newRentalRegistration);
        
        // Assert
        _dbContext.Verify(m => m.RentalRegistrations.Add(It.IsAny<RentalRegistration>()), Times.Once);
        _dbContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        Assert.That(result, Is.EqualTo(newRentalRegistration));
    }
}