using AirportTicketBookingSystem.Filters;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Interfaces;

public interface IFlightService
{
    public void AddFlights(IEnumerable<Flight> flights);
    public IEnumerable<Flight> GetSearchFlights(Sieve flightSieve);
}