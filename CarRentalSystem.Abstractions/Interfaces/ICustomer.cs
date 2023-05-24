namespace CarRentalSystem.Interfaces;

/// <summary>
/// Represents a customer who can rent cars.
/// </summary>
public interface ICustomer
{
    /// <summary>
    /// Gets or sets the first name of the customer.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the social security number of the customer.
    /// </summary>
    public string SocialSecurityNumber { get; set; }
}