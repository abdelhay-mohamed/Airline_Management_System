using AirlineManagementSystem.ENUMS;

namespace AirlineManagementSystem.Models
{
    public class Flight
    {
        public int FlightId { get; private set; }
        public string FlightNumber { get; private set; }
        public Airport DepartureAirport { get; private set; }
        public Airport ArrivalAirport { get; private set; }
        public DateTime DepartureTime { get; private set; }
        public DateTime ArrivalTime { get; private set; }
        public Aircraft Aircraft { get; private set; }
        public decimal BasePrice { get; private set; }
        public FlightStatus Status { get; private set; }

        public Flight(
            int flightId,
            string flightNumber,
            Airport departureAirport,
            Airport arrivalAirport,
            DateTime departureTime,
            DateTime arrivalTime,
            Aircraft aircraft,
            decimal basePrice,
            FlightStatus status)
        {
            FlightId = flightId;
            FlightNumber = flightNumber?.Trim().ToUpperInvariant() ?? string.Empty;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            Aircraft = aircraft;
            BasePrice = basePrice;
            Status = status;
        }
    }
}
