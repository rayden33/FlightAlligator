using FlightAlligator.Models;
using FlightAlligator.Models.DTOs;
using FlightAlligator.Models.DTOs.SkyScanner;
using Newtonsoft.Json;

namespace FlightAlligator.Adapters
{
    public class SkyScannerFlightAdapter : IFlightAdapter
    {
        private readonly HttpClient _httpClient;

        public SkyScannerFlightAdapter(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Flight>> AdaptFlightAsync(HttpResponseMessage apiResponse)
        {
            var json = await apiResponse.Content.ReadAsStringAsync();
            var flightDtos = JsonConvert.DeserializeObject<List<FlightDTO>>(json);
            return flightDtos.Select(f => new Flight
            {
                FlightNumber = f.FligthNumber,
                Airline = f.Airline,
                DepartureAirport = f.DepartureAirport,
                ArrivalAirport = f.ArrivalAirport,
                ArrivalDateTime = f.ArrivalDateTime,
                DepartureDateTime = f.DepartureDateTime,
                TransfersCount = 0,
                isCharterFlight = true,
                Price = f.Price
            }).ToList();
        }

        public async Task<Booking> BookFlightAsync(string url, BookingRequestMainDTO bookingRequest)
        {
            var booking = new Booking();
            var requestDTO = new SSBookingRequestDTO()
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
