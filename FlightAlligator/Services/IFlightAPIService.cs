using FlightAlligator.Models;
using FlightAlligator.Models.DTOs;

namespace FlightAlligator.Services
{
    public interface IFlightAPIService
    {
        public Task<IEnumerable<Flight>> GetFlightsAsync();
        public Task<Booking> BookFlightAsync(BookingRequestMainDTO bookingRequest);

    }
}
