using AirportTicketBookingSystem.Filters;
using AirportTicketBookingSystem.Interfaces;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Services;

public class BookingSearchService : IBookingSearchService
{

    private readonly List<Flight> _flights;
    private readonly List<Booking> _bookings;

    public BookingSearchService(List<Flight> flights, List<Booking> bookings)
    {
        _flights = flights;
        _bookings = bookings;
    }

    public IEnumerable<Booking> GetSearchBookings(Sieve sieve)
    {
        var query = _bookings.AsQueryable();

        if (sieve.MaxPrice.HasValue || !string.IsNullOrEmpty(sieve.DepartureCountry) || !string.IsNullOrEmpty(sieve.DestinationCountry) 
            || sieve.DepartureDate.HasValue || !string.IsNullOrEmpty(sieve.DepartureAirport) || !string.IsNullOrEmpty(sieve.DestinationAirport))
        {
            var _flightService = new FlightService(_flights);
            var matchingFlights = _flightService.GetSearchFlights(sieve);
            var matchingFlightIds = matchingFlights.Select(flight => flight.FlightId);
            query = query.Where(booking => matchingFlightIds.Contains(booking.FlightId));
        }
        
        if (sieve.FlightId.HasValue)
        {
            query = query.Where(b => b.FlightId == sieve.FlightId.Value);
        }

        if (sieve.PassengerId.HasValue)
        {
            query = query.Where(b => b.PassengerId == sieve.PassengerId.Value);
        }

        if (sieve.FlightClass.HasValue) 
        {
            query = query.Where(b => b.FlightClass == sieve.FlightClass);
        }
        
        
        
        return query.ToList();
    }
}