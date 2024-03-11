namespace FlightAlligator.Models.DTOs
{
    public class BookingRequestMainDTO
    {
        public int FlightNumber { get; set; }
        public string PassengerName { get; set; }
        public int PassengerCount { get; set; }
        public string AdapterType { get; set; }
    }
}
