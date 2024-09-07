using AirportTicketBookingSystem.Filters;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Interfaces;

public interface IFlightService
{
    public void AddFlights(IEnumerable<Flight> flights);
    public void ImportFlightsFromCsv(string filePath);
    
    public IEnumerable<Flight> GetSearchFlights(Sieve flightSieve);
}