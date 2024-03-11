namespace FlightAlligator.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int FlightNumber { get; set; }
        public string PassengerName { get; set; }
        public int APIBookingId { get; set; }
        public int UserId { get; set; }
    }
}
