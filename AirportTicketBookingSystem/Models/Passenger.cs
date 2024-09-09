using System.ComponentModel;
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
    
    [Required]
    public string Password { get; set; }
    
    
    public List<Booking> Bookings { get; set; }

    public string FullName => $"{FirstName} {LastName}";

    public Passenger(string firstName, string lastName, string email, string phoneNum, string password)
    {
        PassengerId = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNum = phoneNum;
        Password = password;
        Bookings = new List<Booking> { };
    }
}