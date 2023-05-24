namespace CarRentalSystem.App;

using Microsoft.EntityFrameworkCore;
using Models;

public class CarRentalDbContext : DbContext, ICarRentalDbContext
{
    public DbSet<Car> Cars { get; set; }
    
    public DbSet<RentalRegistration> RentalRegistrations { get; set; }
    
    public DbSet<Customer> Customers { get; set; }
    
    public DbSet<RentalRate> RentalRates { get; set; }

    public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options)
        : base(options)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=Database.db;");
        }

        base.OnConfiguring(optionsBuilder);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var smallCarRentalRate = new RentalRate { Id = 1, Category = "Small car", BaseDayRental = 100, BaseKmPrice = 0, DayRentalScale = 1, KmPriceScale = 0 };
        var combiRentalRate = new RentalRate { Id = 2, Category = "Combi", BaseDayRental = 150, BaseKmPrice = 10, DayRentalScale = 1.3m, KmPriceScale = 1m };
        var truckRentalRate = new RentalRate { Id = 3, Category = "Truck", BaseDayRental = 300, BaseKmPrice = 20, DayRentalScale = 1.5m, KmPriceScale = 1.5m };

        var smallCar = new Car { Id = 1, RentalRateId = smallCarRentalRate.Id, RegistrationNumber = "ABC123" };
        var combiCar = new Car { Id = 2, RentalRateId = combiRentalRate.Id, RegistrationNumber = "BCD234" };
        var truckCar = new Car { Id = 3, RentalRateId = truckRentalRate.Id, RegistrationNumber = "CDF345" };

        var customer = new Customer { Id = 1, FirstName = "John", SocialSecurityNumber = "1234567890" };

        var smallCarRentalRegistration = new RentalRegistration { Id = 1, CustomerId = customer.Id, CarId = smallCar.Id, PickupMeterReadingKm = 123, ReturnMeterReadingKm = 234, PickupDateAndTime = DateTime.Now, ReturnDateAndTime = DateTime.Now.AddDays(7), BookingNumber = Path.GetRandomFileName() };
        var combiRentalRegistration = new RentalRegistration { Id = 2, CustomerId = customer.Id, CarId = combiCar.Id, PickupMeterReadingKm = 234, ReturnMeterReadingKm = 345, PickupDateAndTime = DateTime.Now, ReturnDateAndTime = DateTime.Now.AddDays(14), BookingNumber = Path.GetRandomFileName() };
        var truckRentalRegistration = new RentalRegistration { Id = 3, CustomerId = customer.Id, CarId = truckCar.Id, PickupMeterReadingKm = 345, ReturnMeterReadingKm = 456, PickupDateAndTime = DateTime.Now, ReturnDateAndTime = DateTime.Now.AddDays(27), BookingNumber = Path.GetRandomFileName() };
        
        // Rental rates
        modelBuilder.Entity<RentalRate>().HasData(smallCarRentalRate, combiRentalRate, truckRentalRate);
        
        // Seed Cars
        modelBuilder.Entity<Car>().HasData(smallCar, combiCar, truckCar);

        // Seed Customers
        modelBuilder.Entity<Customer>().HasData(customer);

        // Seed Rental Registrations
        modelBuilder.Entity<RentalRegistration>().HasData(smallCarRentalRegistration, combiRentalRegistration, truckRentalRegistration);
    }
}
