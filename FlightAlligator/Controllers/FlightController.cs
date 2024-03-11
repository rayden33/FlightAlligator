using FlightAlligator.Models;
using FlightAlligator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightAlligator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private IFlightService _flightService;
        private readonly ILogger<FlightController> _logger;

        public FlightController(IFlightService flightService, ILogger<FlightController> logger)
        {
            _flightService = flightService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlights([FromQuery] FlightSearchConditions conditions)
        {
            try
            {
                var flights = await _flightService.SearchFlightsAsync(conditions);
                _logger.LogInformation($"Найдено {flights.Count()} рейсов.");
                return Ok(flights);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Не удалось получить информацию о рейсах.");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
