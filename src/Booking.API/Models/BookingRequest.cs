namespace Booking.API.Models;

// model for booking request
public class BookingRequest
{
    // booking requested time
    public string Time { set; get; }
    
    // booking requested by
    public string Name { set; get; }
}