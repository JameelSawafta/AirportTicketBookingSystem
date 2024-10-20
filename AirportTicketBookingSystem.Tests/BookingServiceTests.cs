using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Services;

namespace AirportTicketBookingSystem.Tests;

public class BookingServiceTests
{
    private readonly List<Booking> _bookings;
    private readonly BookingService _bookingService;


    public BookingServiceTests()
    {
        _bookings = new List<Booking>
        {
            new Booking(Guid.NewGuid(), Guid.NewGuid(), FlightClass.Economy, DateTime.Now, 100),
            new Booking(Guid.NewGuid(), Guid.NewGuid(), FlightClass.Business, DateTime.Now, 150)
        };

        _bookingService = new BookingService(_bookings);
    }

    [Fact]
    public void CreateBooking_ShouldReturnsTrue_WhenValidBooking()
    {
        // Arrange
        var newBooking = new Booking(Guid.NewGuid(), Guid.NewGuid(), FlightClass.Economy, DateTime.Now, 200);

        // Act
        var result = _bookingService.CreateBooking(newBooking);

        // Assert
        Assert.True(result);
        Assert.Contains(newBooking, _bookings);
    }

    [Fact]
    public void CreateBooking_ShouldReturnsFalse_WhenInvalidBooking()
    {
        // Arrange
        var invalidBooking =
            new Booking(Guid.NewGuid(), Guid.NewGuid(), FlightClass.Economy, DateTime.Now, -200);

        // Act
        var result = _bookingService.CreateBooking(invalidBooking);

        // Assert
        Assert.False(result);
        Assert.DoesNotContain(invalidBooking, _bookings);
    }

    [Fact]
    public void DeleteBooking_ShouldRemovesBooking_WhenExistingBooking()
    {
        // Arrange
        var bookingToDelete = _bookings[0];

        // Act
        _bookingService.DeleteBooking(bookingToDelete.BookingId);

        // Assert
        Assert.DoesNotContain(bookingToDelete, _bookings);
    }

    [Fact]
    public void DeleteBooking_ShouldDoNothing_WhenNonExistingBooking()
    {
        // Arrange
        var nonExistingId = Guid.NewGuid();

        // Act
        _bookingService.DeleteBooking(nonExistingId);

        // Assert
        Assert.Equal(2, _bookings.Count);
    }

    [Fact]
    public void UpdateBooking_ShouldUpdatesBooking_WhenExistingBooking()
    {
        // Arrange
        var bookingToUpdate = _bookings[0];
        var updatedBooking = new Booking(bookingToUpdate.FlightId, bookingToUpdate.PassengerId, FlightClass.Business,
            DateTime.Now,250)
        {
            BookingId = bookingToUpdate.BookingId
        };

        // Act
        _bookingService.UpdateBooking(updatedBooking);

        // Assert
        Assert.Equal(FlightClass.Business, _bookings[0].FlightClass);
        Assert.Equal(250, _bookings[0].Price);
    }

    [Fact]
    public void UpdateBooking_ShouldDoNothing_WhenNonExistingBooking()
    {
        // Arrange
        var nonExistingBooking = new Booking(Guid.NewGuid(), Guid.NewGuid(), FlightClass.Economy, DateTime.Now, 300);

        // Act
        _bookingService.UpdateBooking(nonExistingBooking);

        // Assert
        Assert.Equal(2, _bookings.Count); // Should still contain two bookings
    }

    [Fact]
    public void GetAllBookings_ShouldReturnsAllBookings()
    {
        // Act
        var result = _bookingService.GetAllBookings();

        // Assert
        Assert.Equal(_bookings.Count, result.Count());
        Assert.Equal(_bookings, result.ToList());
    }
}