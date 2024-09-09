using AirportTicketBookingSystem.Filters;
using AirportTicketBookingSystem.Interfaces;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utils.PassengerMenuHandler;

public class PassengerMenuHandler
{
    private readonly IPassengerService _passengerService;
    private readonly IBookingService _bookingService;
    private readonly IFlightService _flightService;
    private Passenger _passenger;
    private readonly MenuHandler _menuHandler;
    

    public PassengerMenuHandler(IPassengerService passengerService, IBookingService bookingService, IFlightService flightService, MenuHandler menuHandler)
    {
        _passengerService = passengerService;
        _bookingService = bookingService;
        _flightService = flightService;
        _menuHandler = menuHandler;
    }

    public void PassengerMenu()
    {
        Console.WriteLine("Passenger Menu:");
        Console.WriteLine("1. Sign In");
        Console.WriteLine("2. Create New User");
        Console.WriteLine("3. Back to Main Menu");

        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                SignInPassenger();
                break;
            case "2":
                CreateNewPassenger();
                break;
            case "3":
                _menuHandler.MainMenu(); 
                break;
            default:
                Console.WriteLine("Invalid option, please try again.");
                PassengerMenu();
                break;
        }
    }

    private void SignInPassenger()
    {
        Console.Write("Enter your email: ");
        var email = Console.ReadLine();

        Console.Write("Enter your password: ");
        var password = Console.ReadLine();

        _passenger = _passengerService.PassengerSignIn(email, password);
        if (_passenger != null)
        {
            Console.WriteLine($"Welcome {_passenger.FirstName}!");
            PassengerOptions();
        }
        else
        {
            Console.WriteLine("Sign-in failed. Please try again.");
            PassengerMenu();
        }
    }

    private void CreateNewPassenger()
    {
        Console.WriteLine("Creating a new passenger account.");

        Console.Write("Enter your first name: ");
        var firstName = Console.ReadLine();

        Console.Write("Enter your last name: ");
        var lastName = Console.ReadLine();

        Console.Write("Enter your email: ");
        var email = Console.ReadLine();

        Console.Write("Enter your password: ");
        var password = Console.ReadLine();
        
        Console.Write("Enter your phone number: ");
        var phoneNum = Console.ReadLine();

        _passenger = new Passenger(firstName, lastName, email, phoneNum, password);
        _passenger =  _passengerService.CreatePassenger(_passenger);
        if (_passenger != null)
        {
            Console.WriteLine("Passenger account created successfully.");
            Console.WriteLine($"Welcome {_passenger.FirstName}!");
            PassengerOptions();
        }
        else
        {
            Console.WriteLine("Sign-in failed. Please try again.");
            PassengerMenu();
        }
        PassengerOptions();
    }

    private void PassengerOptions()
    {
        Console.WriteLine("1. Search for Flights");
        Console.WriteLine("2. Book a Flight");
        Console.WriteLine("3. View Bookings");
        Console.WriteLine("4. Cancel a Booking");
        Console.WriteLine("5. Back to Main Menu");

        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                SearchFlights();
                break;
            case "2":
                BookFlight(_passenger);
                break;
            case "3":
                ViewBookings(_passenger);
                break;
            case "4":
                CancelBooking(_passenger);
                break;
            case "5":
                PassengerMenu();
                break; 
            default:
                Console.WriteLine("Invalid option, please try again.");
                PassengerOptions();
                break;
        }
    }
    
    
    
    private void SearchFlights()
    {
        Console.WriteLine("Search for Flights:");
    
        Console.Write("Enter maximum price (optional): ");
        var maxPriceInput = Console.ReadLine();
        decimal? maxPrice = string.IsNullOrEmpty(maxPriceInput) ? null : decimal.Parse(maxPriceInput);
    
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
    
        
        var flightSieve = new Sieve
        {
            MaxPrice = maxPrice,
            DepartureCountry = departureCountry,
            DestinationCountry = destinationCountry,
            DepartureDate = departureDate,
            DepartureAirport = departureAirport,
            DestinationAirport = destinationAirport,
            FlightClass = flightClass
        };
    
        var flights = _flightService.GetSearchFlights(flightSieve);
    
        if (!flights.Any())
        {
            Console.WriteLine("No flights found matching the search criteria.");
        }
        else
        {
            Console.WriteLine("Flights found:");
            foreach (var flight in flights)
            {
                Console.WriteLine($"Flight ID: {flight.FlightId}");
                Console.WriteLine($"Flight Name: {flight.FlightName}");
                Console.WriteLine($"Airline: {flight.Airline}");
                Console.WriteLine($"From: {flight.DepartureCountry} ({flight.DepartureAirport})");
                Console.WriteLine($"To: {flight.DestinationCountry} ({flight.DestinationAirport})");
                Console.WriteLine($"Departure: {flight.DepartureDate}");
                Console.WriteLine($"Arrival: {flight.ArrivalDate}");
                Console.WriteLine($"Economy Price: {flight.EconomyPrice}");
                Console.WriteLine($"Business Price: {flight.BusinessPrice}");
                Console.WriteLine($"First Class Price: {flight.FirstClassPrice}");
                Console.WriteLine();
            }
        }
    
        PassengerOptions();
        
    }

    private void BookFlight(Passenger passenger)
{
    Console.WriteLine("Booking a Flight:");

    

    Console.Write("Enter the Flight ID you wish to book: ");
    var flightIdInput = Console.ReadLine();

    if (!Guid.TryParse(flightIdInput, out var flightId))
    {
        Console.WriteLine("Invalid Flight ID.");
        PassengerOptions();
        return;
    }

    var flight = _flightService.GetSearchFlights(new Sieve { }).FirstOrDefault(f => f.FlightId == flightId);

    if (flight == null)
    {
        Console.WriteLine("Flight not found. Please try again.");
        PassengerOptions();
        return;
    }

    Console.Write("Enter flight class (Economy, Business, FirstClass): ");
    var flightClassInput = Console.ReadLine();
    if (!Enum.TryParse(flightClassInput, true, out FlightClass flightClass))
    {
        Console.WriteLine("Invalid flight class.");
        PassengerOptions();
        return;
    }

    var availableSeats = flightClass switch
    {
        FlightClass.Economy => flight.AvailableSeatsEconomy,
        FlightClass.Business => flight.AvailableSeatsBusiness,
        FlightClass.FirstClass => flight.AvailableSeatsFirstClass,
        _ => 0
    };
    
    var price = flightClass switch
    {
        FlightClass.Economy => flight.EconomyPrice,
        FlightClass.Business => flight.BusinessPrice,
        FlightClass.FirstClass => flight.FirstClassPrice,
        _ => 0
    };

    if (availableSeats <= 0)
    {
        Console.WriteLine("No seats available in the selected class.");
        PassengerOptions();
        return;
    }

    var booking = new Booking(flight.FlightId, passenger.PassengerId, flightClass, DateTime.Now, price);
    _bookingService.CreateBooking(booking);
    _passenger.Bookings.Add(booking);

    switch (flightClass)
    {
        case FlightClass.Economy:
            flight.AvailableSeatsEconomy--;
            break;
        case FlightClass.Business:
            flight.AvailableSeatsBusiness--;
            break;
        case FlightClass.FirstClass:
            flight.AvailableSeatsFirstClass--;
            break;
    }

    Console.WriteLine("Flight booked successfully!");
    PassengerOptions();
}


    private void ViewBookings(Passenger passenger)
    {
        Console.WriteLine("Your Bookings:");

        var bookings = passenger.Bookings;

        if (!bookings.Any())
        {
            Console.WriteLine("No bookings found.");
            PassengerOptions();
            return;
        }

        foreach (var booking in bookings)
        {
            var flight = _flightService.GetSearchFlights(new Sieve { }).FirstOrDefault(f => f.FlightId == booking.FlightId);
            if (flight != null)
            {
                Console.WriteLine($"Booking ID: {booking.BookingId}");
                Console.WriteLine($"Flight Name: {flight.FlightName}");
                Console.WriteLine($"Airline: {flight.Airline}");
                Console.WriteLine($"From: {flight.DepartureCountry} ({flight.DepartureAirport})");
                Console.WriteLine($"To: {flight.DestinationCountry} ({flight.DestinationAirport})");
                Console.WriteLine($"Class: {booking.FlightClass}");
                Console.WriteLine($"Booking Date: {booking.BookingDate}");
                Console.WriteLine($"Price: {booking.Price}");
                Console.WriteLine();
            }
        }

        PassengerOptions();
    }


    private void CancelBooking(Passenger passenger)
{
    Console.WriteLine("Cancel a Booking:");

    var bookings = passenger.Bookings;

    if (!bookings.Any())
    {
        Console.WriteLine("No bookings found to cancel.");
        PassengerOptions();
        return;
    }

    Flight? flight;
    foreach (var booking in bookings)
    {
        flight = _flightService.GetSearchFlights(new Sieve { }).FirstOrDefault(f => f.FlightId == booking.FlightId);
        if (flight != null)
        {
            Console.WriteLine($"Booking ID: {booking.BookingId}");
            Console.WriteLine($"Flight Name: {flight.FlightName}");
            Console.WriteLine($"From: {flight.DepartureCountry} ({flight.DepartureAirport})");
            Console.WriteLine($"To: {flight.DestinationCountry} ({flight.DestinationAirport})");
            Console.WriteLine($"Class: {booking.FlightClass}");
            Console.WriteLine();
        }
    }

    Console.Write("Enter the Booking ID to cancel: ");
    var bookingIdInput = Console.ReadLine();

    if (!Guid.TryParse(bookingIdInput, out var bookingId))
    {
        Console.WriteLine("Invalid Booking ID.");
        PassengerOptions();
        return;
    }

    var bookingToCancel = bookings.FirstOrDefault(b => b.BookingId == bookingId);
    if (bookingToCancel == null)
    {
        Console.WriteLine("Booking not found.");
        PassengerOptions();
        return;
    }

    _passengerService.DeleteBooking(passenger.PassengerId,bookingToCancel);
    _bookingService.DeleteBooking(bookingToCancel.BookingId);
    Console.WriteLine("Booking cancelled successfully.");

    
    var flightClass = bookingToCancel.FlightClass;
    flight = _flightService.GetSearchFlights(new Sieve { }).FirstOrDefault(f => f.FlightId == bookingToCancel.FlightId);
    if (flight != null)
    {
        switch (flightClass)
        {
            case FlightClass.Economy:
                flight.AvailableSeatsEconomy++;
                break;
            case FlightClass.Business:
                flight.AvailableSeatsBusiness++;
                break;
            case FlightClass.FirstClass:
                flight.AvailableSeatsFirstClass++;
                break;
        }
    }

    PassengerOptions();
}

    
    
}