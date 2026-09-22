using AirlineManagementSystem.Models;

namespace AirlineManagementSystem.Interfaces
{
    public interface IFlightService
    {
        bool AddFlight(Flight flight);
        Flight? GetFlightById(int id);
        Flight? GetFlightByNumber(string flightNumber);
        List<Flight> GetAllFlights();
        bool RemoveFlight(int id);



    }
}

