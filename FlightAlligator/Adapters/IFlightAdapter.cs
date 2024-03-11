using FlightAlligator.Models;
using FlightAlligator.Models.DTOs;

namespace FlightAlligator.Adapters
{
    public interface IFlightAdapter
    {
        public Task<IEnumerable<Flight>> AdaptFlightAsync(HttpResponseMessage apiResponse);
        public Task<Booking> BookFlightAsync(string url, BookingRequestMainDTO bookingRequest);
    }
}
