using FlightAlligator.Models;

namespace FlightAlligator.Services
{
    public interface IFlightService
    {
        public Task<IEnumerable<Flight>> SearchFlightsAsync(FlightSearchConditions conditions);
    }
}
