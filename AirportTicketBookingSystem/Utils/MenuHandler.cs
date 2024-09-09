using AirportTicketBookingSystem.Interfaces;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utils;

public class MenuHandler
{
    private readonly IBookingService _bookingService;
    private readonly IBookingSearchService _bookingSearchService;
    private readonly IFlightService _flightService;
    private readonly IPassengerService _passengerService;

    public MenuHandler(IBookingService bookingService, IBookingSearchService bookingSearchService, IFlightService flightService, IPassengerService passengerService)
    {
        _bookingService = bookingService;
        _bookingSearchService = bookingSearchService;
        _flightService = flightService;
        _passengerService = passengerService;
    }

    public void MainMenu()
    {
        Console.WriteLine("Welcome to the Airport Ticket Booking System");
        Console.WriteLine("1. Passenger");
        Console.WriteLine("2. Manager");
        Console.WriteLine("3. Exit");

        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                var passengerMenuHandler = new PassengerMenuHandler.PassengerMenuHandler(_passengerService,_bookingService,_flightService,this);
                passengerMenuHandler.PassengerMenu();
                
                break;
            case "2":
                var managerMenuHandler = new ManagerMenuHandler.ManagerMenuHandler(_bookingService,_bookingSearchService,_passengerService,_flightService,this);
                managerMenuHandler.ManagerMenu();
                
                break;
            case "3":
                Console.WriteLine("Goodbye!");
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid option, please try again.");
                MainMenu();
                break;
        }
    }
}