namespace FlightAlligator.Models.DTOs.AirFlyInfo
{
    public class FlightDTO
    {
        public int FlightNumber { get; set; }
        public string Airline { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public int TrasnfersCount { get; set; }
        public decimal Price { get; set; }
    }
}
