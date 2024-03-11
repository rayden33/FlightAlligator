using FlightAlligator.Models;
using FlightAlligator.Models.DTOs;
using FlightAlligator.Models.DTOs.AirFlyInfo;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace FlightAlligator.Adapters
{
    public class AirFlyInfoFlightAdapter : IFlightAdapter
    {
        private readonly HttpClient _httpClient;
        public AirFlyInfoFlightAdapter(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Flight>> AdaptFlightAsync(HttpResponseMessage apiResponse)
        {
            var json = await apiResponse.Content.ReadAsStringAsync();
            var flightDtos = JsonConvert.DeserializeObject<List<FlightDTO>>(json);
            return flightDtos.Select(f => new Flight
            {
                FlightNumber = f.FlightNumber,
                Airline = f.Airline,
                DepartureAirport = f.Departure,
                ArrivalAirport = f.Destination,
                ArrivalDateTime = f.ArrivalDateTime,
                DepartureDateTime = f.DepartureDateTime,
                TransfersCount = f.TrasnfersCount,
                isCharterFlight = false,
                Price = f.Price
            }).ToList();
        }

        public async Task<Booking> BookFlightAsync(string url, BookingRequestMainDTO bookingRequest)
        {
            var booking = new Booking();
            var requestDTO = new AFIBookingRequestDTO()
            {
                FlightNumber = bookingRequest.FlightNumber,
                PassengerName = bookingRequest.PassengerName
            };

            var response = await _httpClient.PostAsJsonAsync(url, requestDTO);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var bookingResponseDtos = JsonConvert.DeserializeObject<BookingResponseDTO>(json);

                booking.FlightNumber = bookingResponseDtos.FlightNumber;
                booking.PassengerName = bookingResponseDtos.PassengerName;
                booking.APIBookingId = bookingResponseDtos.Id;
            }

            return booking;
        }
    }
}
