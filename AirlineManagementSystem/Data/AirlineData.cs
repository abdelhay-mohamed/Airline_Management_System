using AirlineManagementSystem.Models;

namespace AirlineManagementSystem.Data
{
    public class AirlineData
    {
        public Dictionary<int, Passenger> Passengers { get; } = new Dictionary<int, Passenger>();
        public Dictionary<int, Employee> Employees { get; } = new Dictionary<int, Employee>();
        public Dictionary<int, Aircraft> Aircrafts { get; } = new Dictionary<int, Aircraft>();
        public Dictionary<int, Flight> Flights { get; } = new Dictionary<int, Flight>();
        public Dictionary<int, Reservation> Reservations { get; } = new Dictionary<int, Reservation>();
        public List<Airport> Airports { get; } = new List<Airport>();
        public List<Baggage> Baggages { get; } = new List<Baggage>();
    }
}
