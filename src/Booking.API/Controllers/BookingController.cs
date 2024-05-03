using System.Net;
using Booking.API.Models;
using Booking.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingRepository _bookingRepository;
    public BookingController(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    [HttpPost(Name = "CreateBooking")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    public IActionResult CreateBooking([FromBody] BookingRequest request)
    {
        // check modelstate validation
        if (!ModelState.IsValid)
            return BadRequest();
        
        // check name has a value
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest();
        
        // check time string is in correct format and convert it to TimeSpan
        TimeSpan bookingTime;
        try
        {
            TimeSpan.TryParse(request.Time, out bookingTime);
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        // check requested time is in business hours
        if (bookingTime < new TimeSpan(9, 0, 0) || bookingTime > new TimeSpan(16, 0, 0))
            return BadRequest();

        // check simultaneous booking do not exceed 4
        if (_bookingRepository.BookingCount(bookingTime) >= 4)
            return Conflict();
        
        // everything is alright, so we add request to repository
        var bookedTime = new BookedTime();
        try
        {
            bookedTime = new BookedTime
            {
                Id = Guid.NewGuid().ToString(),
                Time = bookingTime,
                Name = request.Name
                
            };
            _bookingRepository.CreatBooking(bookedTime);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal server error"); 
        }
        return Ok(new { BookingId = bookedTime.Id });
    }
}