namespace CarRentalSystem.Interfaces;

/// <summary>
/// Represents a registration of a rented car to a customer.
/// </summary>
public interface IRentalRegistration
{
    /// <summary>
    /// Gets or sets this registration's booking number.
    /// </summary>
    public string BookingNumber { get; set; }

    /// <summary>
    /// Gets or sets this registration's customer.
    /// </summary>
    public ICustomer Customer { get; set; }

    /// <summary>
    /// Gets or sets this registration's car.
    /// </summary>
    public ICar Car { get; set; }
    
    /// <summary>
    /// Gets or sets this registration's Km read before handing out the car.
    /// </summary>
    public int PickupMeterReadingKm { get; set; }

    /// <summary>
    /// Gets or sets the date and time car was handed out.
    /// </summary>
    public DateTime PickupDateAndTime { get; set; }
    
    /// <summary>
    /// Gets or sets this registration's Km read after car was returned.
    /// </summary>
    public int ReturnMeterReadingKm { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time car was returned.
    /// </summary>
    public DateTime ReturnDateAndTime { get; set; }

    /// <summary>
    /// Calculate the price of this finished rent.
    /// </summary>
    /// <returns></returns>
    public decimal CalculatePrice();
}
