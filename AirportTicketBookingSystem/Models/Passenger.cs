using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.Models;

public class Passenger
{
    [Required]
    public Guid PassengerId;
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [Phone]
    public string PhoneNum { get; set; }
    
    
    public List<Booking> Bookings { get; set; }

    public Passenger(string firstName, string lastName, string email, string phoneNum, List<Booking> bookings)
    {
        PassengerId = new Guid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNum = phoneNum;
        Bookings = bookings;
    }
}