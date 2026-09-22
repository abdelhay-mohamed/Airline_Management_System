using AirlineManagementSystem.ENUMS;

namespace AirlineManagementSystem.Models
{
    public class Seat
    {
        public string SeatNumber { get; private set; }
        public string Class { get; private set; }
        public bool IsAvailable { get; set; }
        public decimal PriceMultiplier { get; private set; }

        public Seat(string seatNumber, string seatClass, bool isAvailable, decimal priceMultiplier)
        {
            SeatNumber = seatNumber?.Trim().ToUpperInvariant() ?? string.Empty;
            Class = seatClass?.Trim() ?? string.Empty;
            IsAvailable = isAvailable;
            PriceMultiplier = priceMultiplier;
        }

        public SeatClass GetSeatClass()
        {
            return Enum.TryParse(Class, true, out SeatClass seatClass)
                ? seatClass
                : SeatClass.Economy;
        }
    }
}
