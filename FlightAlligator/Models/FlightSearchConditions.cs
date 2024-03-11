using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightAlligator.Models
{
    public class FlightSearchConditions
    {
        private int propIndex = 0;
        [Required]
        public string DepartureAirport { get; set; }
        public string? ArrivalAirport { get; set; }
        public DateTime? BeginDepartureDateTime { get; set; }
        public DateTime? EndDepartureDateTime { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int MaxTrasnfers { get; set; }
        public string? Airline { get; set; }

        public string GetDictCode()
        {
            string res = $"{DepartureAirport}" +
                $"-{ArrivalAirport}" +
                $"-{BeginDepartureDateTime?.ToString("yyyyMMddHHmmss")}" +
                $"-{EndDepartureDateTime?.ToString("yyyyMMddHHmmss")}" +
                $"-{MinPrice}" +
                $"-{MaxPrice}" +
                $"-{MaxTrasnfers}" +
                $"-{Airline}";

            return res ;
        }
    }
}
