namespace CarRentalSystem.App.Services;

using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

public class RentalRegistrationService : IRentalRegistrationService
{
    private readonly ICarRentalDbContext _context;

    public RentalRegistrationService(ICarRentalDbContext context)
    {
        _context = context;
    }

    public async Task<RentalRegistration?> GetRentalRegistrationByBookingNumber(string bookingNumber)
    {
        var registration = _context.RentalRegistrations
            .Include(r => r.Customer)
            .Include(r => r.Car)
            .ThenInclude(c => c.RentalRate)
            .FirstOrDefault(r => r.BookingNumber == bookingNumber);

        return await Task.FromResult(registration);
    }
    
    public async Task<RentalRegistration> CreateRentalRegistration(RentalRegistration rentalRegistration)
    {
        _context.RentalRegistrations.Add(rentalRegistration);
        await _context.SaveChangesAsync();
        return rentalRegistration;
    }
}