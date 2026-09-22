using AirlineManagementSystem.ENUMS;
using AirlineManagementSystem.Interfaces;
using AirlineManagementSystem.Models;
using System.Text.RegularExpressions;

namespace AirlineManagementSystem.Payments
{
    public class CreditCardPayment : IPayment
    {
        public string CardNumber { get; }

        public CreditCardPayment(string cardNumber)
        {
            CardNumber = cardNumber?.Trim() ?? string.Empty;
        }

        public bool ProcessPayment(Reservation reservation, decimal amount)
        {
            if (!CanPay(reservation, amount) || !IsValidCardNumber())
            {
                return false;
            }

            reservation.PaymentStatus = PaymentStatus.Paid;
            return true;
        }

        private bool IsValidCardNumber()
        {
            if (!Regex.IsMatch(CardNumber, @"^[0-9 -]+$"))
            {
                return false;
            }

            string digits = CardNumber.Replace(" ", string.Empty).Replace("-", string.Empty);

            return Regex.IsMatch(digits, @"^[0-9]{13,19}$") &&
                   IsLuhnValid(digits);
        }

        private static bool IsLuhnValid(string digits)
        {
            int sum = 0;
            bool doubleDigit = false;

            for (int index = digits.Length - 1; index >= 0; index--)
            {
                int digit = digits[index] - '0';

                if (doubleDigit)
                {
                    digit *= 2;

                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }

                sum += digit;
                doubleDigit = !doubleDigit;
            }

            return sum % 10 == 0;
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
