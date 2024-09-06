using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Filters;

public class BookingSieve
{
    public decimal? MaxPrice { get; set; }
    public string DepartureCountry { get; set; }
    public string destinationCountry { get; set; }
    public DateTime? departureDate { get; set; }
    public string departureAirport { get; set; }
    public string arrivalAirport { get; set; }
    public Guid? FlightId { get; set; }
    public Guid? PassengerId { get; set; }
    public FlightClass FlightClass { get; set; }
}