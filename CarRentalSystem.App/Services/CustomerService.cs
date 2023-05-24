namespace CarRentalSystem.App.Services;

using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

public class CustomerService : ICustomerService
{
    private readonly ICarRentalDbContext _context;

    public CustomerService(ICarRentalDbContext context)
    {
        _context = context;
    }
}