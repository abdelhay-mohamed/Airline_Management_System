using AirlineManagementSystem.Models;

namespace AirlineManagementSystem.Interfaces
{
    public interface IPayment
    {
        bool ProcessPayment(Reservation reservation, decimal amount);
    }
}
