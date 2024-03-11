using FlightAlligator.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.CompilerServices;

namespace FlightAlligator.Services
{
    public class FlightService : IFlightService
    {
        private readonly ILogger<FlightService> _logger;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);
        private IFlightAPIService _FlightAPI;
        public FlightService(IFlightAPIService flightAPI, IMemoryCache cache, ILogger<FlightService> logger) 
        {
            _FlightAPI = flightAPI;
            _cache = cache;
            _logger = logger;
        }
        public async Task<IEnumerable<Flight>> SearchFlightsAsync(FlightSearchConditions conditions)
        {
            var cacheKey = $"Flights-{conditions.GetDictCode()}";
            _logger.LogInformation("Поиск внутри кеша.");
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<Flight> cachedFlights))
            {
                cachedFlights = await _FlightAPI.GetFlightsAsync();
                _cache.Set(cacheKey, cachedFlights, _cacheDuration);
                _logger.LogInformation("В кешах не найдено.");
            }

            var searchResult = cachedFlights.ToList();
            //Search with datetime
            if (conditions.BeginDepartureDateTime.HasValue && conditions.EndDepartureDateTime.HasValue)
            {
                searchResult = searchResult.Where(f => f.DepartureDateTime >= conditions.BeginDepartureDateTime 
                                                        && f.DepartureDateTime <= conditions.EndDepartureDateTime).ToList();
            }
            //Search with departure and arrival airports
            searchResult = searchResult.Where(f => f.DepartureAirport == conditions.DepartureAirport 
                                                    && (string.IsNullOrEmpty(conditions.ArrivalAirport)?true: f.ArrivalAirport == conditions.ArrivalAirport)).ToList();
            //Search with price
            searchResult = searchResult.Where(f => f.Price >= conditions.MinPrice
                                                        && (f.Price <= conditions.MaxPrice || conditions.MaxPrice == 0)).ToList();
            //Search with transfers count
            searchResult = searchResult.Where(f => f.TransfersCount <= conditions.MaxTrasnfers).ToList();

            //Search with airline
            if(!string.IsNullOrEmpty(conditions.Airline))
                searchResult = searchResult.Where(f => f.Airline == conditions.Airline).ToList();

            return searchResult;
        }
    }
}
