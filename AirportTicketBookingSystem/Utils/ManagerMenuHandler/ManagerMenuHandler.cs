using AirportTicketBookingSystem.Filters;
using AirportTicketBookingSystem.Interfaces;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utils.ManagerMenuHandler;

public class ManagerMenuHandler
{
    private readonly IBookingService _bookingService;
    private readonly IBookingSearchService _bookingSearchService;
    private readonly IPassengerService _passengerService;
    private readonly IFlightService _flightService;
    private readonly MenuHandler _menuHandler;

    

    public ManagerMenuHandler(IBookingService bookingService, IBookingSearchService bookingSearchService, IPassengerService passengerService, IFlightService flightService, MenuHandler menuHandler)
    {
        _bookingService = bookingService;
        _bookingSearchService = bookingSearchService;
        _passengerService = passengerService;
        _flightService = flightService;
        _menuHandler = menuHandler;
    }

    public void ManagerMenu()
    {
        Console.WriteLine("Manager Menu:");
        Console.WriteLine("1. Search Bookings");
        Console.WriteLine("2. Import Flights");
        Console.WriteLine("3. View All Bookings");
        Console.WriteLine("4. Back to Main Menu");

        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                SearchBookings();
                break;
            case "2":
                ImportFlights();
                break;
            case "3":
                GetAllBookings();
                break;
            case "4":
                _menuHandler.MainMenu();
                break;
            default:
                Console.WriteLine("Invalid option, please try again.");
                ManagerMenu();
                break;
        }
    }

    private void SearchBookings()
{
    Console.WriteLine("Search for Bookings:");
    
    Console.Write("Enter maximum price (optional): ");
    var maxPriceInput = Console.ReadLine();
    decimal? maxPrice = string.IsNullOrEmpty(maxPriceInput) ? null : decimal.Parse(maxPriceInput);

    Console.Write("Enter flightId (optional): ");
    var flightIdInput = Console.ReadLine();
    Guid? flightId = string.IsNullOrEmpty(flightIdInput) ? null : Guid.Parse(flightIdInput);

    Console.Write("Enter passengerId (optional): ");
    var passengerIdInput = Console.ReadLine();
    Guid? passengerId = string.IsNullOrEmpty(passengerIdInput) ? null : Guid.Parse(passengerIdInput);

    Console.Write("Enter departure country (optional): ");
    var departureCountry = Console.ReadLine();

    Console.Write("Enter destination country (optional): ");
    var destinationCountry = Console.ReadLine();

    Console.Write("Enter departure date (yyyy-mm-dd) (optional): ");
    var departureDateInput = Console.ReadLine();
    DateTime? departureDate = string.IsNullOrEmpty(departureDateInput) ? null : DateTime.Parse(departureDateInput);

    Console.Write("Enter departure airport (optional): ");
    var departureAirport = Console.ReadLine();

    Console.Write("Enter destination airport (optional): ");
    var destinationAirport = Console.ReadLine();

    Console.Write("Enter flight class (Economy, Business, FirstClass) (optional): ");
    var flightClassInput = Console.ReadLine();
    FlightClass? flightClass = string.IsNullOrEmpty(flightClassInput)
        ? null
        : Enum.TryParse(flightClassInput, true, out FlightClass parsedClass) ? parsedClass : null;

    var bookingSieve = new Sieve
    {
        MaxPrice = maxPrice,
        FlightId = flightId,
        PassengerId = passengerId,
        DepartureCountry = departureCountry,
        DestinationCountry = destinationCountry,
        DepartureDate = departureDate,
        DepartureAirport = departureAirport,
        DestinationAirport = destinationAirport,
        FlightClass = flightClass
    };

    var bookings = _bookingSearchService.GetSearchBookings(bookingSieve);

    if (!bookings.Any())
    {
        Console.WriteLine("No bookings found matching the search criteria.");
    }
    else
    {
        Console.WriteLine("Bookings found:");
        foreach (var booking in bookings)
        {
            var flight = _flightService.GetSearchFlights(new Sieve { }).FirstOrDefault(f => f.FlightId == booking.FlightId);

            
            Console.WriteLine($"Booking ID: {booking.BookingId}");
            Console.WriteLine($"Passenger Name: {_passengerService.GetAllPassengers().FirstOrDefault(p => p.Id == booking.PassengerId).FullName}");
            Console.WriteLine($"Flight Name: {flight.FlightName}");
            Console.WriteLine($"Airline: {flight.Airline}");
            Console.WriteLine($"From: {flight.DepartureCountry} ({flight.DepartureAirport})");
            Console.WriteLine($"To: {flight.DestinationCountry} ({flight.DestinationAirport})");
            Console.WriteLine($"Class: {booking.FlightClass}");
            Console.WriteLine($"Booking Date: {booking.BookingDate}");
            Console.WriteLine($"Price: {booking.Price}");
        }
    }
    ManagerMenu();
}


    private void ImportFlights()
    {
        _flightService.ImportFlightsFromCsv("../../../Data/Flights/flights.csv");
        ManagerMenu();
    }
    
    private void GetAllBookings()
    {
        Console.WriteLine("View All Bookings:");

        var allBookings = _bookingService.GetAllBookings();

        if (!allBookings.Any())
        {
            Console.WriteLine("No bookings available.");
            ManagerMenu();
            return;
        }

        // Display all bookings
        foreach (var booking in allBookings)
        {
            Console.WriteLine($"Booking ID: {booking.BookingId}");
            Console.WriteLine($"Passenger Name: {_passengerService.GetAllPassengers().FirstOrDefault(p => p.Id == booking.PassengerId).FullName}");
            Console.WriteLine($"Flight Name: {_flightService.GetSearchFlights(new Sieve{}).FirstOrDefault(f => f.FlightId == booking.FlightId).FlightName}");
            Console.WriteLine($"Class: {booking.FlightClass}");
            Console.WriteLine($"Booking Date: {booking.BookingDate}");
            Console.WriteLine();
        }

        ManagerMenu();
    }

    
    
}