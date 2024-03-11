namespace FlightAlligator.Models.DTOs.SkyScanner
{
    public class FlightDTO
    {
        public int FligthNumber { get; set; }
        public string Airline { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public string Status { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public decimal Price { get; set; }
    }
}
