namespace AirportTicketBookingSystem.Utils;

public class FileHandler
{
    public string ReadFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found: {filePath}");
        }

        return File.ReadAllText(filePath);
    }
}