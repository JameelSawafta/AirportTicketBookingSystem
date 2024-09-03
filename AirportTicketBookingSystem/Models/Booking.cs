using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.Models;

public class Booking
{
    [Required]
    public Guid BookingId;
    
    [Required]
    public Guid FlightId;
    
    [Required]
    public Guid PassengerId;
    
    
    
    
    
}