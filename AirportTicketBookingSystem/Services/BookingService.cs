using AirportTicketBookingSystem.Filters;
using AirportTicketBookingSystem.Interfaces;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Services;

public class BookingService : IBookingService
{
    private readonly List<Booking> _bookings;

    public BookingService(List<Booking> bookings)
    {
        _bookings = bookings;
    }

    
    public void CreateBooking(Booking booking)
    {
        _bookings.Add(booking);
    }

    public void DeleteBooking(Guid BookingId)
    {
        var _booking = _bookings.FirstOrDefault(b => b.BookingId == BookingId);
        if (_booking != null)
        {
            _bookings.Remove(_booking);
        }
    }

    public void UpdateBooking(Booking booking)
    {
        var _booking = _bookings.FirstOrDefault(b => b.BookingId == booking.BookingId);
        if (_booking != null)
        {
            _booking.FlightId = booking.FlightId;
            _booking.FlightClass = booking.FlightClass;
            _booking.BookingDate = booking.BookingDate;
        }
    }

    public IEnumerable<Booking> GetAllBookings()
    {
        return _bookings;
    }
}