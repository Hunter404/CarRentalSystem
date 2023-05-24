namespace CarRentalSystem.App.Services;

using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

public class RentalRateService : IRentalRateService
{
    private readonly ICarRentalDbContext _context;

    public RentalRateService(ICarRentalDbContext context)
    {
        _context = context;
    }

    public async Task<IList<RentalRate>> GetAllRentalRates()
    {
        return await _context.RentalRates.ToListAsync();
    }
}
