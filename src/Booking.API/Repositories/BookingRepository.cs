using Booking.API.Models;

namespace Booking.API.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly List<BookedTime> _bookedTimes;
    
    public BookingRepository()
    {
        _bookedTimes = new List<BookedTime>();
    }
    
    public int BookingCount(TimeSpan time)
    {
        int count = _bookedTimes.Count(b => b.Time == time);
        return count;
    }

    public void CreatBooking(BookedTime bookedTime)
    {
        _bookedTimes.Add(bookedTime);
    }
}