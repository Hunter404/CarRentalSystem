namespace CarRentalSystem.App.Models;

using System.ComponentModel.DataAnnotations;
using CarRentalSystem.Interfaces;

public class Customer : ICustomer
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string SocialSecurityNumber { get; set; }
}