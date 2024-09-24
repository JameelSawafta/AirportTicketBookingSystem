using System.ComponentModel.DataAnnotations;
using AirportTicketBookingSystem.Validations;

namespace AirportTicketBookingSystem.Models;

public class Flight
{
    [Required]
    public Guid FlightId { get; set; }
    
    [Required]
    [StringLength(20)]
    public string FlightName { get; set; }
    
    [Required]
    [StringLength(20)]
    public string Airline { get; set; }
    
    [Required]
    [StringLength(20)]
    public string DepartureCountry { get; set; }
    
    [Required]
    [StringLength(20)]
    public string DestinationCountry { get; set; }
    
    [Required]
    [StringLength(20)]
    public string DepartureAirport { get; set; }
    
    [Required]
    [StringLength(20)]
    public string DestinationAirport { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    [FutureDate]
    public DateTime DepartureDate { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    [FutureDate]
    public DateTime ArrivalDate { get; set; }
    
    [Required]
    [Range(0, double.MaxValue)]
    public decimal EconomyPrice { get; set; }
    
    [Required]
    [Range(0, double.MaxValue)]
    public decimal BusinessPrice { get; set; }
    
    [Required]
    [Range(0, double.MaxValue)]
    public decimal FirstClassPrice { get; set; }
    
    [Required]
    [Range(0, int.MaxValue)]
    public int AvailableSeatsEconomy { get; set; }
    
    [Required]
    [Range(0, int.MaxValue)]
    public int AvailableSeatsBusiness { get; set; }
    
    [Required]
    [Range(0, int.MaxValue)]
    public int AvailableSeatsFirstClass { get; set; }


    public Flight(
        string flightName, 
        string airline, 
        string departureCountry, 
        string destinationCountry, 
        string departureAirport, 
        string destinationAirport, 
        DateTime departureDate, 
        DateTime arrivalDate, 
        decimal economyPrice, 
        decimal businessPrice, 
        decimal firstClassPrice, 
        int availableSeatsEconomy, 
        int availableSeatsBusiness, 
        int availableSeatsFirstClass)
    {
        FlightId = Guid.NewGuid();
        FlightName = flightName;
        Airline = airline;
        DepartureCountry = departureCountry;
        DestinationCountry = destinationCountry;
        DepartureAirport = departureAirport;
        DestinationAirport = destinationAirport;
        DepartureDate = departureDate;
        ArrivalDate = arrivalDate;
        EconomyPrice = economyPrice;
        BusinessPrice = businessPrice;
        FirstClassPrice = firstClassPrice;
        AvailableSeatsEconomy = availableSeatsEconomy;
        AvailableSeatsBusiness = availableSeatsBusiness;
        AvailableSeatsFirstClass = availableSeatsFirstClass;
    }
}