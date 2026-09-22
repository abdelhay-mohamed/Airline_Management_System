using AirlineManagementSystem.Models;

namespace AirlineManagementSystem.Interfaces
{
    public interface IPassengerService
    {
        bool AddPassenger(Passenger passenger);
        Passenger? GetPassengerById(int id);
        Passenger? GetPassengerByEmail(string email);
        List<Passenger> GetAllPassengers();
        bool RemovePassenger(int id);
    }
}