using System.ComponentModel.DataAnnotations;
using AirportTicketBookingSystem.Interfaces;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Services;

public class PassengerService : IPassengerService
{
    private readonly List<Passenger> _passengers;

    public PassengerService(List<Passenger> passengers)
    {
        _passengers = passengers;
    }


    public Passenger PassengerSignIn(string email, string password)
    {
        var passenger = _passengers.FirstOrDefault(p => p.Email == email && p.Password == password);
        
        if (passenger == null)
        {
            Console.WriteLine("Invalid email or password.");
        }
        
        return passenger;
    }

    public Passenger CreatePassenger(Passenger passenger)
    {
        var isValid = ValidationService.Validation(passenger,x => _passengers.Add((Passenger)x));
        if (isValid)
        {
            return passenger;
        }
        return null;
    }
}