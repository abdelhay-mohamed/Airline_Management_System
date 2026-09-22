using AirlineManagementSystem.ENUMS;
using AirlineManagementSystem.Interfaces;
using AirlineManagementSystem.Models;
using System.Text.RegularExpressions;

namespace AirlineManagementSystem.Payments
{
    public class PayPalPayment : IPayment
    {
        public string Email { get; }

        public PayPalPayment(string email)
        {
            Email = email?.Trim() ?? string.Empty;
        }

        public bool ProcessPayment(Reservation reservation, decimal amount)
        {
            if (!CanPay(reservation, amount) || !IsValidEmail())
            {
                return false;
            }

            reservation.PaymentStatus = PaymentStatus.Paid;
            return true;
        }

        private bool IsValidEmail()
        {
            return Regex.IsMatch(Email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$");
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
