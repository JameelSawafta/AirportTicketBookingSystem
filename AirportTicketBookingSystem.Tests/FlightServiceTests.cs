using AirportTicketBookingSystem.Filters;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Services;

namespace AirportTicketBookingSystem.Tests;

public class FlightServiceTests
{
    private List<Flight> _flights;
    private FlightService _flightService;

    public FlightServiceTests()
    {
        
        _flights = new List<Flight>
        {
            new Flight("Flight 1", "Airline A", "USA", "France", "JFK", "CDG", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), 300, 500, 1000, 10, 5, 2),
            new Flight("Flight 2", "Airline B", "Canada", "Germany", "YYZ", "FRA", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), 200, 400, 800, 20, 10, 4),
            new Flight("Flight 3", "Airline C", "USA", "Japan", "LAX", "HND", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), 150, 300, 600, 30, 15, 5),
            new Flight("Flight 4", "Airline F", "UAE", "Germany", "JFK", "FRA", DateTime.Now.AddDays(2), DateTime.Now.AddDays(3), 150, 300, 600, 30, 0, 5),
        };
        _flightService = new FlightService(_flights);
    }

    [Fact]
    public void AddFlights_ShouldAddFlightsToList()
    {
        // Arrange
        var newFlights = new List<Flight>
        {
            new Flight("Flight 4", "Airline A", "USA", "UK", "JFK", "LHR", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), 200, 500, 1000, 100, 50, 20),
            new Flight("Flight 5", "Airline F", "France", "Germany", "CDG", "FRA", DateTime.Now.AddDays(3), DateTime.Now.AddDays(4), 150, 450, 900, 120, 40, 10)
        };

        // Act
        _flightService.AddFlights(newFlights);

        // Assert
        Assert.Equal(6, _flights.Count); 
        Assert.Contains(newFlights[0], _flights); 
        Assert.Contains(newFlights[1], _flights); 
    }

    [Fact]
    public void GetSearchFlights_ShouldReturnFlights_WhenDepartureAndDestinationCountriesAreSpecified()
    {
        // Arrange
        var sieve = new Sieve { DepartureCountry = "USA", DestinationCountry = "Japan" };

        // Act
        var result = _flightService.GetSearchFlights(sieve);

        // Assert
        Assert.Single(result); 
        Assert.Equal("Flight 3", result.First().FlightName);
    }

    [Fact]
    public void GetSearchFlights_ShouldReturnFlights_WhenDepartureDateIsSpecified()
    {
        // Arrange
        var sieve = new Sieve { DepartureDate = DateTime.Now.AddDays(1) };

        // Act
        var result = _flightService.GetSearchFlights(sieve);

        // Assert
        Assert.Equal(3, result.Count()); 
    }

    [Fact]
    public void GetSearchFlights_ShouldReturnFlights_WhenFlightClassIsSpecified()
    {
        // Arrange
        var sieve = new Sieve { FlightClass = FlightClass.Business };

        // Act
        var result = _flightService.GetSearchFlights(sieve);

        // Assert
        Assert.Equal(3, result.Count()); 
    }
}