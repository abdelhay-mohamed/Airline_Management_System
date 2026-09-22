namespace AirlineManagementSystem.Models
{
    public class Pilot : Employee
    {
        public string LicenseNumber { get; private set; }
        public int YearsOfExperience { get; private set; }

        public Pilot(
            int id,
            string name,
            string email,
            string phone,
            int employeeId,
            decimal salary,
            string licenseNumber,
            int yearsOfExperience
        ) : base(id, name, email, phone, employeeId, salary)
        {
            LicenseNumber = licenseNumber?.Trim().ToUpperInvariant() ?? string.Empty;
            YearsOfExperience = yearsOfExperience;
        }
    }
}
