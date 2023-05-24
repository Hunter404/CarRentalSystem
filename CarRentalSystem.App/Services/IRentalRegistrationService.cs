namespace CarRentalSystem.App.Interfaces;

using Models;

public interface IRentalRegistrationService
{
    Task<RentalRegistration?> GetRentalRegistrationByBookingNumber(string bookingNumber);
    Task<RentalRegistration> CreateRentalRegistration(RentalRegistration rentalRegistration);
}
