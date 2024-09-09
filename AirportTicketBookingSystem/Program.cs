using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Services;
using AirportTicketBookingSystem.Utils;

namespace AirportTicketBookingSystem;

class Program
{
    static void Main(string[] args)
    {
        var flights = new List<Flight>();
        var bookings = new List<Booking>();
        var passengers = new List<Passenger>();
        var flightService = new FlightService(flights);
        var bookingService = new BookingService(bookings);
        var bookingSearchService = new BookingSearchService(flights,bookings);
        var passengerService = new PassengerService(passengers);
        
        var menuHandler = new MenuHandler(bookingService, bookingSearchService, flightService,passengerService);
        
        menuHandler.MainMenu();
        
    }
}