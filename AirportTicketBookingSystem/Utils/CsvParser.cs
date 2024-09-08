using System.ComponentModel.DataAnnotations;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utils;

public class CsvParser
{
    public List<Flight> ParseFlightsCsv(string csvContent)
    {
         var flights = new List<Flight>();
            using (var reader = new StringReader(csvContent))
            {
                string line;
                reader.ReadLine();

                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');

                    if (parts.Length >= 13)
                    {
                        try
                        {
                            var flight = new Flight(
                                flightName: parts[0].Trim(),
                                airline: parts[1].Trim(),
                                departureCountry: parts[2].Trim(),
                                destinationCountry: parts[3].Trim(),
                                departureAirport: parts[4].Trim(),
                                destinationAirport: parts[5].Trim(),
                                departureDate: DateTime.Parse(parts[6].Trim()),
                                arrivalDate: DateTime.Parse(parts[7].Trim()),
                                economyPrice: decimal.Parse(parts[8].Trim()),
                                businessPrice: decimal.Parse(parts[9].Trim()),
                                firstClassPrice: decimal.Parse(parts[10].Trim()),
                                availableSeatsEconomy: int.Parse(parts[11].Trim()),
                                availableSeatsBusiness: int.Parse(parts[12].Trim()),
                                availableSeatsFirstClass: int.Parse(parts[13].Trim())
                            );
                            
                            var validationResults = new List<ValidationResult>();
                            var validationContext = new ValidationContext(flight);
                            bool isValid = Validator.TryValidateObject(flight, validationContext, validationResults, true);

                            if (isValid)
                            {
                                flights.Add(flight);
                            }
                            else
                            {
                                Console.WriteLine($"Validation errors for flight '{flight.FlightName}':");
                                foreach (var validationResult in validationResults)
                                {
                                    Console.WriteLine($"- {validationResult.ErrorMessage}");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error parsing flight: {ex.Message}");
                        }
                    }
                }
            }
            return flights;
    }
}