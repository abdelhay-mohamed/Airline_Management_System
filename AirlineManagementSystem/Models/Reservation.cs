using AirlineManagementSystem.ENUMS;

namespace AirlineManagementSystem.Models
{
    public class Reservation
    {
        public int ReservationId { get; private set; }
        public Passenger Passenger { get; private set; }
        public Flight Flight { get; private set; }
        public Seat Seat { get; private set; }
        public DateTime ReservationDate { get; private set; }
        public ReservationStatus Status { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public decimal TotalPrice { get; private set; }

        public Reservation(
            int reservationId,
            Passenger passenger,
            Flight flight,
            Seat seat,
            DateTime reservationDate,
            ReservationStatus status,
            decimal totalPrice)
        {
            ReservationId = reservationId;
            Passenger = passenger;
            Flight = flight;
            Seat = seat;
            ReservationDate = reservationDate;
            Status = status;
            PaymentStatus = PaymentStatus.Pending;
            TotalPrice = totalPrice;
        }
    }
}
