namespace CarRentalSystem.App.Interfaces;

using Models;

public interface ICarService
{
    Task<IList<Car>> GetAllCarsWithRentalRates();
}