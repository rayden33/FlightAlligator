namespace FlightAlligator.Models.DTOs.SkyScanner
{
    public class SSBookingRequestDTO
    {
        public int FlightNumber { get; set; }
        public int BookingPlacesCount { get; set; }
        public string PassengerName { get; set; }
    }
}
