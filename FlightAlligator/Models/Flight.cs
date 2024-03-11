namespace FlightAlligator.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string AdapterType { get; set; }
        public int FlightNumber { get; set; }
        public string Airline { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public int TransfersCount { get; set; }
        public bool isCharterFlight { get; set; }
        public decimal Price { get; set; }
    }
}
