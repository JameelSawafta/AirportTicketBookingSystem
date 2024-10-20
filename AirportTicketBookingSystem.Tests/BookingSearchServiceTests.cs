using AirportTicketBookingSystem.Filters;
using AirportTicketBookingSystem.Interfaces;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Services;
using Moq;

namespace AirportTicketBookingSystem.Tests;

public class BookingSearchServiceTests
{
    private readonly List<Flight> _flights;
    private readonly List<Booking> _bookings;
    private readonly Mock<IFlightService> _mockFlightService;
    private readonly BookingSearchService _bookingSearchService;

    public BookingSearchServiceTests()
    {
        _flights = new List<Flight>
        {
            new Flight("Flight A", "Airline A", "Country A", "Country B", "Airport A", "Airport B", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), 200, 400, 600, 100, 50, 20),
            new Flight("Flight B", "Airline B", "Country C", "Country D", "Airport C", "Airport D", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), 300, 500, 700, 150, 80, 30)

        };
        
        _bookings = new List<Booking>
        {
            new Booking(_flights[0].FlightId, Guid.NewGuid(), FlightClass.Economy, DateTime.Now, 100),
            new Booking(_flights[1].FlightId, Guid.NewGuid(), FlightClass.Business, DateTime.Now, 150)
        };

        _mockFlightService = new Mock<IFlightService>();
        
        _bookingSearchService = new BookingSearchService(_flights, _bookings, _mockFlightService.Object);
    }
    
    
    [Fact]
    public void GetSearchBookings_ShouldReturnsMatchingBookings_WhenValidSieve()
    {
        // Arrange
        var sieve = new Sieve
        {
            DepartureCountry = "Country A",
            DestinationCountry = "Country B"
        };

        _mockFlightService.Setup(fs => fs.GetSearchFlights(sieve)).Returns(new List<Flight> { _flights[0] });

        // Act
        var result = _bookingSearchService.GetSearchBookings(sieve);

        // Assert
        Assert.Single(result);
        Assert.Equal(_bookings[0].BookingId, result.First().BookingId);
    }

    [Fact]
    public void GetSearchBookings_ShouldReturnsEmptyList_WhenNonExistingFlight()
    {
        // Arrange
        var sieve = new Sieve
        {
            FlightId = Guid.NewGuid() // This flight ID does not exist
        };

        // Act
        var result = _bookingSearchService.GetSearchBookings(sieve);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetSearchBookings_ShouldReturnsFilteredBookings_WhenFlightClass()
    {
        // Arrange
        var sieve = new Sieve
        {
            FlightClass = FlightClass.Business
        };
        
        // Act
        var result = _bookingSearchService.GetSearchBookings(sieve);

        // Assert
        Assert.Single(result);
        Assert.Equal(_bookings[1].BookingId, result.First().BookingId);
    }

    [Fact]
    public void GetSearchBookings_ShouldReturnsAllBookings()
    {
        // Arrange
        var sieve = new Sieve();

        // Act
        var result = _bookingSearchService.GetSearchBookings(sieve);

        // Assert
        Assert.Equal(_bookings.Count, result.Count());
    }
}