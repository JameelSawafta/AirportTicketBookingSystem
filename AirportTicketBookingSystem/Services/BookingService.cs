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

    
    public bool CreateBooking(Booking booking)
    {
        // I think splitting the validation from adding the book will be better
        
        // 1. Check if it's valid object.
        // 2. yes? add it , no ignore it and add a validation message.
        var isValid = ValidationService.Validation(booking,b => _bookings.Add(b));
        return isValid;
    }

    public void DeleteBooking(Guid bookingId)
    {
        var booking = _bookings.FirstOrDefault(b => b.BookingId == bookingId);
        if (booking != null)
        {
            _bookings.Remove(booking);
        }
    }

    public void UpdateBooking(Booking booking)
    {
        var currentBooking = _bookings.FirstOrDefault(b => b.BookingId == booking.BookingId);
        if (currentBooking != null)
        {
            currentBooking.FlightId = booking.FlightId;
            currentBooking.FlightClass = booking.FlightClass;
            currentBooking.BookingDate = DateTime.Now;
            currentBooking.Price = booking.Price;
            // the passenger may change the class, so the price may change
        }
    }

    public IEnumerable<Booking> GetAllBookings()
    {
        return _bookings;
    }
}