namespace CarRentalSystem.App;

using Microsoft.EntityFrameworkCore;
using Models;

public interface ICarRentalDbContext
{
    DbSet<Car> Cars { get; set; }
    DbSet<RentalRegistration> RentalRegistrations { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<RentalRate> RentalRates { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}