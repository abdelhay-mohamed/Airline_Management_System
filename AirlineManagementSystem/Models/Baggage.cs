using AirlineManagementSystem.ENUMS;

namespace AirlineManagementSystem.Models
{
    public class Baggage
    {
        public int Id { get; private set; }
        public decimal Weight { get; private set; }
        public BaggageType Type { get; private set; }

        public Baggage(int id, decimal weight, BaggageType type)
        {
            Id = id;
            Weight = weight;
            Type = type;
        }
    }
}
