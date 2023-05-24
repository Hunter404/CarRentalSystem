namespace CarRentalSystem.App.Models;

using System.ComponentModel.DataAnnotations;
using CarRentalSystem.Interfaces;

public class RentalRate : IRentalRate
{
    [Key]
    public int Id { get; set; }
    public decimal BaseDayRental { get; set; }

    public decimal DayRentalScale { get; set; }
    
    public decimal BaseKmPrice { get; set; }
    
    public decimal KmPriceScale { get; set; }
    
    public string Category { get; set; }

    public decimal CalculateRentalPrice(int numberOfDays, int numberOfKm)
    {
        return BaseDayRental * numberOfDays * DayRentalScale
               + BaseKmPrice * numberOfKm * KmPriceScale;
    }
}
