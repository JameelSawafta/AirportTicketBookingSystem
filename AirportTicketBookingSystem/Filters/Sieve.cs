using AirportTicketBookingSystem.Interfaces;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Filters;

public class Sieve
{
    public decimal? MaxPrice { get; set; }
    public string DepartureCountry { get; set; }
    public string DestinationCountry { get; set; }
    public DateTime? DepartureDate { get; set; }
    public string DepartureAirport { get; set; }
    public string DestinationAirport { get; set; }
    public Guid? FlightId { get; set; }
    public Guid? PassengerId { get; set; }
    public FlightClass FlightClass { get; set; }
}