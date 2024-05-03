using Booking.API.Models;

namespace Booking.API.Repositories;

public interface IBookingRepository
{
    int BookingCount(TimeSpan time);
    void CreatBooking(BookedTime bookedTime);
}