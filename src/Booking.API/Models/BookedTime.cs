namespace Booking.API.Models;

public class BookedTime
{
    public string Id { set; get; }
    public TimeSpan Time { set; get; }
    public string Name { set; get; }
}