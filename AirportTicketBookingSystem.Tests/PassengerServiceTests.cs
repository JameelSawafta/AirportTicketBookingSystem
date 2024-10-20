using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Services;

namespace AirportTicketBookingSystem.Tests;

public class PassengerServiceTests
{
    private readonly List<Passenger> _passengers;
    private readonly PassengerService _passengerService;

    public PassengerServiceTests()
    {
        _passengers = new List<Passenger>
        {
            new Passenger("John", "Doe", "john.doe@example.com", "0597487359", "password123"),
            new Passenger("Jane", "Smith", "jane.smith@example.com", "0482369485", "securepass")
        };
        _passengerService = new PassengerService(_passengers);
    }

    [Fact]
    public void PassengerSignIn_ShouldReturnPassenger_WhenValidEmailAndPassword()
    {
        // Act
        var result = _passengerService.PassengerSignIn("john.doe@example.com", "password123");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John", result.FirstName);
        Assert.Equal("Doe", result.LastName);
    }

    [Fact]
    public void PassengerSignIn_ShouldReturnNull_WhenInvalidEmailOrPassword()
    {
        // Act
        var result = _passengerService.PassengerSignIn("invalid.email@example.com", "wrongpassword");

        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public void CreatePassenger_ShouldAddPassenger_WhenValidPassenger()
    {
        // Arrange
        var newPassenger = new Passenger("Alice", "Wonderland", "alice@example.com", "1122334455", "mypass");

        // Act
        var result = _passengerService.CreatePassenger(newPassenger);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(newPassenger, _passengers);
        Assert.Equal("Alice", result.FirstName);
        Assert.Equal("Wonderland", result.LastName);
    }

    [Fact]
    public void CreatePassenger_ShouldReturnNull_WhenInvalidPassenger()
    {
        // Arrange
        var invalidPassenger = new Passenger("", "", "invalid", "invalid", "");

        // Act
        var result = _passengerService.CreatePassenger(invalidPassenger);

        // Assert
        Assert.Null(result);
        Assert.DoesNotContain(invalidPassenger, _passengers);
    }
    
    [Fact]
    public void DeleteBooking_ShouldRemoveBookingFromPassenger_WhenBookingExists()
    {
        // Arrange
        var booking = new Booking(Guid.NewGuid(), Guid.NewGuid(), FlightClass.Economy, DateTime.Now, 150);
        var passenger = _passengers[0];
        passenger.Bookings.Add(booking);

        // Act
        _passengerService.DeleteBooking(passenger.PassengerId, booking);

        // Assert
        Assert.DoesNotContain(booking, passenger.Bookings); 
    }
}