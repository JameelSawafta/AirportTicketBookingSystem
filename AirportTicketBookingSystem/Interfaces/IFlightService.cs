using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Interfaces;

public interface IFlightService
{
    public void AddFlights(IEnumerable<Flight> flights);
    public void GetSearchFlights(); // i need to add FlightSieve
}