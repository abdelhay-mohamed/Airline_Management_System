using AirlineManagementSystem.Data;
using AirlineManagementSystem.ENUMS;
using AirlineManagementSystem.Interfaces;
using AirlineManagementSystem.Models;
using System.Text.RegularExpressions;

namespace AirlineManagementSystem.Services
{
    public class FlightService : IFlightService
    {
        private readonly AirlineData _data;

        public FlightService(AirlineData data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public bool AddFlight(Flight flight)
        {
            if (flight is null || !IsValidFlight(flight))
            {
                return false;
            }

            if (_data.Flights.ContainsKey(flight.FlightId))
            {
                return false;
            }

            if (!_data.Airports.Any(airport => airport.Id == flight.DepartureAirport.Id) ||
                !_data.Airports.Any(airport => airport.Id == flight.ArrivalAirport.Id) ||
                !_data.Aircrafts.ContainsKey(flight.Aircraft.Id))
            {
                return false;
            }

            if (flight.DepartureAirport.Id == flight.ArrivalAirport.Id ||
                flight.ArrivalTime <= flight.DepartureTime ||
                flight.BasePrice <= 0)
            {
                return false;
            }

            if (_data.Flights.Values.Any(existing =>
                existing.FlightNumber.Equals(flight.FlightNumber, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            bool aircraftConflict = _data.Flights.Values.Any(existing =>
                existing.Aircraft.Id == flight.Aircraft.Id &&
                TimesOverlap(existing.DepartureTime, existing.ArrivalTime, flight.DepartureTime, flight.ArrivalTime));

            if (aircraftConflict)
            {
                return false;
            }

            _data.Flights.Add(flight.FlightId, flight);
            return true;
        }

        public Flight? GetFlightById(int id)
        {
            _data.Flights.TryGetValue(id, out Flight? flight);
            return flight;
        }

        public Flight? GetFlightByNumber(string flightNumber)
        {
            if (string.IsNullOrWhiteSpace(flightNumber))
            {
                return null;
            }

            return _data.Flights.Values.FirstOrDefault(flight =>
                flight.FlightNumber.Equals(flightNumber.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public List<Flight> GetAllFlights()
        {
            return _data.Flights.Values.ToList();
        }

        public bool RemoveFlight(int id)
        {
            if (!_data.Flights.TryGetValue(id, out Flight? flight))
            {
                return false;
            }

            bool hasReservations = _data.Reservations.Values.Any(reservation =>
                reservation.Flight.FlightId == flight.FlightId &&
                reservation.Status != ReservationStatus.Cancelled);

            if (hasReservations)
            {
                return false;
            }

            return _data.Flights.Remove(id);
        }

        private static bool IsValidFlight(Flight flight)
        {
            return flight.FlightId > 0 &&
                   !string.IsNullOrWhiteSpace(flight.FlightNumber) &&
                   Regex.IsMatch(flight.FlightNumber, @"^[A-Z0-9-]{2,10}$", RegexOptions.IgnoreCase) &&
                   flight.DepartureAirport is not null &&
                   flight.ArrivalAirport is not null &&
                   flight.Aircraft is not null &&
                   flight.DepartureTime >= DateTime.Now &&
                   flight.DepartureTime < flight.ArrivalTime &&
                   flight.BasePrice > 0 &&
                   Enum.IsDefined(flight.Status);
        }

        private static bool TimesOverlap(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return start1 < end2 && start2 < end1;
        }
    }
}
