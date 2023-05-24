namespace CarRentalSystem.Interfaces;

/// <summary>
/// Represents a rentable car.
/// </summary>
public interface ICar
{
    /// <summary>
    /// Represents a car's registration number.
    /// </summary>
    public string RegistrationNumber { get; set; }
    
    /// <summary>
    /// Represents a car's rental category and rate.
    /// </summary>
    public IRentalRate RentalRate { get; set; }
}
