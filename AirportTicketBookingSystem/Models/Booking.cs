using System.ComponentModel.DataAnnotations;
using AirportTicketBookingSystem.Validations;

namespace AirportTicketBookingSystem.Models;

public class Booking
{
    [Required]
    public Guid BookingId;
    
    [Required]
    public Guid FlightId { get; set; }
    
    [Required]
    public Guid PassengerId { get; set; }

    [Required]
    public FlightClass FlightClass { get; set; }
    
    [Required]
    [FutureDate]
    public DateTime BookingDate { get; set; }

    public Booking(Guid flightId, Guid passengerId, FlightClass flightClass, DateTime bookingDate)
    {
        BookingId = Guid.NewGuid();
        FlightId = flightId;
        PassengerId = passengerId;
        FlightClass = flightClass;
        BookingDate = bookingDate;
    }
}