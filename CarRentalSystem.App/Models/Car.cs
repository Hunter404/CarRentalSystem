namespace CarRentalSystem.App.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarRentalSystem.Interfaces;

public class Car : ICar
{
    [Key]
    public int Id { get; set; }

    public string RegistrationNumber { get; set; }

    public int RentalRateId { get; set; }
    [ForeignKey("RentalRateId")]
    public RentalRate RentalRate { get; set; }

    IRentalRate ICar.RentalRate
    {
        get => RentalRate;
        set => RentalRate = (RentalRate) value;
    }
}
