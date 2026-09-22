using AirlineManagementSystem.ENUMS;
using AirlineManagementSystem.Interfaces;
using AirlineManagementSystem.Models;

namespace AirlineManagementSystem.Payments
{
    public class CashPayment : IPayment
    {
        public bool ProcessPayment(Reservation reservation, decimal amount)
        {
            if (!CanPay(reservation, amount))
            {
                return false;
            }

            reservation.PaymentStatus = PaymentStatus.Paid;
            return true;
        }

        private static bool CanPay(Reservation reservation, decimal amount)
        {
            return reservation is not null &&
                   reservation.Status == ReservationStatus.Confirmed &&
                   reservation.PaymentStatus == PaymentStatus.Pending &&
                   amount > 0 &&
                   amount == reservation.TotalPrice &&
                   reservation.TotalPrice > 0;
        }
    }
}
