namespace FlightAlligator.Models.DTOs.SkyScanner
{
    public class BookingResponseDTO
    {
        public int Id { get; set; }
        public int FlightNumber { get; set; }
        public int BookingPlacesCount { get; set; }
        public string PassengerName { get; set; }
        public DateTime BookingDateTime { get; set; }
    }
}
