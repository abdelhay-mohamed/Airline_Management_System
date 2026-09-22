using AirlineManagementSystem.Interfaces;
using AirlineManagementSystem.Models;

namespace AirlineManagementSystem.Services
{
    public class PaymentService
    {
        private readonly IPayment _payment;

        public PaymentService(IPayment payment)
        {
            _payment = payment ?? throw new ArgumentNullException(nameof(payment));
        }

        public bool ProcessPayment(Reservation reservation, decimal amount)
        {
            if (reservation is null || amount <= 0)
            {
                return false;
            }

            return _payment.ProcessPayment(reservation, amount);
        }
    }
}
