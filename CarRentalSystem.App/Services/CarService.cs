namespace CarRentalSystem.App.Services;

using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

public class CarService : ICarService
{
    private readonly ICarRentalDbContext _context;

    public CarService(ICarRentalDbContext carRentalDbContext)
    {
        _context = carRentalDbContext;
    }

    public async Task<IList<Car>> GetAllCarsWithRentalRates()
    {
        return await _context.Cars.Include(x => x.RentalRate).ToListAsync();
    }
}
