namespace FlightAlligator.Models.DTOs.AirFlyInfo
{
    public class BookingResponseDTO
    {
        public int Id { get; set; }
        public int FlightNumber { get; set; }
        public string PassengerName { get; set; }
        public DateTime BookingTime { get; set; }
    }
}
