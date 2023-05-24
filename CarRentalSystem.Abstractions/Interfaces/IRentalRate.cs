namespace CarRentalSystem.Interfaces;

/// <summary>
/// Represents the different rental rates per car category.
/// </summary>
public interface IRentalRate
{
    /// <summary>
    /// Represents the base rate for one day's rental of a car.
    /// </summary>
    public decimal BaseDayRental { get; set; }

    /// <summary>
    /// Represents the scaling factor applied to the base day rental rate based on car category.
    /// </summary>
    public decimal DayRentalScale { get; set; }
    
    /// <summary>
    /// Represents the base price per kilometer driven for the car rental.
    /// </summary>
    public decimal BaseKmPrice { get; set; }
    
    /// <summary>
    /// Represents the scaling factor applied to the base kilometer price based on car category.
    /// </summary>
    public decimal KmPriceScale { get; set; }

    /// <summary>
    /// Represents the category of the car. The category influences the day rental rate and km price scaling factor.
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// Calculates the total rental price based on the number of days the car is rented and the number of kilometers driven.
    /// </summary>
    /// <param name="numberOfDays">The number of days the car is rented.</param>
    /// <param name="numberOfKm">The number of kilometers driven during the rental period.</param>
    /// <returns>The total price for the car rental.</returns>
    public decimal CalculateRentalPrice(int numberOfDays, int numberOfKm);
}