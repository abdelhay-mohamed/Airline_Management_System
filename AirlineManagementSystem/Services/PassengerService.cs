using AirlineManagementSystem.Data;
using AirlineManagementSystem.ENUMS;
using AirlineManagementSystem.Interfaces;
using AirlineManagementSystem.Models;
using System.Text.RegularExpressions;

namespace AirlineManagementSystem.Services
{
    public class PassengerService : IPassengerService
    {
        private readonly AirlineData _data;

        public PassengerService(AirlineData data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public bool AddPassenger(Passenger passenger)
        {
            if (passenger is null || !IsValidPassenger(passenger))
            {
                return false;
            }

            if (_data.Passengers.ContainsKey(passenger.Id))
            {
                return false;
            }

            if (_data.Passengers.Values.Any(existing =>
                existing.Email.Equals(passenger.Email, StringComparison.OrdinalIgnoreCase) ||
                existing.PassportNumber.Equals(passenger.PassportNumber, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            _data.Passengers.Add(passenger.Id, passenger);
            return true;
        }

        public Passenger? GetPassengerById(int id)
        {
            _data.Passengers.TryGetValue(id, out Passenger? passenger);
            return passenger;
        }

        public Passenger? GetPassengerByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            return _data.Passengers.Values.FirstOrDefault(passenger =>
                passenger.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public List<Passenger> GetAllPassengers()
        {
            return _data.Passengers.Values.ToList();
        }

        public bool RemovePassenger(int id)
        {
            if (!_data.Passengers.ContainsKey(id))
            {
                return false;
            }

            bool hasReservations = _data.Reservations.Values.Any(reservation =>
                reservation.Passenger.Id == id &&
                reservation.Status != ReservationStatus.Cancelled);

            if (hasReservations)
            {
                return false;
            }

            return _data.Passengers.Remove(id);
        }

        private static bool IsValidPassenger(Passenger passenger)
        {
            return passenger.Id > 0 &&
                   Regex.IsMatch(passenger.Name, @"^[\p{L}][\p{L} .'-]*$") &&
                   Regex.IsMatch(passenger.Email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$") &&
                   Regex.IsMatch(passenger.Phone, @"^\+?[0-9]{7,15}$") &&
                   Regex.IsMatch(passenger.PassportNumber, @"^[A-Z0-9]{5,20}$") &&
                   Regex.IsMatch(passenger.Nationality, @"^[\p{L}][\p{L} .'-]*$") &&
                   passenger.DateOfBirth.Date <= DateTime.Today &&
                   !string.IsNullOrWhiteSpace(passenger.Address.Country) &&
                   !string.IsNullOrWhiteSpace(passenger.Address.City) &&
                   !string.IsNullOrWhiteSpace(passenger.Address.Street);
        }
    }
}
