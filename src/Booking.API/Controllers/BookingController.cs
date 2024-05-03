using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BookingController : ControllerBase
{
    public BookingController()
    {
        
    }

    [HttpPost]
    public IActionResult CreateProduct([FromBody] Models.Booking request)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest();
        TimeSpan bookingTime;
        try
        {
            TimeSpan.TryParse(request.Time, out bookingTime);
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        if (bookingTime < new TimeSpan(9, 0, 0) || bookingTime >= new TimeSpan(16, 0, 0))
            return BadRequest();
        
        
        
        return Ok();
    }
}