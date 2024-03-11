using FlightAlligator.Models;
using FlightAlligator.Models.DTOs;
using FlightAlligator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightAlligator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private IFlightAPIService _flightAPIService;
        private readonly ILogger<BookingController> _logger;

        public BookingController(IFlightAPIService flightAPIService, ILogger<BookingController> logger)
        {
            _flightAPIService = flightAPIService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> BookFlight([FromBody] BookingRequestMainDTO bookingRequest)
        {
            try
            {
                var booking = await _flightAPIService.BookFlightAsync(bookingRequest);
                _logger.LogInformation($"Успешно забронировано {booking.APIBookingId}.");
                return Ok(booking);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Не удалось забронировать.");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
