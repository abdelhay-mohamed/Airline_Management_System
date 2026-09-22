using AirlineManagementSystem.Data;
using AirlineManagementSystem.ENUMS;
using AirlineManagementSystem.Interfaces;
using AirlineManagementSystem.Models;
using AirlineManagementSystem.Utilities;

namespace AirlineManagementSystem.Services
{
    public class ReservationService : IReservationService
    {
        private readonly AirlineData _data;

        public ReservationService(AirlineData data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public decimal CalculatePrice(Flight flight, Seat seat)
        {
            if (flight is null || seat is null || flight.BasePrice <= 0 || seat.PriceMultiplier <= 0)
            {
                return 0;
            }

            return flight.BasePrice * seat.PriceMultiplier;
        }

        public Reservation? CreateReservation(Passenger passenger, Flight flight, Seat seat)
        {
            if (!IsRegisteredPassenger(passenger) || !IsRegisteredFlight(flight) || seat is null)
            {
                return null;
            }

            if (!IsFlightBookable(flight))
            {
                return null;
            }

            Seat? actualSeat = GetFlightSeat(flight, seat);

            if (actualSeat is null || IsSeatBooked(flight, actualSeat))
            {
                return null;
            }

            if (HasActiveReservation(passenger, flight))
            {
                return null;
            }

            decimal totalPrice = CalculatePrice(flight, actualSeat);

            if (totalPrice <= 0)
            {
                return null;
            }

            int reservationId = IdGenerator.GenerateId();

            if (_data.Reservations.ContainsKey(reservationId))
            {
                return null;
            }

            Reservation reservation = new Reservation(
                reservationId,
                passenger,
                flight,
                actualSeat,
                DateTime.Now,
                ReservationStatus.Confirmed,
                totalPrice);

            _data.Reservations.Add(reservationId, reservation);

            return reservation;
        }

        public bool CancelReservation(int reservationId)
        {
            if (!_data.Reservations.TryGetValue(reservationId, out Reservation? reservation))
            {
                return false;
            }

            if (reservation.Status == ReservationStatus.Cancelled ||
                reservation.Status == ReservationStatus.Completed ||
                reservation.Flight.Status is FlightStatus.Departed or FlightStatus.Arrived)
            {
                return false;
            }

            reservation.Status = ReservationStatus.Cancelled;

            if (reservation.PaymentStatus == PaymentStatus.Paid)
            {
                reservation.PaymentStatus = PaymentStatus.Refunded;
            }
            return true;
        }

        public List<Reservation> GetAllReservations()
        {
            return _data.Reservations.Values.ToList();
        }

        public Reservation? GetReservationById(int id)
        {
            _data.Reservations.TryGetValue(id, out Reservation? reservation);
            return reservation;
        }

        private bool IsRegisteredPassenger(Passenger passenger)
        {
            return passenger is not null &&
                   passenger.Id > 0 &&
                   _data.Passengers.TryGetValue(passenger.Id, out Passenger? registeredPassenger) &&
                   ReferenceEquals(registeredPassenger, passenger);
        }

        private bool IsRegisteredFlight(Flight flight)
        {
            return flight is not null &&
                   flight.FlightId > 0 &&
                   _data.Flights.TryGetValue(flight.FlightId, out Flight? registeredFlight) &&
                   ReferenceEquals(registeredFlight, flight);
        }

        private static bool IsFlightBookable(Flight flight)
        {
            return flight.Status is FlightStatus.Scheduled or FlightStatus.Boarding or FlightStatus.Delayed;
        }

        private static Seat? GetFlightSeat(Flight flight, Seat seat)
        {
            return flight.Aircraft.Seats.FirstOrDefault(existing =>
                existing is not null &&
                existing.SeatNumber.Equals(seat.SeatNumber, StringComparison.OrdinalIgnoreCase));
        }

        private bool IsSeatBooked(Flight flight, Seat seat)
        {
            return _data.Reservations.Values.Any(reservation =>
                reservation.Flight.FlightId == flight.FlightId &&
                reservation.Seat.SeatNumber.Equals(seat.SeatNumber, StringComparison.OrdinalIgnoreCase) &&
                reservation.Status != ReservationStatus.Cancelled);
        }

        private bool HasActiveReservation(Passenger passenger, Flight flight)
        {
            return _data.Reservations.Values.Any(reservation =>
                reservation.Passenger.Id == passenger.Id &&
                reservation.Flight.FlightId == flight.FlightId &&
                reservation.Status != ReservationStatus.Cancelled);
        }
    }
}
