using FlightAlligator.Adapters;
using FlightAlligator.Configurations;
using FlightAlligator.Models;
using FlightAlligator.Models.DTOs;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace FlightAlligator.Services
{
    public class FlightAPIService : IFlightAPIService
    {
        private readonly HttpClient _httpClient;
        private readonly FlightApiSettings _apiSettings;
        private readonly IServiceProvider _serviceProvider;
        private static int flightIdCounter;
        private static int bookingIdCounter = 1;

        public FlightAPIService(HttpClient httpClient, IOptions<FlightApiSettings> options, IServiceProvider serviceProvider)
        {
            _httpClient = httpClient;
            _apiSettings = options.Value;
            _serviceProvider = serviceProvider;
        }

        public async Task<IEnumerable<Flight>> GetFlightsAsync()
        {
            List<Flight> allFlights = new();
            flightIdCounter = 1;

            foreach (var api in _apiSettings.APIs)
            {
                var response = await _httpClient.GetAsync(api.APIUrl + api.Flight);
                if (response.IsSuccessStatusCode)
                {
                    var type = Type.GetType(api.AdapterType);
                    if (type == null)
                    {
                        throw new InvalidOperationException($"Не удалось загрузить тип: {api.AdapterType}");
                    }
                    var adapter = (IFlightAdapter)_serviceProvider.GetRequiredService(type);
                    var flights = await adapter.AdaptFlightAsync(response);
                    foreach (var flight in flights)
                    {
                        flight.Id = flightIdCounter++;
                        flight.AdapterType = api.AdapterType;
                    }
                    allFlights.AddRange(flights);
                }
            }

            return allFlights;
        }

        public async Task<Booking> BookFlightAsync(BookingRequestMainDTO bookingRequest)
        {
            Booking booking = new Booking();
            var type = Type.GetType(bookingRequest.AdapterType);
            if (type == null)
            {
                throw new InvalidOperationException($"Не удалось загрузить тип: {bookingRequest.AdapterType}");
            }
            var adapter = (IFlightAdapter)_serviceProvider.GetRequiredService(type);
            var api = _apiSettings.APIs.FirstOrDefault(a => a.AdapterType == bookingRequest.AdapterType);

            booking = await adapter.BookFlightAsync(api.APIUrl + api.Booking, bookingRequest);
            booking.Id = bookingIdCounter++;

            return booking;
        }
    }
}
