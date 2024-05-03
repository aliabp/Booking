using Booking.API.Controllers;
using Booking.API.Models;
using Booking.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Booking.API.Test;

public class BookingController_UnitTest
{
    [Fact]
    public void CreateBooking_ValidObjectPassed_ReturnsOk()
    {
        // Arrange
        Mock<IBookingRepository> _mockRepo = new Mock<IBookingRepository>();
        _mockRepo.Setup(repo => 
            repo.BookingCount(new TimeSpan(9, 0, 0))).Returns(GetMockCount(false));
        BookingController _controller = new BookingController(_mockRepo.Object);
        var testBooking = new BookingRequest
        {
            Time = "09:00",
            Name = "Ali"
        };

        // Act
        var Response = _controller.CreateBooking(testBooking);

        // Assert
        Assert.IsType<OkObjectResult>(Response);
    }
    
    [Fact]
    public void CreateBooking_EmptyName_ReturnsBadRequest()
    {
        // Arrange
        Mock<IBookingRepository> _mockRepo = new Mock<IBookingRepository>();
        _mockRepo.Setup(repo => 
            repo.BookingCount(new TimeSpan(9, 0, 0))).Returns(GetMockCount(false));
        BookingController _controller = new BookingController(_mockRepo.Object);
        var testBooking = new BookingRequest
        {
            Time = "09:00",
            Name = string.Empty
        };

        // Act
        var Response = _controller.CreateBooking(testBooking);

        // Assert
        Assert.IsType<BadRequestResult>(Response);
    }
    
    [Fact]
    public void CreateBooking_WrongTimeFormat_ReturnsBadRequest()
    {
        // Arrange
        Mock<IBookingRepository> _mockRepo = new Mock<IBookingRepository>();
        _mockRepo.Setup(repo => 
            repo.BookingCount(new TimeSpan(9, 0, 0))).Returns(GetMockCount(false));
        BookingController _controller = new BookingController(_mockRepo.Object);
        var testBooking = new BookingRequest
        {
            Time = "09s30",
            Name = "Ali"
        };

        // Act
        var Response = _controller.CreateBooking(testBooking);

        // Assert
        Assert.IsType<BadRequestResult>(Response);
    }
    
    [Fact]
    public void CreateBooking_OutOfBusinessHours_ReturnsBadRequest()
    {
        // Arrange
        Mock<IBookingRepository> _mockRepo = new Mock<IBookingRepository>();
        _mockRepo.Setup(repo => 
            repo.BookingCount(new TimeSpan(9, 0, 0))).Returns(GetMockCount(false));
        BookingController _controller = new BookingController(_mockRepo.Object);
        var testBooking = new BookingRequest
        {
            Time = "16:30",
            Name = "Ali"
        };

        // Act
        var Response = _controller.CreateBooking(testBooking);

        // Assert
        Assert.IsType<BadRequestResult>(Response);
    }
    
    [Fact]
    public void CreateBooking_SimultaneousBookingExceed_ReturnsConflict()
    {
        // Arrange
        Mock<IBookingRepository> _mockRepo = new Mock<IBookingRepository>();
        _mockRepo.Setup(repo => 
            repo.BookingCount(new TimeSpan(9, 0, 0))).Returns(GetMockCount(true));
        BookingController _controller = new BookingController(_mockRepo.Object);
        var testBooking = new BookingRequest
        {
            Time = "09:00",
            Name = "Ali"
        };

        // Act
        var Response = _controller.CreateBooking(testBooking);

        // Assert
        Assert.IsType<ConflictResult>(Response);
    }

    private int GetMockCount(bool shouldBeConflict)
    {
        if (shouldBeConflict)
            return 4;
        return 1;
    }
}