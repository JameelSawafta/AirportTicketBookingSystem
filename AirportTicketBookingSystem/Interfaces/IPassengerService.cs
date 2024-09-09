using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Interfaces;

public interface IPassengerService
{
    public Passenger PassengerSignIn(string email, string password);
    public Passenger CreatePassenger(Passenger passenger);

    public IEnumerable<Passenger> GetAllPassengers();

    public void DeleteBooking(Guid PassengerId, Booking booking);
}