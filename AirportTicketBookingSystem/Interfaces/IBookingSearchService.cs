using AirportTicketBookingSystem.Filters;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Interfaces;

public interface IBookingSearchService
{
    public IEnumerable<Booking> GetSearchBookings(BookingSieve bookingSieve);
}