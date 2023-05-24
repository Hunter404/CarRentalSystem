using CarRentalSystem.App;
using CarRentalSystem.App.Interfaces;
using CarRentalSystem.App.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<App>()
    .AddTransient<ICarService, CarService>()
    .AddTransient<ICustomerService, CustomerService>()
    .AddTransient<IRentalRateService, RentalRateService>()
    .AddTransient<IRentalRegistrationService, RentalRegistrationService>()
    .AddDbContext<ICarRentalDbContext, CarRentalDbContext>(options => options.UseSqlite("Data Source=Database.db;"))
    .BuildServiceProvider();

serviceProvider
    .GetService<App>()
    !.Run();

namespace CarRentalSystem.App
{
    using System.Text.RegularExpressions;
    using Models;

    internal class App
    {
        private readonly ICarService _carService;
        private readonly ICustomerService _customerService;
        private readonly IRentalRateService _rentalRateService;
        private readonly IRentalRegistrationService _rentalRegistrationService;

        public App(ICarService carService, ICustomerService customerService, IRentalRateService rentalRateService, IRentalRegistrationService rentalRegistrationService)
        {
            _carService = carService;
            _customerService = customerService;
            _rentalRateService = rentalRateService;
            _rentalRegistrationService = rentalRegistrationService;
        }

        public void Run()
        {
            RegistrationOfCarDelivery();
            RegistrationOfReturnedCar();
        }

        private async void RegistrationOfCarDelivery()
        {
            // Note: More optimal to search for cars, but in this example we don't know the actual registration numbers of each car.
            // We could also lazy load RentalRate, but in this case we always want it included so we explicitly load it.
            var cars = await _carService.GetAllCarsWithRentalRates();
        
            foreach (var car in cars)
            {
                Console.WriteLine($"Car: {car.RegistrationNumber} Category: {car.RentalRate.Category}");
            }

            Console.Write("Enter car registration number to rent: ");
            var carRegistrationNumber = Console.ReadLine();
            var selectedCar = cars.FirstOrDefault(x => x.RegistrationNumber == carRegistrationNumber);
            if (selectedCar == null)
            {
                Console.WriteLine($"Couldn't find a car by registration number {carRegistrationNumber}");
                return;
            }
        
            // Registration of car delivery
        
            // Optional: register new customer
            var customer = new Customer()
            {
                FirstName = Path.GetRandomFileName(),
                SocialSecurityNumber = Path.GetRandomFileName(),
            };
            
            // Create new rental registration
            var newRentalRegistration = new RentalRegistration()
            {
                BookingNumber = Path.GetRandomFileName(),
                Customer = customer,
                Car = selectedCar,
                PickupDateAndTime = DateTime.Now,
                PickupMeterReadingKm = 12345,
            };
        
            Console.WriteLine($"Booking number = {newRentalRegistration.BookingNumber}");
            Console.WriteLine($"Customer Social Security Number = {newRentalRegistration.Customer.SocialSecurityNumber}");
            Console.WriteLine($"Pickup Date and Time = {newRentalRegistration.PickupDateAndTime}");
            Console.WriteLine($"Pickup Meter Reading KM = {newRentalRegistration.PickupMeterReadingKm}");

            await _rentalRegistrationService.CreateRentalRegistration(newRentalRegistration);
        }

        private async void RegistrationOfReturnedCar()
        {
            Console.Write("Enter booking number: ");
            var bookingNumber = Console.ReadLine();
            if (bookingNumber == null)
            {
                Console.WriteLine("Incorrect booking number");
                return;
            }

            var returnedRentalRegistration = await _rentalRegistrationService.GetRentalRegistrationByBookingNumber(bookingNumber);
            if (returnedRentalRegistration == null)
            {
                Console.WriteLine($"Incorrect booking number {bookingNumber}");
                return;
            }
        
            returnedRentalRegistration.ReturnMeterReadingKm = returnedRentalRegistration.PickupMeterReadingKm + 1000;
            returnedRentalRegistration.ReturnDateAndTime = returnedRentalRegistration.PickupDateAndTime + TimeSpan.FromDays(7);
            if ((returnedRentalRegistration.ReturnDateAndTime - returnedRentalRegistration.PickupDateAndTime).TotalDays < 0)
            {
                Console.WriteLine($"Cannot return car before pickup date and time.");
                return;
            }

            Console.WriteLine($"Rented Days = {(returnedRentalRegistration.ReturnDateAndTime - returnedRentalRegistration.PickupDateAndTime).TotalDays:F0}");
            Console.WriteLine($"Driven KM = {returnedRentalRegistration.ReturnMeterReadingKm - returnedRentalRegistration.PickupMeterReadingKm}");
            Console.WriteLine($"Price = {returnedRentalRegistration.CalculatePrice()}");
        }
    }
}
