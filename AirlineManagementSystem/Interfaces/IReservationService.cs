using AirlineManagementSystem.Models;

namespace AirlineManagementSystem.Interfaces
{
    public interface IReservationService
    {
        Reservation? CreateReservation(Passenger passenger, Flight flight, Seat seat);
        Reservation? GetReservationById(int id);
        List<Reservation> GetAllReservations();
        bool CancelReservation(int reservationId);
        decimal CalculatePrice(Flight flight, Seat seat);
    }

}





