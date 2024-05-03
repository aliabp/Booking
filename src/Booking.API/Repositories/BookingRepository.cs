using Booking.API.Models;

namespace Booking.API.Repositories;

public class BookingRepository : IBookingRepository
{
    private List<BookedTime> _bookedTimes;
    
    public BookingRepository()
    {
        _bookedTimes = new List<BookedTime>();
    }
    
    public int BookingCount(TimeSpan time)
    {
        // return simultaneous bookings count
        var endTime = time.Add(TimeSpan.FromHours(1));
        var startTime = time.Add(TimeSpan.FromHours(-1));

        int count = _bookedTimes.Count(b => b.Time > startTime && b.Time < endTime);
        
        return count;
    }

    public void CreatBooking(BookedTime bookedTime)
    {
        // reserve a new booking
        _bookedTimes.Add(bookedTime);
    }
}