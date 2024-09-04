using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Interfaces;

public interface IBookingService
{
    public void CreateBooking(FlightClass flightClass,Guid FlightId );
    public void DeleteBooking(Guid BookingId);
    public void UpdateBooking(Guid BookingId);
    public IEnumerable<Booking> GetAllBookings();
}