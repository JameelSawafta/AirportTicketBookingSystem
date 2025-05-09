using AirportTicketBookingSystem.Filters;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Interfaces;

public interface IBookingService
{
    public bool CreateBooking(Booking booking);
    public void DeleteBooking(Guid BookingId);
    public void UpdateBooking(Booking booking);
    public IEnumerable<Booking> GetAllBookings();
}