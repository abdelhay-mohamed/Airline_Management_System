namespace AirlineManagementSystem.Models
{
    public class FlightAttendant : Employee
    {
        public List<string> Languages { get; private set; }

        public FlightAttendant(
            int id,
            string name,
            string email,
            string phone,
            int employeeId,
            decimal salary
        ) : base(id, name, email, phone, employeeId, salary)
        {
            Languages = new List<string>();
        }
    }
}
