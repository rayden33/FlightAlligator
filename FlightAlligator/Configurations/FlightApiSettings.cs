using FlightAlligator.Configurations.Models;

namespace FlightAlligator.Configurations
{
    public class FlightApiSettings
    {
        public List<ApiConfiguration> APIs { get; set; } = new List<ApiConfiguration>();
    }
}
