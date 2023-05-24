namespace CarRentalSystem.App.Interfaces;

using Models;

public interface IRentalRateService
{
    Task<IList<RentalRate>> GetAllRentalRates();
}
