using AirportTicketBookingSystem.Filters;
using AirportTicketBookingSystem.Interfaces;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Services;

public class FlightService : IFlightService
{
    private readonly List<Flight> _flights;

    public FlightService(List<Flight> flights)
    {
        _flights = flights;
    }

    public void AddFlights(IEnumerable<Flight> flights)
    {
        _flights.AddRange(flights);
    }

    public void ImportFlightsFromCsv(string filePath)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Flight> GetSearchFlights(Sieve flightSieve)
    {
        var query = _flights.AsQueryable();
        if (flightSieve.MaxPrice.HasValue)
        {
            query = query.Where(f => 
                (flightSieve.FlightClass == FlightClass.Economy && f.EconomyPrice <= flightSieve.MaxPrice) ||
                (flightSieve.FlightClass == FlightClass.Business && f.BusinessPrice <= flightSieve.MaxPrice) ||
                (flightSieve.FlightClass == FlightClass.FirstClass && f.FirstClassPrice <= flightSieve.MaxPrice));

        }

        if (!string.IsNullOrEmpty(flightSieve.DepartureCountry))
        {
            query = query.Where(b => b.DepartureCountry.Equals(flightSieve.DepartureCountry, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(flightSieve.DestinationCountry))
        {
            query = query.Where(b => b.DestinationCountry.Equals(flightSieve.DestinationCountry, StringComparison.OrdinalIgnoreCase));
        }

        if (flightSieve.DepartureDate.HasValue)
        {
            query = query.Where(b => b.DepartureDate.Date == flightSieve.DepartureDate.Value.Date);
        }

        if (!string.IsNullOrEmpty(flightSieve.DepartureAirport))
        {
            query = query.Where(b => b.DepartureAirport.Equals(flightSieve.DepartureAirport, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(flightSieve.DestinationAirport))
        {
            query = query.Where(b => b.DestinationAirport.Equals(flightSieve.DestinationAirport, StringComparison.OrdinalIgnoreCase));
        }

        if (flightSieve.FlightClass.HasValue)
        {
            if (flightSieve.FlightClass == FlightClass.Economy)
            {
                query = query.Where(f => f.AvailableSeatsEconomy > 0);
            }
            else if (flightSieve.FlightClass == FlightClass.Business)
            {
                query = query.Where(f => f.AvailableSeatsBusiness > 0);
            }
            else if (flightSieve.FlightClass == FlightClass.FirstClass)
            {
                query = query.Where(f => f.AvailableSeatsFirstClass > 0);
            }
        }
        
        return query.ToList();
    }
}