namespace AirlineManagementSystem.Models
{
    public class Passenger : Person
    {
        public string PassportNumber { get; set; }
        public string Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Address Address { get; set; }

        public Passenger(
            int id,
            string name,
            string email,
            string phone,
            string passportNumber,
            string nationality,
            DateTime dateOfBirth,
            Address address
        ) : base(id, name, email, phone)
        {
            PassportNumber = passportNumber?.Trim().ToUpperInvariant() ?? string.Empty;
            Nationality = nationality?.Trim() ?? string.Empty;
            DateOfBirth = dateOfBirth.Date;
            Address = address;
        }
    }
}
