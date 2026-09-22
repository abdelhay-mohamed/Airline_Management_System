using AirlineManagementSystem.Data;
using AirlineManagementSystem.Interfaces;
using AirlineManagementSystem.Models;
using System.Text.RegularExpressions;

namespace AirlineManagementSystem.Services
{
    public class AircraftService : IAircraftService
    {
        private readonly AirlineData _data;

        public AircraftService(AirlineData data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public bool AddAircraft(Aircraft aircraft)
        {
            if (aircraft is null || !IsValidAircraft(aircraft))
            {
                return false;
            }

            if (_data.Aircrafts.ContainsKey(aircraft.Id))
            {
                return false;
            }

            _data.Aircrafts.Add(aircraft.Id, aircraft);
            return true;
        }

        public Aircraft? GetAircraftById(int id)
        {
            _data.Aircrafts.TryGetValue(id, out Aircraft? aircraft);
            return aircraft;
        }

        public List<Aircraft> GetAllAircraft()
        {
            return _data.Aircrafts.Values.ToList();
        }

        public bool RemoveAircraft(int id)
        {
            if (!_data.Aircrafts.ContainsKey(id))
            {
                return false;
            }

            bool isUsedByFlight = _data.Flights.Values.Any(flight =>
                flight.Aircraft.Id == id);

            if (isUsedByFlight)
            {
                return false;
            }

            return _data.Aircrafts.Remove(id);
        }

        private static bool IsValidAircraft(Aircraft aircraft)
        {
            if (aircraft.Id <= 0 ||
                aircraft.Capacity <= 0 ||
                aircraft.Capacity > 500 ||
                string.IsNullOrWhiteSpace(aircraft.Model) ||
                aircraft.Seats is null ||
                aircraft.Seats.Length != aircraft.Capacity)
            {
                return false;
            }

            string[] seatNumbers = aircraft.Seats
                .Where(seat => seat is not null)
                .Select(seat => seat.SeatNumber)
                .ToArray();

            if (seatNumbers.Length != aircraft.Capacity ||
                seatNumbers.Distinct(StringComparer.OrdinalIgnoreCase).Count() != aircraft.Capacity)
            {
                return false;
            }

            return aircraft.Seats.All(seat =>
                seat is not null &&
                Regex.IsMatch(seat.SeatNumber, @"^[A-F][1-9][0-9]?$", RegexOptions.IgnoreCase) &&
                Enum.TryParse(seat.Class, true, out AirlineManagementSystem.ENUMS.SeatClass _) &&
                seat.PriceMultiplier > 0);
        }
    }
}
