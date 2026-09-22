using AirlineManagementSystem.Models;

namespace AirlineManagementSystem.Interfaces
{
    public interface IAircraftService
    {
        bool AddAircraft(Aircraft aircraft);
        Aircraft? GetAircraftById(int id);
        List<Aircraft> GetAllAircraft();
        bool RemoveAircraft(int id);

    }
}