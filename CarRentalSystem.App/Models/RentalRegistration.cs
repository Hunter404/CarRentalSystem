namespace CarRentalSystem.App.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarRentalSystem.Interfaces;
using Microsoft.EntityFrameworkCore;

[Index("BookingNumber", IsUnique = true)]
public class RentalRegistration : IRentalRegistration
{
    [Key]
    public int Id { get; set; }
    
    public string BookingNumber { get; set; }
    
    public int CustomerId { get; set; }
    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; }

    public int CarId { get; set; }
    [ForeignKey("CarId")]
    public Car Car { get; set; }

    public DateTime PickupDateAndTime { get; set; }
    
    public int PickupMeterReadingKm { get; set; }

    public DateTime ReturnDateAndTime { get; set; }
    
    public int ReturnMeterReadingKm { get; set; }
    
    // Interface implementation
    ICar IRentalRegistration.Car
    {
        get => Car;
        set => Car = (Car)value;
    }

    ICustomer IRentalRegistration.Customer
    {
        get => Customer;
        set => Customer = (Customer)value;
    }

    public decimal CalculatePrice()
    {
        var numberOfDays = (int) Math.Ceiling((ReturnDateAndTime - PickupDateAndTime).TotalDays);
        var numberOfKm = (ReturnMeterReadingKm - PickupMeterReadingKm);

        return Car.RentalRate.CalculateRentalPrice(numberOfDays, numberOfKm);
    }
}